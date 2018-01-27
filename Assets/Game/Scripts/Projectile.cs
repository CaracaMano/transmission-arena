using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public Player source;
	
	// Use this for initialization
	void Start () {
		Invoke("AutoDestroy", 3);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void AutoDestroy() {
		Destroy(gameObject);
	}
}
