using UnityEngine;

public class ResettableStartPosition : ResettableEntity
{
    public Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = transform.localPosition;
    }

    public override void ResetEntity()
    {
        transform.localPosition = initialPosition;
    }
}