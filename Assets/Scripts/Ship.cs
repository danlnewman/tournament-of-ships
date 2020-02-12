using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField]
    public int shipId;

    ClientMessage message;
    GridRotationController gridRotationController;
    //GridMoveDirection[] directions = { GridMoveDirection.left, GridMoveDirection.up, GridMoveDirection.right, GridMoveDirection.down };
    GridRotationDirection[] directions;// = { GridRotationDirection.right, GridRotationDirection.forward };
    int i = 0;
    public ConcurrentQueue<ClientMessage> queue;

    // Start is called before the first frame update
    void Start()
    {
        gridRotationController = new GridRotationController(transform);
        directions = new GridRotationDirection[] { GridRotationDirection.none };
        //message = JsonUtility.FromJson<ClientMessage>("{\"ship\" : 1,\"commands\" : [ \"right\", \"forward\"]}");
        //directions = System.Array.ConvertAll(message.commands, value => stringToGridRotationDirection(value));
        queue = new ConcurrentQueue<ClientMessage>();
    }

    GridRotationDirection stringToGridRotationDirection(string s)
    {
        switch(s)
        {
            case "left":
                return GridRotationDirection.left;
            case "right":
                return GridRotationDirection.right;
            case "forward":
                return GridRotationDirection.forward;
        }
        return GridRotationDirection.none;
    }

    // Update is called once per frame
    void Update()
    {
        ClientMessage newMessage;
        if (queue.TryDequeue(out newMessage))
        {
            i = 0;
            directions = System.Array.ConvertAll(newMessage.commands, value => stringToGridRotationDirection(value));

        }

        if (!gridRotationController.Move() && i < directions.Length)
        {
            GridRotationDirection gridRotationDirection = directions[i++];
            gridRotationController.SetDirection(gridRotationDirection);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Got coin");
        GameManager.addPoints(shipId);
        Destroy(collision.gameObject);
    }

}
