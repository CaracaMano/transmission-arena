using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Skill {
    

    public Shoot()
    {
        Init();
    }


    public override void UseSkill(Player player) {
        if (player.CanShoot) {
            player.Shoot();
            player.CanShoot = false;
            player.GetComponent<Animator>().SetTrigger("MakeItAttack");
            player.audioPool.PlayAudio(player.shootSound, 1, 0.5f);
        }
        
    }
    
}
