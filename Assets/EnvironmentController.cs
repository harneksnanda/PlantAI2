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

        // HUMIDITY → PARTICLE FOG
        if (humiditySlider != null && humidityFog != null)
        {
            var emission = humidityFog.emission;

            float humidityPercent = humiditySlider.value / 1f;

            emission.rateOverTime = Mathf.Lerp(
                0f,
                800f,
                humidityPercent
            );
            Debug.Log("Emiddion Value: " + emission.rateOverTime);

            if (!humidityFog.isPlaying)
            {
                humidityFog.Play();
            }
        }

        // DISPLAY VALUES
        if (lightValue != null)
            lightValue.text = Mathf.RoundToInt(lightSlider.value) + "%";

        if (humidityValue != null)
            humidityValue.text = Mathf.RoundToInt(humiditySlider.value) + "%";

        if (temperatureValue != null)
            temperatureValue.text =
                Mathf.RoundToInt(temperatureSlider.value) + "°C";


        if (lightValueText != null)
            lightValueText.text = Mathf.RoundToInt(lightSlider.value) + "%";

        if (humidityValueText != null)
            humidityValueText.text = Mathf.RoundToInt(humiditySlider.value) + "%";

        if (temperatureValueText != null)
            temperatureValueText.text = Mathf.RoundToInt(temperatureSlider.value) + "°C";
    }

}