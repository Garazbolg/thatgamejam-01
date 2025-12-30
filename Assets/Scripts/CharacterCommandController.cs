using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCommandController : MonoBehaviour, IMoveable
{
    public List<Command> commands;
    public PathPreviz pathPreviz;
    public int spawnIndex;
    public LayerMask collision;
    
    public void TakeTurn(int index)
    {
        var command = Command.Wait;
        if (index >= 0 && index < commands.Count)
        {
            command = commands[index];
        }
        ExecuteCommand(command);
    }
    
    private void ExecuteCommand(Command command)
    {
        switch (command)
        {
            case Command.MoveUp:
                TryMove(Vector3.up);
                break;
            case Command.MoveDown:
                TryMove(Vector3.down);
                break;
            case Command.MoveLeft:
                TryMove(Vector3.left);
                break;
            case Command.MoveRight:
                TryMove(Vector3.right);
                break;
            case Command.Wait:
                // Do nothing
                break;
        }
    }
    
    private void TryMove(Vector3 direction)
    {
        if (CanMove(direction))
        {
            Move(direction);
        }
        else
        {
            GameManager.instance.sequencePlayer.SendOpCode(SequenceOpCode.Invalid);
        }
    }

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