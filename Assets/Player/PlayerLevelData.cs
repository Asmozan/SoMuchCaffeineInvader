using System;

namespace Invader.Player
{
    [Serializable]
    public class PlayerLevelData
    {
        public int Level;
        public int ExperienceRequired;
        public float SpecialAbilityCooldown;
        public int Damage;
    }
}
