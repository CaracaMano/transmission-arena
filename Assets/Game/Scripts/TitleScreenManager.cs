using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour {

    private const int MINIMUM_PLAYERS = 2;
    
    // Input Types
    public const string GAME_PAD = "GamepadControlled";

    public const string KEYBOARD = "KeyboardControlled";
    
    public string sceneName;

    public AudioClip themeMusic;
    public AudioPool audioPool;

    public GameObject PressToStartText;
    
    public TitleWiggle player1Avatar;
    public TitleWiggle player2Avatar;
    public TitleWiggle player3Avatar;
    public TitleWiggle player4Avatar;

    public Sprite GamepadSprite;
    public Sprite keyboardSprite;

    private int playerCount = 0;

    private void Start()
    {
        audioPool.PlayMusic(themeMusic);
        PlayersManagerSingleton.Instance.Reset();
    }

    private void Update()
    {
        
        PressToStartText.SetActive(playerCount >= MINIMUM_PLAYERS);
        
        if (Input.GetKeyDown(KeyCode.Return) && playerCount >= MINIMUM_PLAYERS)
        {
            SceneManager.LoadScene(sceneName);
        }

        HandleGamepadPlayerEntrance("j1a0", "j1", player1Avatar);
        HandleGamepadPlayerEntrance("j2a0", "j2", player2Avatar);
        HandleGamepadPlayerEntrance("j3a0", "j3", player3Avatar);
        HandleGamepadPlayerEntrance("j4a0", "j4", player4Avatar);
        
        HandleKeyboardPlayerEntrance(KeyCode.Semicolon, "j3", player3Avatar);
        HandleKeyboardPlayerEntrance(KeyCode.F, "j4", player4Avatar);
    }

    private void HandleGamepadPlayerEntrance(string key, string playerPrefix, TitleWiggle avatar) {
        if (Input.GetButtonDown(key)) {
            var avatarObject = avatar.gameObject;
            var hasPlayer = HandlePlayerEntrance(playerPrefix, GAME_PAD, avatar);
            avatarObject.SetActive(hasPlayer);
            playerCount += hasPlayer ? 1 : -1;   
        }
    }

    private void HandleKeyboardPlayerEntrance(KeyCode keycode, string playerPrefix, TitleWiggle avatar) {
        if (Input.GetKeyDown(keycode)) {
            var avatarObject = avatar.gameObject;
            var hasPlayer = HandlePlayerEntrance(playerPrefix, KEYBOARD, avatar);
            avatarObject.SetActive(hasPlayer);
            playerCount += hasPlayer ? 1 : -1;   
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
