using UnityEngine;

namespace Invader.General.Events
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MakePanelVisibleTrigger : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        
        private void Awake()
        {
            if (!TryGetComponent(out _canvasGroup))
            {
                Debug.LogError("CanvasGroup component is missing!");
            }
        }
        
        public void SetVisible()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}
