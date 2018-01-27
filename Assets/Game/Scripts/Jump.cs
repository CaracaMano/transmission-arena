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
        float radius = 10;
        RaycastHit2D hit =  Physics2D.Raycast(player.transform.localPosition, -Vector2.up, radius);

        return hit.collider != null;
    }

}
