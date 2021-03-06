﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour {

	public string PlayerName;
	
	public GameConstants gameConstants;

	public GameObject Crown;

    public bool isNPC = false;

    public Transform groundCheck;
	public Transform groundCheck2;
	public Transform wallCheck;
	public Transform wallCheck2;

	[HideInInspector]
	public SpriteRenderer sprite;

    public bool hasCrown = false;
    
    [HideInInspector]
	public Jump jump;

	[HideInInspector] 
	public Shoot shoot;
	public bool CanShoot = true;

    [HideInInspector] 
    public bool wasStunned = false;
    public float stunnedTimeInSeconds = 1;

    [HideInInspector]
    public Walk walk;
	
	public Rigidbody2D body;
	
	public Skill currentSpecialSkill;

    public Color playerColor;

	[HideInInspector]
    public Animator anim;

	public Transform ShootDirection;
	public GameObject ProjectileObject;

    private GameObject crown;

    public GameObject crownPrefab;

    public Transform crownHeadTransform;

    public AudioPool audioPool;

    public AudioClip jumpSound;
    public AudioClip shootSound;
    public AudioClip StunnedSound;
    public AudioClip changeSound;
    public AudioClip getCrownSound;
    public AudioClip teleportSound;
	
    public void pickCrown() {
	    Crown.GetComponent<CrownManager>().wasCaught = true;
        crown.GetComponent<Renderer>().enabled = true;
        audioPool.PlayAudio(getCrownSound, 1, 0.5f);
        hasCrown = true;
    }
    public void loseCrown()
    {
        if (hasCrown)
        {
	        
            hasCrown = false;
            crown.GetComponent<Renderer>().enabled = false;
	        
	        Crown.SetActive(true);

	        Crown.transform.position = crownHeadTransform.position;

	        Crown.GetComponent<CrownManager>().wasCaught = false;
        }
    }


	

    private bool previousGrounded = true;
    public bool isGrounded {
        get {
            //previousGrounded = isGrounded;

            int layerMask = LayerMask.NameToLayer("Stage");
            RaycastHit2D hit = Physics2D.Linecast(transform.position, groundCheck.position, 1 << layerMask);
	        RaycastHit2D hit2 = Physics2D.Linecast(transform.position, groundCheck2.position, 1 << layerMask);

            return hit.collider != null || hit2.collider != null;
        }
    }
	
	// TODO too ineficient change it
	public bool isOnWall {
		get {
			int layerMask = LayerMask.NameToLayer("Stage");
			RaycastHit2D hit = Physics2D.Linecast(transform.position, wallCheck.position, 1 << layerMask);
			RaycastHit2D hit2 = Physics2D.Linecast(transform.position, wallCheck2.position, 1 << layerMask);
			
			return hit.collider != null || hit2.collider != null;
		}
	}
	
	// Use this for initialization
	void Start () {
		jump = new Jump();
        walk = new Walk();
		shoot = new Shoot();

        anim = GetComponent<Animator>();

		sprite = GetComponent<SpriteRenderer>();
		
        sprite.color = playerColor;

        crown = this.transform.Find("crown").gameObject;
        
        audioPool = FindObjectOfType<AudioPool>();
	}

	
	// Update is called once per frame
	void Update () {
		
		anim.SetBool("Running?", Mathf.Abs(body.velocity.x) > 0);

        bool grounded = isGrounded;
        if (previousGrounded == false && grounded == true)
        {
            anim.SetBool("Jumping?", false);
			this.MakeItIdle ();
        }
        previousGrounded = grounded;

		if (Mathf.Abs(body.velocity.x) > gameConstants.WALK_SPEED) {
			var clampVelocity = new Vector2(transform.localScale.x * gameConstants.WALK_SPEED, body.velocity.y);
			body.velocity = clampVelocity;
		}
	}

	public void LearnSkill(Skill skill) {
		if (currentSpecialSkill == null) {
			currentSpecialSkill = skill;
		}
	}

	public void ForgetSkill(Skill skill) {
		currentSpecialSkill = null;
	}

	public void Shoot() {
		GameObject projectile = Instantiate(ProjectileObject);

		Projectile projectileScript = projectile.GetComponent<Projectile>(); 
		
		projectileScript.source = this;
		projectileScript.sprite.color = playerColor;

		projectile.transform.position = ShootDirection.position;

		Vector3 currentScale = projectile.transform.localScale;
		
		projectile.transform.localScale = new Vector3(Mathf.Sign(transform.localScale.x), currentScale.y, currentScale.z);

		Rigidbody2D projBody = projectile.GetComponent<Rigidbody2D>();
		projBody.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * 10, 0);
	}

	public void GetShot(Projectile projectile) {
		if (projectile.source != this) {

            loseCrown();

            var thisPos = transform.position;
			transform.DOMove(projectile.source.transform.position, gameConstants.SWAP_TIME_S);
			FadeOutIn(transform);
			projectile.source.transform.DOMove(thisPos, gameConstants.SWAP_TIME_S);
			FadeOutIn(projectile.source.transform);
			
			projectile.AutoDestroy();

            StartCoroutine("getStunned");
		}
	}

    IEnumerator getStunned() {
        this.wasStunned = true;

        this.anim.SetBool("Stunned?", true);

        audioPool.PlayAudio(this.StunnedSound, 1, 0.5f);

        yield return new WaitForSeconds(stunnedTimeInSeconds);
        this.wasStunned = false;
        this.anim.SetBool("Stunned?", false);
        this.MakeItIdle();
    }

	private void FadeOutIn(Transform transform) {
		Tween sequence = DOTween.Sequence().Append(
			transform.DOScaleY(0.1f, gameConstants.SWAP_TIME_S / 2f)
		).Append(
			transform.DOScaleY(1, gameConstants.SWAP_TIME_S / 2f)
		);
	}

	public void MakeItIdle(){
		anim.SetTrigger ("MakeItIdle");
	}

	public void FlipPlayerXTween(){

		float time = 0.6f;


		DOTween.Sequence ().Append (
			transform.DOScaleX (Mathf.Sign(-transform.localScale.x), time).SetEase(Ease.OutExpo)
		).Append(
			transform.DOScaleX (Mathf.Sign(transform.localScale.x), time).SetEase(Ease.OutExpo)
		).SetLoops(-1);

	}
}
