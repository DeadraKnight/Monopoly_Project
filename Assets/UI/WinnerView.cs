using System.Linq;
using TMPro;
using UnityEngine;

public sealed class WinnerView : View
{
    [SerializeField] private TMP_Text winnerText;

    private void Update()
    {
        // Check if there is a winner and display their username
        if (GameManager.Instance != null && GameManager.Instance.Players != null)
        {
            Player winner = GameManager.Instance.Players.FirstOrDefault(player => player.isWinner);
            if (winner != null)
            {
                winnerText.text = $"Winner: {winner.username}";
            }
            else
            {
                winnerText.text = "No winner yet";
            }
        }
    }
}
