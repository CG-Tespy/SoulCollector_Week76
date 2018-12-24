using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingEntity : MonoBehaviour2D, IMovementHandler
{
    [SerializeField] float _moveSpeed = 5;

    public float moveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public bool canMove { get; set; }

    protected override void Awake()
    {
        base.Awake();
        canMove = true;
    }

    public abstract void HandleMovement();
}
