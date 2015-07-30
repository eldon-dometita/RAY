using UnityEngine;
using System.Collections;
using System.IO;

public class StageCon : MonoBehaviour {

	int selectIndex;

	PlayerData pd;
	EffectSoundManagerScript efm;

	GameObject[] go_Stage;
	GameObject go_StagePopup;

	public static float scrollBar_Value;
	public static bool useConveyorBoost = false;
	public static bool useInventoryBoost = false;

	GameObject go_HotMenuBase;
	GameObject go_HotMenu;
	GameObject go_FillRect_HotMenu;

	GameObject scrollBar;
	GameObject go_Minimap_popup;


	// Use this for initialization
	void Awake () {


		pd = PlayerData.Instance;
		if (pd == null) {
						Debug.Log ("pd = PlayerData.Instance;" + pd);
				}

		efm = EffectSoundManagerScript.Instance;

		scrollBar = GameObject.Find ("ScrollBar");

		go_Minimap_popup = GameObject.Find("Popup_MiniMap");
		go_Minimap_popup.transform.localPosition = new Vector3(0, 800, 0);
//		sm = SoundManagerScript.Instance;
		//sm.playSnd ();

		go_HotMenuBase = GameObject.Find ("HotMenuBase");
		go_HotMenu = GameObject.Find ("HotMenu");

		go_FillRect_HotMenu = GameObject.Find ("FillRect_HotMenu");
		go_FillRect_HotMenu.SetActive (false);

		go_Stage = new GameObject[20];
		go_StagePopup = GameObject.Find ("Popup_Stage");
		selectIndex = 0;

		//Debug.Log ("PlayerData.nowPlanet:   "+PlayerData.nowPlanet);
		createState ();

		setScrollBarValue ();

		go_mini = new GameObject[144];
		for(int i=0; i<144; i++)
		{
			go_mini[i] = GameObject.Find("mini"+i);
			go_mini[i].transform.localPosition = new Vector3(-1800, 1800, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {

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

	void createState()
	{

		for (int i=0; i<20; i++) 
		{
			go_Stage[i] = GameObject.Find("Stage"+i);
			go_Stage[i].transform.FindChild("text_stageName").GetComponent<UILabel>().text = "STAGE "+(PlayerData.nowPlanet*20+i+1);

			if(pd.isOpen[PlayerData.nowPlanet*20+i] == 0)	//lock
			{
				go_Stage[i].transform.FindChild("Star0").gameObject.SetActive(false);
				go_Stage[i].transform.FindChild("Star1").gameObject.SetActive(false);
				go_Stage[i].transform.FindChild("Star2").gameObject.SetActive(false);
				go_Stage[i].transform.FindChild("Star3").gameObject.SetActive(false);
				//go_Stage[i].transform.FindChild("text_norm").gameObject.SetActive(false);
				go_Stage[i].transform.FindChild("text_highScore").gameObject.SetActive(false);

				go_Stage[i].transform.FindChild("text_norm").localPosition = new Vector3(0, -130, 0);
				go_Stage[i].transform.FindChild("text_norm").GetComponent<UILabel>().text = "LIMIT PIPE: [FF0000]"+pd.LimitNorm[PlayerData.nowPlanet*20+i]+"[-]";
			}
			else //open
			{
				go_Stage[i].transform.FindChild("text_norm").GetComponent<UILabel>().text = "LIMIT PIPE: [FF0000]"+pd.LimitNorm[PlayerData.nowPlanet*20+i]+"[-]";
				go_Stage[i].transform.FindChild("text_highScore").GetComponent<UILabel>().text = "SCORE: [00FFFF]"+pd.record[PlayerData.nowPlanet*20+i]+"[-]";
				go_Stage[i].transform.FindChild("ImgLock").gameObject.SetActive(false);
				setStar(i);
			}

		}

//		Debug.Log("-------------------------------");
//		Debug.Log ("pd.isOpen [0]"+pd.isOpen [0]);
//		Debug.Log ("pd.isOpen [1]"+pd.isOpen [1]);
//		
//		Debug.Log ("pd.record [0]"+pd.record [0]);
//		Debug.Log ("pd.record [1]"+pd.record [1]);
	}

	void setStar(int _index)
	{

		for (int i=0; i<4; i++) 
		{
			go_Stage[_index].transform.FindChild("Star"+i).GetComponent<UISprite>().spriteName = "ItemSprite013";
		}

		for (int i=0; i<pd.star[PlayerData.nowPlanet*20+_index]; i++) 
		{
			go_Stage[_index].transform.FindChild("Star"+i).GetComponent<UISprite>().spriteName = "ItemSprite003";
		}
	}

	void toggleActive(bool _toggle)
	{
		for(int i=0; i<20; i++)
		{
			if (_toggle)
			{
				go_Stage[i].GetComponent<BoxCollider>().enabled = true;
			}
			else
			{
				go_Stage[i].GetComponent<BoxCollider>().enabled = false;
			}
		}

		if (_toggle)
		{
			GameObject.Find("ButtonBack").GetComponent<BoxCollider>().enabled = true;
			GameObject.Find("Button_MENU").GetComponent<BoxCollider>().enabled = true;
			GameObject.Find("Button_Shop").GetComponent<BoxCollider>().enabled = true;

			scrollBar.SetActive(true);
		}
		else
		{
			GameObject.Find("ButtonBack").GetComponent<BoxCollider>().enabled = false;
			GameObject.Find("Button_MENU").GetComponent<BoxCollider>().enabled = false;
			GameObject.Find("Button_Shop").GetComponent<BoxCollider>().enabled = false;
			scrollBar.SetActive(false);
		}
	}

	void moveStagePopup ()
	{
		toggleActive(false);

		useConveyorBoost = false;
		useInventoryBoost = false;
		GameObject.Find ("ConveyorBoost").transform.FindChild("ImgCheck").GetComponent<UISprite>().spriteName = "empty";
		GameObject.Find ("InventoryBoost").transform.FindChild("ImgCheck").GetComponent<UISprite>().spriteName = "empty";

		GameObject.Find ("ConveyorBoost").transform.FindChild("text_posCount").GetComponent<UILabel>().text = pd.consumeItem[1].ToString();
		GameObject.Find ("InventoryBoost").transform.FindChild("text_posCount").GetComponent<UILabel>().text = pd.consumeItem[3].ToString();

		efm.Play(8);
		TweenPosition twPosition = TweenPosition.Begin (go_StagePopup, 0.3f, new Vector3 (0, 0, 0));
		twPosition.method = UITweener.Method.BounceIn;

	}

	void Close_Popup_Stage() 
	{
		toggleActive(true);

		efm.Play(0);

		TweenPosition twPosition = TweenPosition.Begin (go_StagePopup, 0.3f, new Vector3 (0, 800, 0));
		twPosition.method = UITweener.Method.BounceOut;
	}

	void OnClick_Start()
	{
		efm.Play(0);

		if(pd.isOpen[PlayerData.nowPlanet * 20 + selectIndex] == 1)
		{
			if (pd.life > 0) 
			{
				pd.life--;
				pd.saveLifeTime = System.DateTime.UtcNow.ToFileTimeUtc();
				pd.saveData ();
				
				PlayerData.nowMapNumber1 = PlayerData.nowPlanet * 20 + selectIndex;
				efm.Play (0);
				StartCoroutine(LoadAfterDelay("GameStage"));    
				//Application.LoadLevel ("GameStage");
			}
		}
		else{
		}

	}

	////////////////////////////////////////////
	/// 
	void OnClick_StageButton0()
	{
		selectIndex = 0;
		getScrollBarValue ();
		moveStagePopup ();

	}
	
	IEnumerator LoadAfterDelay(string levelName){
		//yield return new WaitForSeconds(2.5f); // wait 1 seconds
		yield return new WaitForSeconds(efm.audioClip.length); //Waits till Audio is done playing
		Application.LoadLevel(levelName);
		
	}

	void OnClick_StageButton1()
	{
		selectIndex = 1;
		getScrollBarValue ();
		moveStagePopup ();
	}

	void OnClick_StageButton2()
	{
		selectIndex = 2;
		getScrollBarValue ();
		moveStagePopup ();
	}

	void OnClick_StageButton3()
	{
		selectIndex = 3;
		getScrollBarValue ();
		moveStagePopup ();
	}

	void OnClick_StageButton4()
	{
		selectIndex = 4;
		getScrollBarValue ();
		moveStagePopup ();
	}

	void OnClick_StageButton5()
	{
		selectIndex = 5;
		getScrollBarValue ();
		moveStagePopup ();
	}

	void OnClick_StageButton6()
	{
		selectIndex = 6;
		getScrollBarValue ();
		moveStagePopup ();
	}

	void OnClick_StageButton7()
	{
		selectIndex = 7;
		getScrollBarValue ();
		moveStagePopup ();
	}

	void OnClick_StageButton8()
	{
		selectIndex = 8;
		getScrollBarValue ();
		moveStagePopup ();
	}

	void OnClick_StageButton9()
	{
		selectIndex = 9;
		getScrollBarValue ();
		moveStagePopup ();
	}

	void OnClick_StageButton10()
	{
		selectIndex = 10;
		getScrollBarValue ();
		moveStagePopup ();
	}

	void OnClick_StageButton11()
	{
		selectIndex = 11;
		getScrollBarValue ();
		moveStagePopup ();
	}

	void OnClick_StageButton12()
	{
		selectIndex = 12;
		getScrollBarValue ();
		moveStagePopup ();
	}

	void OnClick_StageButton13()
	{
		selectIndex = 13;
		getScrollBarValue ();
		moveStagePopup ();
	}

	void OnClick_StageButton14()
	{
		selectIndex = 14;
		getScrollBarValue ();
		moveStagePopup ();
	}

	void OnClick_StageButton15()
	{
		selectIndex = 15;
		getScrollBarValue ();
		moveStagePopup ();
	}

	void OnClick_StageButton16()
	{
		selectIndex = 16;
		getScrollBarValue ();
		moveStagePopup ();
	}

	void OnClick_StageButton17()
	{
		selectIndex = 17;
		getScrollBarValue ();
		moveStagePopup ();
	}

	void OnClick_StageButton18()
	{
		selectIndex = 18;
		getScrollBarValue ();
		moveStagePopup ();
	}

	void OnClick_StageButton19()
	{
		selectIndex = 19;
		getScrollBarValue ();
		moveStagePopup ();
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
	void OnClick_Back()
	{
		efm.Play(0);
		StartCoroutine(LoadAfterDelay("MapScene"));
		//Application.LoadLevel ("MapScene");
	}

	////////////////////////////////////////////////////////////////////////////////////////////////
	void OnClick_Goto_Shop()
	{
		efm.Play(0);

		shopMainController.whereFrom = 1;
		StartCoroutine(LoadAfterDelay("ShopMain"));
		//Application.LoadLevel ("ShopMain");
	}

	////////////////////////////////////////////////////////////////////////////////////////////////
	void OnClick_goto_HelpScene()
	{
		efm.Play(0);
		
		HelpPipeCon.whereFrom = 1;
		
		StartCoroutine(LoadAfterDelay("HelpPipeScene"));    
	}
	
	////////////////////////////////////////////////////////////////////////////////////////////////

	void OnClick_Touch_ConveyorItem()
	{
		efm.Play(0);
		if (pd.consumeItem [1] > 0) 
		{
			if (useConveyorBoost) 
			{
					useConveyorBoost = false;
					GameObject.Find ("ConveyorBoost").transform.FindChild ("ImgCheck").GetComponent<UISprite> ().spriteName = "empty";
			} else {
					useConveyorBoost = true;
					GameObject.Find ("ConveyorBoost").transform.FindChild ("ImgCheck").GetComponent<UISprite> ().spriteName = "check";
			}
		}
	}

	////////////////////////////////////////////////////////////////////////////////////////////////
	
	void OnClick_Touch_InventoryItem()
	{
		efm.Play(0);
		if (pd.consumeItem [3] > 0) {
				if (useInventoryBoost) {
						useInventoryBoost = false;
						GameObject.Find ("InventoryBoost").transform.FindChild ("ImgCheck").GetComponent<UISprite> ().spriteName = "empty";
				} else {
						useInventoryBoost = true;
						GameObject.Find ("InventoryBoost").transform.FindChild ("ImgCheck").GetComponent<UISprite> ().spriteName = "check";
				}
		}
	}


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








	float mapWidth;
	float mapHeight;
	int tmpCol;
	int tmpRow;
	public void loadMiniMapData(int level) 
	{	
		int width = 0;
		int height = 0;
		int emptySpace = 0;
		
		int mapdata = 0;
		int perfectplan = 0;
		int clearType = 0;
		
		try 
		{
			string gameLevel = string.Format("m1{0:D4}", level);
			
			TextAsset textAssets = Resources.Load("MAP_DATA/"+gameLevel) as TextAsset;
			
			Stream stream = new MemoryStream(textAssets.bytes);
			
			//string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, gameLevel);
			
			using(BinaryReader br = new BinaryReader(stream))
			{
				//int length = (int)br.BaseStream.Length;
				
				width = br.ReadInt16();
				height = br.ReadInt16();
				emptySpace = br.ReadByte();
				
				//				star1_Score = br.ReadInt32();
				//				star2_Score = br.ReadInt32();
				//				star3_Score = br.ReadInt32();
				
				//				clearType = br.ReadByte();
				//limitNorm = br.ReadByte();
				//				limitTime = br.ReadInt32();
				//map_maxScore = br.ReadInt32();
				//				Debug.Log("map_maxScore:  "+map_maxScore);
				
				mapdata = (width+2) * (height+2);
				perfectplan = (width+2) * (height+2);
				
				tmpCol = width+2;
				tmpRow = height+2;
				
				
				reSizeMapBuf (tmpRow, tmpCol);
				
				for (int i = 0; i < mapdata; i++)
				{
					map[i/tmpCol][i%tmpCol] = br.ReadByte();
				}
			}
		} 
		catch(IOException ex)
		{
			Debug.Log(ex.Message);
		}
		
	}

	int[][] map;
	public void reSizeMapBuf(int _Row, int _Col)
	{
		
		if (map != null) {
			map = null;
		}
		
		map = new int[_Row][];
		for (int i=0; i<_Row; i++) {
			map [i] = new int[_Col];
		}
		
		for (int i=0; i<_Row; i++) 
		{
			for (int j=0; j<_Col; j++) 
			{
				map [i][j] = new int();
			}
		}
	}

	GameObject[] go_mini;
	private void drawMinimap()
	{
		int gap = 45;

		for(int i=0; i<144; i++)
		{
			go_mini[i] = GameObject.Find("mini"+i);
			go_mini[i].transform.localPosition = new Vector3(-1800, 1800, 0);
		}

		int startX = -((tmpCol/2) * gap);
		int startY = ((tmpRow/2) * gap);

		if(tmpCol %2 == 0)
		{
			startX = -((tmpCol/2) * gap);
		}
		else
		{
			startX = -((tmpCol/2) * gap) - 22;
		}

		if(tmpRow %2 == 0)
		{
			startY = ((tmpRow/2) * gap);
		}
		else
		{
			startY = ((tmpRow/2) * gap) + 22;
		}

		for(int i=0; i<tmpRow; i++)
		{
			for(int j=0; j<tmpCol; j++)
			{
				go_mini[i*tmpCol+j].transform.localPosition = new Vector3(startX+j*gap, startY-(i*gap), 0);
				go_mini[i*tmpCol+j].GetComponent<UISprite>().spriteName = string.Format("NewPipe1{0:D3}", map[i][j]);
			}
		}


	}

	////////////////////////////////////////////////////////////////////////////////////////////////
	private void OnClick_ShowMiniMap0()
	{
		selectIndex = 0;
		moveMiniMap_Popup();
	}

	private void OnClick_ShowMiniMap1()
	{
		selectIndex = 1;
		moveMiniMap_Popup();
	}

	private void OnClick_ShowMiniMap2()
	{
		selectIndex = 2;
		moveMiniMap_Popup();
	}

	private void OnClick_ShowMiniMap3()
	{
		selectIndex = 3;
		moveMiniMap_Popup();
	}

	private void OnClick_ShowMiniMap4()
	{
		selectIndex = 4;
		moveMiniMap_Popup();
	}

	private void OnClick_ShowMiniMap5()
	{
		selectIndex = 5;
		moveMiniMap_Popup();
	}

	private void OnClick_ShowMiniMap6()
	{
		selectIndex = 6;
		moveMiniMap_Popup();
	}

	private void OnClick_ShowMiniMap7()
	{
		selectIndex = 7;
		moveMiniMap_Popup();
	}

	private void OnClick_ShowMiniMap8()
	{
		selectIndex = 8;
		moveMiniMap_Popup();
	}

	private void OnClick_ShowMiniMap9()
	{
		selectIndex = 9;
		moveMiniMap_Popup();
	}

	private void OnClick_ShowMiniMap10()
	{
		selectIndex = 10;
		moveMiniMap_Popup();
	}

	private void OnClick_ShowMiniMap11()
	{
		selectIndex = 11;
		moveMiniMap_Popup();
	}

	private void OnClick_ShowMiniMap12()
	{
		selectIndex = 12;
		moveMiniMap_Popup();
	}

	private void OnClick_ShowMiniMap13()
	{
		selectIndex = 13;
		moveMiniMap_Popup();
	}

	private void OnClick_ShowMiniMap14()
	{
		selectIndex = 14;
		moveMiniMap_Popup();
	}

	private void OnClick_ShowMiniMap15()
	{
		selectIndex = 15;
		moveMiniMap_Popup();
	}

	private void OnClick_ShowMiniMap16()
	{
		selectIndex = 16;
		moveMiniMap_Popup();
	}

	private void OnClick_ShowMiniMap17()
	{
		selectIndex = 17;
		moveMiniMap_Popup();
	}

	private void OnClick_ShowMiniMap18()
	{
		selectIndex = 18;
		moveMiniMap_Popup();
	}

	private void OnClick_ShowMiniMap19()
	{
		selectIndex = 19;
		moveMiniMap_Popup();
	}

	void moveMiniMap_Popup()
	{
		toggleActive(false);

		go_Minimap_popup.transform.localPosition = new Vector3(0, 800, 0);
		//go_Minimap_popup.SetActive(true);

		int mapNum = PlayerData.nowPlanet * 20 + selectIndex;
		//mapNum = 400;
		loadMiniMapData(mapNum);

		GameObject.Find("text_LimitPipeValue").GetComponent<UILabel>().text = pd.LimitNorm[PlayerData.nowPlanet * 20 + selectIndex].ToString();

		//Debug.Log("mapNum: "+(mapNum));
		drawMinimap();

		efm.Play(8);
		
		TweenPosition twPosition = TweenPosition.Begin (go_Minimap_popup, 0.3f, new Vector3(0, 0, 0));
		twPosition.method = UITweener.Method.BounceIn;
		//EventDelegate.Add (twPosition.onFinished, callback_finish_move_minimap_popup, true);
	}
	
	void OnClick_Close_Minimap()
	{
		toggleActive(true);

		efm.Play(0);
		
		TweenPosition twPosition = TweenPosition.Begin (go_Minimap_popup, 0.3f, new Vector3(0, -800, 0));
		twPosition.method = UITweener.Method.BounceOut;

//		TweenPosition twPosition = TweenPosition.Begin (go_HotMenu, 0.3f, new Vector3(0, 0, 0));
//		twPosition.method = UITweener.Method.BounceIn;
//		//EventDelegate.Add (twPosition.onFinished, callback_finish_base, true);
	}



	////////////////////////////////////////////////////////////////////////////////////////////////

}
