using System;
using UnityEngine;
using UnityEngine.Events;

namespace Invader.Utility.Events
{
    [Serializable]
    public class ExtendedUnityEvent : UnityEvent<Component, object> { }

    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent _gameEvent;
        [SerializeField] private ExtendedUnityEvent _response;
    
        private void OnEnable()
        {
            _gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            _gameEvent.UnregisterListener(this);
        }

        public void OnEventRaised(Component sender, object data)
        {
            _response.Invoke(sender, data);
        }
    }

}
