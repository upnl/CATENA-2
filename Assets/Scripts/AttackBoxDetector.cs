using System;
using UnityEngine;

public class AttackBoxDetector : MonoBehaviour
{
    
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }

    public void SetAttackBox(Vector3 size, Vector3 pos, Quaternion rot)
    {
        
    }

    public void Attack()
    {
        Collider[] results = new Collider[10];
        Physics.OverlapBoxNonAlloc(transform.position, Vector3.one, results, transform.rotation);

        foreach (var result in results)
        {
            if (result == null) continue;
            if (result.gameObject == gameObject) continue;
            
            if (result.TryGetComponent<EntityController>(out var controller))
            {
                controller.Hit(new AttackContext
                {
                    Damage = 10f,
                    IsIgnoringDodge = false,
                    KnockBack = transform.forward * 2 + Vector3.up * 3,
                    StunTime = 2f,
                });
            }
        }
    }
}
