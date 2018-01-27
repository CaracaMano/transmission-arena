using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameController gameController;
    private Transform playerGroupTransform;

    public GameObject playerPrefab;

    public List<Color> colors;

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

        for (int i = 0; i < respawnGroup.transform.childCount; i++)
        {
            Transform respawnTransform = respawnGroup.transform.GetChild(i);
            GameObject p = createPlayer(respawnTransform.position);
            Player player = p.GetComponent<Player>();

            player.playerColor = colors[i];

            gameController.addPlayer("j" + (i + 1), player);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
