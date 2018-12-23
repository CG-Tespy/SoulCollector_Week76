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

    bool forwardsInPath = true; // Whether or not this is moving forwards in the path

    protected override void Awake()
    {
        base.Awake();
        _originalPos =              transform.position;
        SetWorldPathPoints();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        HandleMovement();
    }

    public override void HandleMovement()
    {
        if (_pathPoints.Length == 0)
            return;

        Vector2 newPos;

        // Move towards the next point
        _targetPathPoint =                  _absPathPoints[_pathPointIndex];
        newPos =                            Vector2.Lerp(transform.position, _targetPathPoint, Time.deltaTime * moveSpeed);

        // Can't always get perfect precision with deltaTime, so we use a threshold to set newPos to 
        // the target pos when necessary.
        if (Vector2.Distance(newPos, _targetPathPoint) <= 0.1f)
            newPos =                        _targetPathPoint;

        transform.position =                newPos;

        if (newPos == _targetPathPoint) // Decide on a new destination
        {
            // When one end of the path was just reached, reverse direction along the path.
            bool atFirstPoint =             _pathPointIndex == 0;
            bool atLastPoint =              _pathPointIndex == _pathPoints.Length - 1;

            bool reverseDirection =         (atLastPoint && forwardsInPath) ||
                                            (atFirstPoint && !forwardsInPath);

            if (reverseDirection)
                forwardsInPath =            !forwardsInPath;

            if (forwardsInPath)
                _pathPointIndex++;
            else
                _pathPointIndex--;
        }

        
    }

    void SetWorldPathPoints()
    {
        if (_pathPoints == null || _pathPoints.Length == 0)
            return;

        _absPathPoints =                        new Vector2[_pathPoints.Length];
        Vector2 worldPoint;

        for (int i = 0; i < _pathPoints.Length; i++)
        {
            worldPoint =                        _originalPos + _pathPoints[i];
            _absPathPoints[i] =                 worldPoint;
        }


    }
    
}
