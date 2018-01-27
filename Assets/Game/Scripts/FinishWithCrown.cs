using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishWithCrown : WinCondition {

	// Use this for initialization
	void Start () {
		
	}

	public override bool CheckCondition() {
		foreach (var playerPrefix in GameController.playersPrefix) {
			if (GameController.players[playerPrefix].hasCrown) {
				winner = GameController.players[playerPrefix];
				ReachCondition();
				return true;
			}
		}

		return false;
	}
}
