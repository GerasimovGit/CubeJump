using UnityEngine;

public class RandomChanceHandler : MonoBehaviour
{
    [SerializeField] private int _chanceToSpawnBoost;

    private const int MAXRandomValue = 100;
    private const int MINRandomValue = 0;

    public bool TryGetChance()
    {
        int randomChanceValue = Random.Range(MINRandomValue, MAXRandomValue);
        bool chance = randomChanceValue <= _chanceToSpawnBoost;
        return chance;
    }
}