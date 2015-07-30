using UnityEngine;
using System.Collections;

public class ScoreEffectScript : MonoBehaviour {

	public EffectSoundManagerScript efm;

	float tick;

	//float timer;
	UISprite uis;
	int id;

	bool bAni;

	// Use this for initialization
	void Awake () {

		efm = EffectSoundManagerScript.Instance;

		//timer = 0;
		id = 0;
		tick = 0;
		bAni = false;
		uis = this.GetComponent<UISprite> ();
	}
	
	// Update is called once per frame
	//	void FixedUpdate () {
	void Update () {
		

		
		//Debug.Log("tick");
		if(bAni)
		{
//			timer += Time.deltaTime;
//
//			tick = (timer / 10);
//
			if (tick == 0) 
			{
			}
			else if (tick == 1) 
			{
				uis.spriteName = "Shop115";
				uis.width = 600;
				uis.height = 34;
			}
			else if (tick == 2) 
			{
				uis.spriteName = "Shop116";
				uis.width = 650;
				uis.height = 34;
			}
			else if (tick == 3) 
			{
				uis.spriteName = "Shop117";
				uis.width = 700;
				uis.height = 51;
			}
			else if (tick == 4) 
			{
				uis.spriteName = "Shop118";
				uis.width = 750;
				uis.height = 36;
			}
			else if (tick == 5) 
			{
				uis.spriteName = "Shop119";
				uis.width = 800;
				uis.height = 34;

			}
			else if (tick == 6) 
			{
				uis.spriteName = "Shop120";
				uis.width = 1280;
				uis.height = 5;
			}
			else if (tick == 7) 
			{
				uis.spriteName = "Shop121";
				uis.width = 1280;
				uis.height = 20;
			}
			else if (tick == 8) 
			{
				uis.spriteName = "Shop122";
				uis.width = 1280;
				uis.height = 28;
				GameObject.Find("Popup_Score").GetComponent<EndScoreScript>().setTextScore(id);
			}
			else if (tick == 9) 
			{
				uis.spriteName = "Shop123";
				uis.width = 1280;
				uis.height = 10;
			}
			else if (tick == 10) 
			{
				destroy_this();
			}
			tick++;
		}

	}
	
	public void play(int _id)
	{
		efm.Play(9);
		id = _id;
		bAni = true;
		tick = 0;

		//timer = 0;
	}
	
	public void destroy_this()
	{
		Destroy(this.gameObject);
	}
}
