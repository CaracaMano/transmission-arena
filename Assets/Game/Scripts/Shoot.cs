using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Skill {
    
    public override void UseSkill(Player player) {
        if (player.CanShoot) {
            player.Shoot();
            player.CanShoot = false;
            player.GetComponent<Animator>().SetTrigger("MakeItAttack");
        }
        
    }
    
}
