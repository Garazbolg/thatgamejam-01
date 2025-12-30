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
        
        if(!hit2D) 
        {
            return true;
        }
        
        if(hit2D.collider.TryGetComponent<IMoveable>(out var moveable))
        {
            return moveable.CanMove(direction);
        }
        
        return false;
    }

    public void Move(Vector3 direction)
    {
        var position = transform.position;
        position.z = 0;
        var hit2D = Physics2D.Raycast(position + direction*0.5f, direction, .5f, collision);
        
        if(hit2D && hit2D.collider.TryGetComponent<IMoveable>(out var moveable))
        {
            moveable.Move(direction);
        }
        
        transform.position += direction;
    }
}