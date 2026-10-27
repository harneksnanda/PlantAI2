
using UnityEngine;
using TMPro;

public class PlantStageManager : MonoBehaviour
{
    [Header("Environment Sliders")]
    public UnityEngine.UI.Slider lightSlider;
    public UnityEngine.UI.Slider humiditySlider;
    public UnityEngine.UI.Slider temperatureSlider;

    [Header("Stage 1 - Sapling")]
    public GameObject healthySapling;
    public GameObject stressedSapling;
    public GameObject criticalSapling;

    [Header("Stage 2 - Flowering")]
    public GameObject healthyFlowering;
    public GameObject stressedFlowering;
    public GameObject criticalFlowering;

    [Header("Stage 3 - Fruiting")]
    public GameObject healthyFruiting;
    public GameObject stressedFruiting;
    public GameObject criticalFruiting;

    [Header("UI")]
    public TMP_Text conditionText;

    [Header("Progress")]
    [Range(1, 3)]
    public int currentStage = 1;

    public enum PlantCondition
    {
        Healthy,
        Stressed,
        Critical
    }

    public PlantCondition currentCondition =
        PlantCondition.Healthy;

    public void EvaluateStage()
    {
        if (lightSlider == null ||
            humiditySlider == null ||
            temperatureSlider == null)
        {
            Debug.LogError(
                "Assign all three environment sliders!");
            return;
        }

        int unsuitable = 0;

        // Light target changes by stage.
        float minimumLight = currentStage == 1 ? 50f : 60f;

        if (lightSlider.value < minimumLight)
            unsuitable++;

        // Humidity target.
        if (humiditySlider.value < 40f ||
            humiditySlider.value > 70f)
            unsuitable++;

        // Temperature target.
        float maximumTemperature =
            currentStage == 3 ? 30f : 28f;

        if (temperatureSlider.value < 20f ||
            temperatureSlider.value > maximumTemperature)
            unsuitable++;

        if (unsuitable == 0)
            currentCondition = PlantCondition.Healthy;
        else if (unsuitable == 1)
            currentCondition = PlantCondition.Stressed;
        else
            currentCondition = PlantCondition.Critical;

        ShowPlantCondition();
    }

    public void ShowPlantCondition()
    {
        // Hide every variant first.
        SetActiveSafe(healthySapling, false);
        SetActiveSafe(stressedSapling, false);
        SetActiveSafe(criticalSapling, false);

        SetActiveSafe(healthyFlowering, false);
        SetActiveSafe(stressedFlowering, false);
        SetActiveSafe(criticalFlowering, false);

        SetActiveSafe(healthyFruiting, false);
        SetActiveSafe(stressedFruiting, false);
        SetActiveSafe(criticalFruiting, false);

        GameObject selectedPlant = null;

        if (currentStage == 1)
        {
            if (currentCondition == PlantCondition.Healthy)
                selectedPlant = healthySapling;
            else if (currentCondition == PlantCondition.Stressed)
                selectedPlant = stressedSapling;
            else
                selectedPlant = criticalSapling;
        }
        else if (currentStage == 2)
        {
            if (currentCondition == PlantCondition.Healthy)
                selectedPlant = healthyFlowering;
            else if (currentCondition == PlantCondition.Stressed)
                selectedPlant = stressedFlowering;
            else
                selectedPlant = criticalFlowering;
        }
        else if (currentStage == 3)
        {
            if (currentCondition == PlantCondition.Healthy)
                selectedPlant = healthyFruiting;
            else if (currentCondition == PlantCondition.Stressed)
                selectedPlant = stressedFruiting;
            else
                selectedPlant = criticalFruiting;
        }

        SetActiveSafe(selectedPlant, true);
        UpdateConditionText();
    }

    public bool TryAdvanceAfterTwoDays()
    {
        if (currentCondition != PlantCondition.Healthy)
        {
            if (conditionText != null)
            {
                conditionText.text =
                    "Your plant needs care before it can progress. " +
                    "Adjust the environment and submit again.";
            }

            return false;
        }

        if (currentStage >= 3)
        {
            if (conditionText != null)
            {
                conditionText.text =
                    "Your tomato plant has reached the fruiting stage!";
            }

            return false;
        }

        currentStage++;

        // Show the healthy version of the new growth stage.
        currentCondition = PlantCondition.Healthy;
        ShowPlantCondition();

        if (conditionText != null)
        {
            conditionText.text = currentStage == 2
                ? "Two days passed! Your tomato plant is now flowering."
                : "Two days passed! Your tomato plant has reached the fruiting stage.";
        }

        return true;
    }

    void UpdateConditionText()
    {
        if (conditionText == null)
            return;

        string stageName = currentStage == 1
            ? "Sapling"
            : currentStage == 2
                ? "Flowering"
                : "Fruiting";

        string result = currentCondition == PlantCondition.Healthy
            ? "Healthy - growing well!"
            : currentCondition == PlantCondition.Stressed
                ? "Stressed - adjust the environment."
                : "Critical - emergency care needed!";

        conditionText.text = stageName + "\n" + result;
    }

    void SetActiveSafe(GameObject obj, bool active)
    {
        if (obj != null)
            obj.SetActive(active);
    }
}
