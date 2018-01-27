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

        Transform safeAreaTransform = destiny.GetChild(0).transform;
        ob.transform.position = safeAreaTransform.position;
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
