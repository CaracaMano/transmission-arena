﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour {

    private const int MINIMUM_PLAYERS = 2;

    public List<AudioClip> enterAudios;
    
    // Input Types
    public const string GAME_PAD = "GamepadControlled";

    public const string KEYBOARD = "KeyboardControlled";

    public string scene2Players;
    public string scene3Players;
    public string scene4Players;

    public AudioClip themeMusic;
    public AudioPool audioPool;

    public GameObject PressToStartText;
    
    public TitleWiggle player1Avatar;
    public TitleWiggle player2Avatar;
    public TitleWiggle player3Avatar;
    public TitleWiggle player4Avatar;
    
    public GameObject player1Enter;
    public GameObject player2Enter;
    public GameObject player3Enter;
    public GameObject player4Enter;

    public Sprite GamepadSprite;
    public Sprite keyboardSprite;

    private int playerCount = 0;

    private void Start()
    {
        audioPool.PlayMusic(themeMusic);
        PlayersManagerSingleton.Instance.Reset();
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < 4; i++)
        {
            string playerPrefName = "Player" + i;
            PlayerPrefs.SetInt(playerPrefName, 0);
        }
    }

    private void Update()
    {
        
        PressToStartText.SetActive(playerCount >= MINIMUM_PLAYERS);
        
        if (Input.GetKeyDown(KeyCode.Return) && playerCount >= MINIMUM_PLAYERS)
        {
            if (playerCount == 2)
            {
                SceneManager.LoadScene(scene2Players);
            }

            if (playerCount == 3)
            {
                SceneManager.LoadScene(scene3Players);
            }

            else
            {
                SceneManager.LoadScene(scene4Players);
            }
        }

        HandleGamepadPlayerEntrance("j1a0", "j1", player1Avatar, player1Enter);
        HandleGamepadPlayerEntrance("j2a0", "j2", player2Avatar, player2Enter);
        HandleGamepadPlayerEntrance("j3a0", "j3", player3Avatar, player3Enter);
        HandleGamepadPlayerEntrance("j4a0", "j4", player4Avatar, player4Enter);
        
        HandleKeyboardPlayerEntrance(KeyCode.Semicolon, "j3", player3Avatar, player3Enter);
        HandleKeyboardPlayerEntrance(KeyCode.F, "j4", player4Avatar, player4Enter);
    }

    private void EnterPlayerAudio(bool enter) {
        if (enter) {
            audioPool.PlayAudio(enterAudios[UnityEngine.Random.Range(0, enterAudios.Count)]);
        }
    }

    private void HandleGamepadPlayerEntrance(string key, string playerPrefix, TitleWiggle avatar, GameObject enter) {
        if (Input.GetButtonDown(key)) {
            var avatarObject = avatar.gameObject;
            var hasPlayer = HandlePlayerEntrance(playerPrefix, GAME_PAD, avatar);
            avatarObject.SetActive(hasPlayer);
            enter.SetActive(!hasPlayer);
            playerCount += hasPlayer ? 1 : -1;
            EnterPlayerAudio(hasPlayer);
            
        }
    }

    private void HandleKeyboardPlayerEntrance(KeyCode keycode, string playerPrefix, TitleWiggle avatar, GameObject enter) {
        if (Input.GetKeyDown(keycode)) {
            var avatarObject = avatar.gameObject;
            var hasPlayer = HandlePlayerEntrance(playerPrefix, KEYBOARD, avatar);
            avatarObject.SetActive(hasPlayer);
            enter.SetActive(!hasPlayer);
            playerCount += hasPlayer ? 1 : -1;
            EnterPlayerAudio(hasPlayer);
        }
    }

    private bool HandlePlayerEntrance(string playerPrefix, string controller, TitleWiggle avatar) {
        var players = PlayersManagerSingleton.Instance.players;

        if (controller == GAME_PAD) {
            avatar.controllerSprite.sprite = GamepadSprite;
        }
        else {
            avatar.controllerSprite.sprite = keyboardSprite;
        }

        if (players.ContainsKey(playerPrefix)) {
            var currentType = players[playerPrefix];
            if (currentType == controller) {
                players.Remove(playerPrefix);
                return false;
            }
            else {
                players[playerPrefix] = controller;
                return true;
            }
        }
        else {
            players.Add(playerPrefix, controller);
            return true;
        }
    }
}
