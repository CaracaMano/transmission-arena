using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public GameConstants gameConstants;

	[HideInInspector]
	public Jump jump;

    [HideInInspector]
    public Walk walk;
	
	public Rigidbody2D body;
	
	public Skill currentSpecialSkill;

    public Color color;
	
	// Use this for initialization
	void Start () {
		jump = new Jump();
        walk = new Walk();
	}
	
	// Update is called once per frame
	void Update () {
		
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
