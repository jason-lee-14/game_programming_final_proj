using UnityEngine;
using UnityEngine.UI;

public class PointBarRed : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxPoints(int points)
    {
        slider.maxValue = points;
        slider.value = points;

        fill.color = gradient.Evaluate(1f);
    }
    public void SetPoints(int points)
    {
        slider.value = points;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
