using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementHandler 
{
    float moveSpeed { get; }
    void HandleMovement();
}
