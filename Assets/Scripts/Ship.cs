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
    [SerializeField]
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        gridRotationController = new GridRotationController(transform);
        directions = new GridRotationDirection[] { GridRotationDirection.none };
        //message = JsonUtility.FromJson<ClientMessage>("{\"ship\" : 1,\"commands\" : [ \"right\", \"forward\"]}");
        //directions = System.Array.ConvertAll(message.commands, value => stringToGridRotationDirection(value));
        queue = new ConcurrentQueue<ClientMessage>();
        animator = GetComponentInChildren<Animator>();

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
        if (!GameManager.InCompetition())
        {
            ClientMessage newMessage;
            if (queue.TryDequeue(out newMessage))
            {
                i = 0;
                directions = System.Array.ConvertAll(newMessage.commands, value => stringToGridRotationDirection(value));

            }
        }

        bool moving = gridRotationController.Move();
        animator.SetBool("Moving", moving);
        if (!moving && i < directions.Length)
        {
            GridRotationDirection gridRotationDirection = directions[i++];
            gridRotationController.SetDirection(gridRotationDirection);
        }
        

    }

    public void StartMoving()
    {
        while(true)
        {
            ClientMessage newMessage;
            if (queue.TryDequeue(out newMessage))
            {
                i = 0;
                directions = System.Array.ConvertAll(newMessage.commands, value => stringToGridRotationDirection(value));
            }
            else
            {
                break;
            }

        }

    }

    public void ResetMove()
    {
        while (!queue.IsEmpty)
        {
            ClientMessage message;
            queue.TryDequeue(out message);
        }

        directions = new GridRotationDirection[] { GridRotationDirection.none };
        GridRotationDirection gridRotationDirection = directions[0];
        gridRotationController.SetDirection(gridRotationDirection);
        i = 0;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Got coin");
        GameManager.addPoints(shipId);
        Destroy(collision.gameObject);
    }

}
