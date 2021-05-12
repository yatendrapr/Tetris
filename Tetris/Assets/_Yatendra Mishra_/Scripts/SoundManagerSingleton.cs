using UnityEngine;

public class SoundManagerSingleton : MonoBehaviour
{

    #region Data Members

    #region Global Variables

    //Singleton
    private static SoundManagerSingleton instance = null;
    public static SoundManagerSingleton Instance { get => instance; private set { } }

    #endregion

    #region Local Variables

    //Audio Source
    [SerializeField] private AudioSource lineClearAudioSource = null;
    [SerializeField] private AudioSource buttonClickAudioSource = null;
    [SerializeField] private AudioSource gameOverAudioSource = null;

    #endregion

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    #endregion

    #region Member Functions

    public void ButtonClickPlay() => buttonClickAudioSource.Play();

    public void LineClearPlay() => lineClearAudioSource.Play();

    public void GameOverPlay() => gameOverAudioSource.Play();

    #endregion

}
