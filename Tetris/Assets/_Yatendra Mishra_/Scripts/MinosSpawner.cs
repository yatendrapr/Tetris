using UnityEngine;

public class MinosSpawner : MonoBehaviour
{

    #region Data Members

    #region Global Variables
    
    //Spawning Variables
    private static bool spawnMino = true;
    public static bool SpawnMino { set { spawnMino = value; } }

    //This variable will hold the index of the next mino i.e the mino that is going to spawn next
    private static int spawnedMinoIndex = 0;
    public static int SpawnedMinoIndex { get { return MinosSpawner.spawnedMinoIndex; } }

    [Header("Events")]
    //Game Events
    [SerializeField] private GameEvent tetrominoSpawnEvent = null;

    #endregion

    #region Local Variables
    
    [Header("Time Variables")]
    //Time Variables
    [SerializeField] private float spawnStartTime = 1f;
    private float currentSpawnTime = 0f;
    private int spawnerCalls = 0;

    [Header("Mino Spawning Variables")]
    //Mino Spawning Variables
    [SerializeField] private Transform minoSpawnPoint = null;
    [SerializeField] private GameObject[] minos = null;
    private GameObject currentSpawnedMino = null;
    private GameObject nextMino = null;

    #endregion

    #endregion

    #region Unity Methods
    
    /*Current time is set to 0 here because, when the game start from Main Menu the 
     elapsed time has already crossed spawnStartTime*/
    private void Awake()
    {
        currentSpawnTime = Time.time;
        MinosSpawner.spawnMino = true;
    }

    private void Update()
    {
        if(MinosSpawner.spawnMino)
        {
            switch(spawnerCalls)
            {
                case 0:
                    if ((Time.time - currentSpawnTime) >= spawnStartTime)
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

    #endregion

    #region Member Functions

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

    #endregion

}
