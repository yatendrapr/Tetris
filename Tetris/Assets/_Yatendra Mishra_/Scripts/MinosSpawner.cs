using UnityEngine;

public class MinosSpawner : MonoBehaviour
{
    //Global Variables//

    //Spawning Variables
    private static bool spawnMino = true;
    public static bool SpawnMino { set { spawnMino = value; } }

    //This variable will hold the index of the next mino i.e the mino that is going to spawn next
    private static int spawnedMinoIndex = 0;
    public static int SpawnedMinoIndex { get { return MinosSpawner.spawnedMinoIndex; } }

    //Game Events
    [SerializeField] private GameEvent tetrominoSpawnEvent = null;

    //Local Variables//

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
                        MinosSpawner.SpawnMino = false;
                        spawnerCalls++;
                        SpawnMinoGameObject();
                    }
                    break;
                default:
                    MinosSpawner.SpawnMino = false;
                    spawnerCalls++;
                    SpawnMinoGameObject();
                    break;
            }
        }
    }

    private void SpawnMinoGameObject()
    {
        if(nextMino == null)
        {
            int randomValue = Random.Range(0, minos.Length);
            currentSpawnedMino = Instantiate<GameObject>(minos[randomValue], minoSpawnPoint.transform.position, Quaternion.identity);
            nextMino = GetNextMino();
            tetrominoSpawnEvent.RaiseEvent();
        }
        else
        {
            currentSpawnedMino = nextMino;
            nextMino.SetActive(true);
            nextMino = GetNextMino();
            tetrominoSpawnEvent.RaiseEvent();
        }
    }

    private GameObject GetNextMino()
    {
        int randomValue = Random.Range(0, minos.Length);
        spawnedMinoIndex = randomValue;
        GameObject mino = Instantiate<GameObject>(minos[randomValue], minoSpawnPoint.transform.position, Quaternion.identity);
        mino.SetActive(false);
        return mino;
    }
}
