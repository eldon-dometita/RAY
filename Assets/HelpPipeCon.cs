using UnityEngine;
using System.Collections;

public class HelpPipeCon : MonoBehaviour {

	GameObject go_popup;



	


	TextScript ts;

	public EffectSoundManagerScript efm;

	public static HelpPipeCon Instance;

	GameObject[] go_HState;

	HelpPopupCon helpPopupCon;

	GlobalData gd;
	//public static HelpPipeCon Instance;

//	private static HelpPipeCon _instance;
//	
//	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	/// 
//	// Static singleton property
//	public static HelpPipeCon Instance
//	{
//		// Here we use the ?? operator, to return 'instance' if 'instance' does not equal null
//		// otherwise we assign instance to a new component and return that
//		get 
//		{ 
//			if(_instance == null)
//			{
//				GameObject obj = new GameObject ();
//				obj.hideFlags = HideFlags.HideAndDontSave;
//				_instance = obj.AddComponent<HelpPipeCon> ();
//			}
//			
//			return _instance;
//		}
//	}


	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Use this for initialization
	void Awake () {

		Instance = this;

		helpPopupCon = GameObject.Find("Popup_Help").GetComponent<HelpPopupCon>();

		ts = TextScript.Instance;

		helpState = 0;

		efm = EffectSoundManagerScript.Instance;

		go_HState = new GameObject[12];
		for(int i=0; i<12; i++)
		{
			go_HState[i] = GameObject.Find("HStage"+i);
		}
//		Debug.Log("1");
//		gd = GlobalData.Instance;
//		gameCon = GameCon.Instance;//GameObject.Find ("GameCon").GetComponent<GameCon>();

		go_popup = GameObject.Find("Popup_Help");

		gd = GlobalData.Instance;
		gd.setTileNum(0, true);


//		help_PipeIndex  = 173;
//		setHelpPipe(help_PipeIndex);
//
//
////		TweenPosition twPosition = TweenPosition.Begin(go_popup, 0.3f, new Vector3(0, 0, 0));
////		twPosition.method = UITweener.Method.BounceIn;
	}



	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Update is called once per frame
	void Update () {
	
	}



	int helpState = 0;
	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void OnClick_Prev()
	{
		efm.Play(0);

		if(helpState > 0)
		{
			if(go_popup.transform.localPosition.y == 0)
			{
				EnableControls(go_HState[helpState]);
				TweenPosition twPosition2 = TweenPosition.Begin(go_popup, 0.3f, new Vector3(0, -800, 0));
				twPosition2.method = UITweener.Method.BounceIn;
			}

			TweenPosition twPosition = TweenPosition.Begin(go_HState[helpState], 0.3f, new Vector3(1500, 0, 0));
			twPosition.method = UITweener.Method.BounceIn;
			
			helpState--;
			
			TweenPosition twPosition1 = TweenPosition.Begin(go_HState[helpState], 0.3f, new Vector3(0, 0, 0));
			twPosition1.method = UITweener.Method.BounceIn;

			GameObject.Find("text_page").GetComponent<UILabel>().text = (helpState + 1)+"/12";
		}
	}

	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void OnClick_Next()
	{
		efm.Play(0);

		if(helpState < 11)
		{
			if(go_popup.transform.localPosition.y == 0)
			{
				EnableControls(go_HState[helpState]);
				TweenPosition twPosition2 = TweenPosition.Begin(go_popup, 0.3f, new Vector3(0, -800, 0));
				twPosition2.method = UITweener.Method.BounceIn;
			}

			TweenPosition twPosition = TweenPosition.Begin(go_HState[helpState], 0.3f, new Vector3(-1500, 0, 0));
			twPosition.method = UITweener.Method.BounceIn;

			helpState++;

			TweenPosition twPosition1 = TweenPosition.Begin(go_HState[helpState], 0.3f, new Vector3(0, 0, 0));
			twPosition1.method = UITweener.Method.BounceIn;

			GameObject.Find("text_page").GetComponent<UILabel>().text = (helpState + 1)+"/12";
		}
	}

	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void Close_AniPopup()
	{
		efm.Play(0);

		EnableControls(go_HState[helpState]);
		TweenPosition twPosition = TweenPosition.Begin(go_popup, 0.3f, new Vector3(0, -800, 0));
		twPosition.method = UITweener.Method.BounceIn;
	}

	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void set_Common(int _index)
	{
		GameObject.Find ("text_helpPipeName").GetComponent<UILabel>().text = ts.PName[_index];

		DisableControls(go_HState[helpState]);
		HelpPopupCon.help_PipeIndex  = _index;
		//Debug.Log("helpPopupCon.help_PipeIndex:   "+HelpPopupCon.help_PipeIndex);
		helpPopupCon.setHelpPipe(HelpPopupCon.help_PipeIndex);

		if(_index >= 170 && _index <= 173)
		{
			GameObject.Find("text_explain").GetComponent<UILabel>().text = ts.HStr[170];
		}
		else
		{
			GameObject.Find("text_explain").GetComponent<UILabel>().text = ts.HStr[_index];
		}

		efm.Play(8);

		go_popup.transform.localPosition = new Vector3(0, 800, 0);
		TweenPosition twPosition = TweenPosition.Begin(go_popup, 0.3f, new Vector3(0, 0, 0));
		twPosition.method = UITweener.Method.BounceIn;
	}

	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void OnClick_P10(){set_Common(10);}
	void OnClick_P11(){set_Common(11);}
	void OnClick_P12(){set_Common(12);}
	void OnClick_P13(){set_Common(13);}
	void OnClick_P14(){set_Common(14);}
	void OnClick_P15(){set_Common(15);}
	void OnClick_P16(){set_Common(16);}




