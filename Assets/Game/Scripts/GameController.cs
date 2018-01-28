using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public float GameTimer;
    public float fastGameTimer;

    float audioVolume = 0.5f;

    private float fastMusicGameTime;

	private bool hurryUp = false;
	private bool canReload = false;
	private float reloadTimer = 5;
	public Text ReloadText;

	private bool gameFinished = false;
	public List<ParticleSystem> PartyParticleSystems;
	
	public Dictionary<string, Player> players = new Dictionary<string,Player>();
	public List<string> playersPrefix = new List<string>();

	public WinCondition WinCondition;

	public Text TimerText;
    public Text MessageText;
    public Text getTheCrownText;
	
	private const string SHOOT_ACTION = "a0";
	private const string JUMP_ACTION = "a1";
	private const string SPECIAL_ACTION = "a5";
	private const string HORIZONTAL_AXIS = "axis1";


    public AudioClip slowMusic;
    public AudioClip fastMusic;
    public AudioClip winMusic;
    public AudioClip suddenDeathSound;

    private bool fastMusicStarted = false;

    AudioPool audioPool;
    private bool suddenDeathSoundPlayed = false;
    private bool canPlay = false;

    public PlayerCounterManager playerCounterManager;
    private bool winMessageShown = false;

    private void playMusic(bool isFast) 
    {
        audioPool = GetComponent<AudioPool>();

        if (isFast)
        {
            audioPool.PlayMusic(fastMusic,0.6f);
            fastMusicStarted = true;
        }
        else
        {
            audioPool.PlayMusic(slowMusic, 0.6f);
        }
    
    }

    public void addPlayer(string name, Player player)
    {
        players.Add(name, player);
        playersPrefix.Add(name);
    }

	// Use this for initialization
	void Start () {
        audioPool = GetComponent<AudioPool>();

        fastMusicGameTime = fastGameTimer;
		WinCondition.ConditionReached = EndGame;
        playMusic(false);

        gameFinished = false;

        StartGame();
	}

    void StartGame() {

        GameObject crown = GameObject.FindGameObjectWithTag("Crown") as GameObject;

        Vector3 camPosition = Camera.main.gameObject.transform.position;

        Camera.main.gameObject.transform.position = new Vector3(
            crown.transform.position.x,
            crown.transform.position.y + 1,
            crown.transform.position.z - 5
        );

		getTheCrownText.DOFade (0f, 0f);
		getTheCrownText.rectTransform.DOScale (new Vector3 (10f, 10f, 10f), 0f);
		DOTween.Sequence().Append(getTheCrownText.DOFade(1f, 0.05f)).Append(getTheCrownText.rectTransform.DOScale(new Vector3(1,1,1), 0.3f)).SetDelay(0.5f).SetEase(Ease.InQuad);


        Tween cameraAnimationIn = Camera.main.gameObject.transform.DOMove(new Vector3(
              camPosition.x,
              camPosition.y,
              camPosition.z
          ), 2).SetDelay(1.5f).Play();

        cameraAnimationIn.onComplete += delegate
        {
            canPlay = true;

            getTheCrownText.DOFade(0f,0.1f).OnComplete(() =>
            {
                getTheCrownText.enabled = false;
            });
        };
    
    }

	void EndGame() {
		if (!gameFinished) {
            audioPool.PlayMusic(winMusic);
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
    void Update()
    {
        if (canPlay)
        {
            GameTimer -= Time.deltaTime;
            if (GameTimer <= fastMusicGameTime && !fastMusicStarted)
            {
                playMusic(true);
            }
            else if (GameTimer < 0)
            {
                WinCondition.CheckCondition();
                if (WinCondition.winner != null)
                {
                   TimerText.gameObject.SetActive(false);

                   MessageText.text = "Winner: " + WinCondition.winner.PlayerName;
                   MessageText.color = WinCondition.winner.playerColor;
                   MessageText.gameObject.SetActive(true);


                   if (!winMessageShown)
                   {
                       winMessageShown = true;
                       int playerIndex = int.Parse(WinCondition.winner.PlayerName.Replace("Player", ""));
                       playerCounterManager.setWinner(playerIndex); 
                   }

                }
                else
                {

                    WinCondition.CheckCondition();

                    TimerText.gameObject.SetActive(false);


                    MessageText.text = "Sudden Death!!!";
                    MessageText.color = Color.red;

                    MessageText.gameObject.SetActive(true);

                    if (!suddenDeathSoundPlayed)
                    {
                        audioPool.PlayAudio(suddenDeathSound);
                        suddenDeathSoundPlayed = true;
                    }
                }
            }
            else
            {
                int minutes = Mathf.FloorToInt(GameTimer / 60f);
                int seconds = Mathf.FloorToInt(GameTimer % 60f);

                if (GameTimer > 10)
                {
                    TimerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
                }
                else
                {
                    if (!hurryUp)
                    {
                        DOTween.Sequence().Append(
                            TimerText.transform.DOScale(1.2f, 0.5f)
                        ).Append(
                            TimerText.transform.DOScale(1f, 0.5f)
                        ).SetLoops(10);
                        hurryUp = true;
                    }
                    TimerText.text = seconds.ToString("00");
                    TimerText.color = new Color(1f, 0.26f, 0.27f);
                }
            }

            if (WinCondition.winner == null)
            {
                foreach (var playerPrefix in playersPrefix)
                {
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
            else
            {
                reloadTimer -= Time.deltaTime;

                if (reloadTimer < 0)
                {
                    if (!ReloadText.gameObject.activeSelf)
                    {
                        ReloadText.gameObject.SetActive(true);
                    }
                    canReload = true;
                }

                if (Input.anyKeyDown && canReload)
                {
                    SceneManager.LoadScene("Arena01");
                }
            } 
        }
    }
	
    void HandleButtons(string playerPrefix, Player player) {
        switch (playerPrefix) {
                case "j3":
                    if ((Input.GetKey(KeyCode.RightArrow)))
                    {
                        player.walk.UseSkill(player, 1);
                    }
                    else if ((Input.GetKey(KeyCode.LeftArrow)))
                    {
                        player.walk.UseSkill(player, -1);
                    }

                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        Jump(player);
                    }

                    if (Input.GetKeyDown(KeyCode.Semicolon))
                    {
                        player.shoot.UseSkill(player);
                    }
                    break;
                case "j4":
                    if ((Input.GetKey(KeyCode.D)))
                    {
                        player.walk.UseSkill(player, 1);
                    }
                    else if ((Input.GetKey(KeyCode.A)))
                    {
                        player.walk.UseSkill(player, -1);
                    }

                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        Jump(player);
                    }

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        player.shoot.UseSkill(player);
                    }
                    break;
                default:
                    break;
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

        float horizontalTranslation = Input.GetAxis(playerPrefix + HORIZONTAL_AXIS);

        if (horizontalTranslation != 0)
        {
            player.walk.UseSkill(player, horizontalTranslation);
        }
	}
}
