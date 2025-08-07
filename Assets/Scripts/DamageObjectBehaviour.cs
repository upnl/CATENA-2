using TMPro;
using UnityEngine;

public class DamageObjectBehaviour : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private Transform _cameraTransform;
    private float _lifeTime;

    private void Awake()
    {
        _cameraTransform = Camera.main?.transform;
    }

    private void Update()
    {
        if (_lifeTime <= 0f)
        {
            Destroy(gameObject);
            return;
        }
        
        _lifeTime -= Time.deltaTime;
    }

    private void LateUpdate()
    {
        transform.LookAt(_cameraTransform);
        transform.localRotation *= Quaternion.Euler(0f, 180f, 0f);
    }

    public void Initialize(string damageText, float lifeTime)
    {
        text.text = damageText;
        _lifeTime = lifeTime;
    }
}
