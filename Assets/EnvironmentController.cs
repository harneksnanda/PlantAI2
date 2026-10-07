using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnvironmentController : MonoBehaviour
{
    [Header("Sliders")]
    public Slider lightSlider;
    public Slider humiditySlider;
    public Slider temperatureSlider;

    [Header("ICU Light")]
    public Light environmentLight;

    [Header("Fog")]
    public Color fogColor = Color.gray;

    [Header("Value Text")]
    public TMP_Text lightValue;
    public TMP_Text humidityValue;
    public TMP_Text temperatureValue;

    void Start()
    {
        RenderSettings.fog = true;

        UpdateEnvironment();
    }

    public void UpdateEnvironment()
    {
        // LIGHT INTENSITY
        if (environmentLight != null)
        {
            environmentLight.intensity = Mathf.Lerp(
                15f,
                1500f,
                lightSlider.value / 12f
            );
        }

        // TEMPERATURE → LIGHT COLOR
        if (environmentLight != null)
        {
            float temperaturePercent = Mathf.InverseLerp(
                0f,
                1f,
                temperatureSlider.value
            );

            Color coldColor = new Color(1f, 0.35f, 0.05f);
            Color hotColor = new Color(0.3f, 0.6f, 1f);

            environmentLight.color = Color.Lerp(
                coldColor,
                hotColor,
                temperaturePercent
            );
        }

        // HUMIDITY → FOG
        RenderSettings.fogDensity = Mathf.Lerp(
            0f,
            0.08f,
            humiditySlider.value / 100f
        );

        // DISPLAY VALUES
        if (lightValue != null)
            lightValue.text = Mathf.RoundToInt(lightSlider.value) + "%";

        if (humidityValue != null)
            humidityValue.text = Mathf.RoundToInt(humiditySlider.value) + "%";

        if (temperatureValue != null)
            temperatureValue.text =
                Mathf.RoundToInt(temperatureSlider.value) + "°C";
    }
}