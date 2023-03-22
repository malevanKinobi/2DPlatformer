using UnityEngine;

namespace Scripts.Components
{
    public class ModifyHealthComponent : MonoBehaviour
    {
        [SerializeField] private int _modifyValue;

        public void Apply(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();

            if (healthComponent != null)
            {
                healthComponent.ModifyHealth(_modifyValue);
            }
        }
    }
}
