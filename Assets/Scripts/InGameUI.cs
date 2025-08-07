using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider mpSlider;
    [SerializeField] private Image currentIconImage;

    public void SetHpSlider(float value)
    {
        hpSlider.value = value;
    }

    public void SetMpSlider(float value)
    {
        mpSlider.value = value;
    }

    public void SetCurrentIcon(Sprite icon)
    {
        currentIconImage.sprite = icon;
    }
}
