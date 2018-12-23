using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
