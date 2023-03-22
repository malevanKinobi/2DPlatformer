using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;

        [SerializeField] private UnityEvent _onHeal;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;

        public void ModifyHealth(int value)
        {
            _health += value;

            if (value > 0)
                _onHeal?.Invoke();

            if (value < 0)
                _onDamage?.Invoke();

            if (_health <= 0)
                _onDie?.Invoke();

        }
    }
}