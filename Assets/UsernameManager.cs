using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UsernameManager : MonoBehaviour
{
    public static List<string> usernames = new List<string>();

    public static void AddUsername(string username)
    {
        usernames.Add(username);
    }
}


