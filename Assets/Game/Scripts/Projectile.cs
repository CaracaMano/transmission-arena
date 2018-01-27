using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public Player source;
	public SpriteRenderer sprite;
	
	// Use this for initialization
	void Start () {
		Invoke("AutoDestroy", 3);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AutoDestroy() {
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D other) {

		switch (other.tag) {
			case "Projectile":
				AutoDestroy();
				break;
			case "Player":
				other.GetComponent<Player>().GetShot(this);
				break;
			default:
				break;
		}
		
	}
}
