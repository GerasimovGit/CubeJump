using UnityEngine;

namespace Platforms
{
    public class StartPlatform : MonoBehaviour
    {
        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}