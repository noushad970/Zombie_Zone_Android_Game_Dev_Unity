using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonStateTracker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isPressed = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }

    public bool IsNotPressed()
    {
        return !isPressed;
    }
}
