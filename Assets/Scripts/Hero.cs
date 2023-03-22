using UnityEngine;
using Scripts.Components;

namespace Scripts
{
    public class Hero : MonoBehaviour
    {
        [Header("SPEED PROPERTY")]
        [SerializeField] private float _speed;

        [Header("JUMP PROPERTY")]
        [SerializeField] private float _forceJump;
        [SerializeField] private int _maxCountJump;
        [SerializeField] private float _damageForceJump;
        [SerializeField] private LayerCheckComponent _groundCheck;

        [Header("COINS PROPERTY")]
        [SerializeField] private int _coinCount;
        [SerializeField] private int _lostCoinsAtTime;

        [Header("INTERACTIONS PROPERTY")]
        [SerializeField] private float _interactCircleRadius;
        [SerializeField] private ContactFilter2D _interactLayer;
        [SerializeField] private Collider2D[] _resultInteraction = new Collider2D[1];

        

        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private SpriteRenderer _sprite;

        [Header("PARTICLE SYSTEM")]
        [SerializeField] private ParticleSystem _particleSystem;

        [SerializeField] private SpawnCustomParticleComponent _spawnComponent;

        private Vector2 _direction;      
        private bool _isGrounded;
        private int _currentCountJump;
        private bool _isJumping;
        
        

        private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        private static readonly int IsRunning = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _sprite = GetComponent<SpriteRenderer>();
        }        

        private void FixedUpdate()
        {
            var xVelocity = _direction.x * _speed;
            var yVelocity = CalculateYVelocity();            
            _rigidbody.velocity = new Vector2(xVelocity, yVelocity);

            UpdateSpriteDirection();
        }

        private void Update()
        {
            _isGrounded = IsGrounded();

            _animator.SetBool(IsRunning, _rigidbody.velocity.x != 0);
            _animator.SetBool(IsGroundKey, _isGrounded);
            _animator.SetFloat(VerticalVelocity, _rigidbody.velocity.y);
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        public void SaySomething()
        {
            Debug.Log("Something");
        }

        private float CalculateYVelocity()
        {
            var YVelocity = _rigidbody.velocity.y;
            var isJumpPressing = _direction.y > 0;

            if (_isGrounded)
            {
                _currentCountJump = _maxCountJump; 
                _isJumping = false;
            }

            if (isJumpPressing)
            {
                _isJumping = true;
                YVelocity = CalculateJumpVelocity(YVelocity); 
            }
            else if (YVelocity > 0 && _isJumping)
            {
                YVelocity *= 0.5f;  
            }

            return YVelocity;
        }

        private float CalculateJumpVelocity( float YVelocity)
        {
            var isFalling = _rigidbody.velocity.y <= 0.001f;

            if (!isFalling) return YVelocity;
            if (_currentCountJump == 0) return YVelocity;

            if (_isGrounded)
            {
                YVelocity += _forceJump;
                _currentCountJump--;
            }
            else
            {
                YVelocity = _forceJump;
                _currentCountJump--;
            }
            return YVelocity;
        }

        private void UpdateSpriteDirection()
        {
            if (_direction.x > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if (_direction.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
        }

        private bool IsGrounded()
        {
            return _groundCheck.IsTouchingLayer;
        }

        public void TakeDamage()
        {
            _isJumping = false;
            _animator.SetTrigger("is-hit");

            if (_coinCount > 0)
                SpawnCoinsParticle();

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damageForceJump);
        }

        public void AddCoins( int coins )
        {
            _coinCount += coins;
            Debug.Log($"{coins} - монет добавлено! Всего монет {_coinCount}");
        }

        public void InteractWithObject()
        {
            int hit = Physics2D.OverlapCircle(transform.position, _interactCircleRadius, _interactLayer, _resultInteraction);
            bool isHit = hit > 0;

            if (isHit)
            {
                var interactObject = _resultInteraction[0].GetComponent<InteractionComponent>();

                if (interactObject != null)
                    interactObject.InteractEvent();
            }
        }

        public void SpawnFootDustCustomParticle(string particleName)
        {
            _spawnComponent.Spawn(particleName);
        }    

        public void SpawnCoinsParticle()
        {
            var lostCoins = Mathf.Min(_coinCount, _lostCoinsAtTime);
            _coinCount -= lostCoins;

            var currentBurst = _particleSystem.emission.GetBurst(0);
            currentBurst.count = lostCoins;
            _particleSystem.emission.SetBurst(0, currentBurst);

            _particleSystem.gameObject.SetActive(true);
            _particleSystem.Play();
        }
    }
}

