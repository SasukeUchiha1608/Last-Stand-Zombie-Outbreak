using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI reloadingText;
    public TextMeshProUGUI healthText;

    void Start()
    {
        if (reloadingText != null)
            reloadingText.gameObject.SetActive(false);
    }

    public void UpdateAmmo(int current, int max)
    {
        if (ammoText != null)
            ammoText.text = $"Ammo: {current} / {max}";
    }

    public void SetReloading(bool state)
    {
        if (reloadingText != null)
            reloadingText.gameObject.SetActive(state);
    }

    public void UpdateHealth(int current, int max)
    {
        if (healthText != null)
            healthText.text = $"HP: {current} / {max}";
    }
}
