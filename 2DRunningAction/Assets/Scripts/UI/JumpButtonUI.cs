using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButtonUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Player player = PlayerManager.instance.player;

        if (player.getJumpKey || player.IsGroundDetected() == false)
        {
            player.getSecondJumpKey = true;
        }

        player.getJumpKey = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
        //PlayerManager.instance.player.getJumpKey = false;
    }
}
