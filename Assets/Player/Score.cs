using Invader.Utility.Events;
using UnityEngine;

namespace Invader.Player
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private GameEvent UpdateScoreEvent;

        private int _currentScore = 0;

        public void AddScore(object score)
        {
            if (score is int scoreInt)
            {
                _currentScore += scoreInt;
            }
            
            UpdateScoreEvent.Raise(_currentScore);
        }
    }
}
