using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item", fileName = "NewInventoryItem")]
public class InventoryItem : ScriptableObject
{
    public GameObject Prefab; 
    public Sprite Sprite;
    public ItemType Type;
    public string HeadKey;
    public string BodyKey;
    public string LegsKey;

    public enum ItemType
    {
        Head,
        Body,
        Legs
    }
}