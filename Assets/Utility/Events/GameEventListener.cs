using System;
using UnityEngine;
using UnityEngine.Events;

namespace Invader.Utility.Events
{
    [Serializable]
    public class ExtendedUnityEvent : UnityEvent<object> { }
    
    [Serializable]
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent _gameEventListeningTo;
        [SerializeField] private ExtendedUnityEvent _response;
    
        private void OnEnable()
        {
            _gameEventListeningTo.RegisterListener(this);
        }

        private void OnDisable()
        {
            _gameEventListeningTo.UnregisterListener(this);
        }

        public void OnEventRaised(object data)
        {
            _response.Invoke(data);
        }
    }

}
