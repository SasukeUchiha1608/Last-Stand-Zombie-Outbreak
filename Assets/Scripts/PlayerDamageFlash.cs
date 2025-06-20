using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageFlash : MonoBehaviour
{
    public Image damageImage;
    public float flashDuration = 0.2f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.5f); // Red with 50% transparency

    private float currentTime = 0f;
    private bool isFlashing = false;

    void Update()
    {
        if (isFlashing)
        {
            currentTime += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(flashColor.a, 0, currentTime / flashDuration);
            damageImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, alpha);

            if (currentTime >= flashDuration)
            {
                isFlashing = false;
                damageImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0f);
            }
        }
    }

    public void TriggerFlash()
    {
        isFlashing = true;
        currentTime = 0f;
        damageImage.color = flashColor;
    }
}
