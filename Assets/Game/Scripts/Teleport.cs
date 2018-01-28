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

        Transform safeAreaTransform = destiny.transform;
        ob.transform.position = safeAreaTransform.position;

        Player player = collider.GetComponent<Player>();
        if (player != null)
        {
            player.audioPool.PlayAudio(player.teleportSound, 1, 0.5f);
        }

    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
