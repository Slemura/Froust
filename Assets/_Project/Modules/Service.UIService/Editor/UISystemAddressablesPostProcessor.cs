using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace RpDev.Services.UI.Editor
{
	[InitializeOnLoad]
	public class UISystemAddressablesPostProcessor
	{
		static UISystemAddressablesPostProcessor ()
		{
			AddressableAssetSettingsDefaultObject.Settings.OnModification -= OnSettingsModification;
			AddressableAssetSettingsDefaultObject.Settings.OnModification += OnSettingsModification;
		}

		private static void OnSettingsModification (AddressableAssetSettings settings, AddressableAssetSettings.ModificationEvent @event, object obj)
		{
			if (IsSuitableEvent(@event) == false)
				return;
			
			if (obj is not List<AddressableAssetEntry> entryList)
				return;

			foreach (var assetEntry in entryList.Where(IsScreenAsset))
				assetEntry.SetLabel(UIServiceConstants.UIScreenLabel, true);
		}

		private static bool IsSuitableEvent (AddressableAssetSettings.ModificationEvent @event)
		{
			return @event is
				AddressableAssetSettings.ModificationEvent.EntryCreated or
				AddressableAssetSettings.ModificationEvent.EntryAdded or
				AddressableAssetSettings.ModificationEvent.EntryModified;
		}

		private static bool IsScreenAsset (AddressableAssetEntry assetEntry)
		{
			if (assetEntry.MainAssetType != typeof(GameObject))
				return false;

			var mainAsset = assetEntry.MainAsset as GameObject;

			return mainAsset != null &&
			       mainAsset.TryGetComponent(typeof(UIScreen), out var _);
		}
	}
}
