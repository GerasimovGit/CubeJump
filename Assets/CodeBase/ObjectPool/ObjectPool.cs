using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace CodeBase.ObjectPool
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject _container;
        [SerializeField] private float _capacity;

        private readonly float _cameraDisablePointY = -0.5f;
        private readonly List<GameObject> _pool = new();

        private Camera _camera;

        protected void Initialize(GameObject prefab)
        {
            _camera = Camera.main;

            for (int i = 0; i < _capacity; i++)
            {
                var item = Instantiate(prefab, _container.transform);
                _pool.Add(item);
                item.SetActive(false);
            }
        }

        protected bool TryGetObject(out GameObject result)
        {
            result = _pool.FirstOrDefault(item => item.activeInHierarchy == false);
            return result != null;
        }

        protected void CreateFirstPlatform(GameObject prefab, Vector3 firstPlatformPosition)
        {
            Instantiate(prefab, firstPlatformPosition, quaternion.identity);
        }

        protected void DisableObjectAboardScreen()
        {
            Vector3 disablePoint = _camera.ViewportToWorldPoint(new Vector2(0f, _cameraDisablePointY));

            foreach (var item in _pool)
            {
                if (item.activeSelf)
                {
                    if (item.transform.position.y < disablePoint.y)
                    {
                        item.SetActive(false);
                    }
                }
            }
        }

        public void ResetPool()
        {
            foreach (var item in _pool)
            {
                item.SetActive(false);
            }
        }
    }
}