using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMoveController
{
    public enum GridMoveDirection
    {
        left,
        right,
        up,
        down
    };

    public GridMoveDirection gridMoveDirection = GridMoveDirection.left;
    public float moveStartTime;
    public float moveEndTime;
    public float startPosition;
    public float endPosition;
    public bool moving = false;
    public float t;
    public float period = 1.0f;
    GridMoveDirection[] directions = { GridMoveDirection.left, GridMoveDirection.up, GridMoveDirection.right, GridMoveDirection.down };
    int i = 0;
    private Transform transform;

    public GridMoveController(Transform transform)
    {
        this.transform = transform;
    }

    public void Move()
    {
        if (!moving)
        {
            gridMoveDirection = directions[i++ % directions.Length];
            moveStartTime = Time.time;
            moveEndTime = Time.time + period;
            moving = true;
            switch (gridMoveDirection)
            {
                case GridMoveDirection.left:
                    transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                    startPosition = transform.position.x;
                    endPosition = transform.position.x - 1.0f;
                    break;
                case GridMoveDirection.right:
                    transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                    startPosition = transform.position.x;
                    endPosition = transform.position.x + 1.0f;
                    break;
                case GridMoveDirection.up:
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    startPosition = transform.position.y;
                    endPosition = transform.position.y + 1.0f;
                    break;
                case GridMoveDirection.down:
                    transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                    startPosition = transform.position.y;
                    endPosition = transform.position.y - 1.0f;
                    break;
            }
        }

        t = (Time.time - moveStartTime) / (moveEndTime - moveStartTime);

        switch (gridMoveDirection)
        {
            case GridMoveDirection.left:
                if (transform.position.x <= endPosition)
                {
                    moving = false;
                }
                transform.position = new Vector3(Mathf.Lerp(startPosition, endPosition, t), transform.position.y, transform.position.z);
                break;
            case GridMoveDirection.right:
                if (transform.position.x >= endPosition)
                {
                    moving = false;
                }
                transform.position = new Vector3(Mathf.Lerp(startPosition, endPosition, t), transform.position.y, transform.position.z);
                break;
            case GridMoveDirection.up:
                if (transform.position.y >= endPosition)
                {
                    moving = false;
                }
                transform.position = new Vector3(transform.position.x, Mathf.Lerp(startPosition, endPosition, t), transform.position.z);
                break;
            case GridMoveDirection.down:
                if (transform.position.y <= endPosition)
                {
                    moving = false;
                }
                transform.position = new Vector3(transform.position.x, Mathf.Lerp(startPosition, endPosition, t), transform.position.z);
                break;
        }
    }
}
