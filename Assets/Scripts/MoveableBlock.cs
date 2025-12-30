using System;
using UnityEngine;

public class MoveableBlock : MonoBehaviour, IMoveable
{
    public LayerMask collision;

    public bool CanMove(Vector3 direction)
    {
        var position = transform.position;
        position.z = 0;
        var hit2D = Physics2D.Raycast(position + direction*0.5f, direction, .5f, collision);

        return !hit2D;
    }

    public void Move(Vector3 direction)
    {
        transform.position += direction;
    }
}