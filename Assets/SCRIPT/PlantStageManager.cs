
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantStageManager : MonoBehaviour
{
    [Header("Environment Sliders")]
    public Slider lightSlider;
    public Slider humiditySlider;
    public Slider temperatureSlider;

    [Header("Stage 1 Plant Models")]
    public GameObject healthySapling;
    public GameObject stressedSapling;
    public GameObject criticalSapling;

    [Header("UI")]
    public TMP_Text conditionText;
    public Button submitButton;

    public enum PlantCondition
    {
        Healthy,
        Stressed,
        Critical
    }

    public PlantCondition currentCondition;

    public void EvaluateStage()
    {
        if (lightSlider == null ||
            humiditySlider == null ||
            temperatureSlider == null)
        {
            Debug.LogError("Assign all three environment sliders.");
            return;
        }

        int unsuitableSettings = 0;

        // Stage 1 target conditions for this prototype.
        if (lightSlider.value < 50f)
            unsuitableSettings++;

        if (humiditySlider.value < 40f ||
            humiditySlider.value > 70f)
            unsuitableSettings++;

        if (temperatureSlider.value < 20f ||
            temperatureSlider.value > 28f)
            unsuitableSettings++;

        if (unsuitableSettings == 0)
        {
            currentCondition = PlantCondition.Healthy;
        }
        else if (unsuitableSettings == 1)
        {
            currentCondition = PlantCondition.Stressed;
        }
        else
        {
            currentCondition = PlantCondition.Critical;
        }

        ShowPlantCondition();
    }

    void ShowPlantCondition()
    {
        if (healthySapling != null)
            healthySapling.SetActive(
                currentCondition == PlantCondition.Healthy);

        if (stressedSapling != null)
            stressedSapling.SetActive(
                currentCondition == PlantCondition.Stressed);

        if (criticalSapling != null)
            criticalSapling.SetActive(
                currentCondition == PlantCondition.Critical);

        if (conditionText != null)
        {
            switch (currentCondition)
            {
                case PlantCondition.Healthy:
                    conditionText.text =
                        "HEALTHY: Your sapling is growing well!";
                    break;

                case PlantCondition.Stressed:
                    conditionText.text =
                        "WARNING: Your sapling is stressed. Adjust the environment!";
                    break;

                case PlantCondition.Critical:
                    conditionText.text =
                        "EMERGENCY: Your sapling needs immediate care!";
                    break;
            }
        }

        Debug.Log("Stage 1 condition: " + currentCondition);
    }
}
