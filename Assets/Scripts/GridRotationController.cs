using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridRotationController
{

    public enum GridRotationDirection
    {
        left,
        right,
        forward
    };

    private GridMoveController gridMoveController;

    public GridRotationController(Transform transform)
    {
        this.gridMoveController = new GridMoveController(transform);
    }

    public void Move()
    {
        gridMoveController.Move();
    }

}

//    public ForwardMoveDirection moveDirection = MoveDirection.left;
//    public float moveStartTime;
//    public float moveEndTime;
//    public float startPosition;
//    public float endPosition;
//    public bool moving = false;
//    public float t;
//    public float period = 1.0f;
//    MoveDirection[] directions = { MoveDirection.left, MoveDirection.up, MoveDirection.right, MoveDirection.down };
//    int i = 0;

//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//}
