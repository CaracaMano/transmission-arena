using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    public Transform destiny;
	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        Transform ob = collider.transform;

        ob.transform.position = destiny.transform.position;
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
