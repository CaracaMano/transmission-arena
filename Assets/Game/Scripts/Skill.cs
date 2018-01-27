using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour {

	public const int JUMP_FORCE = 10;


    protected AudioPool audioPool;



    protected void Init()
    {
        audioPool = FindObjectOfType<AudioPool>();
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void UseSkill(Player player) {
	}

    public virtual void UseSkill(Player player, float amount) { 
    }
}
