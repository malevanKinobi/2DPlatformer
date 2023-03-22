using UnityEngine;

namespace Scripts.Components
{
    public class DestroyObjectComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _destroyObject;

        public void DestroyObject()
        {
            Destroy(gameObject);
        }
    }

}
