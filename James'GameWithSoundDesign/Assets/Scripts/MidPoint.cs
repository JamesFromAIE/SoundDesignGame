using UnityEngine;

public class MidPoint : MonoBehaviour
{
    public GameObject onRay;
    [SerializeField] AudioSource OffClick;
    [SerializeField] AudioClip foley_player_click;

    private int state;

    // Start is called before the first frame update
    void Start()
    {
        onRay.SetActive(false);  
    }


    // Update is called once per frame
    void Update()
    {
        // Establishes a temporary Ray pointing out of camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Establishes that attached object can be hit by Raycast
        RaycastHit hit;

        

        if (Physics.Raycast(ray, out hit, 6) && hit.transform.tag == "ReactionScreen")
        {
            state = 1;
        }
        else if (Physics.Raycast(ray, out hit, 4) && hit.transform.tag == "ExitButton")
        {
            state = 2;
        }
        else if (Physics.Raycast(ray, out hit, 4) && hit.transform.tag == "ClearButton")
        {
            state = 3;
        }
        else if (Physics.Raycast(ray, out hit, 4) && hit.transform.tag == "StartButton")
        {
            state = 4;
        }
        else if (Physics.Raycast(ray, out hit, 5) && hit.transform.tag == "ReflexButton")
        {
            state = 5;
        }
        else if (Physics.Raycast(ray, out hit, 5) && hit.transform.tag == "WorldObjects")
        {
            state = 6;
        }
        else
        {
            state = 0;
        }
        StateOfPoint();
        PlayOffClick();
    }

    void StateOfPoint()
    {
        switch (state)
        {
            case 0:
                onRay.SetActive(false);
                break;

            case 1:
                onRay.SetActive(true);
                break;

            case 2:
                onRay.SetActive(true);
                break;

            case 3:
                onRay.SetActive(true);
                break;

            case 4:
                onRay.SetActive(true);
                break;

            case 5:
                onRay.SetActive(true);
                break;
            case 6:
                onRay.SetActive(false);
                break;
        }
    }

    void PlayOffClick()
    {
        if (Input.GetMouseButtonDown(0) && state == 6)
        {
            OffClick.clip = foley_player_click;
            OffClick.Play();
        }
    }
}
