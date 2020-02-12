using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    static Random rnd = new Random();

    [SerializeField]
    public int[] coinCounter = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    static GameManager instance = null;
    [SerializeField]
    GameObject CoinRef;
    [SerializeField]
    Text[] scoresText = new Text[12];
    [SerializeField]
    Ship[] ship;

    // Use this for initialization
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static void addPoints(int shipId)
    {
        instance.coinCounter[shipId]++;
        Debug.Log("GM: " + instance.coinCounter[shipId]);
    }

    private void Start()
    {
        placeCoins();
    }

    public void Update()
    {
        for (int i = 0; i < 12; i++)
        {
            scoresText[i].text = "Ship_" +i+": " + instance.coinCounter[i];
        }
           
    }

    static public void SendClientMessage(ClientMessage message)
    {
        instance.ship[message.client].queue.Enqueue(message);
    }

    private void placeCoins()
    {
        //Random rnd = new Random();
        //for (int a = 0; a < 16; a++)
        //{

        //    float x = (float)rnd.Next(-7, 7) + 0.5f;
        //    float y = (float)rnd.Next(-3, 3) + 0.5f;
        //    Instantiate(CoinRef, new Vector3(x, y, 0), Quaternion.identity);
        //}

        List<Tuple<float, float>> usedValue = new List<Tuple<float, float>>();

        for (int a = 0; a < 4; a++)
        {
            Debug.Log("Q1");
            while(true)
            {
                float x = (float)rnd.Next(0, 7) + 0.5f;
                float y = (float)rnd.Next(0, 3) + 0.5f;

                Tuple<float, float> location = new Tuple<float, float>(x, y);
                if(!usedValue.Contains(location))
                {
                    usedValue.Add(location);
                    Instantiate(CoinRef, new Vector3(x, y, 0), Quaternion.identity);
                    break;
                }
            }
        }

        for (int a = 0; a < 4; a++)
        {
            Debug.Log("Q2");
            while (true)
            {
                float x = (float)rnd.Next(0, 7) + 0.5f;
                float y = (float)rnd.Next(-3, 0) + 0.5f;

                Tuple<float, float> location = new Tuple<float, float>(x, y);
                if (!usedValue.Contains(location))
                {
                    usedValue.Add(location);
                    Instantiate(CoinRef, new Vector3(x, y, 0), Quaternion.identity);
                    break;
                }
            }
        }

        for (int a = 0; a < 4; a++)
        {
            Debug.Log("Q3");
            while (true)
            {
                float x = (float)rnd.Next(-7, 0) + 0.5f;
                float y = (float)rnd.Next(-3, 0) + 0.5f;

                Tuple<float, float> location = new Tuple<float, float>(x, y);
                if (!usedValue.Contains(location))
                {
                    usedValue.Add(location);
                    Instantiate(CoinRef, new Vector3(x, y, 0), Quaternion.identity);
                    break;
                }
            }
        }

        for (int a = 0; a < 4; a++)
        {
            Debug.Log("Q4");
            while (true)
            {
                float x = (float)rnd.Next(-7, 0) + 0.5f;
                float y = (float)rnd.Next(0, 3) + 0.5f;

                Tuple<float, float> location = new Tuple<float, float>(x, y);
                if (!usedValue.Contains(location))
                {
                    usedValue.Add(location);
                    Instantiate(CoinRef, new Vector3(x, y, 0), Quaternion.identity);
                    break;
                }
            }
        }
    }
}
