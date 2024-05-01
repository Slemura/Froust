namespace Froust.EntryPoint.States
{
    public struct GameOverStatePayload
    {
        public int SecondsInLevel { get; }
        public int EnemiesCount { get; }

        public GameOverStatePayload(int secondsInLevel, int enemiesCount)
        {
            SecondsInLevel = secondsInLevel;
            EnemiesCount = enemiesCount;
        }
    }
}
