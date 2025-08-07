using UnityEngine;
using UnityEngine.UI;

public class AboveEnemyUI : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;

    private Transform _cameraTransform;

    private void Awake()
    {
        _cameraTransform = Camera.main?.transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(_cameraTransform);
        transform.localRotation *= Quaternion.Euler(0f, 180f, 0f);
    }

    public void SetHpSlider(float value)
    {
        hpSlider.value = value;
    }
}
