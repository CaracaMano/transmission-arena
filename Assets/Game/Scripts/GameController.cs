using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour {

	public Player player1;

	public Dictionary<string, Player> players;
	private List<string> playersPrefix;

	private const string SHOOT_ACTION = "a0";
	private const string JUMP_ACTION = "a1";
	private const string SPECIAL_ACTION = "a5";
	private const string HORIZONTAL_AXIS = "axis1";
	
	// Use this for initialization
	void Start () {
		players = new Dictionary<string, Player>();
		
		players.Add("j1", player1);

		playersPrefix = players.Keys.ToList();
	}
	
	
	
	// Update is called once per frame
	void Update () {
		foreach (var playerPrefix in playersPrefix) {
			HandleControls(playerPrefix, players[playerPrefix]);
		}
	}

	void HandleControls(string playerPrefix, Player player) {
		if (Input.GetButtonDown(playerPrefix + SHOOT_ACTION)) {
			player.shoot.UseSkill(player);
		}

		if (Input.GetButtonDown(playerPrefix + JUMP_ACTION)) {
			player.jump.UseSkill(player);
            player.GetComponent<Animator>().SetBool("Jumping?",true);
        }

		if (Input.GetButtonDown(playerPrefix + SPECIAL_ACTION)) {
			Debug.Log(playerPrefix + " uses special action!");
		}
		
        float horizontalTranslation = Input.GetAxis(playerPrefix + HORIZONTAL_AXIS);

        if (horizontalTranslation != 0)
        {
            player.walk.UseSkill(player, horizontalTranslation);
        }

	}
}
