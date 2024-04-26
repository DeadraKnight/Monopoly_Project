using UnityEngine;
using UnityEngine.Serialization;

public class Tile : MonoBehaviour
{
    [field: SerializeField]
    public Transform[] PawnPositions { get; private set; }

    public Player owningPlayer;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Color _defaultColor;

    [SerializeField]
    public string tileName; // The name of the tile

    [SerializeField]
    public int cost; // The cost of the tile

    [SerializeField]
    public int rent; // Thes cost of rent

    [SerializeField]
    public int sellPrice; // The price the tile can be sold for

    [SerializeField]
    public bool owned; // If the tile is owned
    public bool IsOwned { get; set; }

    [FormerlySerializedAs("CantBeOwned")] [SerializeField]
    public bool IsOwnable;

    [SerializeField]
    public bool ChanceTile;

    [SerializeField]
    public bool TaxTile;

    [SerializeField]
    public int TaxCost;

    [SerializeField]
    public Sprite sprite;

    private void Start()
    {
        sprite = spriteRenderer.sprite;
        _defaultColor = spriteRenderer.sharedMaterial.color;
    }

    private void LateUpdate()
    {
        if (owningPlayer == null)
        {
            spriteRenderer.material.color = _defaultColor;
            IsOwned = false; // The tile is not owned
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
