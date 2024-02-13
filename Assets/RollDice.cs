using System.Collections;
using System.Xml.Schema;
using UnityEngine;

public class RollDice : MonoBehaviour
{
    public GameObject pawnGameObject1;
    public GameObject gameDice;

    public static event System.Action<int, bool> OnDiceRolled;


    // Array of dice sides sprites to load from Resources folder
    private Sprite[] diceSides;

    // Reference to sprite renderer to change sprites
    private SpriteRenderer rend1;
    private SpriteRenderer rend2;

    [Header("Debug")]
    public bool overrideDiceRoll = false;
    public int  diceRollValue= 0;
    public bool diceRolledDouble = false;


    // Start is called before the first frame update
    private void Start()
    {
        // Assign Sprite Renderer component
        rend1 = GetComponent<SpriteRenderer>();
        rend2 = gameDice.GetComponent<SpriteRenderer>();

        // Load dice sides sprites to array from DiceSides subfolder of Resources folder
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
    }

    //If you left click over the dice then RollTheDice is started
    private void OnMouseDown()
    {
        StartCoroutine("RollTheDice");
    }

    //Coroutine that rolls the dice
    private IEnumerator RollTheDice()
    {
        // Variables to contain random dice side numbers
        int randomDiceSide1 = 0;
        int randomDiceSide2 = 0;

        // Final sides that dice read in the end of coroutine
        int finalSide1 = 0;
        int finalSide2 = 0;

        // Loop to switch dice sides randomly
        // before final side appears. 20 iterations here.
        for (int i = 0; i <= 20; i++)
        {
            // Pick up random values from 0 to 5 for both dice
            randomDiceSide1 = Random.Range(0, 6);
            randomDiceSide2 = Random.Range(0, 6);

            // Set sprite to upper face of dice from array according to random values
            rend1.sprite = diceSides[randomDiceSide1];
            rend2.sprite = diceSides[randomDiceSide2];

            // Pause before next iteration
            yield return new WaitForSeconds(0.05f);
        }

        //Assigning final sides for player movement
        finalSide1 = randomDiceSide1 + 1;
        finalSide2 = randomDiceSide2 + 1;

        // Show final dice values in Console
        Debug.Log("Dice 1: " + finalSide1);
        Debug.Log("Dice 2: " + finalSide2);

        int total = finalSide1 + finalSide2;
        bool rolledDouble = finalSide1 == finalSide2;

        if (overrideDiceRoll)
        {
            total = diceRollValue;
            rolledDouble = diceRolledDouble;
        }

        // After a dice roll occurs...
        OnDiceRolled?.Invoke(total, rolledDouble);
        Debug.Log("OnDiceRolled event logged");
    }
}
