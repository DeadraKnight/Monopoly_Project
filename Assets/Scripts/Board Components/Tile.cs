using FishNet.Object.Synchronizing;
using UnityEngine;
using UnityEngine.Serialization;

public class Tile : MonoBehaviour
{
    [field: SerializeField]
    public Transform[] PawnPositions { get; private set; }

    [SyncVar]
    public Player owningPlayer;

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    
    [FormerlySerializedAs("sprite")] [SerializeField]
    public Sprite panelSprite;

    private Color _defaultColor;

    [SerializeField]
    public string tileName; // The name of the tile

    [SerializeField]
    public int cost; // The cost of the tile

    [SerializeField]
    public int rent; // Thes cost of rent

    [SerializeField]
    public int sellPrice; // The price the tile can be sold for

    [FormerlySerializedAs("owned")]
    [SerializeField]
    [SyncVar]
    public bool isOwned; // If the tile is owned

    [FormerlySerializedAs("CantBeOwned")] [SerializeField]
    public bool IsOwnable;

    [SerializeField]
    public bool ChanceTile;

    [SerializeField]
    public bool TaxTile;

    [SerializeField]
    public int TaxCost;

    private void Start()
    {
        _defaultColor = spriteRenderer.sharedMaterial.color;
    }

    private void LateUpdate()
    {
        if (owningPlayer == null)
        {
            spriteRenderer.material.color = _defaultColor;
            isOwned = false; // The tile is not owned
            return;
        }

        spriteRenderer.material.color = GameManager.Instance.Players.IndexOf(owningPlayer) switch
        {
            0 => Color.red,
            1 => Color.green,
            2 => Color.blue,
            3 => Color.yellow,

            _ => Color.white,
        };
    }
}
