using UnityEngine;
using UnityEngine.EventSystems;

public class ClickSound : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private AudioClip _clickSound;

    public void OnPointerDown(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySound(_clickSound);
    }
}