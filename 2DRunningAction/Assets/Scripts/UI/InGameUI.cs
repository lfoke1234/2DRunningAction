using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    private Player player;

    [SerializeField] private Slider hpSlider;

    [SerializeField] private TextMeshProUGUI distance;
    [SerializeField] private TextMeshProUGUI energy;

    private void Start()
    {
        player = PlayerManager.instance.player;

        if (hpSlider != null)
        {
            player.onHealthChanged += HealthChanged;
        }
    }
    private void Update()
    {
        Distance();
    }

    private void Distance()
    {
        distance.text = GameManager.instance.distance.ToString("#,#") + " M";
        energy.text = GameManager.instance.energy.ToString();
    }

    private void HealthChanged()
    {
        hpSlider.maxValue = player.playerMaxHealth;
        hpSlider.value = player.playerHealth;
    }
}
