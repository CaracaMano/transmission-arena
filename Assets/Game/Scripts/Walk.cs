using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Walk : Skill {

    private float currentDirection = 1;

	public override void UseSkill(Player player, float amount) {
		base.UseSkill(player);

		if (Mathf.Abs(player.body.velocity.x) < player.gameConstants.WALK_SPEED && !player.isOnWall) {
			player.body.AddForce(new Vector2(amount * player.gameConstants.WALK_ACCELERATION, 0));	
		}

		if (!player.isGrounded && !player.isOnWall) {
			player.body.AddForce(new Vector2(amount * player.gameConstants.ACCELERATION_ON_AIR, 0));
		}

		float direction = 1;
        if(amount < 0)
            direction = -1;

        if (currentDirection != direction)
        {
            Vector3 currentScale = player.transform.localScale;
            //player.transform.localScale = new Vector3( -1 * currentScale.x, currentScale.y, currentScale.z);
	        player.transform.DOScaleX(-1 * Mathf.Sign(currentScale.x), 0.2f);
            currentDirection = direction;
        }
	}
}