	private void DisableControls(GameObject _gameObject) {
		BoxCollider[] colls =  _gameObject.GetComponentsInChildren<BoxCollider>();
		foreach(BoxCollider b in colls)
		{
			b.enabled = false;
		}
	}
	
	private void EnableControls(GameObject _gameObject) {
		BoxCollider[] colls =  _gameObject.GetComponentsInChildren<BoxCollider>();
		foreach(BoxCollider b in colls)
		{
			b.enabled = true;
		}
	}

	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void OnClick_P20(){set_Common(20);}
	void OnClick_P21(){set_Common(21);}
	void OnClick_P22(){set_Common(22);}
	void OnClick_P23(){set_Common(23);}
	void OnClick_P24(){set_Common(24);}
	void OnClick_P25(){set_Common(25);}
	void OnClick_P30(){set_Common(30);}
	void OnClick_P31(){set_Common(31);}
	void OnClick_P32(){set_Common(32);}
	void OnClick_P33(){set_Common(33);}
	void OnClick_P34(){set_Common(34);}
	void OnClick_P35(){set_Common(35);}

	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void OnClick_P40(){set_Common(40);}
	void OnClick_P41(){set_Common(41);}
	void OnClick_P42(){set_Common(42);}
	void OnClick_P43(){set_Common(43);}
	void OnClick_P44(){set_Common(44);}
	void OnClick_P45(){set_Common(45);}
	void OnClick_P46(){set_Common(46);}

	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void OnClick_P50(){set_Common(50);}
	void OnClick_P51(){set_Common(51);}
	void OnClick_P52(){set_Common(52);}
	void OnClick_P53(){set_Common(53);}
	void OnClick_P54(){set_Common(54);}
	void OnClick_P55(){set_Common(55);}
	void OnClick_P60(){set_Common(60);}
	void OnClick_P61(){set_Common(61);}
	void OnClick_P62(){set_Common(62);}
	void OnClick_P63(){set_Common(63);}
	void OnClick_P64(){set_Common(64);}
	void OnClick_P65(){set_Common(65);}

	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void OnClick_P70(){set_Common(70);}
	void OnClick_P71(){set_Common(71);}
	void OnClick_P72(){set_Common(72);}
	void OnClick_P73(){set_Common(73);}
	void OnClick_P74(){set_Common(74);}
	void OnClick_P75(){set_Common(75);}
	void OnClick_P76(){set_Common(76);}
	void OnClick_P77(){set_Common(77);}

	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void OnClick_P80(){set_Common(80);}
	void OnClick_P81(){set_Common(81);}
	void OnClick_P82(){set_Common(82);}
	void OnClick_P83(){set_Common(83);}
	void OnClick_P84(){set_Common(84);}
	void OnClick_P85(){set_Common(85);}
	void OnClick_P86(){set_Common(86);}
	void OnClick_P87(){set_Common(87);}

	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void OnClick_P90(){set_Common(90);}
	void OnClick_P91(){set_Common(91);}
	void OnClick_P92(){set_Common(92);}
	void OnClick_P93(){set_Common(93);}
	void OnClick_P94(){set_Common(94);}
	void OnClick_P95(){set_Common(95);}
	void OnClick_P96(){set_Common(96);}
	void OnClick_P97(){set_Common(97);}
	
	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void OnClick_P100(){set_Common(100);}
	void OnClick_P101(){set_Common(101);}
	void OnClick_P102(){set_Common(102);}
	void OnClick_P103(){set_Common(103);}
	void OnClick_P104(){set_Common(104);}
	void OnClick_P105(){set_Common(105);}
	void OnClick_P106(){set_Common(106);}
	void OnClick_P107(){set_Common(107);}

	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void OnClick_P130(){set_Common(130);}
	void OnClick_P131(){set_Common(131);}
	void OnClick_P132(){set_Common(132);}
	void OnClick_P133(){set_Common(133);}

	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void OnClick_P170(){set_Common(170);}
	void OnClick_P171(){set_Common(171);}
	void OnClick_P172(){set_Common(172);}
	void OnClick_P173(){set_Common(173);}



	public static int whereFrom = 0;	//0: Planet 	1:Stage
	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void OnClick_BackScene()
	{
		efm.Play(0);
		
		if (whereFrom == 0) 
		{
			//Application.LoadLevel ("MapScene");
			StartCoroutine(LoadAfterDelay("MapScene"));    
		}
		else if (whereFrom == 1)
		{
			StartCoroutine(LoadAfterDelay("StageScene"));    
			//Application.LoadLevel ("StageScene");
		}

	}

	IEnumerator LoadAfterDelay(string levelName)
	{
		//yield return new WaitForSeconds(2.5f); // wait 1 seconds
		yield return new WaitForSeconds(efm.audioClip.length); //Waits till Audio is done playing
		Application.LoadLevel(levelName);
		
	}
	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
