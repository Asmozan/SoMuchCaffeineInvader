namespace Invader.Enemies
{
    using UnityEngine;

    [RequireComponent(typeof(Collider))]
    public class SpawnArea : MonoBehaviour
    {
        private Collider _collider;

        private void Awake()
        {
            TryGetComponent(out _collider);
        }
        
        public Vector3 GetRandomPosition()
        {
            if (_collider == null)
            {
                Debug.LogError("Collider is not set.");
                return Vector3.zero;
            }

            Bounds bounds = _collider.bounds;

            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = bounds.min.y;
            float z = Random.Range(bounds.min.z, bounds.max.z);

            return new Vector3(x, y, z);
        }

        private void OnDrawGizmos()
        {
            if (_collider == null)
            {
                return;
            }
            
            Gizmos.color = new Color(0, 1, 0, 0.5f);
            Gizmos.DrawCube(_collider.bounds.center, _collider.bounds.size);
        }
    }
}
