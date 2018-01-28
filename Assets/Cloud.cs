using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cloud : MonoBehaviour {

	public RectTransform posMin;
	public RectTransform posMax;

	public float minTime;
	public float maxTime;

	private float yPos;
	private float finalXPos;
	private float time;

	private RectTransform rect;
	private Tween fly;

	// Use this for initialization
	void Start () {

		rect = this.GetComponent<RectTransform> ();

		prepareAndFly ();

		DOTween.Sequence ().AppendCallback (() => {prepareAndFly();}).SetDelay(maxTime*1.05f).SetLoops(-1);

	}

	private void prepareAndFly(){

		finalXPos = rect.localPosition.x * -1;
		time = Random.Range (minTime, maxTime);
		yPos = Random.Range (posMax.localPosition.y, posMin.localPosition.y);
		Debug.Log (yPos);

		DOTween.Sequence().Append(rect.DOLocalMoveY (yPos, 0f)).Append(rect.DOLocalMoveX (finalXPos, time).SetEase (Ease.Linear));

	}

}
