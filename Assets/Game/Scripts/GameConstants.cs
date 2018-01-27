using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "Configs/GameConstants", order = 1)]
public class GameConstants : ScriptableObject {

    public const float GAME_TIME_S = 60;
    
    [SerializeField]
    public float JUMP_FORCE;
    
    [SerializeField]
    public float WALK_SPEED;
    
    [SerializeField]
    public float WALK_ACCELERATION;
    
    [SerializeField]
    public float ACCELERATION_ON_AIR;

    [SerializeField]
    public float PROJECTILE_DISMISS_TIME_S;

    [SerializeField] 
    public float KNOCKBACK_SPEED;
    
    [SerializeField] 
    public float SHOOT_COOLDOWN_TIME_S;

    [SerializeField] 
    public float SPECIAL_SKILL_TIME_S;

    [SerializeField] public float SWAP_TIME_S;

    [SerializeField] public Color GRAY_COLOR;
}
