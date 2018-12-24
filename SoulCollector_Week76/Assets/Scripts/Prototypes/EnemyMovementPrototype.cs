using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemySoulType
{
    basic, frantic, angry
}

/// <summary>
/// Enemies move in a path set in the inspector.
/// </summary>
public class EnemyMovementPrototype : MovingEntity, IStunnable
{
    [SerializeField] EnemySoulType soulType;

    /* It's better to use an enum than three bools. Using bools like this hurts readability.
    [Tooltip("1) Basic 2) Frantic 3) Angry")]
    
    [SerializeField] bool[] _SoulType;

*/
    [Tooltip("Points on the path this enemy moves relative to its starting position.")]
    [SerializeField] Vector2[] _pathPoints;

    // World space positions of the points this is to move to
    Vector2[] _absPathPoints;

    Vector2 _originalPos;
    Vector2 _targetPathPoint;
    int _pathPointIndex = 0;

    bool forwardsInPath = true; // Whether or not this is moving forwards in the path

    float stunTimer;

    //Frantic Movement
    [SerializeField] FranticMovement _FranticStats;



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

        if (soulType == EnemySoulType.basic)
        {
            if (_pathPoints.Length == 0)
                return;

            Vector2 newPos;

            // Move towards the next point
            _targetPathPoint = _absPathPoints[_pathPointIndex];
            newPos = Vector2.Lerp(transform.position, _targetPathPoint, Time.deltaTime * moveSpeed);

            // Can't always get perfect precision with deltaTime, so we use a threshold to set newPos to 
            // the target pos when necessary.
            if (Vector2.Distance(newPos, _targetPathPoint) <= 0.1f)
                newPos = _targetPathPoint;

            transform.position = newPos;

            if (newPos == _targetPathPoint) // Decide on a new destination
            {
                // When one end of the path was just reached, reverse direction along the path.
                bool atFirstPoint = _pathPointIndex == 0;
                bool atLastPoint = _pathPointIndex == _pathPoints.Length - 1;

                bool reverseDirection = (atLastPoint && forwardsInPath) ||
                                                (atFirstPoint && !forwardsInPath);

                if (reverseDirection)
                    forwardsInPath = !forwardsInPath;

                if (forwardsInPath)
                    _pathPointIndex++;
                else
                    _pathPointIndex--;
            }
            return;
        }

        else if (soulType == EnemySoulType.frantic)
        {
            if (_FranticStats.limit == 0) { return; }
            if (_FranticStats.currentDirection == Vector2.zero)
            {
                int _RNG;
                _RNG = Random.Range(0, 3);
                Debug.Log(_RNG);
                switch (_RNG)
                {
                    case 0:
                        _FranticStats.currentDirection = Vector2.right;
                        break;
                    case 1:
                        _FranticStats.currentDirection = Vector2.left;
                        break;
                    case 2:
                        _FranticStats.currentDirection = Vector2.up;
                        break;
                    case 3:
                        _FranticStats.currentDirection = Vector2.down;
                        break;
                    default:
                        Debug.Log("Error");
                        break;
                }
                
            }



            Vector2 newPos;
            newPos = Vector2.Lerp(transform.position, 
            new Vector2(transform.position.x + _FranticStats.currentDirection.x, transform.position.y + _FranticStats.currentDirection.y), 
            Time.deltaTime * moveSpeed);
            
            if (Vector2.Distance(_originalPos, newPos) > _FranticStats.limit) { _FranticStats.currentDirection = -_FranticStats.currentDirection; }
            transform.position = newPos;
            _FranticStats.timeTillChange -= Time.fixedDeltaTime;
            if (_FranticStats.timeTillChange <= 0) {
                _FranticStats.timeTillChange = Random.Range(0.5f, 3);
                SetNewDirection();
            }
            return;
        }

        else if (soulType == EnemySoulType.angry)
        {
            //AngrySoulAI
            return;
        }


    }

    void SetNewDirection()
    {
        int _RNG;
        _RNG = Random.Range(0, 3);
        Debug.Log(_RNG);
        switch (_RNG)
        {
            case 0:
                _FranticStats.currentDirection = Vector2.right;
                break;
            case 1:
                _FranticStats.currentDirection = Vector2.left;
                break;
            case 2:
                _FranticStats.currentDirection = Vector2.up;
                break;
            case 3:
                _FranticStats.currentDirection = Vector2.down;
                break;
            default:
                Debug.Log("Error");
                break;
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

    public bool GetStunned(float duration)
    {
        throw new System.NotImplementedException();
    }

    IEnumerator StunCountdown(float stunDuration)
    {

        yield return null;
    }
    
}
