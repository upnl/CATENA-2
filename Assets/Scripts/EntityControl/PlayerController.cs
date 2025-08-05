using UnityEngine;

public class PlayerController : EntityController
{
    public Transform cameraTransform;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        LookDirection = cameraTransform.forward;
    }
}
