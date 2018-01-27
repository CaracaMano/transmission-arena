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
        if (isGround(player))
        {
            player.body.AddForce(new Vector3(0, player.gameConstants.JUMP_FORCE, 0));
        }
	}

    private bool isGround(Player player)
    {
       float radius = 1;

       Transform playerTransform = player.transform;
       RaycastHit2D hit = Physics2D.Raycast(playerTransform.position, -Vector2.up, 0.1);

       return hit.collider != null;

       return Physics2D.Raycast(playerTransform.localPosition, -playerTransform.up, radius);
    }

}
