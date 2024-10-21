using System.Collections.Generic;

namespace Invader.Enemies
{
    [System.Serializable]
    public class EnemyDataList
    {
        public float BaseEnemySpeed;
        public List<EnemyData> Enemies;
    }
}
