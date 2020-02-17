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
    Text timer;
    [SerializeField]
    Ship[] ship;
    bool inCompetition = false;
    bool startedMoves = false;
    float timeLeft;

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

    public static bool InCompetition()
    {
        return instance.inCompetition;
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
        if (Input.GetButtonDown("Jump") && !inCompetition)
        {
            StartCompetition();
        }

        for (int i = 0; i < 12; i++)
        {
            scoresText[i].text = "Ship_" +i+": " + instance.coinCounter[i];
        }

        if (!startedMoves && inCompetition)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                startedMoves = true;
                timer.text = "";
                foreach(Ship s in ship)
                {
                    s.StartMoving();
                }
                StartCoroutine(EndCompetition());
            }
            else
            {
                timer.text = ((int)timeLeft).ToString();

            }
        }
           
    }

    private IEnumerator EndCompetition()
    {
        yield return new WaitForSeconds(30);

        ResetGame();

        startedMoves = false;
        inCompetition = false;
    }

    private void ResetGame()
    {
        foreach (Ship s in ship)
        {
            s.ResetMove();
        }

        for (int i = 0; i < 12; i++)
        {
            instance.coinCounter[i] = 0;
            ship[i].transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        }

        for (int i = 0; i < 6; i++)
        {
            ship[i].transform.position = new Vector3(-6.5f + i, -3.5f, 0f);
        }

        for (int i = 0; i < 6; i++)
        {
            ship[i + 6].transform.position = new Vector3(1.5f + i, -3.5f, 0f);
        }

        // Remove all coins
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject obj in allObjects)
        {
            Destroy(obj);
        }

        placeCoins();
    }

    private void StartCompetition()
    {

        startedMoves = false;
        timeLeft = 30;

        ResetGame();
 
        inCompetition = true;
    }

    static public void SendClientMessage(ClientMessage message)
    {
        instance.ship[message.client].queue.Enqueue(message);
    }

    private void placeCoins()
    {
        List<Tuple<float, float>> usedValue = new List<Tuple<float, float>>();

        for (int a = 0; a < 4; a++)
        {
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
