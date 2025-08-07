using UnityEngine;

public class DamageObjectSpawner : MonoBehaviour
{
    public static DamageObjectSpawner Instance { get; private set; }

    [SerializeField] private GameObject damageObjectPrefab;
    [SerializeField] private Color playerDamageColor;
    [SerializeField] private Color enemyDamageColor;

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

    public void SpawnDamageObject(int damageAmount, Vector3 position, bool isPlayer = false)
    {
        var damageObject = Instantiate(damageObjectPrefab, position, Quaternion.identity);

        var damageObjectBehaviour = damageObject.GetComponent<DamageObjectBehaviour>();
        damageObjectBehaviour.Initialize(damageAmount.ToString(), isPlayer ? playerDamageColor : enemyDamageColor);

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
