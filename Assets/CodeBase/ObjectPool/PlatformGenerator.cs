using CodeBase.Player;
using UnityEngine;

namespace CodeBase.ObjectPool
{
    public class PlatformGenerator : ObjectPool
    {
        private const float MinPlatformWidth = 1f;

        [SerializeField] private CubeMover _cubeMover;
        [SerializeField] private GameObject _template;
        [SerializeField] private GameObject _startPlatform;
        [SerializeField] private GameObject _boost;
        [SerializeField] private Vector3 _startPlatformPosition;
        [SerializeField] private float _maxSpawnPositionX;
        [SerializeField] private float _minSpawnPositionX;
        [SerializeField] private float _rangeBetweenPlatforms;
        [SerializeField] private float _changeSizeValue;
        [SerializeField] private int _chanceToSpawnBoost;

        private readonly int _maxRandomValue = 100;
        private readonly int _minRandomValue = 0;

        private Vector3 _defaultPlatformPosition;
        private Vector3 _defaultPlatformSize;
        private Vector3 _newPlatformSize;
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

        private void SetParameters()
        {
            _defaultPlatformPosition = transform.position;
            _nextPlatformPosition = _defaultPlatformPosition;
            _defaultPlatformSize = _template.transform.localScale;
            _newPlatformSize = _defaultPlatformSize;
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
            int randomChanceValue = Random.Range(_minRandomValue, _maxRandomValue);

            if (randomChanceValue <= _chanceToSpawnBoost)
            {
                if (!platform.GetComponentInChildren<PowerUp>())
                {
                    var boost = Instantiate(_boost, platform.transform);
                    SetBoostSize(platform, boost);
                }
            }
        }

        private void SetBoostSize(GameObject platform, GameObject boost)
        {
            boost.transform.parent = null;
            boost.transform.localScale = _boost.transform.localScale;
            boost.transform.SetParent(platform.transform);
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
            CalculateSize(platform);
            platform.transform.localScale = _newPlatformSize;
        }

        private void CalculateSize(GameObject platform)
        {
            float currentPositionY = platform.transform.position.y;
            float maxPlatformWidth = _template.transform.lossyScale.x;
            float changeSizeValue = _newPlatformSize.x - currentPositionY / _changeSizeValue;
            _newPlatformSize.x = Mathf.Clamp(changeSizeValue, MinPlatformWidth, maxPlatformWidth);
        }

        public void ResetGenerator()
        {
            _nextPlatformPosition = _defaultPlatformPosition;
            _newPlatformSize = _defaultPlatformSize;
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
            Vector3 position = new Vector3(0f,
                _cubeMover.transform.position.y + _cubeMover.NextPositionYAfterBoost - 3.5f,
                0f);
            newPlatform.transform.position = position;
            _nextPlatformPosition.y = position.y;
        }
    }
}