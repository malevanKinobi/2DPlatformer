using UnityEngine;

namespace Scripts.Components
{
    public class TeleportComponent : MonoBehaviour
    {
        [SerializeField] private Transform _teleportPoint;

        public void Teleport(GameObject target)
        {
            target.transform.position = _teleportPoint.position;
        }
    }
}

