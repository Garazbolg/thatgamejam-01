using UnityEngine;

public class ResettableCharacterController : ResettableEntity
{
    public Vector3 initialPosition;

    public override void ResetEntity()
    {
        transform.localPosition = initialPosition;
    }
}