using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class uiController : MonoBehaviour
{
    //Global Varibles
    //Scriptable Object Refrences
    [SerializeField] private GameScriptableObject gameVariables = null;
    [SerializeField] private GameObject gameOverPanel = null;

    //Image Fields
    [SerializeField] private Image nextMinoImage = null;

    //Text Fields
    [SerializeField] private TextMeshProUGUI scoreText = null;

    public void AddScoreAndDisplay()
    {
        gameVariables.AddScore();
        scoreText.text =  $"Score :\n {gameVariables.currentSessionScore}" ;
    }

    //Yet to be completed
    /*Right now the next mino image is not linked to any image, only the next mino gameobject is stored, after completing the image for the minos and storing them. 
     The linking part will be done */
    public void DisplayGameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
