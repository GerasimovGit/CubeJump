using UnityEngine;

namespace CodeBase.Platforms
{
    public class StartPlatform : MonoBehaviour
    {
        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}