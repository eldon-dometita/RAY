using UnityEngine;
using System.Collections;

public class FadeScript : MonoBehaviour {

	GameCon gameCon;
	UISprite uiSprite;
	float timer;
	bool bStartFade;

	// Use this for initialization
	void Start () {
		gameCon = GameObject.Find("GameCon").GetComponent<GameCon>();

		uiSprite = this.GetComponent<UISprite> ();
		timer = 0;
		bStartFade = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (bStartFade) 
		{
			timer -= Time.deltaTime;


			uiSprite.alpha = timer;

			if(timer <= 0)
			{
				uiSprite.alpha = 0;
				bStartFade = false;
				gameCon.startGame();
			}
		}
	}

	public void start()
	{
		timer = 1;
		bStartFade = true;
	}
}
