using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "Configs/GameConstants", order = 1)]
public class GameConstants : ScriptableObject {

    [SerializeField]
    public float JUMP_FORCE;
    
    [SerializeField]
    public float WALK_SPEED;
    
}
