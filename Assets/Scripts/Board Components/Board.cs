using FishNet.Object;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public sealed class Board : NetworkBehaviour
{
    public static Board Instance { get; private set; }

    [field: SerializeField]
    public Tile[] Tiles {  get; private set; }

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

        int steps = Math.Abs(end - start);

        if (end > start)
        {
            for (int i = start; i <= start + steps; i++)
            {
                tileSlice.Add(Tiles[Wrap(i)]);
            }
        }
        else
        {
            for (int i = start; i >= start - steps; i--)
            {
                tileSlice.Add(Tiles[Wrap(i)]);
            }
        }

        return tileSlice.ToArray();
    }

    [ServerRpc(RequireOwnership = false)]
    public void ServerSetTileOwner(int tileIndex, Player value)
    {
        ObserversSetTileOwner(tileIndex, value);
    }

    [ObserversRpc(BufferLast = true)]
    private void ObserversSetTileOwner(int tileIndex, Player value)
    {
        Tiles[tileIndex].owningPlayer = value;
    }
}
