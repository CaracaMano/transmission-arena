using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public GameConstants gameConstants;

    public Transform groundCheck;

	[HideInInspector]
	public Jump jump;

    [HideInInspector]
    public Walk walk;
	
	public Rigidbody2D body;
	
	public Skill currentSpecialSkill;

    public Color playerColor;

    private Animator anim;

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
        anim = GetComponent<Animator>();

        GetComponent<SpriteRenderer>().color = playerColor;
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
}
