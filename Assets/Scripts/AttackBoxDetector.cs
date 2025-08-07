using System;
using UnityEngine;

public class AttackBoxDetector : MonoBehaviour
{
    // tmp
    public GameObject hitEffect;

    public Vector3 boxOffset;
    public Vector3 boxSize;

    public bool showGizmos = true;

    public string hitDetectTag = "Player";
    
    private void OnDrawGizmos()
    {
        if (!showGizmos) return;
        var boxPos = transform.position + Quaternion.LookRotation(transform.forward, Vector3.up) * boxOffset;
        
        Vector3 center = boxPos;
        Vector3 halfExtents = boxSize;
        Quaternion rotation = transform.rotation;

        Gizmos.color = Color.red;

        // 회전, 위치 적용
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(boxPos, rotation, Vector3.one);
        Gizmos.matrix = rotationMatrix;

        // Draw at origin, because matrix already moves it
        Gizmos.DrawWireCube(Vector3.zero, halfExtents * 2f);

        // 원래 matrix로 되돌리기
        Gizmos.matrix = Matrix4x4.identity;
    }

    public void SetAttackBox(Vector3 size, Vector3 pos, Quaternion rot)
    {
        
    }

    public void Attack(AttackContext ctx)
    {
        Collider[] results = new Collider[100];
        
        var boxPos = transform.position + Quaternion.LookRotation(transform.forward, Vector3.up) * ctx.boxOffset;
        Physics.OverlapBoxNonAlloc(boxPos, ctx.boxSize, results, transform.rotation);

        foreach (var result in results)
        {
            if (result == null) continue;
            if (result.gameObject == gameObject || result.CompareTag(hitDetectTag)) continue;
            
            if (result.TryGetComponent<EntityController>(out var controller))
            {
                var a = (result.transform.position - transform.position);
                a.y = 0;
                
                ctx.knockBack = Quaternion.LookRotation(
                    a.normalized, 
                    Vector3.up) * ctx.knockBack;
                if (controller.Hit(ctx))
                {
                    hitEffect = ctx.hitEffect;
                    var pos = result.ClosestPoint(transform.position);

                    if (hitEffect != null) Instantiate(hitEffect, pos, Quaternion.identity);
                    DamageObjectSpawner.Instance.SpawnDamageObject((int) ctx.damage, pos);
                }
            }
        }
    }
}
