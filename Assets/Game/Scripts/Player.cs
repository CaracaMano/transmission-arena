using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public GameConstants gameConstants;

	[HideInInspector]
	public Jump jump;
	
	public Rigidbody2D body;
	
	public Skill currentSpecialSkill;
	
	// Use this for initialization
	void Start () {
		
		jump = new Jump();
		
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
