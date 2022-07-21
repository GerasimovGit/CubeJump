using UnityEngine;

namespace CodeBase
{
    public class PowerUp : MonoBehaviour
    {
        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}