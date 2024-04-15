using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceRoller2D : MonoBehaviour
{
    public event Action<int> OnRoll;
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
                if (_results[0] == _results[1]) // Check for doubles
                {
                    Player.Instance.isInJail = false; // Player gets out of jail
                    Debug.Log("You rolled doubles and are now free!");
                }
                else
                {
                    Debug.Log("You're still in jail. Try again next turn!");
                }
            }
            else
            {
                // Player is not in jail, proceed with normal roll handling
                OnRoll?.Invoke(totalRoll); // Pass the total roll result
            }

            // Clear results for the next roll
            _results.Clear();
        }
    }

}
