using Invader.Player;
using UnityEngine;

namespace Invader.Utility.Events
{
    [RequireComponent(typeof(Movement))]
    public class OnEventSpeedUp : MonoBehaviour
    {
        [Range(0, 1)]
        [SerializeField] private float _speedIncreasePercentage = 0.10f;
        
        private Movement _movement;
        
        private void Awake()
        {
            TryGetComponent(out _movement);
        }

        public void SpeedUp()
        {
            _movement.IncreaseSpeedBy(_speedIncreasePercentage);
        }
    }
}
