using System.Collections;
using TMPro;
using UnityEngine;

namespace Invader.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class AbilityCooldownTimer : MonoBehaviour
    {
        [SerializeField] private string _abilityReadyText = "Ready!";
        
        private TextMeshProUGUI _cooldownText;
        private Coroutine _cooldownCoroutine;

        private void Awake()
        {
            TryGetComponent(out _cooldownText);
            _cooldownText.text = _abilityReadyText;
        }
        
        public void Trigger(object data)
        {
            if (data is not float cooldown)
            {
                return;
            }
            
            if (_cooldownCoroutine != null)
            {
                StopCoroutine(_cooldownCoroutine);
            }
            _cooldownCoroutine = StartCoroutine(CooldownCoroutine(cooldown));
        }
        
        private IEnumerator CooldownCoroutine(float cooldownTime)
        {
            float remainingTime = cooldownTime;
            while (remainingTime > 0f)
            {
                _cooldownText.text = $"{remainingTime:F1}";
                yield return null;
                remainingTime -= Time.deltaTime;
            }

            _cooldownText.text = _abilityReadyText;
            _cooldownCoroutine = null;
        }
        
    }
}
