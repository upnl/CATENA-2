using TMPro;
using UnityEngine;

public class DamageObjectBehaviour : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

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

    public void Initialize(string damageText)
    {
        text.text = damageText;
    }
}
