using Player;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ObjectPool
{
    [RequireComponent(typeof(PlatformSizeChanger),typeof(RandomChanceHandler))]
    public class PlatformGenerator : ObjectPool
    {
        [SerializeField] private CubeMover _cubeMover;
        [SerializeField] private GameObject _template;
        [SerializeField] private GameObject _startPlatform;
        [SerializeField] private GameObject _boost;
        [SerializeField] private Vector3 _startPlatformPosition;
        [SerializeField] private float _maxSpawnPositionX;
        [SerializeField] private float _minSpawnPositionX;
        [SerializeField] private float _rangeBetweenPlatforms;

        private RandomChanceHandler _randomChance;
        private PlatformSizeChanger _platformSizeChanger;
        private Vector3 _defaultPlatformPosition;
        private Vector3 _nextPlatformPosition;

        private void Awake()
        {
            _randomChance = GetComponent<RandomChanceHandler>();
            _platformSizeChanger = GetComponent<PlatformSizeChanger>();
            SetParameters();
        }

        private void Start()
        {
            Initialize(_template);
            CreateFirstPlatform(_startPlatform, _startPlatformPosition);
            CreateNextPlatform();
        }

        private void Update()
        {
            DisableObjectAboardScreen();
        }

        private void SetParameters()
        {
            _defaultPlatformPosition = transform.position;
            _nextPlatformPosition = _defaultPlatformPosition;
        }

        public void CreateNextPlatform()
        {
            if (TryGetObject(out GameObject platform))
            {
                SetPosition(platform);
                ChangePlatformSize(platform);
                AddBoost(platform);
                platform.SetActive(true);
            }
        }

        private void AddBoost(GameObject platform)
        {
            if (_randomChance.TryGetChance() && platform.GetComponentInChildren<PowerUp>() == false)
            {
                var boost = Instantiate(_boost, platform.transform.position,quaternion.identity);
                boost.transform.SetParent(platform.transform);
            }
        }
        
        private void SetPosition(GameObject platform)
        {
            float spawnPositionX = Random.Range(_minSpawnPositionX, _maxSpawnPositionX);
            _nextPlatformPosition.y += _rangeBetweenPlatforms;
            _nextPlatformPosition.x = spawnPositionX;
            platform.transform.position = _nextPlatformPosition;
        }

        private void ChangePlatformSize(GameObject platform)
        {
            _platformSizeChanger.ChangeSize(_template,platform,out Vector3 newPlatformSize);
           platform.transform.localScale = newPlatformSize;
        }
        

        public void ResetGenerator()
        {
            _nextPlatformPosition = _defaultPlatformPosition;
            CreateFirstPlatform(_startPlatform, _startPlatformPosition);
            CreateNextPlatform();
        }

        public void CreatePlatformAfterBoost()
        {
            GameObject newPlatform = Instantiate(_startPlatform);
            SetPlatformPositionAfterBoost(newPlatform);
        }

        private void SetPlatformPositionAfterBoost(GameObject newPlatform)
        {
            var offsetY = 3.5f;
            Vector3 position = new Vector3(0f,
                _cubeMover.transform.position.y + _cubeMover.NextPositionYAfterBoost - offsetY,
                0f);
            newPlatform.transform.position = position;
            _nextPlatformPosition.y = position.y;
        }
    }
}