using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Projectile : MonoBehaviour {

	public Player source;
	public SpriteRenderer sprite;
	
	// Use this for initialization
	void Start () {
		Invoke("AutoDestroy", source.gameConstants.PROJECTILE_DISMISS_TIME_S);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AutoDestroy() {

		this.transform.DOScale(new Vector3(0f, 0f, 0f), 0.15f).OnComplete(() =>{
			Destroy(gameObject);
			source.CanShoot = true; 
		});

	}

	private void OnTriggerEnter2D(Collider2D other) {

		switch (other.tag) {
			case "Projectile":
				AutoDestroy();
				break;
			case "Player":
				other.GetComponent<Player>().GetShot(this);
				break;
			case "Portal":
				break;
			default:
				AutoDestroy();
				break;
		}
		
	}
}
