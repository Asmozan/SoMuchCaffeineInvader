using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

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

            _playerAbility.SetCooldown(currentLevelData.SpecialAbilityCooldown);
            _playerAbility.SetDamage(currentLevelData.Damage);
            _playerAbility.SetRadius(currentLevelData.SpecialAbilityRadius);

            Debug.Log($"Leveled up to Level {currentLevelData.Level}");
        }
        
        #if UNITY_EDITOR
        
        [ContextMenu("Load Level Data")]
        private void LoadLevelData()
        {
            PlayerLevelDataList levelDataList = JsonConvert.DeserializeObject<PlayerLevelDataList>(_levelDataJson.text);
            _levels = levelDataList.Levels;
        }

        [ContextMenu("Save Level Data")]
        private void SaveLevelData()
        {
            PlayerLevelDataList levelDataList = new PlayerLevelDataList {Levels = _levels};
            string json = JsonConvert.SerializeObject(levelDataList, Formatting.Indented);
            string path = AssetDatabase.GetAssetPath(_levelDataJson);
            System.IO.File.WriteAllText(path, json);
        }
        
        #endif // UNITY_EDITOR
    }
}
