using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsViewManager : MonoBehaviour
{

    #region Data Members

    //Global Variables//

    [Header("UI References")]
    //Button References
    [SerializeField] private Button soundButton = null;
    [SerializeField] private Button highscoreButton = null;
    [SerializeField] private Button creditButton = null;

    [Header("View References")]
    //View References
    [SerializeField] private OptionsView soundView = null;
    [SerializeField] private OptionsView highscoreView = null;
    [SerializeField] private OptionsView creditView = null;

    [Header("Start Button and View")]
    [Space][SerializeField] private OptionsView startView = null;
    [SerializeField] private Button startButton = null;

    [Header("Options View Offset")]
    [SerializeField] private float yAxisOffset = 0f;

    //Local Variables//

    //Data Structers
    private OptionsView[] views = null;

    //View Variables
    private OptionsView currentView = null;
    private OptionsView previousView = null;

    //Button Variables
    private Button previousButton = null;

    [Header("HighLighted Color Block")]
    //Color Variables
    [SerializeField] private ColorBlock highlightedColorBlock = ColorBlock.defaultColorBlock;
    private ColorBlock defaultHighlightedColorBlock = ColorBlock.defaultColorBlock;
    

    #endregion

    #region Unity Messages

    private void Awake()
    {
        views = transform.GetComponentsInChildren<OptionsView>(true);
        AssignAndActiveCurrentView(startView);
        HighLightButtonAndView(startButton, startView);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Main Menu");
    }
    
    #endregion

    #region Member Functions

    public void DisplaySoundView()
    {
        DisableAllViews();
        AssignAndActiveCurrentView(soundView);
        HighLightButtonAndView(soundButton,soundView);
    }

    public void DisplayHighScoreView()
    {
        DisableAllViews();
        AssignAndActiveCurrentView(highscoreView);
        HighLightButtonAndView(highscoreButton,highscoreView);
    }

    public void DisplayCreditView()
    {
        DisableAllViews();
        AssignAndActiveCurrentView(creditView);
        HighLightButtonAndView(creditButton,creditView);
    }

    private void AssignAndActiveCurrentView(OptionsView localCurrentView)
    {
        currentView = localCurrentView;
        currentView.gameObject.SetActive(true);
    }

    private void DisableAllViews()
    {
        foreach (OptionsView item in views)
        {
            item.gameObject.SetActive(false);
        }
    }

    private void HighLightButtonAndView(Button button,OptionsView view)
    {
        if (previousButton == null && previousView == null) 
        {
            AssignPreviousViewAndButton(button, view);
        }
        else
        {
            SetButtonColor(previousButton, defaultHighlightedColorBlock);
            SetButtonOffset(previousButton, yAxisOffset);
            AssignPreviousViewAndButton(button, view);
        }
        SetButtonColor(button,highlightedColorBlock);
        SetButtonOffset(button, -yAxisOffset);
    }

    private void AssignPreviousViewAndButton(Button button,OptionsView view)
    {
        previousView = view;
        previousButton = button;
    }

    /*Unity seems to have a bug, where if the entire color block is changed at runtime, 
    the changes will not be reflected instantly. The changes will reflect only if the targeted 
    button's active status is set to false and in then instantly set to true.*/
    private void SetButtonColor(Button button,ColorBlock colorBlock)
    {
        button.colors = colorBlock;
        button.gameObject.SetActive(false);
        button.gameObject.SetActive(true);
    }

    private void SetButtonOffset(Button button,float yAxisOffset)
    {
        button.transform.position += new Vector3(0f, yAxisOffset, 0f);
    }

    #endregion

}
