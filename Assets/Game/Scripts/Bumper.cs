using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour {

	public Rigidbody2D PlayerBody;

	private void OnCollisionEnter2D(Collision2D other) {
		PlayerBody.velocity = Vector2.zero;
	}
}
