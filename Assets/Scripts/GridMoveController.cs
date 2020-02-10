using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridMoveDirection
{
    up = 0,
    left = 1,
    down = 2,
    right = 3,
    none
};

public class GridMoveController
{
    private GridMoveDirection gridMoveDirection = GridMoveDirection.none;
    private float moveStartTime = 0f;
    private float moveEndTime = 0.0001f;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float t;
    private float period = 1.0f;
    private readonly Transform transform;

    public GridMoveController(Transform transform)
    {
        this.transform = transform;
    }

    public void SetDirection(GridMoveDirection setGridMoveDirection)
    {
        this.gridMoveDirection = setGridMoveDirection;
        moveStartTime = Time.time;
        moveEndTime = Time.time + period;
        startPosition = transform.position;


        switch (gridMoveDirection)
        {
            case GridMoveDirection.left:
                endPosition = new Vector3(transform.position.x - 1.0f, transform.position.y, transform.position.z);
                break;
            case GridMoveDirection.right:
                endPosition = new Vector3(transform.position.x + 1.0f, transform.position.y, transform.position.z);
                break;
            case GridMoveDirection.up:
                endPosition = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
                break;
            case GridMoveDirection.down:
                endPosition = new Vector3(transform.position.x, transform.position.y - 1.0f, transform.position.z);
                break;
        }
    }

    public bool Move()
    {
        if (gridMoveDirection == GridMoveDirection.none)
            return false;

        t = (Time.time - moveStartTime) / (moveEndTime - moveStartTime);

        if (t >= 1.0f)
        {
            transform.position = endPosition;
            return false;
        }

        transform.position = Vector3.Lerp(startPosition, endPosition, t);
        return true;
    }
}
