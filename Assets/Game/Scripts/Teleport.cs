using UnityEngine;

public class Teleport : MonoBehaviour {

	public bool IsVertical;
	
    public Transform destiny;
	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        Transform ob = collider.transform;

        Transform safeAreaTransform = destiny.transform;

	    if (IsVertical) {
		    var position = new Vector3(ob.transform.position.x, safeAreaTransform.position.y, ob.transform.position.z);
		    ob.transform.position = position;    
	    }
	    else {
		    var position = new Vector3(safeAreaTransform.position.x, ob.transform.position.y, ob.transform.position.z);
		    ob.transform.position = position;
	    }
        

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
