using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] protected GameObject enemy;
    [SerializeField] protected Transform spawnPoint;

    public void Spawn()
    {
        Instantiate(enemy, spawnPoint.position, Quaternion.identity);
    }
}
