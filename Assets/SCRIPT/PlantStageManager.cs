
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantStageManager : MonoBehaviour
{
    [Header("Environment Sliders")]
    public Slider lightSlider;
    public Slider humiditySlider;
    public Slider temperatureSlider;

    [Header("Growth Stage Parent Objects")]
    public GameObject stage1SaplingParent;
    public GameObject stage2FloweringParent;
    public GameObject stage3FruitingParent;

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

    [Header("Page 4 UI")]
    public TMP_Text conditionText;
    public GameObject regulateAgainButton;
    public GameObject nextStageButton;

    [Header("Environment Page Stage Text")]
    public TMP_Text environmentStageText;

    [Header("Growth Progress")]
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


    // ==========================================
    // 1. EVALUATE ENVIRONMENT
    // ==========================================

    

    public void EvaluateStage()
    {
        if (lightSlider == null ||
            humiditySlider == null ||
            temperatureSlider == null)
        {
            Debug.LogError(
                "PlantStageManager: Assign all three sliders!"
            );
            return;
        }

        int unsuitable = 0;

        // Light requirement
        float minimumLight = currentStage == 1 ? 50f : 60f;

        if (lightSlider.value < minimumLight)
            unsuitable++;

        // Humidity requirement
        if (humiditySlider.value < 40f ||
            humiditySlider.value > 70f)
            unsuitable++;

        // Temperature requirement
        float maximumTemperature =
            currentStage == 3 ? 30f : 28f;

        if (temperatureSlider.value < 20f ||
            temperatureSlider.value > maximumTemperature)
            unsuitable++;

        // Determine plant condition
        if (unsuitable == 0)
        {
            currentCondition = PlantCondition.Healthy;
        }
        else if (unsuitable == 1)
        {
            currentCondition = PlantCondition.Stressed;
        }
        else
        {
            currentCondition = PlantCondition.Critical;
        }

        ShowPlantCondition();

        Debug.Log(
            "Stage: " + currentStage +
            " | Condition: " + currentCondition
        );
    }

    public void UpdateEnvironmentStageText()
    {
        if (environmentStageText == null)
            return;

        switch (currentStage)
        {
            case 1:
                environmentStageText.text = "STAGE 1 - SAPLING";
                break;

            case 2:
                environmentStageText.text = "STAGE 2 - FLOWERING";
                break;

            case 3:
                environmentStageText.text = "STAGE 3 - FRUITING";
                break;
        }
    }


    // ==========================================
    // 2. SHOW CORRECT GROWTH STAGE
    // ==========================================

    public void ShowPlantCondition()
    {
        // Activate the correct growth-stage parent first.
        SetStageObjectsActive();

        // Hide all nine plant models.
        SetPlantActive(healthySapling, false);
        SetPlantActive(stressedSapling, false);
        SetPlantActive(criticalSapling, false);

        SetPlantActive(healthyFlowering, false);
        SetPlantActive(stressedFlowering, false);
        SetPlantActive(criticalFlowering, false);

        SetPlantActive(healthyFruiting, false);
        SetPlantActive(stressedFruiting, false);
        SetPlantActive(criticalFruiting, false);

        GameObject selectedPlant = null;

        // Select model according to stage and condition.
        switch (currentStage)
        {
            case 1:

                if (currentCondition == PlantCondition.Healthy)
                    selectedPlant = healthySapling;

                else if (currentCondition == PlantCondition.Stressed)
                    selectedPlant = stressedSapling;

                else
                    selectedPlant = criticalSapling;

                break;


            case 2:

                if (currentCondition == PlantCondition.Healthy)
                    selectedPlant = healthyFlowering;

                else if (currentCondition == PlantCondition.Stressed)
                    selectedPlant = stressedFlowering;

                else
                    selectedPlant = criticalFlowering;

                break;


            case 3:

                if (currentCondition == PlantCondition.Healthy)
                    selectedPlant = healthyFruiting;

                else if (currentCondition == PlantCondition.Stressed)
                    selectedPlant = stressedFruiting;

                else
                    selectedPlant = criticalFruiting;

                break;
        }

        // Activate the selected model.
        if (selectedPlant != null)
        {
            selectedPlant.SetActive(true);
        }
        else
        {
            Debug.LogError(
                "Plant model is missing! Check Stage " +
                currentStage + " model references in Inspector."
            );
        }

        UpdateConditionText();
        UpdateButtons();
    }


    // ==========================================
    // 3. ACTIVATE CORRECT PARENT OBJECT
    // ==========================================

    void SetStageObjectsActive()
    {
        if (stage1SaplingParent != null)
            stage1SaplingParent.SetActive(currentStage == 1);

        if (stage2FloweringParent != null)
            stage2FloweringParent.SetActive(currentStage == 2);

        if (stage3FruitingParent != null)
            stage3FruitingParent.SetActive(currentStage == 3);
    }


    // ==========================================
    // 4. UPDATE CONDITION TEXT
    // ==========================================

    void UpdateConditionText()
    {
        if (conditionText == null)
            return;

        string stageName =
            currentStage == 1 ? "Sapling" :
            currentStage == 2 ? "Flowering" :
            "Fruiting";

        string result =
            currentCondition == PlantCondition.Healthy
                ? "Healthy - growing well!"
            : currentCondition == PlantCondition.Stressed
                ? "Stressed - adjust the environment."
            : "Critical - emergency care needed!";

        conditionText.text = stageName + "\n" + result;

        if (currentStage == 3 &&
            currentCondition == PlantCondition.Healthy)
        {
            conditionText.text =
                "Fruiting\nHealthy - your tomato plant has completed all stages!";
        }
    }


    // ==========================================
    // 5. SHOW CORRECT BUTTONS
    // ==========================================

    void UpdateButtons()
    {
        bool isHealthy =
            currentCondition == PlantCondition.Healthy;

        // Unhealthy = Regulate Again
        if (regulateAgainButton != null)
        {
            regulateAgainButton.SetActive(!isHealthy);
        }

        // Healthy and Stage 1 or 2 = Next Stage
        if (nextStageButton != null)
        {
            nextStageButton.SetActive(
                isHealthy && currentStage < 3
            );
        }
    }


    // ==========================================
    // 6. ADVANCE TO NEXT GROWTH STAGE
    // ==========================================

    public bool TryAdvanceToNextStage()
    {
        if (currentCondition != PlantCondition.Healthy)
        {
            Debug.LogWarning(
                "Plant is not healthy. Cannot advance!"
            );
            return false;
        }

        if (currentStage >= 3)
        {
            Debug.Log("Plant has completed all growth stages!");
            return false;
        }

        // Advance growth stage.
        currentStage++;

        // Start the new stage as healthy until evaluated.
        currentCondition = PlantCondition.Healthy;

        // Display the healthy model for the new stage.
        ShowPlantCondition();

        Debug.Log(
            "Plant advanced to Stage " + currentStage
        );

        return true;
    }

    void SetPlantActive(GameObject plant, bool active)
    {
        if (plant != null)
        {
            plant.SetActive(active);
        }
    }
}
