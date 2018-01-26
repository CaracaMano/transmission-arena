using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : Skill {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void UseSkill(Player player) {
		base.UseSkill(player);
		player.body.AddForce(new Vector3(0, player.gameConstants.JUMP_FORCE, 0));
	}
}
