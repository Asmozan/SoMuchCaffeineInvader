using System.Collections.Generic;
using UnityEngine;

namespace Invader.Utility.Events
{
    [CreateAssetMenu(menuName = "GameEvents/GameEvent", fileName = "GameEvent")]
    public class GameEvent : ScriptableObject
    {
        public List<GameEventListener> listeners = new List<GameEventListener>();

        public void Raise(Component sender)
        {
            foreach (var listener in listeners)
            {
                listener.OnEventRaised(sender, null);
            }
        }
    
        public void Raise(Component sender, object data)
        {
            foreach (var listener in listeners)
            {
                listener.OnEventRaised(sender, data);
            }
        }
    
        public void RegisterListener(GameEventListener listener)
        {
            if (listeners.Contains(listener))
            {
                Debug.Log("Listener already registered, registration skipped");
                return;
            }
        
            listeners.Add(listener);
        }
    
        public void UnregisterListener(GameEventListener listener)
        {
            if (!listeners.Contains(listener))
            {
                Debug.Log("Listener is not registered, unable to unregister");
                return;
            }
        
            listeners.Remove(listener);
        }
    }

}
