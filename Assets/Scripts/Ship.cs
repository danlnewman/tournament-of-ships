using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    ClientMessage message;
    // Start is called before the first frame update
    void Start()
    {
        message = JsonUtility.FromJson<ClientMessage>("{\"client\" : 1,\"commands\" : [ \"left\", \"left\", \"up\", \"right\" ]}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
