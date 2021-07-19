using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class EXIT : MonoBehaviour
{

    // CLOSE GAME WHEN PLAYER CLICKS OBJECT

    private float rayLength = 4f;
    private float verifyTime = 6;
    private bool clicked = false;

    public AudioSource AudioS_buttonClick;
    public AudioClip ui_button_click;

    public Text exitTest;
    public Text exitNumber;
    public GameObject exitMask;

    void Start()
    {
        // Hides Verification Window
        exitTest.enabled = false;
        exitNumber.enabled = false;
        exitMask.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;



        if (!clicked)
        {
            if (Physics.Raycast(ray, out hit, rayLength) && hit.transform.tag == "ExitButton" && Input.GetMouseButtonDown(0))
            {
                clicked = true;
                AudioS_buttonClick.PlayOneShot(ui_button_click);
            }
        }

        if (clicked)
        {
            verifyTime -= Time.deltaTime;
            //verifyTime = (Mathf.Round(verifyTime * 1) / 1);

            exitNumber.text = "" + verifyTime;

            exitTest.enabled = true;
            exitNumber.enabled = true;
            exitMask.SetActive(true);


            if (Physics.Raycast(ray, out hit, rayLength) && hit.transform.tag == "ExitButton" && Input.GetMouseButtonDown(0) && verifyTime > 0 && verifyTime <= 5.75f)
            {
                Application.Quit();

                // DISABLE THIS STATEMENT ON BUILD
                //EditorApplication.ExitPlaymode();
            }
            else if (verifyTime <= 1)
            {
                verifyTime = 6;
                exitTest.enabled = false;
                exitNumber.enabled = false;
                exitMask.SetActive(false);
                clicked = false;
            }
        }


        
    }

}
