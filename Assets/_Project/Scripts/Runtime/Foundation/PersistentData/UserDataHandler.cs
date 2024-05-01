using RpDev.Services.Persistence.PersistenceHandlers;
using VContainer.Unity;

namespace Froust.Runtime.PersistentData
{
	public class UserDataHandler : IStartable
	{
		private const string FileName = "user_data";
		
		private readonly UserData _userData = new UserData();
		
		public int LevelTimeScore => _userData.LevelTimeScore;
		public int LevelEnemyDefeatedScore => _userData.LevelEnemyDefeatedScore;

		private readonly PersistenceHandler<UserData> _persistenceHandler;

		public UserDataHandler ()
		{
			_persistenceHandler = new PlayerPrefsPersistenceHandler<UserData>(FileName);
		}

		public void Start ()
		{
			_persistenceHandler.TryLoadInto(_userData);
		}

		public void Reset ()
		{
			_userData.LevelTimeScore = default;
			_userData.LevelEnemyDefeatedScore = default;
			_persistenceHandler.Save(_userData);
		}

		public bool TrySetBestScore (int levelTimeBestScore, int levelEnemyDefeatedBestScore)
		{
			if (levelTimeBestScore <= _userData.LevelTimeScore && levelEnemyDefeatedBestScore <= _userData.LevelEnemyDefeatedScore)
				return false;

			_userData.LevelTimeScore = levelTimeBestScore;
			_userData.LevelEnemyDefeatedScore = levelEnemyDefeatedBestScore;
			
			_persistenceHandler.Save(_userData);
			
			return true;
		}
	}
}
