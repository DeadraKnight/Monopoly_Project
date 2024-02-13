using DG.Tweening;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Linq;
using UnityEngine;

public sealed class Pawn : NetworkBehaviour
{
    [SyncVar]
    public Player controllingPlayer;

    [SyncVar]
    public int currentPosition;

    private bool _isMoving;

    [ServerRpc(RequireOwnership = false)]
    public void ServerMove(int steps)
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
