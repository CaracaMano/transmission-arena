﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCounterManager : MonoBehaviour {

    bool isInitialized = false;
    GameManager gameManager;

    public List<Text> Texts;

    void Awake()
    {
    }

	// Use this for initialization
	void Start () {
        gameManager = FindObjectOfType<GameManager>();

        

	}
    void Init(int playersCount)
    {
        int maxPlayers = 4;



        for (int i = 0; i < maxPlayers; i++)
        {
            Text currentText = Texts[i];
            currentText.color = gameManager.colors[i];           

            if (i >= playersCount)
            {
                currentText.gameObject.SetActive(false);
            }
        }

        isInitialized = true;
    }

	// Update is called once per frame
	void Update () {
        int playersCount = gameManager.playersAmount;
        if (!isInitialized &&  playersCount > 0)
        {
            Init(playersCount);
            isInitialized = true;
        }

        for (int i = 0; i < Texts.Count; i++)
        {
            string playerPrefName = "Player" + i;
            Texts[i].text = PlayerPrefs.GetInt(playerPrefName).ToString();    
        }
        
	}

    public void setWinner(int playerIndex)
    {
        playerIndex -= 1;
        string playerPrefName = "Player" + playerIndex;
        int score = PlayerPrefs.GetInt(playerPrefName) + 1;
        PlayerPrefs.SetInt(playerPrefName, score);
    }
}
