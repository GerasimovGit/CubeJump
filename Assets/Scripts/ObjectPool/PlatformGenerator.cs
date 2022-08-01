using Level;
using Player;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ObjectPool
{
    public class PlatformGenerator : ObjectPool
    {
        private const int MAXRandomValue = 100;
        private const int MINRandomValue = 0;

        [SerializeField] private CubeMover _cubeMover;
        [SerializeField] private GameObject _template;
        [SerializeField] private GameObject _startPlatform;
        [SerializeField] private GameObject _boost;
        [SerializeField] private Vector3 _startPlatformPosition;
        [SerializeField] private float _maxSpawnPositionX;
        [SerializeField] private float _minSpawnPositionX;
        [SerializeField] private float _minPlatformWidth;
        [SerializeField] private float _changeSizeValue;
        [SerializeField] private float _rangeBetweenPlatforms;
        [SerializeField] private int _chanceToSpawnBoost;

        private Vector3 _defaultPlatformPosition;
        private Vector3 _nextPlatformPosition;

        private void Awake()
        {
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

        public void ResetGenerator()
        {
            _nextPlatformPosition = _defaultPlatformPosition;
            CreateFirstPlatform(_startPlatform, _startPlatformPosition);
            CreateNextPlatform();
        }

        public void CreateNextPlatform()
        {
            if (TryGetObject(out GameObject platform))
            {
                SetPosition(platform);
                ChangePlatformSize(platform);
                AddBoostWithRandomChance(platform);
                platform.SetActive(true);
            }
        }

        private void SetParameters()
        {
            _defaultPlatformPosition = transform.position;
            _nextPlatformPosition = _defaultPlatformPosition;
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
            var newPlatformSize = CalculateNewPlatformSize(platform);
            platform.transform.localScale = newPlatformSize;
        }

        private Vector3 CalculateNewPlatformSize(GameObject platform)
        {
            var newPlatformSize = _template.transform.localScale;
            float currentPositionY = platform.transform.position.y;
            float maxPlatformWidth = _template.transform.lossyScale.x;
            float sizeAccordToPositionY = newPlatformSize.x - currentPositionY / _changeSizeValue;
            newPlatformSize.x = Mathf.Clamp(sizeAccordToPositionY, _minPlatformWidth, maxPlatformWidth);
            return newPlatformSize;
        }

        private void AddBoostWithRandomChance(GameObject platform)
        {
            int randomChanceValue = Random.Range(MINRandomValue, MAXRandomValue);
            bool chance = randomChanceValue <= _chanceToSpawnBoost;

            if (chance && platform.GetComponentInChildren<PowerUp>() == false)
            {
                GameObject boost = Instantiate(_boost, platform.transform.position, quaternion.identity);
                boost.transform.SetParent(platform.transform);
            }
        }

        public void CreatePlatformAfterBoost()
        {
            GameObject newPlatform = Instantiate(_startPlatform);
            SetPlatformPositionAfterBoost(newPlatform);
            CreateNextPlatform();
        }

        private void SetPlatformPositionAfterBoost(GameObject newPlatform)
        {
            float offsetY = 3.5f;
            Vector3 position = new Vector3(0f,
                _cubeMover.transform.position.y + _cubeMover.NextPositionYAfterBoost - offsetY,
                0f);
            newPlatform.transform.position = position;
            _nextPlatformPosition.y = position.y;
        }
    }
}