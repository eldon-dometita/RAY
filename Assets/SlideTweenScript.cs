using UnityEngine;
using System.Collections;

public class SlideTweenScript : MonoBehaviour {


	public int group;
	TweenPosition tweenPosition;
	public int direction;

	// Use this for initialization
	void Start () {

		direction = 0;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClick_Button()
	{
		if (direction == 0) {
			tweenPosition = TweenPosition.Begin (this.gameObject, 0.3f, new Vector3 (-500, 240-group*150, 0));
			tweenPosition.method = UITweener.Method.BounceIn;
			EventDelegate.Add (tweenPosition.onFinished, callback_move_finished);
		} else if (direction == 1) {
			tweenPosition = TweenPosition.Begin (this.gameObject, 0.3f, new Vector3 (-15, 240-group*150, 0));
			tweenPosition.method = UITweener.Method.BounceIn;
			EventDelegate.Add (tweenPosition.onFinished, callback_move_finished);
		}
	}

	void callback_move_finished()
	{
		if (direction == 0) {
			direction = 1;
		} else if (direction == 1) {
			direction = 0;
		}
	}
}
