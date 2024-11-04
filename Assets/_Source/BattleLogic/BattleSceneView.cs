using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneView : MonoBehaviour
{
    [SerializeField] private List<ItemsForlevels> _baseItems;
    [SerializeField] private RectTransform _headsContent;
    [SerializeField] private RectTransform _bodiesContent;
    [SerializeField] private RectTransform _legsContent;
    private int _currentLevel { get; set;}

    private void Start()
    {
        var resources = ResourceBank.Instance;
        RectTransform parent = null;
        _currentLevel = SceneSwitcher.Level;

        resources.Inventory.Clear();
        if (resources.Inventory.Count > 0)
        {
            return;
        }

        foreach (var itemCollections in _baseItems)
        {
            if (_currentLevel == itemCollections.Level)
            {
                foreach (var item in itemCollections.Items)
                {
                    resources.Inventory.Add(item);
                }
            }
        }

        foreach (var item in resources.Inventory)
        {
            switch (item.Type)
            {
                case InventoryItem.ItemType.Head:
                    parent = _headsContent;
                    break;
                case InventoryItem.ItemType.Body:
                    parent = _bodiesContent;
                    break;
                case InventoryItem.ItemType.Legs:
                    parent = _legsContent;
                    break;
            }
            
            var obj = Instantiate(item.Prefab, parent);
            obj.GetComponent<Image>().sprite = item.Sprite;
            obj.GetComponent<ItemSelector>().Item = item;
        }
    }
}