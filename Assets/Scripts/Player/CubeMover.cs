using System.Collections;
using Platforms;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(LayerCheck), typeof(TouchUI))]
    public class CubeMover : MonoBehaviour
    {
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private float _tapUpForce;
        [SerializeField] private float _tapDownForce;
        [SerializeField] private float _angleForce;
        [SerializeField] private float _fallMultiplier;

        public UnityEvent PlatformChanged;
        public UnityEvent BoostActivated;
        public UnityEvent Jumped;

        private readonly float _boostGravity = 0f;
        private readonly float _boostDuration = 2.25f;

        private float _defaultGravity;
        private bool _isOnPlatform;
        private Vector3 _defaultScale;
        private Vector3 _jumpPosition;
        private Coroutine _coroutine;
        private CubeEffects _cubeEffects;
        private LayerCheck _platformCheck;
        private Platform _platform;
        private Rigidbody2D _rigidbody;
        private TouchUI _touchUI;

        public float NextPositionYAfterBoost => 25f;

        public Vector3 LastPlatformJumpPosition { get; private set; }

        public float YVelocity { get; private set; }

        public bool IsBoostActivate { get; private set; }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _platformCheck = GetComponent<LayerCheck>();
            _touchUI = GetComponent<TouchUI>();
        }

        private void Start()
        {
            _defaultScale = transform.localScale;
            _defaultGravity = _rigidbody.gravityScale;
            ResetCube();
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

        public void ResetCube()
        {
            StopCurrentCoroutine();
            IsBoostActivate = false;
            transform.parent = null;
            transform.position = _startPosition;
            transform.localScale = _defaultScale;
            _rigidbody.velocity = Vector2.zero;
            SetGravity(_defaultGravity);
        }

        private void SetParameters()
        {
            YVelocity = _rigidbody.velocity.y;
            _isOnPlatform = _platformCheck.IsTouchingPlatform;
        }

        private void ApplyFallGravity()
        {
            if (YVelocity < 0)
            {
                _rigidbody.velocity += Vector2.up * Physics.gravity * (_fallMultiplier * Time.deltaTime);
            }
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

        private void Jump(Vector2 moveDirection)
        {
            if (IsBoostActivate || Time.timeScale < 1 || _touchUI.IsTouchingUi) return;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Jumped?.Invoke();

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

        public void ActivateBoost()
        {
            StopCurrentCoroutine();
            SetGravity(_boostGravity);
            _rigidbody.velocity = Vector2.zero;
            SetDestinationPoint(out Vector3 destinationPoint);
            _coroutine = StartCoroutine(LerpToPoint(destinationPoint));
        }

        private void StopCurrentCoroutine()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }

        private void SetGravity(float amount)
        {
            _rigidbody.gravityScale = amount;
        }

        private void SetDestinationPoint(out Vector3 destinationPoint)
        {
            destinationPoint = new Vector3(0f, transform.position.y + NextPositionYAfterBoost, transform.position.z);
        }

        private IEnumerator LerpToPoint(Vector3 destinationPoint)
        {
            IsBoostActivate = true;
            BoostActivated?.Invoke();
            float timeElapsed = 0;
            Vector3 startPosition = transform.position;
            
            while (timeElapsed < _boostDuration)
            {
                transform.position =
                    Vector3.Lerp(startPosition, destinationPoint, timeElapsed/_boostDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = destinationPoint;
            IsBoostActivate = false;
            SetGravity(_defaultGravity);
        }
    }
}