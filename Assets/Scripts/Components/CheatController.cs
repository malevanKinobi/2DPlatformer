using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Scripts.Components
{
    public class CheatController : MonoBehaviour
    {
        [SerializeField] private float _inputTimeToLive;
        [SerializeField] private CheatItem[] _cheatItem;

        private string _currentInput;
        private float _currentTime;

        private void Awake()
        {
            Keyboard.current.onTextInput += OnTextInput;
        }


        private void OnDestroy()
        {
            Keyboard.current.onTextInput -= OnTextInput;
        }


        private void OnTextInput(char inputChar)
        {
            _currentInput += inputChar;
            _currentTime = _inputTimeToLive;
            FindAnyCheat();
        }

        private void Update()
        {
            if (_currentTime < 0)
            {
                _currentInput = string.Empty;
            }
            else
            {
                _currentTime -= Time.deltaTime;
            }
        }

        private void FindAnyCheat()
        {
            foreach (var cheat in _cheatItem)
            {
                if (_currentInput.Contains(cheat.Name))
                {
                    cheat.Action?.Invoke();
                    _currentInput = string.Empty;
                }
            }
        }
    }
}

[Serializable]
public class CheatItem
{
    public string Name;
    public UnityEvent Action;
}

