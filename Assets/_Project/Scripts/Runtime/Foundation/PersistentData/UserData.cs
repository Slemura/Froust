using Newtonsoft.Json;

namespace Froust.Runtime.PersistentData
{
    public class UserData
    {
        [JsonProperty("levelTimeScore")]  private int _levelTimeScore;
        [JsonProperty("levelEnemyDefeatedScore")] private int _levelEnemyDefeatedScore;
		
        [JsonIgnore] public int LevelTimeScore
        {
            get => _levelTimeScore;
            set => _levelTimeScore = value;
        }

        [JsonIgnore] public int LevelEnemyDefeatedScore
        {
            get => _levelEnemyDefeatedScore;
            set => _levelEnemyDefeatedScore = value;
        }
    }
}