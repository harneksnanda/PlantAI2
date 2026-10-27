
using UnityEngine;

public class TabletPageManager : MonoBehaviour
{
    [Header("Pages")]
    public GameObject page1_SelectPlant;
    public GameObject page2_TomatoInfo;
    public GameObject page3_Environment;
    public GameObject page4_PlantCondition;

    [Header("Plant Evaluation")]
    public PlantStageManager plantStageManager;

    public void ShowTomatoInfo()
    {
        ShowOnly(page2_TomatoInfo);
    }

    public void ShowEnvironment()
    {
        ShowOnly(page3_Environment);
    }

    public void BackToPlantSelection()
    {
        ShowOnly(page1_SelectPlant);
    }

    public void ConfirmEnvironment()
    {
        if (plantStageManager == null)
        {
            Debug.LogError(
                "PlantStageManager is not assigned!"
            );
            return;
        }

        // Evaluate the current slider settings.
        plantStageManager.EvaluateStage();

        // Display the plant condition.
        ShowOnly(page4_PlantCondition);
    }

    public void BackToEnvironment()
    {
        ShowOnly(page3_Environment);
    }

    private void ShowOnly(GameObject pageToShow)
    {
        page1_SelectPlant.SetActive(
            page1_SelectPlant == pageToShow);

        page2_TomatoInfo.SetActive(
            page2_TomatoInfo == pageToShow);

        page3_Environment.SetActive(
            page3_Environment == pageToShow);

        page4_PlantCondition.SetActive(
            page4_PlantCondition == pageToShow);
    }


    public void SkipTwoDaysAndContinue()
    {
        if (plantStageManager == null)
        {
            Debug.LogError("PlantStageManager is not assigned!");
            return;
        }

        bool advanced = plantStageManager.TryAdvanceAfterTwoDays();

        if (advanced)
        {
            ShowOnly(page3_Environment);
        }
    }

}
