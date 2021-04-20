using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    //Global Varibles
    //Scriptable Object Refrences
    [SerializeField] private ScoreScriptableObject gameVariables = null;
    [SerializeField] private GameObject gameOverPanel = null;

    //Image Fields
    [SerializeField] private Image nextMinoImage = null;
    [SerializeField] private Sprite[] tetroMinoImages = null;

    //Text Fields
    [SerializeField] private TextMeshProUGUI scoreText = null;

    public void AddScoreAndDisplay()
    {
        gameVariables.AddScore();
        scoreText.text =  $"Score :\n {gameVariables.CurrentSessionScore}" ;
    }

    public void DisplayGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void DisplayNextMinoImage()
    {
        nextMinoImage.sprite = tetroMinoImages[MinosSpawner.SpawnedMinoIndex];
    }
}
