using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIComponentEventTrigger : EventTrigger
{
    public Sprite selectedSprite;
    public Sprite deSelectedSprite;

    public override void OnPointerDown(PointerEventData eventData)
    {
        this.GetComponent<Button>().image.sprite = selectedSprite;
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        this.GetComponent<Button>().image.sprite = deSelectedSprite;
    }
}
