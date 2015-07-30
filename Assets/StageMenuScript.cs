using UnityEngine;
using System.Collections;

public class StageMenuScript : MonoBehaviour {

	PlayerData PD;

	int HotMenuBarState = 0;
	//public UIAtlas numberAtlas;

	GameObject go_shopButton;
	GameObject go_gold;
	GameObject go_gem;

	// Use this for initialization
	void Awake () {
		PD = PlayerData.Instance;


		go_gold = GameObject.Find ("text_goldValue");
		go_gem = GameObject.Find ("text_gemValue");

		refresh_Gold();
		refresh_Gem();
	}

	void Start()
	{
	}

	// Update is called once per frame
	void Update () {

	}
	
	public void refresh_Gold()
	{
		go_gold.GetComponent<UILabel> ().text = string.Format("{0:N0}", PD.gold);
	}
	
	public void refresh_Gem()
	{
		go_gem.GetComponent<UILabel> ().text = string.Format("{0:N0}", PD.gem);
	}
	


	void moveChickenHead()
	{
		if (HotMenuBarState == 0) 
		{
			HotMenuBarState = 1;
//			Debug.Log("moveChickenHead()---:"+HotMenuBarState);
				GameObject go = GameObject.Find ("MotMenuIcon");
	
				Vector2 pos = new Vector2 (go.transform.localPosition.x - 810, go.transform.localPosition.y);
	
				TweenPosition twPosition = TweenPosition.Begin (go, 0.5f, pos);
				twPosition.method = UITweener.Method.BounceIn;
				EventDelegate.Add (twPosition.onFinished, showHotMenuBar);
		}
	}

	void showHotMenuBar()
	{
		if (HotMenuBarState == 1) {
			HotMenuBarState = 2;
				GameObject go = GameObject.Find ("HotMenuBase");

				Vector2 pos = new Vector2 (go.transform.localPosition.x + 810, go.transform.localPosition.y);
		
				TweenPosition twPosition = TweenPosition.Begin (go, 0.5f, pos);
				twPosition.method = UITweener.Method.BounceIn;
			EventDelegate.Add (twPosition.onFinished, callback_showHotMenuBar);
		}
	}

	void callback_showHotMenuBar()
	{
		if (HotMenuBarState == 2) 
		{
			HotMenuBarState = 3;
		} 
	}

	void closeHotMenuBar()
	{
		if (HotMenuBarState == 3) 
		{
			HotMenuBarState = 4;
			//Debug.Log("closeHotMenuBar()---:"+HotMenuBarState);
				GameObject go = GameObject.Find ("HotMenuBase");

				Vector2 pos = new Vector2 (go.transform.localPosition.x - 810, go.transform.localPosition.y);

				TweenPosition twPosition = TweenPosition.Begin (go, 0.5f, pos);
				twPosition.method = UITweener.Method.BounceIn;
				EventDelegate.Add (twPosition.onFinished, backChickenHead);
		}

	}

	void backChickenHead()
	{
		if (HotMenuBarState == 4) {
			HotMenuBarState = 5;
				GameObject go = GameObject.Find ("MotMenuIcon");
	
				Vector2 pos = new Vector2 (go.transform.localPosition.x + 810, go.transform.localPosition.y);
	
				TweenPosition twPosition = TweenPosition.Begin (go, 0.5f, pos);
				twPosition.method = UITweener.Method.BounceIn;
				EventDelegate.Add (twPosition.onFinished, finished_backChickenHead);
		}
	}

	void finished_backChickenHead()
	{
		if (HotMenuBarState == 5) 
		{
			HotMenuBarState = 0;
		}
	}










	void setSound()
	{
		GameObject go = GameObject.Find ("Sound");
		if (PD.iSound == 1) {
			go.GetComponent<UISprite>().spriteName = "sound1";
			PD.iSound = 0;
		} else if (PD.iSound == 0) {
			go.GetComponent<UISprite>().spriteName = "sound";
			PD.iSound = 1;
		}
	}

	void setVibration()
	{
		GameObject go = GameObject.Find ("Vibration");
		if (PD.iVibration == 1) {
			go.GetComponent<UISprite>().spriteName = "MobilePhone";
			PD.iVibration = 0;
		} else if (PD.iVibration == 0) {
			go.GetComponent<UISprite>().spriteName = "MobilePhone1";
			PD.iVibration = 1;
		}
	}


	public void OnClick_gotoShop()
	{
		Application.LoadLevel ("ShopMain");
	}


}
