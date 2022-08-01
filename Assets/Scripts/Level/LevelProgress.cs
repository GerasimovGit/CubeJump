using ObjectPool;
using Player;
using UnityEngine;

namespace Level
{
    public class LevelProgress : MonoBehaviour
    {
        [SerializeField] private Cube _cube;
        [SerializeField] private CubeTracker _cubeTracker;
        [SerializeField] private PlatformGenerator _platformGenerator;

        public void Reset()
        {
            _cube.ResetPlayer();
            _cubeTracker.ResetCamera();
            _platformGenerator.ResetPool();
            _platformGenerator.ResetGenerator();
        }
    }
}