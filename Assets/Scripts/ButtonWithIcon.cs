
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonWithIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon; // Assign this in the Inspector

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (icon != null)
            icon.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (icon != null)
            icon.enabled = false;
    }
}