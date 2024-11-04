using UnityEngine;
using UnityEngine.EventSystems;

public class SnapButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] string _url;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!string.IsNullOrEmpty(_url))
        {
            Application.OpenURL(_url);
        }
    }
}
