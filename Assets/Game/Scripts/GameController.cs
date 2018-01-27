using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
		}
		
		foreach (var playerPrefix in playersPrefix) {

            /*
            if (players[playerPrefix].isNPC == true)
            {
                HandleButtons(playerPrefix, players[playerPrefix]);
            }
              
            else
             */ 
            {
                if (!players[playerPrefix].wasStunned)
                {
                    HandleControls(playerPrefix, players[playerPrefix]);
                }
            }
		}

		int minutes = Mathf.RoundToInt(GameTimer / 60f);
		int seconds = Mathf.RoundToInt(GameTimer % 60f);

		TimerText.text = minutes + ":" + seconds;
	}

    /*
    void HandleButtons(string playerPrefix, Player player) {
        float horizontalTranslation = Input.GetAxis(playerPrefix + HORIZONTAL_AXIS);
        if(Input.GetKeyDown())
    
    }

     */ 
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

        if (Input.GetKeyDown("space"))
        {
            player.shoot.UseSkill(player);
        }

	}
}
