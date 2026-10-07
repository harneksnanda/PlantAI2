using UnityEngine;

public class TabletPageManager : MonoBehaviour
{
    public GameObject page1_SelectPlant;
    public GameObject page2_TomatoInfo;
    public GameObject page3_Environment;

    public void ShowTomatoInfo()
    {
        page1_SelectPlant.SetActive(false);
        page2_TomatoInfo.SetActive(true);
        page3_Environment.SetActive(false);
    }

    public void ShowEnvironment()
    {
        page1_SelectPlant.SetActive(false);
        page2_TomatoInfo.SetActive(false);
        page3_Environment.SetActive(true);
    }

    public void BackToPlantSelection()
    {
        page1_SelectPlant.SetActive(true);
        page2_TomatoInfo.SetActive(false);
        page3_Environment.SetActive(false);
    }
}