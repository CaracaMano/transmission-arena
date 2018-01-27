﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour {

	public string PlayerName;
	
	public GameConstants gameConstants;

    public Transform groundCheck;
	public Transform wallCheck;

	[HideInInspector]
	public SpriteRenderer sprite;

    public bool hasCrown = false;
    
    [HideInInspector]
	public Jump jump;

	[HideInInspector] 
	public Shoot shoot;
	public bool CanShoot = true;

    [HideInInspector]
    public Walk walk;
	
	public Rigidbody2D body;
	
	public Skill currentSpecialSkill;

    public Color playerColor;

    private Animator anim;

	public Transform ShootDirection;
	public GameObject ProjectileObject;

    private GameObject crown;

    public void pickCrown()
    {
        crown.GetComponent<Renderer>().enabled = true;
    }
    public void loseCrown()
    {
        crown.GetComponent<Renderer>().enabled = false;

    }

    private bool previousGrounded = true;
    public bool isGrounded {
        get {
            //previousGrounded = isGrounded;

            int layerMask = LayerMask.NameToLayer("Stage");
            RaycastHit2D hit = Physics2D.Linecast(transform.position, groundCheck.position, 1 << layerMask);

            return hit.collider != null;
        }
    }
	
	// TODO too ineficient change it
	public bool isOnWall {
		get {
			int layerMask = LayerMask.NameToLayer("Stage");
			RaycastHit2D hit = Physics2D.Linecast(transform.position, wallCheck.position, 1 << layerMask);

			if (hit.collider != null) {
				Debug.Log("ON WALL");	
			}
			
			return hit.collider != null;
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
	}


	
	// Update is called once per frame
	void Update () {
        anim.SetBool("Running?", Mathf.Abs(body.velocity.x) > 0);

        bool grounded = isGrounded;
        if (previousGrounded == false && grounded == true)
        {
            anim.SetBool("Jumping?", false);
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
		
		projectile.transform.localScale = new Vector3(transform.localScale.x, currentScale.y, currentScale.z);

		Rigidbody2D projBody = projectile.GetComponent<Rigidbody2D>();
		projBody.velocity = new Vector2(transform.localScale.x * 10, 0);
	}

	public void GetShot(Projectile projectile) {
		if (projectile.source != this) {
			var thisPos = transform.position;
			transform.DOMove(projectile.source.transform.position, gameConstants.SWAP_TIME_S);
			FadeOutIn(transform);
			projectile.source.transform.DOMove(thisPos, gameConstants.SWAP_TIME_S);
			FadeOutIn(projectile.source.transform);
			
			projectile.AutoDestroy();

            loseCrown();
		}
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
}
