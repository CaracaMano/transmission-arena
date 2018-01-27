using System;
using UnityEngine;
using UnityEngine.Events;

public class WinCondition : MonoBehaviour {
	
	public Action ConditionReached;
	public Player winner;
	public GameController GameController;
	
	// Use this for initialization
	void Start () {
		
	}

	public virtual bool CheckCondition() {
		throw new Exception("Must implement");
	}

	public void ReachCondition() {
		if (ConditionReached != null) {
			ConditionReached();	
		}
	}
}
