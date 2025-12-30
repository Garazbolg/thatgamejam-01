using UnityEngine;

public interface IMoveable
{
    bool CanMove(Vector3 direction);
    void Move(Vector3 direction);
}