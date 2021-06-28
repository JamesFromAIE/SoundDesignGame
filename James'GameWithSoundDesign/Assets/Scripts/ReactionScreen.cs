using UnityEngine;
using UnityEngine.UI;

public class ReactionScreen : MonoBehaviour
{
    public FPSMovement fpsMovement;

    private GameObject player;
    public GameObject startingPoint;

    // How far until player can no longer reach
    private float rayLength = 8f;

    // Times for test
    private float randomTime = 0;
    private float timer = 0;
    private float reactionTime = 0;

    // How many times the player has reacted in ONE 'attempt'
    private int reactionCount;
    // The average of the last three reaction scores
    public float averageReaction = 0;
    // Variables to store reaction times
    private float reaction1 = 0;
    private float reaction2 = 0;
    private float reaction3 = 0;
    // Helps Passive Red Screen run only once each reaction time
    private bool hasRun = false;

    // Checks whether Database received updated score or not
    public bool reactionSent = false;

    // Establishes UI elements for result
    public Image reactionMask;
    public Text reactionText;
    private float resultTime = 2f;
    private bool resultShown = false;

    // Establishes UI elements for result
    public Image earlyMask;
    public Text earlyText;
    private float earlyTime = 1f;
    private bool earlyShown = false;

    // Establishes UI elements for start
    public Image startMask;
    public Text startText;
    public Text startNumber;
    private float startTime = 4f;
    private bool startUI = false;

    void Start()
    {
        reactionText.enabled = false;
        reactionMask.enabled = false;

        earlyMask.enabled = false;
        earlyText.enabled = false;

        startMask.enabled = false;
        startText.enabled = false;
        startNumber.enabled = false;

        // Finds "Player" in Scene
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        PassiveColours();

        TriggeredColours();

        if (startUI)
        {
            StartingUI();
        }

        TooEarlyUI();

        ShowUI();
    }

    // What happens while colours are present
    void PassiveColours()
    {
        // What happens when Red is first shown
        if (gameObject.GetComponent<Renderer>().material.color == Color.red)
        {

            // Gives ONE random range per attempt
            if (hasRun == false)
            {
                Debug.Log("PLAYING REFLEX TEST!");
                randomTime = Random.Range(1.0f, 7.0f);
                hasRun = true;
            }
            // Counts up from 0 until it hits Random Time
            timer += Time.deltaTime;
        }

        // If Red and timer >= randomTime, Turn Green
        if (gameObject.GetComponent<Renderer>().material.color == Color.red && timer >= randomTime)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 0);
            timer = 0;
        }

        // If Green, start reaction timer
        if (gameObject.GetComponent<Renderer>().material.color == Color.green)
        {
            reactionTime += Time.deltaTime;
        }
    }

    // What happens when colours are triggered
    void TriggeredColours()
    {

        // Establishes a temporary Ray pointing out of camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Establishes that attached object can be hit by Raycast
        RaycastHit hit;

        // When screen gets clicked by player
        if (Physics.Raycast(ray, out hit, rayLength) && hit.transform.tag == "ReactionScreen")
        {

            if (Input.GetMouseButtonDown(0))
            {
                // If Cyan, Turn Red
                if (gameObject.GetComponent<Renderer>().material.color == Color.cyan)
                {
                    // Remove Early notification if playing
                    earlyText.enabled = false;
                    earlyMask.enabled = false;
                    earlyTime = 1;
                    earlyShown = false;

                    startUI = true;
                }
                // If Green, Turn Cyan and find reaction time
                else if (gameObject.GetComponent<Renderer>().material.color == Color.green)
                {
                    gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 1);
                    hasRun = false;
                    Debug.Log("Your reaction time was " + reactionTime + " seconds!");
                    ReactionTimes();
                    reactionTime = 0;
                    reactionCount++;
                    if (reactionCount == 3)
                    {
                        resultShown = true;
                        AverageTime();
                        reactionCount = 0;
                        fpsMovement.enabled = true;
                    }
                }
                // If Red, Turn Cyan and inform player they clicked too early
                else if (gameObject.GetComponent<Renderer>().material.color == Color.red)
                {
                    gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 1);
                    timer = 0;
                    Debug.Log("You clicked TOO EARLY!");
                    hasRun = false;
                    earlyShown = true;
                }
            }

        }
    }

    // Stores Prior reaction time values (up to 3 currently)
    void ReactionTimes()
    {
        if (reaction1 == 0)
        {
            reaction1 = reactionTime;
        }
        else if (reaction2 == 0)
        {
            reaction2 = reactionTime;
        }
        else if (reaction3 == 0)
        {
            reaction3 = reactionTime;
        }
    }
    // Calculates and Prints Average Reaction Time
    void AverageTime()
    {
        // Finds average
        averageReaction = (reaction1 + reaction2 + reaction3) / reactionCount;
        // Puts value into 3 decimal place
        averageReaction = (Mathf.Round(averageReaction * 1000) / 1000);

        // Linked to Function in Database --> Update()
        

        Debug.Log("Your average reaction time is " + averageReaction + " seconds!");

        reactionSent = true;


        // Resets values in script
        reaction1 = 0;
        reaction2 = 0;
        reaction3 = 0;
        
    }
    
    void ShowUI()
    {
        if (resultShown)
        {
            resultTime -= Time.deltaTime;
            reactionText.enabled = true;
            reactionMask.enabled = true;

            if (resultTime <= 0)
            {
                reactionText.enabled = false;
                reactionMask.enabled = false;
                resultTime = 2;
                resultShown = false;
            }
        }
    }

    void StartingUI()
    {
        // Start of an attempt
        if (reactionCount == 0)
        {
            // Sets player position to game
            player.transform.position = startingPoint.transform.position;
            player.transform.rotation = startingPoint.transform.rotation;
            // Turns off Player movement
            fpsMovement.enabled = false;

            startTime -= Time.deltaTime;
            startMask.enabled = true;
            startText.enabled = true;
            startNumber.enabled = true;

            startNumber.text = "" + startTime;

            if (startTime <= 1)
            {
                startMask.enabled = false;
                startText.enabled = false;
                startNumber.enabled = false;
                startUI = false;

                gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0);

                startTime = 4f;
            }
        }
        else if (reactionCount != 0)
        {
            startUI = false;
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
        }
    }

    void TooEarlyUI()
    {
        if (earlyShown)
        {
            earlyTime -= Time.deltaTime;
            earlyText.enabled = true;
            earlyMask.enabled = true;

            if (earlyTime <= 0)
            {
                earlyText.enabled = false;
                earlyMask.enabled = false;
                earlyTime = 1;
                earlyShown = false;
            }
        }
    }
}
