using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using System;

public class Board : NetworkBehaviour
{
    public static Board Instance { get; private set; }

    [field: SerializeField]
    public Tile[] Tiles { get; private set; }

    [SerializeField]
    public int TaxPile = 0;

    private void Awake()
    {
        Instance = this;
    }

    public int Wrap(int index)
    {
        return index < 0 ? Math.Abs((Tiles.Length - Math.Abs(index)) % Tiles.Length) : index % Tiles.Length;
    }

    public Tile[] Slice(int start, int end)
    {
        if (Tiles.Length == 0) return Array.Empty<Tile>();

        List<Tile> tileSlice = new();

        int steps = (end - start + Tiles.Length) % Tiles.Length;

        for (int i = start; i <= start + steps; i++)
        {
            tileSlice.Add(Tiles[Wrap(i)]);
        }

        return tileSlice.ToArray();
    }


    [ServerRpc(RequireOwnership = false)]
    public void ServerSetTileOwner(int tileIndex, Player value)
    {

        // Check if the tile can be owned
        if (Tiles[tileIndex].CantBeOwned)
        {
            Debug.Log("This tile cannot be bought!");
            return;
        }

        // Check if the player has enough money to buy the tile
        if (value.Balance < Tiles[tileIndex].cost)
        {
            Debug.Log("You don't have enough money to buy this tile!");
            return;
        }

        // Subtract the cost of the tile from the player's money
        value.Balance -= Tiles[tileIndex].cost;

        // Add the tile to the player's ownedTiles list
        value.ownedTiles.Add(Tiles[tileIndex]);

        ObserversSetTileOwner(tileIndex, value);

        Tiles[tileIndex].IsOwned = true;
    }

    [ObserversRpc(BufferLast = true)]
    private void ObserversSetTileOwner(int tileIndex, Player value)
    {
        Tiles[tileIndex].owningPlayer = value;
    }
}
