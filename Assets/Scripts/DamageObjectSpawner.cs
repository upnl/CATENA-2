using UnityEngine;

public class DamageObjectSpawner : MonoBehaviour
{
    public static DamageObjectSpawner Instance { get; private set; }

    [SerializeField] private GameObject damageObjectPrefab;

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
        
        // TODO
    }
}
