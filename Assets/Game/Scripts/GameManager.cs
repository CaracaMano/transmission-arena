﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {
    
    private Lifesaver LifeSaver;
    
    public GameController gameController;
    private Transform playerGroupTransform;

    public GameObject playerPrefab;

    public List<Color> colors;

    public int playersAmount;

    public Skill levelSkill;

    private GameObject createPlayer(Vector2 position) { 
        GameObject prefab = Instantiate(playerPrefab, playerGroupTransform);
		prefab.transform.position = position;

        return prefab;
    }
    // Use this for initialization
    void Start()
    {

        playerGroupTransform = GameObject.FindGameObjectWithTag("players").transform;

        GameObject respawnGroup = GameObject.FindGameObjectWithTag("Respawn");
        
        LifeSaver = GameObject.FindGameObjectWithTag("LifeSaver").GetComponent<Lifesaver>();

        playersAmount = PlayersManagerSingleton.Instance.players.Keys.Count;

        var crownObject = GameObject.FindGameObjectWithTag("Crown");
        
        for (int i = 0; i < PlayersManagerSingleton.Instance.players.Keys.Count; i++) {
            
            var playerPrefix = PlayersManagerSingleton.Instance.players.Keys.ToArray()[i];
            
            Transform respawnTransform = respawnGroup.transform.GetChild(i);
            
            if (!respawnTransform.gameObject.activeSelf) {
                continue;
            }
            
            GameObject p = createPlayer(respawnTransform.position);
            Player player = p.GetComponent<Player>();
            
            LifeSaver.RegisterObject(player.transform);

            player.playerColor = colors[i];

            player.Crown = crownObject;

            player.PlayerName = "Player " + (i + 1); 

            gameController.addPlayer(playerPrefix, player);

            var playerSettings = PlayersManagerSingleton.Instance.players[playerPrefix];
            respawnTransform.tag = playerSettings.controllerType;
            player.playerColor = playerSettings.playerColor;

            if (respawnTransform.tag.Contains("KeyboardControlled"))
            {
                player.isNPC = true;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
