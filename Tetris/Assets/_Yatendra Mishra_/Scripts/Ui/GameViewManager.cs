using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameViewManager : MonoBehaviour
{

    #region Data Members
    //Global Varibles//

    //Scriptable Object Refrences
    [Header("Scriptable Object References")]
    [SerializeField] private ScoreScriptableObject scoreVariables = null;

    //UI References
    [Header("UI References")]
    [SerializeField] private Image nextMinoImage = null;
    [SerializeField] private TextMeshProUGUI scoreText = null;
    [SerializeField] private GameObject pauseScreenPanel = null;

    //Data Structres
    [Header("TetroMino Images")]
    [SerializeField] private Sprite[] tetroMinoImages = null;

    #endregion

    #region Unity Messages

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetGameobjectActive(pauseScreenPanel, true);
            SetTimeScale(0f);
        }
    }

    #endregion

    #region Member Functions

    public void AddScoreAndDisplay()
    {
        scoreVariables.AddScore();
        scoreText.text = $"Score :\n {scoreVariables.CurrentSessionScore}";
    }

    public void DisplayNextMinoImage() => nextMinoImage.sprite = tetroMinoImages[MinosSpawner.SpawnedMinoIndex];

    private void SetGameobjectActive(GameObject gameObject, bool value)
    {
        gameObject.SetActive(value);
    }

    public void ContinuePressed()
    {
        SetGameobjectActive(pauseScreenPanel, false);
        SetTimeScale(1f);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        SetTimeScale(1f);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Game Complete");
        SceneManager.LoadScene("Game Complete UI", LoadSceneMode.Additive);
    }

    private void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    #endregion

}
