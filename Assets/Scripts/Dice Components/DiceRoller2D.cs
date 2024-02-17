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
        if (_results.Count == _dice.Length)
        {
            OnRoll?.Invoke(Result);
        }
    }

}
