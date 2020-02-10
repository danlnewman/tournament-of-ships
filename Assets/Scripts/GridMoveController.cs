using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridMoveDirection
{
    up = 0,
    right = 1,
    down = 2,
    left = 3,
    none
};

public class GridMoveController
{
    private GridMoveDirection gridMoveDirection = GridMoveDirection.none;
    private float moveStartTime = 0f;
    private float moveEndTime = 0.0001f;
    private float startPosition;
    private float endPosition;
    private float t;
    private float period = 1.0f;
    private int i = 0;
    private Transform transform;

    public GridMoveController(Transform transform)
    {
        this.transform = transform;
    }

    public void SetDirection(GridMoveDirection setGridMoveDirection)
    {
        this.gridMoveDirection = setGridMoveDirection;
        moveStartTime = Time.time;
        moveEndTime = Time.time + period;

        switch (gridMoveDirection)
        {
            case GridMoveDirection.left:
                startPosition = transform.position.x;
                endPosition = transform.position.x - 1.0f;
                break;
            case GridMoveDirection.right:
                startPosition = transform.position.x;
                endPosition = transform.position.x + 1.0f;
                break;
            case GridMoveDirection.up:
                startPosition = transform.position.y;
                endPosition = transform.position.y + 1.0f;
                break;
            case GridMoveDirection.down:
                startPosition = transform.position.y;
                endPosition = transform.position.y - 1.0f;
                break;
        }
    }

    public bool Move()
    {
        t = (Time.time - moveStartTime) / (moveEndTime - moveStartTime);

        if (t >= 1.0f)
        {
            return false;
        }

        switch (gridMoveDirection)
        {
            case GridMoveDirection.left:
            case GridMoveDirection.right:
                transform.position = new Vector3(Mathf.Lerp(startPosition, endPosition, t), transform.position.y, transform.position.z);
                break;
            case GridMoveDirection.up:
            case GridMoveDirection.down:
                transform.position = new Vector3(transform.position.x, Mathf.Lerp(startPosition, endPosition, t), transform.position.z);
                break;
            case GridMoveDirection.none:
                return false;
        }

        return true;
    }
}
