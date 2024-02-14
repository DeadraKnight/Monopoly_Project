using UnityEngine;

public sealed class Tile : MonoBehaviour
{
    [field: SerializeField]
    public Transform[] PawnPositions {  get; private set; }

    public Player owningPlayer;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Color _defaultColor;

    private void Start()
    {
        _defaultColor = spriteRenderer.sharedMaterial.color;
    }

    private void LateUpdate()
    {
        if (owningPlayer == null)
        {
            spriteRenderer.material.color = _defaultColor;

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
