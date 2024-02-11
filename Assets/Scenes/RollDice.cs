using System.Collections;
using UnityEngine;

public class RollDice : MonoBehaviour
{
    public GameObject pawnGameObject;

    // Array of dice sides sprites to load from Resources folder
    private Sprite[] diceSides;

    // Reference to sprite renderer to change sprites
    private SpriteRenderer rend;

    // Start is called before the first frame update
    private void Start()
    {
        // Assign Sprite Renderer component
        rend = GetComponent<SpriteRenderer>();

        // Load dice sides sprites to array from DiceSides subfolder of Resources folder
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
    }

    //If you left click over the dice then RollTheDice is started
    private void OnMouseDown()
    {
        StartCoroutine("RollTheDice");
    }

    //Courotine that rolls the dice
    private IEnumerator RollTheDice()
    {
        // Variable to contain random dice side number
        // It needs to be assinged. Let it be 0 initially
        int randomDiceSide = 0;

        // Final side that dice reads in the end of coroutine
        int finalSide = 0;

        // Loop to switch dice sides randomly
        // before final side appears. 20 itterations here.
        for (int i = 0; i <= 20; i++)
        {
            // Pick up random value from 0 to 5
            randomDiceSide = Random.Range(0, 6);

            // Set sprite to upper face of dice from array according to random value
            rend.sprite = diceSides[randomDiceSide];

            // Pause before next itteration
            yield return new WaitForSeconds(0.05f);
        }

        //Assigning final side for player movement
        finalSide = randomDiceSide + 1;

        // Show final dice value in Console
        Debug.Log(finalSide);

        // After a dice roll occurs...
        FollowWP followWP = pawnGameObject.GetComponent<FollowWP>();
        followWP.diceRoll = finalSide;

    }
}
