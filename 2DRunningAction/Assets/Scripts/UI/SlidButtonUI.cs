using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlidButtonUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Player player = PlayerManager.instance.player;

        player.getSlidKey = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Player player = PlayerManager.instance.player;

        player.getSlidKey = false;
    }
}
