using UnityEngine;

namespace Invader.Player
{
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(PlayerWeapon))]
    [RequireComponent(typeof(PlayerAbility))]
    public class PlayerController : MonoBehaviour
    {
        public Vector2 Movement { get; set; }
        public bool IsFireButtonPressed { get; set; }
        public bool IsSpecialButtonPressed { get; set; }
    }
}
