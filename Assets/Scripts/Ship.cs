using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
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


    ClientMessage message;
    // Start is called before the first frame update
    void Start()
    {
        message = JsonUtility.FromJson<ClientMessage>("{\"client\" : 1,\"commands\" : [ \"left\", \"left\", \"up\", \"right\" ]}");
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving)
        {
            moveDirection = MoveDirection.left;
            moveStartTime = Time.time;
            moveEndTime = Time.time + 1.0f;
            moving = true;
            startPosition = transform.position.x;
            endPosition = transform.position.x + 1.0f;
        }

        t = (Time.time - moveStartTime) / (moveEndTime - moveStartTime);
        transform.position = new Vector3(Mathf.Lerp( startPosition, endPosition, t), transform.position.y, transform.position.z);

        if (transform.position.x >= endPosition)
        {
            moving = false;
        }

    }

}
