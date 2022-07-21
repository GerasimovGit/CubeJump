using CodeBase.Player;
using UnityEngine;

namespace CodeBase.Platforms
{
    [RequireComponent(typeof(Platform))]
    public class PlatformCollisionHandler : MonoBehaviour
    {
        private Platform _platform;

        private void Awake()
        {
            _platform = GetComponent<Platform>();
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            _platform.RemoveFromParent(other.gameObject.transform);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out CubeMover cubeMover))
            {
                bool isStandAbovePlatform = cubeMover.transform.position.y - transform.position.y >=
                                            _platform.transform.localScale.y;

                if (cubeMover.YVelocity <= 0f && cubeMover.transform.parent == null && isStandAbovePlatform)
                {
                    _platform.SetParent(cubeMover.transform);

                    if (cubeMover.LastPlatformJumpPosition.y < transform.position.y)
                    {
                        cubeMover.OnPlatformChange?.Invoke();
                    }
                }
            }
        }
    }
}