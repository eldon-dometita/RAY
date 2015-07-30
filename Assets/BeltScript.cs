using UnityEngine;
using System.Collections;

public class BeltScript : MonoBehaviour {

	GameCon gameCon;
	public bool bMove;

	// Use this for initialization
	void Start () {
		gameCon = GameObject.Find ("GameCon").GetComponent<GameCon>();
		bMove = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void move()
	{
		bMove = true;
		Vector3 pos = this.transform.localPosition;
		
		if (pos.y > 690) 
		{
			pos.y += 105;
			TweenPosition twPosition = TweenPosition.Begin( this.gameObject, gameCon.conveyorSpeed, pos ); //VenderCon.VENDER_SPEED
			twPosition.method = UITweener.Method.Linear;
			EventDelegate.Add( twPosition.onFinished, callback_move_top, true);
		} else {
			pos.y += 105;
			TweenPosition twPosition = TweenPosition.Begin( this.gameObject, gameCon.conveyorSpeed, pos ); //VenderCon.VENDER_SPEED
			twPosition.method = UITweener.Method.Linear;
			EventDelegate.Add( twPosition.onFinished, callback_move_default, true);
		}
	}

	private void callback_move_top()
	{
		Vector3 pos = this.transform.localPosition;
		pos.y = -35;
		this.transform.localPosition = pos;
		bMove = false;
	}
	
	private void callback_move_default()
	{
		bMove = false;
	}

}
