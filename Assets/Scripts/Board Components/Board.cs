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

    public AudioClip moneySound;

    private AudioSource audioSource;

    [SerializeField]
    public int TaxPile = 0;

    private void Awake()
    {
        Instance = this;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = moneySound;
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
        // Subtract the cost of the tile from the player's money
        value.Balance -= Tiles[tileIndex].cost;

        // Add the tile to the player's ownedTiles list
        //moved to the purchase panel since it doesnt need to be synced
        //value.ownedTiles.Add(Tiles[tileIndex]);

        Tiles[tileIndex].owningPlayer = value;
        ObserversSetTileOwner(tileIndex, value);

        Tiles[tileIndex].isOwned = true;
        
        audioSource.volume = 0.5f;
        audioSource.Play();
    }

    [ObserversRpc(BufferLast = true)]
    private void ObserversSetTileOwner(int tileIndex, Player value)
    {
        Tiles[tileIndex].owningPlayer = value;
        //this is a sync var and is set on the server but isn't syncing so I added it here?????
        //took an hour to find this fix
        Tiles[tileIndex].isOwned = true;

    }
}
