using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifesaver : MonoBehaviour {

	private const float MAX_DISTANCE = 100; 
	
	public List<Transform> ObjectsToSave;

	public void RegisterObject(Transform objecTransform) {
		ObjectsToSave.Add(objecTransform);
	}

	public void UnregisterObject(Transform objectTransform) {
		ObjectsToSave.Remove(objectTransform);
	}

	void Update() {
		foreach (var objecTransform in ObjectsToSave) {
			float distance = (transform.position - objecTransform.position).magnitude;
			if (distance > MAX_DISTANCE) {
				objecTransform.position = transform.position;
			}
		}
	}
}
