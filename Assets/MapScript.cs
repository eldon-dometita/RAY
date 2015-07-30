using UnityEngine;
using System.Collections;
using System.IO;

public class MapScript : MonoBehaviour {


	PlayerData pd;
	//SoundManagerScript sm;
	EffectSoundManagerScript efm;

	public int PLANET_SIZE = 10;
	int select_planet;
	GameObject[] go_Planet;

	GameObject go_HotMenuBase;
	GameObject go_HotMenu;
	GameObject go_FillRect_HotMenu;

	public static float scrollBar_Value;

	GameObject scrollBar;

	// Use this for initialization
	void Awake () {

		pd = PlayerData.Instance;

		//sm = SoundManagerScript.Instance;
		efm = EffectSoundManagerScript.Instance;

		scrollBar = GameObject.Find ("ScrollBar");
		//loadNorm_N_Score ();
//		sm = SoundManagerScript.Instance;
//		sm.playSnd ();

		go_HotMenuBase = GameObject.Find ("HotMenuBase");
		go_HotMenu = GameObject.Find ("HotMenu");

		go_FillRect_HotMenu = GameObject.Find ("FillRect_HotMenu");
		go_FillRect_HotMenu.SetActive (false);

		select_planet = 0;

		go_Planet = new GameObject[PLANET_SIZE];

		createPlanet ();

		setScrollBarValue ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		GameObject.Find ("text_gemValue").GetComponent<UILabel>().text = string.Format("{0:N0}", pd.gem);
		GameObject.Find ("text_goldValue").GetComponent<UILabel>().text = string.Format("{0:N0}", pd.gold);
		refresh_Life ();
	}

	void refresh_Life ()
	{
		GameObject.Find ("text_heartValue").GetComponent<UILabel> ().text = "x"+pd.life;
		if (pd.life >= 5) 
		{
			GameObject.Find ("text_heartTime").GetComponent<UILabel> ().text = "FULL";
		} else 
		{
			GameObject.Find ("text_heartTime").GetComponent<UILabel> ().text = GameThread.getLifeTime();
		}
	}

	void createPlanet()
	{

		for (int i=0; i<PLANET_SIZE; i++) 
		{
			go_Planet[i] = GameObject.Find("Planet"+i);

			go_Planet[i].transform.FindChild("text_planetName").GetComponent<UILabel>().text = "PLANET "+(i+1);
			go_Planet[i].transform.FindChild("text_sum_Star").GetComponent<UILabel>().text = getSum_Star(i)+"/80";

		}
	}

	////////////////////////////////////////////////////////////////////////////////////////////////
	int getSum_Star(int _planet)
	{
		int sum = 0;
		for (int i=0; i<20; i++) 
		{
			sum += pd.star[_planet*20+i];
		}
		return sum;
	}

	////////////////////////////////////////////////////////////////////////////////////////////////
	void OnClick_ButtonPlanet0()
	{
		select_planet = 0;
		PlayerData.nowPlanet = select_planet;
		getScrollBarValue ();

		efm.Play (0);
		StartCoroutine(LoadAfterDelay("StageScene"));    
		//Application.LoadLevel ("StageScene");

	
//		Application.LoadLevel ();
	}

	IEnumerator LoadAfterDelay(string levelName){
		//yield return new WaitForSeconds(2.5f); // wait 1 seconds
		yield return new WaitForSeconds(efm.audioClip.length); //Waits till Audio is done playing
		Application.LoadLevel(levelName);
		
	}

	void OnClick_ButtonPlanet1()
	{
		select_planet = 1;
		PlayerData.nowPlanet = select_planet;
		getScrollBarValue ();
		efm.Play (0);
		StartCoroutine(LoadAfterDelay("StageScene"));    
	}

	void OnClick_ButtonPlanet2()
	{
		select_planet = 2;
		PlayerData.nowPlanet = select_planet;
		getScrollBarValue ();
		efm.Play (0);
		StartCoroutine(LoadAfterDelay("StageScene"));    
	}

	void OnClick_ButtonPlanet3()
	{
		select_planet = 3;
		PlayerData.nowPlanet = select_planet;
		getScrollBarValue ();
		efm.Play (0);
		StartCoroutine(LoadAfterDelay("StageScene"));    
	}

	void OnClick_ButtonPlanet4()
	{
		select_planet = 4;
		PlayerData.nowPlanet = select_planet;
		getScrollBarValue ();
		efm.Play (0);
		StartCoroutine(LoadAfterDelay("StageScene"));    
	}

	void OnClick_ButtonPlanet5()
	{
		select_planet = 5;
		PlayerData.nowPlanet = select_planet;
		getScrollBarValue ();
		efm.Play (0);
		StartCoroutine(LoadAfterDelay("StageScene"));    
	}

	void OnClick_ButtonPlanet6()
	{
		select_planet = 6;
		PlayerData.nowPlanet = select_planet;
		getScrollBarValue ();
		efm.Play (0);
		StartCoroutine(LoadAfterDelay("StageScene"));    
	}

	void OnClick_ButtonPlanet7()
	{
		select_planet = 7;
		PlayerData.nowPlanet = select_planet;
		getScrollBarValue ();
		efm.Play (0);
		StartCoroutine(LoadAfterDelay("StageScene"));    
	}

