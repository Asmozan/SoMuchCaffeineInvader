using Invaders.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Invader.Player
{
    public class PlayerControlsHandler : MonoBehaviour, PlayerControls.IPlayerActions
    {
        private PlayerController _playerController;
        
        private void Awake()
        {
            TryGetComponent(out _playerController);
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            _playerController.Movement = context.ReadValue<Vector2>();
        }
        
        public void OnFire(InputAction.CallbackContext context)
        {
            _playerController.IsFireButtonPressed = context.performed;
        }
        
        public void OnSpecial(InputAction.CallbackContext context)
        {
            _playerController.IsSpecialButtonPressed = context.performed;
        }
    }
}
