using UnityEngine;
using UnityEngine.UI;

public class DisplaySavedUsernames : MonoBehaviour
{
    //public Text savedUsernamesTextField;
    public Text usernamesTextField;
    public int numSavedUsernames = 5; // Change this value to match the number of saved usernames

    private void DisplayUsernames()
    {
        // Retrieve all unique identifiers
        string[] uniqueIds = new string[numSavedUsernames];
        for (int i = 0; i < numSavedUsernames; i++)
        {
            uniqueIds[i] = PlayerPrefs.GetString($"SavedUniqueId{i}");
        }

        // Iterate through unique identifiers and retrieve corresponding usernames
        string savedUsernames = "";
        foreach (string uniqueId in uniqueIds)
        {
            string savedUsername = PlayerPrefs.GetString(uniqueId);
            savedUsernames += savedUsername + "\n";
        }

        usernamesTextField.text = savedUsernames;
    }

    private void Start()
    {
        DisplayUsernames();
    }
}