using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dragable : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        var position = (Vector2)Camera.main.ScreenToWorldPoint(eventData.position);
        transform.position = position;
    }
}
