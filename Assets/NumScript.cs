using UnityEngine;
using System.Collections;

public class NumScript : MonoBehaviour {
	float tick;
	
	UILabel uil;
	
	bool bAni;
	// Use this for initialization
	void Awake () {
		tick = 0;
		bAni = false;
		uil = this.GetComponent<UILabel> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(bAni)
		{

			tick++;
		}
	}

	public void play(int _value)
	{
		bAni = true;
		tick = 0;

		Vector3 pos = this.transform.localPosition;
		pos.y += 50;

		if (_value > 0) 
		{
			uil.text = "+"+_value.ToString ();
		}
		else
		{
			uil.text = _value.ToString ();
		}
						
		TweenPosition twPosition = TweenPosition.Begin( this.gameObject, 1f, pos ); //VenderCon.VENDER_SPEED
		twPosition.method = UITweener.Method.Linear;
		EventDelegate.Add( twPosition.onFinished, destroy_this, true);
	}

	public void play(string _text)
	{
		bAni = true;
		tick = 0;

		Vector3 pos = this.transform.localPosition;
		pos.y += 50;
		
		uil.text = _text;

		TweenPosition twPosition = TweenPosition.Begin( this.gameObject, 1f, pos ); //VenderCon.VENDER_SPEED
		twPosition.method = UITweener.Method.Linear;
		EventDelegate.Add( twPosition.onFinished, destroy_this, true);
	}
	
	public void destroy_this()
	{
		Destroy(this.gameObject);
	}
}
