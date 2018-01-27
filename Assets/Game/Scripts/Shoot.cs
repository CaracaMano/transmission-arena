using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Skill {
    
    public override void UseSkill(Player player) {
        player.Shoot();
    }
    
}
