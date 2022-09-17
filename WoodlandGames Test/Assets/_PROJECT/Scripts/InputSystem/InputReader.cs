using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _PROJECT.Scripts.InputSystem
{
    public class InputReader : MonoBehaviour, PlayerMenuControls.IPlayerActions
    {
        public event Action QuickPauseEvent;
        
        public void OnQuickPause(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
        
            QuickPauseEvent?.Invoke();
        }
    }
}
