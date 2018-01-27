using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void OnCollisionEnter2D(Collision2D collision) {
        Player player = collision.collider.GetComponent<Player>();

        if (player != null)
        {
            player.pickCrown();
            Destroy(this.gameObject);
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
