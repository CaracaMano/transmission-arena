using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownManager : MonoBehaviour {

	private bool firstAppear = true;
	
	// Use this for initialization
	void Start () {
		
	}

	private void OnEnable() {
		if (firstAppear) {
			firstAppear = false;
			return;
		}
		
		Rigidbody2D body = GetComponent<Rigidbody2D>();

		int rndX = Random.Range(-200, 200);
		int rndy = Random.Range(200, 400);

		body.angularVelocity = 1500;

		body.AddForce(new Vector2(rndX, rndy));
	}

	void OnCollisionEnter2D(Collision2D collision) {
        Player player = collision.collider.GetComponent<Player>();

        if (player != null)
        {
            player.pickCrown();
            //Destroy(this.gameObject);
	        gameObject.SetActive(false);
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
