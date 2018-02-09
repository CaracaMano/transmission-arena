using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour {

    private const int MINIMUM_PLAYERS = 2;

    public List<AudioClip> enterAudios;
    
    // Input Types
    public const string GAME_PAD = "GamepadControlled";

    public const string KEYBOARD = "KeyboardControlled";

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

    private bool IsAnyButtonDown(string playerPrefix) {

        for (int i = 0; i <= 5; i++) {
            if (Input.GetButtonDown(playerPrefix + "a" + i)) {
                return true;
            }
        }

        return false;
    }
    
    private void Update()
    {
        
        //PressToStartText.SetActive(playerCount >= MINIMUM_PLAYERS);

        if (playerCount >= MINIMUM_PLAYERS) {
            PressToStartText.GetComponent<Text>().text = "Press enter to start";
        }
        else {
            PressToStartText.GetComponent<Text>().text = "Waiting for " + (MINIMUM_PLAYERS - playerCount) + " more players";
        }
        
        if (Input.GetKeyDown(KeyCode.Return) && playerCount >= MINIMUM_PLAYERS)
        {
            if (playerCount == MINIMUM_PLAYERS) {
                SceneManager.LoadScene("Arena_1x1");
            }
            else {
                SceneManager.LoadScene("Arena01");
            }
        }

        HandleGamepadPlayerEntrance("j1", player1Avatar, player1Enter);
        HandleGamepadPlayerEntrance("j2", player2Avatar, player2Enter);
        HandleGamepadPlayerEntrance("j3", player3Avatar, player3Enter);
        HandleGamepadPlayerEntrance("j4", player4Avatar, player4Enter);
        
        HandleKeyboardPlayerEntrance(KeyCode.M, "j3", player3Avatar, player3Enter);
        HandleKeyboardPlayerEntrance(KeyCode.F, "j4", player4Avatar, player4Enter);
    }

    private void EnterPlayerAudio(bool enter) {
        if (enter) {
            audioPool.PlayAudio(enterAudios[UnityEngine.Random.Range(0, enterAudios.Count)]);
        }
    }

    private void HandleGamepadPlayerEntrance(string playerPrefix, TitleWiggle avatar, GameObject enter) {
        if (IsAnyButtonDown(playerPrefix)) {
            var avatarObject = avatar.gameObject;
            var hasPlayer = HandlePlayerEntrance(playerPrefix, GAME_PAD, avatar);
            avatarObject.SetActive(hasPlayer);
            enter.SetActive(!hasPlayer);
            playerCount = PlayersManagerSingleton.Instance.players.Count;
            EnterPlayerAudio(hasPlayer);
            
        }
    }

    private void HandleKeyboardPlayerEntrance(KeyCode keycode, string playerPrefix, TitleWiggle avatar, GameObject enter) {
        if (Input.GetKeyDown(keycode)) {
            var avatarObject = avatar.gameObject;
            var hasPlayer = HandlePlayerEntrance(playerPrefix, KEYBOARD, avatar);
            avatarObject.SetActive(hasPlayer);
            enter.SetActive(!hasPlayer);
            playerCount = PlayersManagerSingleton.Instance.players.Count;
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
            var currentPlayer= players[playerPrefix];
            if (currentPlayer.controllerType == controller) {
                players.Remove(playerPrefix);
                return false;
            }
            else {
                var playerSettings = new PlayersManagerSingleton.PlayerSettings();
                playerSettings.controllerType = controller;
                playerSettings.playerColor = avatar.controllerSprite.color;
                players[playerPrefix] = playerSettings;
                return true;
            }
        }
        else {
            var playerSettings = new PlayersManagerSingleton.PlayerSettings();
            playerSettings.controllerType = controller;
            playerSettings.playerColor = avatar.controllerSprite.color;
            players.Add(playerPrefix, playerSettings);
            return true;
        }
    }
}