	void OnClick_ButtonPlanet8()
	{
		select_planet = 8;
		PlayerData.nowPlanet = select_planet;
		getScrollBarValue ();
		efm.Play (0);
		StartCoroutine(LoadAfterDelay("StageScene"));    
	}

	void OnClick_ButtonPlanet9()
	{
		select_planet = 9;
		PlayerData.nowPlanet = select_planet;
		getScrollBarValue ();
		efm.Play (0);
		StartCoroutine(LoadAfterDelay("StageScene"));    
	}

	void getScrollBarValue()
	{
		scrollBar_Value = GameObject.Find ("ScrollBar").GetComponent<UIScrollBar> ().value;
	}

	void setScrollBarValue()
	{
		GameObject.Find ("ScrollBar").GetComponent<UIScrollBar> ().value = scrollBar_Value;
	}

	////////////////////////////////////////////////////////////////////////////////////////////////
	void OnClick_Goto_Shop()
	{
		efm.Play(0);

		shopMainController.whereFrom = 0;

		StartCoroutine(LoadAfterDelay("ShopMain"));    
//		//LoadAfterDelay("ShopMain");
//		Application.LoadLevel ("ShopMain");s
	}

	////////////////////////////////////////////////////////////////////////////////////////////////
	void OnClick_goto_HelpScene()
	{
		efm.Play(0);
		
		HelpPipeCon.whereFrom = 0;
		
		StartCoroutine(LoadAfterDelay("HelpPipeScene"));    
	}

	////////////////////////////////////////////////////////////////////////////////////////////////

	void OnClick_Menu()
	{
		toggleActive(false);

		go_FillRect_HotMenu.SetActive (true);

		efm.Play(8);

		TweenPosition twPosition = TweenPosition.Begin (go_HotMenuBase, 0.3f, new Vector3(0, 0, 0));
		twPosition.method = UITweener.Method.BounceIn;
		EventDelegate.Add (twPosition.onFinished, callback_finish_base, true);
	}

	void callback_finish_base()
	{
		TweenPosition twPosition = TweenPosition.Begin (go_HotMenu, 0.3f, new Vector3(0, 0, 0));
		twPosition.method = UITweener.Method.BounceIn;
		//EventDelegate.Add (twPosition.onFinished, callback_finish_base, true);
	}

	void OnClick_Close_HotMenu()
	{
		toggleActive(true);

		efm.Play(0);
		TweenPosition twPosition = TweenPosition.Begin (go_HotMenu, 0.3f, new Vector3(-1600, 0, 0));
		twPosition.method = UITweener.Method.BounceOut;
		EventDelegate.Add (twPosition.onFinished, callback_finish_close_HotMenu, true);
	}

	void callback_finish_close_HotMenu()
	{
		TweenPosition twPosition = TweenPosition.Begin (go_HotMenuBase, 0.3f, new Vector3(0, 800, 0));
		twPosition.method = UITweener.Method.BounceOut;

		go_FillRect_HotMenu.SetActive (false);
		//EventDelegate.Add (twPosition.onFinished, callback_finish_base, true);
	}
	////////////////////////////////////////////////////////////////////////////////////////////////

	void setSound()
	{
		GameObject go = GameObject.Find ("Sound");
		if (pd.iSound == 1) {
			go.GetComponent<UISprite>().spriteName = "sound1";
			pd.iSound = 0;
		} else if (pd.iSound == 0) {
			go.GetComponent<UISprite>().spriteName = "sound";
			pd.iSound = 1;
		}
	}
	
	void setVibration()
	{
		GameObject go = GameObject.Find ("Vibration");
		if (pd.iVibration == 1) {
			go.GetComponent<UISprite>().spriteName = "MobilePhone";
			pd.iVibration = 0;
		} else if (pd.iVibration == 0) {
			go.GetComponent<UISprite>().spriteName = "MobilePhone1";
			pd.iVibration = 1;
		}
	}



	void toggleActive(bool _toggle)
	{
		for(int i=0; i<PLANET_SIZE; i++)
		{
			if (_toggle)
			{
				go_Planet[i].GetComponent<BoxCollider>().enabled = true;
			}
			else
			{
				go_Planet[i].GetComponent<BoxCollider>().enabled = false;
			}
		}
		
		if (_toggle)
		{
			//GameObject.Find("ButtonBack").GetComponent<BoxCollider>().enabled = true;
			GameObject.Find("Button_MENU").GetComponent<BoxCollider>().enabled = true;
			GameObject.Find("Button_Shop").GetComponent<BoxCollider>().enabled = true;
			
			scrollBar.SetActive(true);
		}
		else
		{
			//GameObject.Find("ButtonBack").GetComponent<BoxCollider>().enabled = false;
			GameObject.Find("Button_MENU").GetComponent<BoxCollider>().enabled = false;
			GameObject.Find("Button_Shop").GetComponent<BoxCollider>().enabled = false;
			scrollBar.SetActive(false);
		}
	}




	////////////////////////////////////////////////////////////////////////////////////////////////

}
