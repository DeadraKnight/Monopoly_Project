using UnityEngine;
using UnityEngine.UI;

public class GridElementScript : MonoBehaviour
{
    public int sellPrice;
    
    public void SetSprite(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
    }
}
