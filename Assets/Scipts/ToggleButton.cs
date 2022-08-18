using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ToggleButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool buttonDown;

    [System.Serializable]
    public class ButtonPress : UnityEvent { }

    //[SerializeField]
    public ButtonPress OnPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonDown = false;
    }

    void Update()
    {
        if (buttonDown)
        {
            OnPressed?.Invoke();
        }
    }
}
