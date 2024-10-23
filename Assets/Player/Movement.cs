using UnityEngine;

namespace Invader.Player
{
    public class Movement : MonoBehaviour
    {
        [field: SerializeField, HideInInspector]
        public float Speed { private get; set; }

        private Transform _transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }
        
        public void Move(Vector3 direction)
        {
            _transform.position += direction * (Speed * Time.deltaTime);
        }
        
        public void IncreaseSpeedBy(float speedIncreasePercentage)
        {
            Speed += Speed * speedIncreasePercentage;
        }
        
        public void DecreaseSpeedBy(float speedDecreasePercentage)
        {
            Speed -= Speed * speedDecreasePercentage;
        }
    }
}
