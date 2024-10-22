using Invader.Arena;
using Invader.Utility.Events;
using UnityEngine;

namespace Invader.Enemies.Triggers
{
    public class OnReachingDeadZoneTrigger : MonoBehaviour
    {
        [SerializeField] private GameEvent _onReachingDeadZoneEvent;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<DeadZone>())
            {
                _onReachingDeadZoneEvent.Raise(this, null);
            }
        }
    }
}
