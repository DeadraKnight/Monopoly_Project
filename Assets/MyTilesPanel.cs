using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MyTilesPanel : MonoBehaviour
{
    [SerializeField] CanvasGroup tilesPanel;
    public GameObject tilePrefab;
    public Transform grid;

    private List<Tile> playerTiles;

    public void OpenPanel()
    {
        if (tilesPanel.alpha == 1) return;
        GetPlayerTiles();
        tilesPanel.blocksRaycasts = true;
        tilesPanel.alpha = 1;
    }

    public void ClosePanel()
    {
        tilesPanel.blocksRaycasts = false;
        tilesPanel.alpha = 0;
        ClearTiles();
    }
    
    private void GetPlayerTiles()
    {
        playerTiles = Player.Instance.controlledPawn.controllingPlayer.ownedTiles;

        foreach (var tile in playerTiles)
        {
            GameObject newTile = Instantiate(tilePrefab, grid);
            GridElementScript tileData = newTile.GetComponent<GridElementScript>();
            tileData.sellPrice = tile.sellPrice;
            tileData.SetSprite(tile.defaultSprite);
        }
    }

    private void ClearTiles()
    {
        foreach (Transform tile in grid)
        {
            Destroy(tile.gameObject);
        }
    }
}
