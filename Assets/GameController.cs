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

		var list = new List<string>();
		list.Add("j1a0");
		list.Add("j1a1");
		list.Add("j1a2");
		list.Add("j1a3");
		list.Add("j1a4");
		list.Add("j1a5");
		list.Add("j1a6");
		list.Add("j1a7");
		list.Add("j1a8");
		list.Add("j1a9");
		list.Add("j1a10");
		list.Add("j1a11");
		

		
		foreach (var element in list) {
			if (Input.GetButtonDown(element)) {
				//player1.jump.UseSkill(player1);
				Debug.Log(element);
			}	
		}

		if (Input.GetButtonDown("Submit")) {
			Debug.Log("submit");
		}
		
		
	}
}
