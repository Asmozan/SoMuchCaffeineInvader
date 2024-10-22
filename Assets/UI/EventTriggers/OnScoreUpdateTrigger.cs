using TMPro;
using UnityEngine;

namespace Invader.UI.EventTriggers
{
    public class OnScoreUpdateTrigger : MonoBehaviour
    {
        private TextMeshProUGUI _scoreText;
        
        [SerializeField] private string _scoreTextString = "Score: ";
        
        private void Awake()
        {
            if (!TryGetComponent(out _scoreText))
            {
                Debug.LogError("Score text not found!");
            }
            
            UpdateScore(0);
        }
        
        public void UpdateScore(object data)
        {
            if (data is int score)
            {
                _scoreText.text = _scoreTextString + score;
            }
        }
    }
}
