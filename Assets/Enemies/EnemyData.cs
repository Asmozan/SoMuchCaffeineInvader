using Newtonsoft.Json;

namespace Invader.Enemies
{
    [System.Serializable]
    public class EnemyData
    {
        public string Type;
        public int Health;
        public float SpawnChance;
        public float MovementSpeedMultiplier;
        public int ScorePoints;
        public int ExperiencePoints;
        [JsonIgnore]
        public Enemy Prefab;
    }
}
