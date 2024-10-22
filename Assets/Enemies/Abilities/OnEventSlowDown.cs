using Invader.Player;
using UnityEngine;

namespace Invader.Utility.Events
{
    [RequireComponent(typeof(Movement))]

    public class OnEventSlowDown : MonoBehaviour
    {
        [Range(0, 1)]
        [SerializeField] private float _speedDecreasePercentage = 0.10f;
        
        private Movement _movement;
        
        private void Awake()
        {
            TryGetComponent(out _movement);
        }

        public void SlowDown()
        {
            _movement.DecreaseSpeedBy(_speedDecreasePercentage);
        }
    }
}
