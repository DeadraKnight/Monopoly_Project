using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollerUI : MonoBehaviour
{
    [SerializeField] Button _rollButton;
    [SerializeField] TMP_Text _resultsText, _doublesText;
    [SerializeField] DiceRoller2D _diceRoller;

    void OnEnable()
    {
        _rollButton.onClick.AddListener(RollDice);
        _diceRoller.OnRoll += HandleRoll;
    }
    
    void OnDisable()
    {
        _rollButton.onClick.RemoveListener(RollDice);
        _diceRoller.OnRoll -= HandleRoll;
    }

    void HandleRoll(int obj)
    {
        _resultsText.text = $"You rolled a {obj}";
        _doublesText.text = _diceRoller.Doubles ? "Doubles!" : "";

        Player.Instance.controlledPawn.Move(obj);
    }

    void RollDice()
    {

            if (Player.Instance.hasRolledDiceThisTurn == false)
            {
                Player.Instance.hasRolledDiceThisTurn = true;
                ClearResults();
                _diceRoller.RollDice();
            }
        
        
    }

    void ClearResults()
    {
        _resultsText.text = "";
        _doublesText.text = "";
    }
}
