using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerEnterHandler
{
    public static event Action OnEnter;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnEnter?.Invoke();
    }
}
