using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleIdle : MonoBehaviour {

	// Use this for initialization
	void Start () {

		RectTransform rect = this.GetComponent<RectTransform> ();
		
		float size = 1.1f;
		float rotate = 30;

		this.transform.DOScale (new Vector3(size, size, rect.localScale.z), 4f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InSine);
		//this.transform.DOLocalRotate (new Vector3(rotate, rotate, rect.rotation.z), 4f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutSine);
	}

}
