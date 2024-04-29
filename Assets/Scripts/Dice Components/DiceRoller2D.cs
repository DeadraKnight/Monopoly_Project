using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceRoller2D : MonoBehaviour
{
    public event Action<int> OnRoll;

    int doublesCount = 0; // Track total number of doubles

    int rollCount = 0; // Track total number of rolls
    public int Result => _results.Count == 0 ? 0 : _results.Sum();
    public bool Doubles => _dice.Length == 2 && _results[0] == _results[1];
    
    [SerializeField] DieRoller2D[] _dice;
    
    readonly List<int> _results = new();

    public void RollDice()
    {
        _results.Clear();
        foreach (var die in _dice)
        {
            die.RollDie();
        }
    }
    
    void OnEnable()
    {
        foreach (var die in _dice)
        {
            die.OnRoll += HandleDieRoll;
        }
    }
    
    void OnDisable()
    {
        foreach (var die in _dice)
        {
            die.OnRoll -= HandleDieRoll;
        }
    }

    void HandleDieRoll(int result)
    {
        _results.Add(result);

        // Check if all dice have been rolled
        if (_results.Count == _dice.Length)
        {
            // Calculate the total roll result
            int totalRoll = _results.Sum();

            // Check for doubles and jail status
            if (Player.Instance.isInJail)
            {
                rollCount++; // Increment rollCount

                if (_results[0] == _results[1]) // Check for doubles
                {
                    Player.Instance.isInJail = false; // Player gets out of jail
                    Debug.Log("You rolled doubles and are now free!");
                    rollCount = 0; // Reset roll count after successful escape
                }
                else
                {
                    // Three unsuccessful rolls? Free the player!
                    if (rollCount >= 3)
                    {
                        Player.Instance.isInJail = false;
                        Debug.Log("Three rolls and no doubles! You're out of jail.");
                        rollCount = 0; // Reset roll count after escaping on 3rd roll
                    }
                    else
                    {
                        OnRoll?.Invoke(0); // Pass the total roll result
                        Debug.Log("You're still in jail. Try again next turn!");
                    }
                }
            }
            else
            {
                // Check for doubles
                if (_results[0] == _results[1])
                {
                    Debug.Log("You rolled doubles! Roll Again!");
                    Player.Instance.controlledPawn.controllingPlayer.hasRolledDiceThisTurn = false; // Reset roll status
                    doublesCount++; // Increment doubles count
                    if (doublesCount >= 3)
                    {
                        Debug.Log("You rolled doubles three times in a row! Go to jail!");
                        Player.Instance.controlledPawn.GoToJail(); // Move player to jail
                        doublesCount = 0; // Reset doubles count after going to jail
                    }
                }
                else
                {
                    Debug.Log("You rolled a total of " + totalRoll + "!");
                    doublesCount = 0; // Reset doubles count after rolling non-doubles
                }
                // Player is not in jail, proceed with normal roll handling
                rollCount = 0;
                OnRoll?.Invoke(totalRoll); // Pass the total roll result
            }

            // Clear results for the next roll
            _results.Clear();
        }
    }
}
