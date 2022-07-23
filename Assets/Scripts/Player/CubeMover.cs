using System.Collections;
using Effects;
using Platforms;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(LayerCheck))]
    public class CubeMover : MonoBehaviour
    {
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private float _tapUpForce;
        [SerializeField] private float _tapDownForce;
        [SerializeField] private float _angleForce;
        [SerializeField] private float _fallMultiplier;

        public UnityEvent OnPlatformChange;
        public UnityEvent OnBoostActivate;
        public UnityEvent OnJump;

        private readonly float _boostGravity = 0f;
        private readonly float _speedOnBoost = 12f;

        private float _defaultGravity;
        private bool _isOnPlatform;
        private Coroutine _coroutine;
        private CubeEffectsPlayer _cubeEffectsPlayer;
        private Vector3 _jumpPosition;
        private Platform _platform;
        private LayerCheck _platformCheck;
        private Rigidbody2D _rigidbody;

        public float NextPositionYAfterBoost => 25f;

        public Vector3 LastPlatformJumpPosition { get; private set; }

        public float YVelocity { get; private set; }

        public bool IsBoostActivate { get; private set; }

        public bool IsJumpButtonPressed { get; private set; }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _platformCheck = GetComponent<LayerCheck>();
        }

        private void Start()
        {
            ResetCube();
            _defaultGravity = _rigidbody.gravityScale;
        }

        private void Update()
        {
            SetParameters();
            GetPlatformMoveDirection(out Vector2 direction);
            Jump(direction);
        }

        private void FixedUpdate()
        {
            ApplyFallGravity();
        }

        private void Jump(Vector2 moveDirection)
        {
            if (IsBoostActivate) return;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                OnJump?.Invoke();

                if (_isOnPlatform)
                {
                    LastPlatformJumpPosition = transform.position;

                    if (moveDirection.x != 0)
                    {
                        _rigidbody.AddForce(Vector2.up * _tapUpForce - moveDirection * _angleForce,
                            ForceMode2D.Impulse);
                    }
                    else
                    {
                        _rigidbody.AddForce(Vector2.up * _tapUpForce, ForceMode2D.Impulse);
                    }
                }
                else
                {
                    _rigidbody.AddForce(Vector3.down * _tapDownForce, ForceMode2D.Impulse);
                }
            }
        }

        private void SetParameters()
        {
            YVelocity = _rigidbody.velocity.y;
            _isOnPlatform = _platformCheck.IsTouchingPlatform;
            IsJumpButtonPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
        }

        private bool TryGetPlatform(out Platform result)
        {
            result = gameObject.GetComponentInParent<Platform>();

            return result != null;
        }

        private void GetPlatformMoveDirection(out Vector2 direction)
        {
            direction = Vector2.zero;

            if (TryGetPlatform(out Platform platform))
            {
                direction = platform.Direction;
            }
        }

        private void ApplyFallGravity()
        {
            if (YVelocity < 0)
            {
                _rigidbody.velocity += Vector2.up * Physics.gravity * (_fallMultiplier * Time.deltaTime);
            }
        }

        public void ResetCube()
        {
            transform.position = _startPosition;
        }

        private void SetRigidbodyGravity(float amount)
        {
            _rigidbody.gravityScale = amount;
        }

        public void ActivateBoost()
        {
            CheckExistCoroutine();
            SetDestinationPoint(out Vector3 destinationPoint);
            SetRigidbodyGravity(_boostGravity);
            _coroutine = StartCoroutine(ApplyBoost(destinationPoint));
        }

        private void CheckExistCoroutine()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }

        private void SetDestinationPoint(out Vector3 destinationPoint)
        {
            destinationPoint = new Vector3(0f, transform.position.y + NextPositionYAfterBoost, transform.position.z);
        }

        private IEnumerator ApplyBoost(Vector3 destinationPoint)
        {
            IsBoostActivate = true;
            OnBoostActivate?.Invoke();
            
            while (transform.position.y < destinationPoint.y)
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, destinationPoint, _speedOnBoost * Time.deltaTime);
                yield return null;
            }

            IsBoostActivate = false;
            SetRigidbodyGravity(_defaultGravity);
        }
    }
}