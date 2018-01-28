using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour {

    private const int MINIMUM_PLAYERS = 2;
    
    // Input Types
    private const string GAME_PAD = "GamepadControlled";

    private const string KEYBOARD = "KeyboardControlled";

    public SpawnPointsController SpawnPointsController;
    
    public string sceneName;

    public AudioClip themeMusic;
    public AudioPool audioPool;

    public GameObject PressToStartText;
    
    public GameObject player1Avatar;
    public GameObject player2Avatar;
    public GameObject player3Avatar;
    public GameObject player4Avatar;

    private int playerCount = 0;

    private void Start()
    {
        audioPool.PlayMusic(themeMusic);
        DontDestroyOnLoad(SpawnPointsController);
    }

    private void Update()
    {
        
        PressToStartText.SetActive(playerCount >= MINIMUM_PLAYERS);
        
        if (Input.GetKeyDown(KeyCode.Return) && playerCount >= MINIMUM_PLAYERS)
        {
            SceneManager.LoadScene(sceneName);
        }

        HandleGamepadPlayerEntrance("j1a0", SpawnPointsController.SpawnPoint1, player1Avatar);
        HandleGamepadPlayerEntrance("j2a0", SpawnPointsController.SpawnPoint2, player2Avatar);
        HandleGamepadPlayerEntrance("j3a0", SpawnPointsController.SpawnPoint3, player3Avatar);
        HandleGamepadPlayerEntrance("j4a0", SpawnPointsController.SpawnPoint4, player4Avatar);
        
        HandleKeyboardPlayerEntrance(KeyCode.Semicolon, SpawnPointsController.SpawnPoint3, player3Avatar);
        HandleKeyboardPlayerEntrance(KeyCode.F, SpawnPointsController.SpawnPoint4, player4Avatar);
    }

    private void HandleGamepadPlayerEntrance(string key, GameObject spawnPoint, GameObject avatar) {
        if (Input.GetButtonDown(key)) {
            spawnPoint.tag = GAME_PAD;
            avatar.SetActive(!avatar.activeSelf);
            spawnPoint.SetActive(avatar.activeSelf);
            playerCount += avatar.activeSelf ? 1 : -1;   
        }
    }

    private void HandleKeyboardPlayerEntrance(KeyCode keycode, GameObject spawnPoint, GameObject avatar) {
        if (Input.GetKeyDown(keycode)) {
            spawnPoint.tag = KEYBOARD;
            avatar.SetActive(!avatar.activeSelf);
            spawnPoint.SetActive(avatar.activeSelf);
            playerCount += avatar.activeSelf ? 1 : -1;   
        }
    }
}
