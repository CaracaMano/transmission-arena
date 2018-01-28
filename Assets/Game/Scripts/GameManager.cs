using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
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

        playersAmount = PlayersManagerSingleton.Instance.players.Keys.Count;
        
        for (int i = 0; i < PlayersManagerSingleton.Instance.players.Keys.Count; i++) {
            
            var playerPrefix = PlayersManagerSingleton.Instance.players.Keys.ToArray()[i];
            
            Transform respawnTransform = respawnGroup.transform.GetChild(i);
            
            if (!respawnTransform.gameObject.activeSelf) {
                continue;
            }
            
            GameObject p = createPlayer(respawnTransform.position);
            Player player = p.GetComponent<Player>();

            player.playerColor = colors[i];

            player.PlayerName = "Player " + (i + 1); 

            gameController.addPlayer(playerPrefix, player);

            respawnTransform.tag = PlayersManagerSingleton.Instance.players[playerPrefix];

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
