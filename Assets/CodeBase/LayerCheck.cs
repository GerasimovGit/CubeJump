using UnityEngine;

namespace CodeBase
{
    [RequireComponent(typeof(Collider2D))]
    public class LayerCheck : MonoBehaviour
    {
        [SerializeField] private Collider2D _platformCheck;
        [SerializeField] private LayerMask _whatIsPlatform;

        public bool IsTouchingPlatform { get; private set; }

        private void Awake()
        {
            _platformCheck = GetComponent<Collider2D>();
        }

        private void Update()
        {
            IsTouchingPlatform = _platformCheck.IsTouchingLayers(_whatIsPlatform);
        }
    }
}