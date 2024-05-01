using Cysharp.Threading.Tasks;

namespace RpDev.Services.UI
{
	public interface IUIService
	{
		UniTask<TScreen> SpawnScreen<TScreen> () where TScreen : UIScreen;

		void DestroyScreen<TScreen> (TScreen screen) where TScreen : UIScreen;
	}
}
