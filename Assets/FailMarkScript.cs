using UnityEngine;
using System.Collections;

public class FailMarkScript : MonoBehaviour {

	GameCon gameCon;
	float time;
	public bool bShowFailText;

	UISprite uis;
	// Use this for initialization
	void Awake () {
		uis = GetComponent<UISprite> ();
		time = 0;
		uis.alpha = 1f;

		gameCon = GameObject.Find ("GameCon").GetComponent<GameCon>();
		bShowFailText = false;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time % 3 == 0) {
			uis.alpha = 0.1f;
		} else {
			uis.alpha = 1f;
		}

		//Debug.Log ("if (!bShowFailText) ");
		if (!bShowFailText) 
		{
			//Debug.Log ("time:"+time);
			if (time > 5f) 
			{
					bShowFailText = true;
					gameCon.setBigText (20);
			}
		}
	}
}
