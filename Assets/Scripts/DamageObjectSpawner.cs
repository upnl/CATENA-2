using UnityEngine;

public class DamageObjectSpawner : MonoBehaviour
{
    public static DamageObjectSpawner Instance { get; private set; }

    [SerializeField] private GameObject damageObjectPrefab;

    private const int MinimumScaleDamageAmount = 5;
    private const int MaximumScaleDamageAmount = 30;
    private const float MinimumScaleValue = 0.5f;
    private const float MaximumScaleValue = 1.3f;
    
    private void OnEnable()
    {
        if (Instance != this)
        {
            Instance = this;
        }
    }

    private void OnDisable()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void SpawnDamageObject(int damageAmount, Vector3 position, Transform parent = null)
    {
        var damageObject = parent
            ? Instantiate(damageObjectPrefab, position, Quaternion.identity, parent)
            : Instantiate(damageObjectPrefab, position, Quaternion.identity);

        var damageObjectBehaviour = damageObject.GetComponent<DamageObjectBehaviour>();
        damageObjectBehaviour.Initialize(damageAmount.ToString());

        damageObject.transform.localScale = Vector3.one * damageAmount switch
        {
            < MinimumScaleDamageAmount => MinimumScaleValue,
            > MaximumScaleDamageAmount => MaximumScaleValue,
            _ => Mathf.Lerp(MinimumScaleValue, MaximumScaleValue,
                (float)(damageAmount - MinimumScaleDamageAmount) /
                (MaximumScaleDamageAmount - MinimumScaleDamageAmount))
        };

        // TODO
    }
}
