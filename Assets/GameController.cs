using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public Player player1;
	
	// Use this for initialization
	void Start () {
		
	}
	
	
	
	// Update is called once per frame
	void Update () {




        float horizontalTranslation = Input.GetAxis("Horizontal");

        if (horizontalTranslation != 0)
        {
            player1.walk.UseSkill(player1, horizontalTranslation);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            player1.jump.UseSkill(player1);
        }


	}
}
