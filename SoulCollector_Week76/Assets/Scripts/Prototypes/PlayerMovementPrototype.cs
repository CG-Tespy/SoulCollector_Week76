using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    north, west, south, east,
    northWest, southWest, northEast, southEast
}
/// <summary>
/// Handles 8-way directional movement
/// </summary>
public class PlayerMovementPrototype : MovingEntity
{

    [SerializeField] float _runSpeedMultiplier = 1.5f;
    public float runSpeedMultiplier { get { return _runSpeedMultiplier; } protected set { _runSpeedMultiplier = value; } }

    bool _isRunning = false;

    // Input Axes
    float hAxisRaw, vAxisRaw;

    public Direction directionFacing { get; protected set; }
    

    protected override void Awake()
    {
        base.Awake();
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        UpdateInputAxes();
        UpdateRunningState();
        HandleMovement();
    }

    public override void HandleMovement()
    {
        Vector2 movement =                              new Vector2(hAxisRaw * moveSpeed, vAxisRaw * moveSpeed);

        if (_isRunning)
            movement *= runSpeedMultiplier;

        rigidbody.velocity =                            movement * Time.timeScale;

        UpdateFacingDirection();
        // TODO: Apply animations based on movement
        //ApplyMovementAnimations();
    }

    void UpdateInputAxes()
    {
        hAxisRaw =              Input.GetAxisRaw("Horizontal");
        vAxisRaw =              Input.GetAxisRaw("Vertical");
    }

    void UpdateRunningState()
    {
        bool releasedRightMouseButton = Input.GetMouseButtonUp(1);

        if (releasedRightMouseButton)
            _isRunning = !_isRunning;
        
    }

    void ApplyMovementAnimations()
    {
        throw new System.NotImplementedException();
    }

    void UpdateFacingDirection()
    {
        bool goingEast =                                rigidbody.velocity.x > 0;
        bool goingWest =                                rigidbody.velocity.x < 0;
        bool goingSouth =                               rigidbody.velocity.y < 0;
        bool goingNorth =                               rigidbody.velocity.y > 0;

        if (goingEast && goingSouth)
            directionFacing = Direction.southEast;
        else if (goingEast && goingNorth)
            directionFacing = Direction.northEast;
        else if (goingWest && goingNorth)
            directionFacing = Direction.northWest;
        else if (goingWest && goingSouth)
            directionFacing = Direction.southWest;
        else if (goingEast)
            directionFacing = Direction.east;
        else if (goingWest)
            directionFacing = Direction.west;
        else if (goingSouth)
            directionFacing = Direction.south;
        else if (goingNorth)
            directionFacing = Direction.north;
        
    }
}
