using Invader.Utility.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Invader.UI.EventTriggers
{
    [RequireComponent(typeof(Button))]
    public class OnButtonClickRiseEvent : MonoBehaviour
    {
        [SerializeField] private GameEvent _gameEvent;
        
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(RiseEvent);
        }

        private void RiseEvent()
        {
            _gameEvent.Raise();
        }
    }
}
