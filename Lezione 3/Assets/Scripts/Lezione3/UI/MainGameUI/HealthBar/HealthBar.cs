using UnityEngine;
using UnityEngine.UI;

namespace MiciomaXD
{
    /// <summary>
    /// Manages health display bar. The UpdateHealthBarDisplay method takes a float value between 0 and 1 as an argument, which represents the current health of the player as a percentage. The method updates the value of the slider component to reflect the current health and changes the color of the fill image based on the health gradient, which is a color gradient that can be set in the editor to visually represent different health levels (e.g., green for high health, yellow for medium health, red for low health).
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class HealthBar : MonoBehaviour
    {

        Slider slider;
        public Image fillImage;
        public Gradient healthGradient;


        private void OnValidate()
        {
            if (slider == null)
                slider = GetComponent<Slider>();
        }

        public void UpdateHealthBarDisplay(float fill)
        {
            slider.value = Mathf.Clamp01(fill);

            fillImage.color = healthGradient.Evaluate(fill);
        }

    }
}