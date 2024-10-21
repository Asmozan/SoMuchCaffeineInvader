using System;
using UnityEngine;

namespace Invader.Player
{
    public class Movement : MonoBehaviour
    {
        [field: SerializeField]
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
    }
}
