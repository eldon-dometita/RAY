using UnityEngine;
using System.Collections;

public class PerfectPlanBlink : MonoBehaviour {

	float time;

	UISprite uis;
	// Use this for initialization
	void Awake () {
		uis = GetComponent<UISprite> ();
		time = 0;
		uis.alpha = 0.3f;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		uis.alpha += time/4;
		if(uis.alpha >= 1)
		{
			uis.alpha = 1f;
		}

		if (time >= 2f) 
		{
			uis.alpha = 0.3f;
			time = 0;
		}
	}
}
