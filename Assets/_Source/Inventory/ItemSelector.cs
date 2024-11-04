using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public InventoryItem Item { get; set; }
    [SerializeField] private Image _image;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _defaultColor;
    private static Image _selected = null;
    private Image _head;
    private Image _body;
    private Image _legs;
    private GolemState _golemState;

    private void Start()
    {
        var go = GameObject.Find("GolemContainers");
        _head = go.transform.Find("Head").GetChild(0).GetComponentInChildren<Image>();
        _body = go.transform.Find("Body").GetChild(0).GetComponentInChildren<Image>();
        _legs = go.transform.Find("Legs").GetChild(0).GetComponentInChildren<Image>();
        _golemState = GameObject.Find("GolemState").GetComponent<GolemState>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_selected || Battle.IsStart || LastBattleLogic.IsStart)
        {
            return;
        }

        switch (Item.Type)
        {
            case InventoryItem.ItemType.Head:
                _head.sprite = _selected.sprite;
                Battle.Head = Item.Sprite;
                _golemState.HeadState = Item.HeadKey;
                break;
            case InventoryItem.ItemType.Body:
                _body.sprite = _selected.sprite;
                Battle.Body = Item.Sprite;
                _golemState.BodyState = Item.BodyKey;
                break;
            case InventoryItem.ItemType.Legs:
                _legs.sprite = _selected.sprite;
                Battle.Legs = Item.Sprite;
                _golemState.LegsState = Item.LegsKey;
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _selected = _image;
        _image.color = _selectedColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _selected = null;
        _image.color = _defaultColor;
    }
}