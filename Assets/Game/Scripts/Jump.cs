using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : Skill {


    public GameObject groundCheck;
	// Use this for initialization


	// Update is called once per frame
	void Update () {
		
	}

	public override void UseSkill(Player player) {
		base.UseSkill(player);
       if (player.isGrounded)
       {
            player.body.AddForce(new Vector3(0, player.gameConstants.JUMP_FORCE, 0));

            player.GetComponent<Animator>().SetBool("Jumping?", true);

            player.audioPool.PlayAudio(player.jumpSound, 1, 0.5f);
       }
	}

    

}
