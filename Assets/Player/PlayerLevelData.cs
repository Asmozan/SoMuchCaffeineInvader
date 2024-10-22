using System;

namespace Invader.Player
{
    [Serializable]
    public class PlayerLevelData
    {
        public int Level;
        public int ExperienceRequired;
        public float SpecialAbilityCooldown;
        public float SpecialAbilityRadius;
        public int Damage;
    }
}
