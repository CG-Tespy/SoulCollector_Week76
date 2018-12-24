using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DirectionExtensions
{
    public static Vector2 ToVector2(this Direction dir)
    {
        switch (dir)
        {
            case Direction.east:
                return Vector2.right;
            case Direction.west:
                return Vector2.left;
            case Direction.south:
                return Vector2.down;
            case Direction.north:
                return Vector2.up;

            case Direction.northEast:
                return Vector2.up + Vector2.right;
            case Direction.northWest:
                return Vector2.up + Vector2.left;
            case Direction.southEast:
                return Vector2.down + Vector2.right;
            case Direction.southWest:
                return Vector2.down + Vector2.left;
            default:
                throw new System.NotImplementedException();
        }
    }
}
