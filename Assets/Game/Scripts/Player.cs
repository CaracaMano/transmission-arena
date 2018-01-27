using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public GameConstants gameConstants;

    public Transform groundCheck;

	private SpriteRenderer sprite;

	[HideInInspector]
	public Jump jump;

	[HideInInspector] 
	public Shoot shoot;

    [HideInInspector]
    public Walk walk;
	
	public Rigidbody2D body;
	
	public Skill currentSpecialSkill;

    public Color playerColor;

    private Animator anim;

	public Transform ShootDirection;
	public GameObject ProjectileObject;

    private bool previousGrounded = true;
    public bool isGrounded {
        get {
            //previousGrounded = isGrounded;

            int layerMask = LayerMask.NameToLayer("Stage");
            RaycastHit2D hit = Physics2D.Linecast(transform.position, groundCheck.position, 1 << layerMask);

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
	}


	
	// Update is called once per frame
	void Update () {
        anim.SetBool("Running?",Mathf.Abs(body.velocity.x) > 0);

        bool grounded = isGrounded;
        if (previousGrounded == false && grounded == true)
        {
            anim.SetBool("Jumping?", false);
        }
        previousGrounded = grounded;
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
			var thisColor = playerColor;
			playerColor = projectile.source.playerColor;
			sprite.color = projectile.source.playerColor;
			projectile.source.playerColor = thisColor;

			projectile.source.sprite.color = thisColor;
			projectile.AutoDestroy();
		}
	}
}
