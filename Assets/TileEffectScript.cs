using UnityEngine;
using System.Collections;

public class TileEffectScript : MonoBehaviour {

	float tick;

	UISprite uis;

	bool bAni;
	// Use this for initialization
	void Awake () {
		tick = 0;
		bAni = false;
		uis = this.GetComponent<UISprite> ();
	}
	
	// Update is called once per frame
//	void FixedUpdate () {
	void Update () {

		//tick += Time.deltaTime;

		//Debug.Log("tick");
		if(bAni)
		{
			if (tick == 0) 
			{
				uis.spriteName = "TileEffect2";
			} else if (tick == 1) 
			{
				uis.spriteName = "TileEffect1";
			} else if (tick == 2) 
			{
				uis.spriteName = "TileEffect0";
			} else if (tick > 2) {
				hide_this();
			}
			tick++;
		}
	}

	public void play()
	{
		uis.enabled = true;
		bAni = true;
		tick = 0;
	}

	public void hide_this()
	{
		uis.enabled = false;
	}
}
