using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using Unity.EditorCoroutines.Editor;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.Compilation;

namespace Froust.Editor
{
    public class BuildTools : EditorWindow
    {
        [MenuItem("Tools/Build Tools")]
        public static void OnShowTools()
        {
            EditorWindow.GetWindow<BuildTools>();
        }

        private BuildTargetGroup GetTargetGroupForTarget(BuildTarget target) => target switch
        {
            BuildTarget.StandaloneOSX => BuildTargetGroup.Standalone,
            BuildTarget.StandaloneWindows => BuildTargetGroup.Standalone,
            BuildTarget.iOS => BuildTargetGroup.iOS,
            BuildTarget.Android => BuildTargetGroup.Android,
            BuildTarget.StandaloneWindows64 => BuildTargetGroup.Standalone,
            BuildTarget.WebGL => BuildTargetGroup.WebGL,
            BuildTarget.StandaloneLinux64 => BuildTargetGroup.Standalone,
            _ => BuildTargetGroup.Unknown
        };

        private readonly Dictionary<BuildTarget, bool> _targetsToBuild = new Dictionary<BuildTarget, bool>();
        private readonly List<BuildTarget> _availableTargets = new List<BuildTarget>();

        private void OnEnable()
        {
            _availableTargets.Clear();
            var buildTargets = System.Enum.GetValues(typeof(BuildTarget));
            foreach (var buildTargetValue in buildTargets)
            {
                var target = (BuildTarget)buildTargetValue;

                // skip if unsupported
                if (!BuildPipeline.IsBuildTargetSupported(GetTargetGroupForTarget(target), target))
                    continue;

                _availableTargets.Add(target);

                // add the target if not in the build list
                if (!_targetsToBuild.ContainsKey(target))
                    _targetsToBuild[target] = false;
            }

            // check if any targets have gone away
            if (_targetsToBuild.Count > _availableTargets.Count)
            {
                // build the list of removed targets
                var targetsToRemove = new List<BuildTarget>();
                foreach (var target in _targetsToBuild.Keys)
                {
                    if (!_availableTargets.Contains(target))
                        targetsToRemove.Add(target);
                }

                // cleanup the removed targets
                foreach (var target in targetsToRemove)
                    _targetsToBuild.Remove(target);
            }
        }

        private void OnGUI()
        {
            GUILayout.Label("Platforms to Build", EditorStyles.boldLabel);

            // display the build targets
            var numEnabled = 0;
            foreach (var target in _availableTargets)
            {
                _targetsToBuild[target] = EditorGUILayout.Toggle(target.ToString(), _targetsToBuild[target]);

                if (_targetsToBuild[target])
                    numEnabled++;
            }

            if (numEnabled > 0)
            {
                // attempt to build?
                var prompt = numEnabled == 1 ? "Build 1 Platform" : $"Build {numEnabled} Platforms";
                if (GUILayout.Button(prompt))
                {
                    var selectedTargets = new List<BuildTarget>();
                    foreach (var target in _availableTargets)
                    {
                        if (_targetsToBuild[target])
                            selectedTargets.Add(target);
                    }

                    EditorCoroutineUtility.StartCoroutine(PerformBuild(selectedTargets), this);
                }
            }
        }

        private IEnumerator PerformBuild(List<BuildTarget> targetsToBuild)
        {
            // show the progress display
            var buildAllProgressID =
                Progress.Start("Build All", "Building all selected platforms", Progress.Options.Sticky);
            Progress.ShowDetails();
            yield return new EditorWaitForSeconds(1f);

            var originalTarget = EditorUserBuildSettings.activeBuildTarget;

            // build each target
            for (var targetIndex = 0; targetIndex < targetsToBuild.Count; ++targetIndex)
            {
                var buildTarget = targetsToBuild[targetIndex];

                Progress.Report(buildAllProgressID, targetIndex + 1, targetsToBuild.Count);
                var buildTaskProgressID = Progress.Start($"Build {buildTarget.ToString()}", null,
                    Progress.Options.Sticky, buildAllProgressID);
                yield return new EditorWaitForSeconds(1f);

                // perform the build
                if (!BuildIndividualTarget(buildTarget))
                {
                    Progress.Finish(buildTaskProgressID, Progress.Status.Failed);
                    Progress.Finish(buildAllProgressID, Progress.Status.Failed);

                    if (EditorUserBuildSettings.activeBuildTarget != originalTarget)
                        EditorUserBuildSettings.SwitchActiveBuildTargetAsync(GetTargetGroupForTarget(originalTarget),
                            originalTarget);

                    yield break;
                }

                Progress.Finish(buildTaskProgressID, Progress.Status.Succeeded);
                yield return new EditorWaitForSeconds(1f);
            }

            Progress.Finish(buildAllProgressID, Progress.Status.Succeeded);

            if (EditorUserBuildSettings.activeBuildTarget != originalTarget)
                EditorUserBuildSettings.SwitchActiveBuildTargetAsync(GetTargetGroupForTarget(originalTarget),
                    originalTarget);

            yield return null;
        }

        private static readonly bool IsListening = false;

        public static void BuildAddressables(object o = null)
        {
            if (IsListening)
                CompilationPipeline.compilationFinished -= BuildAddressables;
            
            Debug.Log("Building Addressables!!! START PLATFORM: platform: " + Application.platform + " target: " +
                      EditorUserBuildSettings.selectedStandaloneTarget);

            AddressableAssetSettings.CleanPlayerContent();
            AddressableAssetSettings.BuildPlayerContent();

            Debug.Log("Building Addressables!!! DONE");
        }

        private bool BuildIndividualTarget(BuildTarget target)
        {
            var options = new BuildPlayerOptions();

            // get the list of scenes
            var scenes = new List<string>();
            foreach (var scene in EditorBuildSettings.scenes)
                scenes.Add(scene.path);

            // configure the build
            options.scenes = scenes.ToArray();
            options.target = target;
            options.targetGroup = GetTargetGroupForTarget(target);

            Debug.LogError($"Try to build Addressables for {target.ToString()}");
            BuildAddressables();

            // set the location path name
            if (target == BuildTarget.Android)
            {
                var apkName = PlayerSettings.productName + ".apk";
                options.locationPathName = System.IO.Path.Combine("Builds", target.ToString(), apkName);
            }
            else if (target == BuildTarget.StandaloneWindows64)
            {
                options.locationPathName =
                    System.IO.Path.Combine("Builds", target.ToString(), PlayerSettings.productName + ".exe");
            }
            else if (target == BuildTarget.StandaloneLinux64)
            {
                options.locationPathName =
                    System.IO.Path.Combine("Builds", target.ToString(), PlayerSettings.productName + ".x86_64");
            }
            else
                options.locationPathName =
                    System.IO.Path.Combine("Builds", target.ToString(), PlayerSettings.productName);

            if (BuildPipeline.BuildCanBeAppended(target, options.locationPathName) == CanAppendBuild.Yes)
                options.options = BuildOptions.AcceptExternalModificationsToPlayer;
            else
                options.options = BuildOptions.None;

            // start the build
            var report = BuildPipeline.BuildPlayer(options);

            // was the build successful?
            if (report.summary.result == BuildResult.Succeeded)
            {
                Debug.Log($"Build for {target.ToString()} completed in {report.summary.totalTime.Seconds} seconds");
                return true;
            }

            Debug.LogError($"Build for {target.ToString()} failed");

            return false;
        }
    }
}