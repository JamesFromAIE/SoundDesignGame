using UnityEngine;
using UnityEngine.UI;

public class ClearSave : MonoBehaviour
{
    private float rayLength = 4f;

    public bool clearReflex;
    public bool clearReaction;

    public Text clearText;
    public Image clearMask;
    private float clearTime = 2f;
    private bool clearShown = false;

    void Start()
    {
        clearText.enabled = false;
        clearMask.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        // Establishes a temporary Ray pointing out of camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Establishes that attached object can be hit by Raycast
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength) && Input.GetMouseButtonDown(0) && hit.transform.tag == "ClearButton" && clearReflex == false && clearReaction == false && clearShown == false)
        {
            clearReflex = true;
            clearReaction = true;
            clearShown = true;
        }

        if (clearShown)
        {
            clearTime -= Time.deltaTime;
            clearText.enabled = true;
            clearMask.enabled = true;

            if (clearTime <= 0)
            {
                clearText.enabled = false;
                clearMask.enabled = false;
                clearTime = 2;
                clearShown = false;
            }


        }
    }
}
