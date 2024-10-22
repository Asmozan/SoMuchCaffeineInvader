using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Invader.Player
{
    public class PlayerLevelManager : MonoBehaviour
    {
        [SerializeField] private TextAsset _levelDataJson;
        [SerializeField] private List<PlayerLevelData> _levels;
        
        private int _currentLevelIndex = 0;
        private int _currentExperience = 0;

        private PlayerWeapon _playerWeapon;
        private PlayerAbility _playerAbility;

        private void Awake()
        {
            _playerWeapon = GetComponent<PlayerWeapon>();
            _playerAbility = GetComponent<PlayerAbility>();

            ApplyLevelStats();
        }
        
        [ContextMenu("Load Level Data")]
        private void LoadLevelData()
        {
            PlayerLevelDataList levelDataList = JsonConvert.DeserializeObject<PlayerLevelDataList>(_levelDataJson.text);
            _levels = levelDataList.Levels;
        }

        public void AddExperience(object data)
        {
            if (!(data is int amount))
            {
                Debug.LogError("Invalid experience data.");
                return;
            }
            
            _currentExperience += amount;

            bool isExperienceEnough = _currentExperience >= _levels[_currentLevelIndex].ExperienceRequired;
            bool isMaxLevel = _currentLevelIndex >= _levels.Count - 1;
            
            bool canLevelUp = !isMaxLevel && isExperienceEnough;
            
            if (canLevelUp)
            {
                _currentExperience -= _levels[_currentLevelIndex].ExperienceRequired;
                _currentLevelIndex++;
                ApplyLevelStats();
            }
        }

        private void ApplyLevelStats()
        {
            PlayerLevelData currentLevelData = _levels[_currentLevelIndex];

            _playerWeapon.SetDamage(currentLevelData.Damage);

            // Update special ability cooldown
            //_playerAbility.SetCooldown(currentLevelData.SpecialAbilityCooldown);

            Debug.Log($"Leveled up to Level {currentLevelData.Level}");
        }
    }
}
