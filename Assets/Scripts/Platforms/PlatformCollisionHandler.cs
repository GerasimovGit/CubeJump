using Player;
using UnityEngine;

namespace Platforms
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
            if (!other.gameObject.TryGetComponent(out CubeMover cubeMover)) return;

            bool isStandAbovePlatform = cubeMover.transform.position.y - transform.position.y >=
                                        _platform.transform.localScale.y;

            if (cubeMover.YVelocity <= 0f && cubeMover.transform.parent == null && isStandAbovePlatform &&
                cubeMover.IsBoostActivate == false)
            {
                _platform.SetParent(cubeMover.transform);

                if (cubeMover.LastPlatformJumpPosition.y < transform.position.y)
                {
                    cubeMover.PlatformChanged?.Invoke();
                }
            }
        }
    }
}