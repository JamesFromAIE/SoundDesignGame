using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    // Links script to Database
    public Database database;

    private bool viewingScreen = false;
    public bool resultsSent = false;

    // A variables which holds the current state of the GUI
    private int state;
    
    // Values for Reflex Test
    public Text reflexesRecentText;
    public Text reflexesHighText;
    public Text reflexesAverageText;

    // Values for Reaction Test
    public Text reactionsRecentText;
    public Text reactionsHighText;
    public Text reactionsAverageText;

    // Establishes UI Elements used
    public GameObject reflexesText;
    public GameObject reactionsText;
    public GameObject mask;
    public GameObject esc;



    // Start is called before the first frame update
    void Start()
    {
        // Starts the GUI in a default state
        state = 0;
    }

    // Update is called once per frame
    void Update()
    {   
        // Checking if button has been pressed
        EscButton();
        // Matches variables values to text in GUI
        UpdateValues();
        // Is the GUI on or off with Escape key up or down
        StateOfUI();  
    }


    void EscButton()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && viewingScreen == false)
        {
            state = 0;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && viewingScreen == false)
        {
            state = 1;
        }

        if (Input.GetKeyUp(KeyCode.Escape) && viewingScreen == true)
        {
            state = 2;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && viewingScreen == true)
        {
            state = 3;
        }
    }

    void UpdateValues()
    {
        // Always updates values to text
        reflexesRecentText.text = "Recent Score: " + database.reflexRecent;
        reflexesHighText.text = "High Score: " + database.reflexHigh;
        reflexesAverageText.text = "Average Score: " + database.reflexAverage;

        // Always updates values to text
        reactionsRecentText.text = "Recent Score: " + database.reactionRecent;
        reactionsHighText.text = "High Score: " + database.reactionHigh;
        reactionsAverageText.text = "Average Score: " + database.reactionAverage;

    }

    void StateOfUI()
    {
        switch (state)
        {
            // If Escape Key is Up and GUI is Off
            case 0:
                 Offcase();
                break;
            // If Escape Down is Down and GUI is Off
            case 1:
                OnCase();
                break;
            // If Escape Key is Up and GUI is On
            case 2:
                OnCase();
                break;
            // If Escape Key is Down and GUI is On
            case 3:
                Offcase();
                break;
        }
    }

    void Offcase()
    {
        reflexesRecentText.enabled = false;
        reflexesHighText.enabled = false;
        reflexesAverageText.enabled = false;

        reactionsRecentText.enabled = false;
        reactionsHighText.enabled = false;
        reactionsAverageText.enabled = false;

        reflexesText.SetActive(false);
        reactionsText.SetActive(false);
        mask.SetActive(false);
        esc.SetActive(true);
        viewingScreen = false;
    }

    void OnCase()
    {
        reflexesRecentText.enabled = true;
        reflexesHighText.enabled = true;
        reflexesAverageText.enabled = true;

        reactionsRecentText.enabled = true;
        reactionsHighText.enabled = true;
        reactionsAverageText.enabled = true;

        reflexesText.SetActive(true);
        reactionsText.SetActive(true);
        mask.SetActive(true);
        esc.SetActive(true);
        viewingScreen = true;
    }

}
