
using UnityEngine;

public class TabletPageManager : MonoBehaviour
{
    [Header("Tablet Pages")]
    public GameObject page1_SelectPlant;
    public GameObject page2_TomatoInfo;
    public GameObject page3_Environment;
    public GameObject page4_PlantCondition;

    [Header("Plant Stage Manager")]
    public PlantStageManager plantStageManager;

    void Start()
    {
        ShowOnly(page1_SelectPlant);
    }

    void ShowOnly(GameObject pageToShow)
    {
        page1_SelectPlant.SetActive(false);
        page2_TomatoInfo.SetActive(false);
        page3_Environment.SetActive(false);
        page4_PlantCondition.SetActive(false);

        if (pageToShow != null)
            pageToShow.SetActive(true);
    }

    public void ShowTomatoInfo()
    {
        ShowOnly(page2_TomatoInfo);
    }

    public void ShowEnvironment()
    {
        ShowOnly(page3_Environment);

        if (plantStageManager != null)
            plantStageManager.UpdateEnvironmentStageText();
    }

    public void BackToPlantSelection()
    {
        ShowOnly(page1_SelectPlant);
    }

    public void ConfirmEnvironment()
    {
        if (plantStageManager == null)
        {
            Debug.LogError("PlantStageManager is not assigned!");
            return;
        }

        plantStageManager.EvaluateStage();

        ShowOnly(page4_PlantCondition);
    }

    public void RegulateAgain()
    {
        ShowEnvironment();
    }

    public void MoveToNextStage()
    {
        if (plantStageManager == null)
        {
            Debug.LogError("PlantStageManager is not assigned!");
            return;
        }

        bool advanced =
            plantStageManager.TryAdvanceToNextStage();

        if (advanced)
        {
            ShowOnly(page3_Environment);
        }
    }
}
