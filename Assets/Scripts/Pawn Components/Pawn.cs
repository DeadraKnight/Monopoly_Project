using DG.Tweening;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public sealed class Pawn : NetworkBehaviour
{
    [SyncVar]
    public Player controllingPlayer;

    [SyncVar]
    public int currentPosition;

    // An array of sprites to store the different icons
    [SerializeField]
    private SpriteRenderer[] sprites;
    private SpriteRenderer sprite;

    private bool _isMoving;

    public override void OnStartClient()
    {
        base.OnStartClient();

        // Assign the icon based on the player index
        int playerIndex = GameManager.Instance.Players.IndexOf(controllingPlayer);
        sprite = sprites[playerIndex];
    }

    [ServerRpc(RequireOwnership = false)]
    public void Move(int steps)
    {
        if (_isMoving) return;

        _isMoving = true;

        Tile[] tiles = Board.Instance.Slice(currentPosition, currentPosition + steps);

        int controllingPlayerIndex = GameManager.Instance.Players.IndexOf(controllingPlayer);

        Vector3[] path = tiles.Select(tile => tile.PawnPositions[controllingPlayerIndex].position).ToArray();

        Tween tween = transform.DOPath(path, 2.0f);

        tween.OnComplete(() =>
        {
            _isMoving = false;

            currentPosition += steps;

            GameManager.Instance.EndTurn();

        });

        tween.Play();
    }
}
