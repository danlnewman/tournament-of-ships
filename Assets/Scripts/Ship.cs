using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    ClientMessage message;
    GridRotationController gridRotationController;
    //GridMoveDirection[] directions = { GridMoveDirection.left, GridMoveDirection.up, GridMoveDirection.right, GridMoveDirection.down };
    GridRotationDirection[] directions = { GridRotationDirection.right, GridRotationDirection.forward };
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        gridRotationController = new GridRotationController(transform);
        message = JsonUtility.FromJson<ClientMessage>("{\"client\" : 1,\"commands\" : [ \"left\", \"left\", \"up\", \"right\" ]}");
    }

    // Update is called once per frame
    void Update()
    {
        if (!gridRotationController.Move())
        {
            GridRotationDirection gridRotationDirection = directions[i++ % directions.Length];
            gridRotationController.SetDirection(gridRotationDirection);
        }
    }

}
