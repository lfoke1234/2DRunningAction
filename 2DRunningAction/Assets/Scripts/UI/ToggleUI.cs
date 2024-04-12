using UnityEngine;
using UnityEngine.UI;

public class ToggleUI : MonoBehaviour
{
    Player player;

    void Start()
    {
        player = PlayerManager.instance.player;

        this.GetComponent<Toggle>().onValueChanged.AddListener(player.ToggleTest);
    }

    void Update()
    {

    }
}
