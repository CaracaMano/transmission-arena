using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : Skill {

    private float currentDirection = 1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void UseSkill(Player player, float amount) {
		base.UseSkill(player);

        player.body.velocity = new Vector2(amount * player.gameConstants.WALK_SPEED, player.body.velocity.y);

        


        float direction = 1;
        if(amount < 0)
            direction = -1;

        if (currentDirection != direction)
        {
            Vector3 currentScale = player.transform.localScale;
            player.transform.localScale = new Vector3( -1 * currentScale.x, currentScale.y, currentScale.z);
            currentDirection = direction;
        }
	}
}
