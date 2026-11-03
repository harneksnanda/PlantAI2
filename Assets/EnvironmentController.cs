
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

    [Header("Light Intensity Range")]
    public float minimumLightIntensity = 0.2f;
    public float maximumLightIntensity = 5f;

    [Header("Humidity Fog")]
    public ParticleSystem humidityFog;

    [Header("Value Text")]
    public TMP_Text lightValue;
    public TMP_Text humidityValue;
    public TMP_Text temperatureValue;

    [Header("Slider Value Text")]
    public TMP_Text lightValueText;
    public TMP_Text humidityValueText;
    public TMP_Text temperatureValueText;

    void Start()
    {
        RenderSettings.fog = true;
        UpdateEnvironment();
    }

    public void UpdateEnvironment()
    {
        if (lightSlider == null ||
            humiditySlider == null ||
            temperatureSlider == null)
        {
            Debug.LogError("Please assign all three sliders!");
            return;
        }

        // NORMALIZE SLIDER VALUES
        // Converts each slider's actual range into 0 to 1.

        float lightPercent = Mathf.InverseLerp(
            lightSlider.minValue,
            lightSlider.maxValue,
            lightSlider.value
        );

        float humidityPercent = Mathf.InverseLerp(
            humiditySlider.minValue,
            humiditySlider.maxValue,
            humiditySlider.value
        );

        float temperaturePercent = Mathf.InverseLerp(
            temperatureSlider.minValue,
            temperatureSlider.maxValue,
            temperatureSlider.value
        );

        // 1. LIGHT INTENSITY
        if (environmentLight != null)
        {
            environmentLight.intensity = Mathf.Lerp(
                minimumLightIntensity,
                maximumLightIntensity,
                lightPercent
            );
        }

        // 2. TEMPERATURE - LIGHT COLOUR
        if (environmentLight != null)
        {
            Color coldColor = new Color(0.3f, 0.6f, 1f);
            Color hotColor = new Color(1f, 0.35f, 0.05f);

            environmentLight.color = Color.Lerp(
                coldColor,
                hotColor,
                temperaturePercent
            );
        }

        // 3. HUMIDITY - PARTICLE FOG
        if (humidityFog != null)
        {
            var emission = humidityFog.emission;

            emission.rateOverTime = Mathf.Lerp(
                0f,
                800f,
                humidityPercent
            );

            if (!humidityFog.isPlaying)
            {
                humidityFog.Play();
            }
        }

        // 4. DISPLAY SLIDER VALUES

        string lightText =
            Mathf.RoundToInt(lightSlider.value) + "%";

        string humidityText =
            Mathf.RoundToInt(humiditySlider.value) + "%";

        string temperatureText =
            Mathf.RoundToInt(temperatureSlider.value) + "°C";

        if (lightValue != null)
            lightValue.text = lightText;

        if (humidityValue != null)
            humidityValue.text = humidityText;

        if (temperatureValue != null)
            temperatureValue.text = temperatureText;

        if (lightValueText != null)
            lightValueText.text = lightText;

        if (humidityValueText != null)
            humidityValueText.text = humidityText;

        if (temperatureValueText != null)
            temperatureValueText.text = temperatureText;
    }
}
