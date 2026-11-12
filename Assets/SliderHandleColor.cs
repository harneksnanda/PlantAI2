
using UnityEngine;
using UnityEngine.UI;

public class SliderHandleColor : MonoBehaviour
{
    public Slider slider;
    public Image fillImage;

    public Color minimumColor = Color.white;
    public Color maximumColor = Color.yellow;

    void Start()
    {
        if (slider != null)
        {
            slider.onValueChanged.AddListener(ChangeFillColor);
            ChangeFillColor(slider.value);
        }
    }

    void ChangeFillColor(float value)
    {
        if (slider == null || fillImage == null)
            return;

        float percentage = Mathf.InverseLerp(
            slider.minValue,
            slider.maxValue,
            value
        );

        fillImage.color = Color.Lerp(
            minimumColor,
            maximumColor,
            percentage
        );
    }

    void OnDestroy()
    {
        if (slider != null)
            slider.onValueChanged.RemoveListener(ChangeFillColor);
    }
}
