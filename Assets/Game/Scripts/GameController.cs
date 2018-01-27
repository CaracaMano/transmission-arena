using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public float GameTimer = 60;

    private float fastMusicGameTime;

	private bool gameFinished;
	public List<ParticleSystem> PartyParticleSystems;
	
	public Dictionary<string, Player> players = new Dictionary<string,Player>();
	public List<string> playersPrefix = new List<string>();

	public WinCondition WinCondition;

	public Text TimerText;
	
	private const string SHOOT_ACTION = "a0";
	private const string JUMP_ACTION = "a1";
	private const string SPECIAL_ACTION = "a5";
	private const string HORIZONTAL_AXIS = "axis1";


    public AudioClip slowMusic;
    public AudioClip fastMusic;

    private bool fastMusicStarted = false;

    private void playMusic(bool isFast) 
    {
        AudioPool audioPool = GetComponent<AudioPool>();
        if (isFast)
        {
            audioPool.PlayMusic(fastMusic);
            fastMusicStarted = true;
        }
        else
        {
            audioPool.PlayMusic(slowMusic);
        }
    
    }

    public void addPlayer(string name, Player player)
    {
        players.Add(name, player);
        playersPrefix.Add(name);
    }

	// Use this for initialization
	void Start () {

        fastMusicGameTime = GameTimer / 10;
		WinCondition.ConditionReached = EndGame;
        playMusic(false);
	}

	void EndGame() {
		if (!gameFinished) {
			Invoke("WinAnimationRun", 2);
		}
		
		gameFinished = true;
		Debug.Log("End Game! Winner: " + WinCondition.winner.PlayerName);	
	}
	
	void WinAnimationRun() {
		var winnerTrans = WinCondition.winner.transform;
		
		WinCondition.winner.anim.SetBool("Dancing?", true);
		WinCondition.winner.FlipPlayerXTween ();
	
		Tween cameraAnimation = Camera.main.gameObject.transform.DOMove(new Vector3(
			winnerTrans.position.x,
			winnerTrans.position.y + 1,
			winnerTrans.position.z - 5
		), 1).Play();
	
		cameraAnimation.onComplete += delegate {
		foreach (var particleSystem in PartyParticleSystems) {
			particleSystem.Play();
			}
		};
	}

	// Update is called once per frame
	void Update () {
		GameTimer -= Time.deltaTime;
        if (GameTimer <= fastMusicGameTime && !fastMusicStarted)
        {
            playMusic(true);
        }
		else if (GameTimer < 0) {
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
			int minutes = Mathf.FloorToInt(GameTimer / 60f);
			int seconds = Mathf.FloorToInt(GameTimer % 60f);

			TimerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
		}

		if (WinCondition.winner == null) {
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
		else {
			if (Input.GetButtonDown("Submit")) {
				SceneManager.LoadScene("JunScene");	
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
