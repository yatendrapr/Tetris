using UnityEngine;

public class MinosSpawner : MonoBehaviour
{
    //Global Variable
    //Spawning Variables
    public static bool spawnMino = true;
    
    //Local Variables
    //Time Variables
    [SerializeField] private float spawnStartTime = 1f;
    private int spawnerCalls = 0;


    //Mino Spawning Variables
    [SerializeField] private Transform minoSpawnPoint = null;
    [SerializeField] private GameObject[] minos = null;
    private GameObject currentSpawnedMino = null;
    private GameObject nextMino = null;

    private void Update()
    {
        if(MinosSpawner.spawnMino)
        {
            switch(spawnerCalls)
            {
                case 0:
                    if (Time.time >= spawnStartTime)
                    {
                        MinosSpawner.spawnMino = false;
                        spawnerCalls++;
                        SpawnMinoGameObject();
                    }
                    break;
                default:
                    MinosSpawner.spawnMino = false;
                    spawnerCalls++;
                    SpawnMinoGameObject();
                    break;
            }
        }
    }

    //Under Debugging
    private void SpawnMinoGameObject()
    {
        if(nextMino == null)
        {
            int randomValue = Random.Range(0, minos.Length);
            currentSpawnedMino = Instantiate<GameObject>(minos[randomValue], minoSpawnPoint.transform.position, Quaternion.identity);
            nextMino = GetNextMino();
            Debug.Log(nextMino.name);
        }
        else
        {
            currentSpawnedMino = nextMino;
            nextMino.SetActive(true);
            nextMino = GetNextMino();
            Debug.Log(nextMino.name);
        }
    }

    private GameObject GetNextMino()
    {
        int randomValue = Random.Range(0, minos.Length);
        GameObject mino = Instantiate<GameObject>(minos[randomValue], minoSpawnPoint.transform.position, Quaternion.identity);
        mino.SetActive(false);
        return mino;
    }
}
