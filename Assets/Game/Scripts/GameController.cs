using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	private float GameTimer = 60;
	
	public Dictionary<string, Player> players = new Dictionary<string,Player>();
	public List<string> playersPrefix = new List<string>();

	public WinCondition WinCondition;

	public Text TimerText;
	
	private const string SHOOT_ACTION = "a0";
	private const string JUMP_ACTION = "a1";
	private const string SPECIAL_ACTION = "a5";
	private const string HORIZONTAL_AXIS = "axis1";

    public void addPlayer(string name, Player player)
    {
        players.Add(name, player);
        playersPrefix.Add(name);
    }

	// Use this for initialization
	void Start () {
		WinCondition.ConditionReached = EndGame;
	}

	void EndGame() {
		Debug.Log("End Game! Winner: " + WinCondition.winner.PlayerName);	
	}

	// Update is called once per frame
	void Update () {

		GameTimer -= Time.deltaTime;
		if (GameTimer < 0) {
			WinCondition.CheckCondition();
			if (WinCondition.winner != null) {
				TimerText.text = "Winner: " + WinCondition.winner.PlayerName;
				TimerText.color = WinCondition.winner.playerColor;
			}
			else {
				TimerText.text = "Sudden Death!!!";
			}
		}
		else {
			int minutes = Mathf.RoundToInt(GameTimer / 60f);
			int seconds = Mathf.RoundToInt(GameTimer % 60f);

			TimerText.text = minutes + ":" + seconds;
		}

		foreach (var playerPrefix in playersPrefix) {
            if (!players[playerPrefix].wasStunned)
            {
                if (players[playerPrefix].isNPC == true)
                {
                    HandleButtons(playerPrefix, players[playerPrefix]);
                }

                else
                {
                    HandleControls(playerPrefix, players[playerPrefix]);
                } 
            }
		}
	}
    
    void HandleButtons(string playerPrefix, Player player) {
        if ((Input.GetKey(KeyCode.RightArrow)))
        {
            player.walk.UseSkill(player, 1);
        }
        if ((Input.GetKey(KeyCode.LeftArrow)))
        {
            player.walk.UseSkill(player, -1);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump(player);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.shoot.UseSkill(player);
        }
    }

    void Jump(Player player)
    {
        player.jump.UseSkill(player);
    }
     
	void HandleControls(string playerPrefix, Player player) {
		if (Input.GetButtonDown(playerPrefix + SHOOT_ACTION)) {
			player.shoot.UseSkill(player);
		}

		if (Input.GetButtonDown(playerPrefix + JUMP_ACTION)) {
            Jump(player);
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
