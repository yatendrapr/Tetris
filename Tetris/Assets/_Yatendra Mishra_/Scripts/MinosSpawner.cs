using UnityEngine;

public class MinosSpawner : MonoBehaviour
{
    //Local Variables
    //Time Variables
    [SerializeField] private float spawnStartTime = 1f;
    [SerializeField] private float spawnRepeatRate = 1f;
    private float currentSpawnRepeatTime = 0f;

    //Spawning Variables
    private int SpawningStartedCall = 0;
    [SerializeField] private bool ContinueSpawning = true;

    //Mino Spawning Variables
    [SerializeField] private Transform minoSpawnPoint = null;
    private GameObject currentSpawnedMino = null;
    [SerializeField] private GameObject[] minos = null;

    private void Update()
    {
        if(ContinueSpawning)
        {
            if (Time.time >= spawnStartTime)
            {
                SpawningStartedCall++;
                StartSpawning();
            }
            else
            {
                return;
            }
        }
        //transform.RotateAround()
    }

    private void StartSpawning()
    {
        if (SpawningStartedCall == 1)
        {
            ContinueSpawning = false;
            SpawnMino();
        }
        else if(SpawningStartedCall > 1)
        {
            currentSpawnRepeatTime += Time.deltaTime;
            if(currentSpawnRepeatTime >= spawnRepeatRate)
            {
                currentSpawnRepeatTime = 0f;
                ContinueSpawning = false;
                SpawnMino();
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }

    private void SpawnMino()
    {
        int randomValue = Random.Range(0, minos.Length);
        currentSpawnedMino = Instantiate<GameObject>(minos[randomValue], minoSpawnPoint.transform.position, Quaternion.identity);
    }
}
