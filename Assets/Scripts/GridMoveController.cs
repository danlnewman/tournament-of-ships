using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMoveController : MonoBehaviour
{
    public enum MoveDirection
    {
        left,
        right,
        up,
        down
    };

    public MoveDirection moveDirection = MoveDirection.left;
    public float moveStartTime;
    public float moveEndTime;
    public float startPosition;
    public float endPosition;
    public bool moving = false;
    public float t;
    public float period = 1.0f;
    MoveDirection[] directions = { MoveDirection.left, MoveDirection.up, MoveDirection.right, MoveDirection.down };
    int i = 0;

    // Update is called once per frame
    void Update()
    {
        if (!moving)
        {
            moveDirection = directions[i++ % directions.Length];
            moveStartTime = Time.time;
            moveEndTime = Time.time + period;
            moving = true;
            switch (moveDirection)
            {
                case MoveDirection.left:
                    transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                    startPosition = transform.position.x;
                    endPosition = transform.position.x - 1.0f;
                    break;
                case MoveDirection.right:
                    transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                    startPosition = transform.position.x;
                    endPosition = transform.position.x + 1.0f;
                    break;
                case MoveDirection.up:
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    startPosition = transform.position.y;
                    endPosition = transform.position.y + 1.0f;
                    break;
                case MoveDirection.down:
                    transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                    startPosition = transform.position.y;
                    endPosition = transform.position.y - 1.0f;
                    break;
            }
        }

        t = (Time.time - moveStartTime) / (moveEndTime - moveStartTime);

        switch (moveDirection)
        {
            case MoveDirection.left:
                if (transform.position.x <= endPosition)
                {
                    moving = false;
                }
                transform.position = new Vector3(Mathf.Lerp(startPosition, endPosition, t), transform.position.y, transform.position.z);
                break;
            case MoveDirection.right:
                if (transform.position.x >= endPosition)
                {
                    moving = false;
                }
                transform.position = new Vector3(Mathf.Lerp(startPosition, endPosition, t), transform.position.y, transform.position.z);
                break;
            case MoveDirection.up:
                if (transform.position.y >= endPosition)
                {
                    moving = false;
                }
                transform.position = new Vector3(transform.position.x, Mathf.Lerp(startPosition, endPosition, t), transform.position.z);
                break;
            case MoveDirection.down:
                if (transform.position.y <= endPosition)
                {
                    moving = false;
                }
                transform.position = new Vector3(transform.position.x, Mathf.Lerp(startPosition, endPosition, t), transform.position.z);
                break;
        }
    }
}
