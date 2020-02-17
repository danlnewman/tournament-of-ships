using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GridRotationDirection
{
    left,
    right,
    forward,
    none
};

public class GridRotationController
{
    private GridMoveController gridMoveController;
    private GridRotationDirection gridRotationDirection = GridRotationDirection.none;

    private GridMoveDirection currentDirection = GridMoveDirection.up;

    private float moveStartTime = 0f;
    private float moveEndTime = 0.0001f;
    private Quaternion startAngle;
    private Quaternion endAngle;
    private float t;
    private float period = 1.0f;

    Transform transform;

    public GridRotationController(Transform transform)
    {
        this.gridMoveController = new GridMoveController(transform);
        this.transform = transform;
    }

    public void SetDirection(GridRotationDirection setGridRotationDirection)
    {
        this.gridRotationDirection = setGridRotationDirection;
        moveStartTime = Time.time;
        moveEndTime = Time.time + period;
        startAngle = Quaternion.Euler(0f,0f,(float)currentDirection * 90f);

        switch (gridRotationDirection)
        {
            case GridRotationDirection.left:
                currentDirection = (GridMoveDirection)(((int)currentDirection + 1) % 4);
                endAngle = Quaternion.Euler(0f, 0f, (float)currentDirection * 90f);
                gridMoveController.SetDirection(GridMoveDirection.none);
                break;
            case GridRotationDirection.right:
                currentDirection = (GridMoveDirection)(((int)currentDirection + 3) % 4);
                endAngle = Quaternion.Euler(0f, 0f, (float)currentDirection * 90f);
                gridMoveController.SetDirection(GridMoveDirection.none);
                break;
            case GridRotationDirection.forward:
                gridMoveController.SetDirection(currentDirection);
                break;
        }

    }

    public bool Move()
    {
        if (gridRotationDirection == GridRotationDirection.none)
        {
            return false;
        }
        else if(gridRotationDirection == GridRotationDirection.forward)
        {
            return gridMoveController.Move();
        }

        t = (Time.time - moveStartTime) / (moveEndTime - moveStartTime);

        if (t >= 1.0f)
        {
            transform.rotation = endAngle;
            return false;
        }

        transform.rotation = Quaternion.Slerp(startAngle, endAngle, t);


        return true;
    }

    public void Reset()
    {
        currentDirection = GridMoveDirection.up;
        gridRotationDirection = GridRotationDirection.none;
    }

}
