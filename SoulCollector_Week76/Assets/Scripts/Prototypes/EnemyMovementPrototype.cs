using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemies move in a path set in the inspector.
/// </summary>
public class EnemyMovementPrototype : MovingEntity
{
    [Tooltip("Points on the path this enemy moves relative to its starting position.")]
    [SerializeField] Vector2[] _pathPoints;

    // World space positions of the points this is to move to
    Vector2[] _absPathPoints;

    Vector2 _originalPos;
    Vector2 _targetPathPoint;
    int _pathPointIndex = 0;

    protected override void Awake()
    {
        base.Awake();
        _originalPos =              transform.position;


        

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        HandleMovement();
    }

    public override void HandleMovement()
    {
        if (_pathPoints.Length > 0)
        {
            // Move towards the next point
            _targetPathPoint = _absPathPoints[_pathPointIndex];

        }
    }

    void SetWorldPathPoints()
    {
        if (_pathPoints == null || _pathPoints.Length == 0)
            return;
    }
    
}
