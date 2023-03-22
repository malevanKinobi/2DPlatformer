using UnityEngine;

namespace Scripts.Components
{
    public class SwitchComponent : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _animatorParametr;
        [SerializeField] private bool _state;

        public void Switch()
        {
            _state = !_state;
            _animator.SetBool(_animatorParametr, _state);
        }

    }
}


