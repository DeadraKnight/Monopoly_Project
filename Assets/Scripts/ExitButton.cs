using UnityEngine;


public class ExitButton : MonoBehaviour
{
    public GameObject Panel; // Reference to the panel that displays the rules

    public void OnExitButtonClick()
    {
        Panel.SetActive(false); // Set the visibility of the rules panel to false
    } 
}
