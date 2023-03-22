using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Components
{
    public class InteractionComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _eventInteration;

        public void InteractEvent()
        {
            _eventInteration?.Invoke();
        }
    }

}

