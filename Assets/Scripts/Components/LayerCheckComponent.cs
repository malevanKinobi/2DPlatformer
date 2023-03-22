using UnityEngine;

namespace Scripts.Components
{
    public class LayerCheckComponent : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundCheck;
        private Collider2D _groundCollider;

        public bool IsTouchingLayer;

        private void Awake()
        {
            _groundCollider = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            IsTouchingLayer = _groundCollider.IsTouchingLayers(_groundCheck);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            IsTouchingLayer = _groundCollider.IsTouchingLayers(_groundCheck);
        }

    }
}

