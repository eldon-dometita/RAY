using UnityEngine;
using System.Collections;
using System.Globalization;


public class shopPipesControllerScript : MonoBehaviour {

	PlayerData pd;

	GameObject Popup_Panel_Obj;
	GameObject textObj;

	GameObject Popup_Panel_OK_Obj;
	int popupState;

	GameObject[] item;// = GameObject.Find("Item0");
	GameObject[] posCount;
	GameObject[] posMaxCount;
	GameObject[] costLabel;
	GameObject[] cursorObj;
	GameObject totalCostLabel;
	GameObject goldLabel;			//user gold


	UIAtlas uia;

	int btnIndex;

	int[] price = {
		100, 100, 100, 100, 150, 150, 200, 
		250, 250, 250, 250, 250, 250, 250, 250, 
		350, 350, 350, 350, 350, 350, 350, 350
	};

	//int[] tempBuyCount;
	int totalCost;
	
	public UIAtlas atlasSRC;
	public string spriteName = "conveyor&Inventory-2072";

	// buy pop up
	GameObject Popup_Panel_Buy_Obj;
	// pipe image pop up
	GameObject Popup_Pipe_Panel_Obj;
	// pipe container
	GameObject[] itemContainer;

	GameObject scrollBar;

	public EffectSoundManagerScript efm;

	TextScript ts;

	GameObject go_Popup_Help;
	HelpPopupCon helpPopupCon;
	GlobalData gd;

	// Use this for initialization
	void Awake () 
	{

		efm = EffectSoundManagerScript.Instance;

		ts = TextScript.Instance;

		scrollBar = GameObject.Find ("ScrollBar");

		helpPopupCon = GameObject.Find("Popup_Help").GetComponent<HelpPopupCon>();
		//hpc.InitPopup();

		go_Popup_Help = GameObject.Find("Popup_Help");


		GameCon.bgNum = Random.Range (0, 7);
		GameCon.setBgAtlasSet (GameCon.bgNum);
		setBg ();
		
		// buy pop-up
		Popup_Panel_Buy_Obj = GameObject.Find ("Popup_Panel_Buy");
		Popup_Panel_Buy_Obj.SetActive (false);
		
		

		//info popup Initialize.....................................................................
		Popup_Panel_Obj = GameObject.Find ("Popup_Info");
		Popup_Panel_Obj.SetActive (false);

		Popup_Panel_OK_Obj = GameObject.Find ("Popup_Info_OK");
		Popup_Panel_OK_Obj.SetActive (false);
		popupState = 0;	//no popup
		//end info popup Initialize.....................................................................

		pd = PlayerData.Instance;

		btnIndex = 0;

		//tempBuyCount = new int[PlayerData.PIPE_ITEM_MAX] ;
		item = new GameObject[PlayerData.PIPE_ITEM_MAX];
		posCount = new GameObject[PlayerData.PIPE_ITEM_MAX];
		posMaxCount = new GameObject[PlayerData.PIPE_ITEM_MAX];
		costLabel = new GameObject[PlayerData.PIPE_ITEM_MAX];
		cursorObj = new GameObject[PlayerData.PIPE_ITEM_MAX];
		
		itemContainer = new GameObject[PlayerData.PIPE_ITEM_MAX];

		gd = GlobalData.Instance;
		gd.setTileNum(0, true);
		GameCon.setAtlasSet (GlobalData.baseTileNum);
		uia = Resources.Load<UIAtlas> (GameCon.basePipeAtlasPath);

		Vector3 v3pos;
		for (int i=0; i<PlayerData.PIPE_ITEM_MAX; i++) 
		{
			item [i] = GameObject.Find ("Item" + i);

			v3pos = item[i].transform.localPosition;
			v3pos.x = 30 + 390*i;
			item[i].transform.localPosition = v3pos;

			if(i < pd.invenOpenLevel)
			{
				setItemValue(i);
			}
			else
			{
				item [i].SetActive(false);
			}
		}

//		Debug.Log ("pd.invenOpenLevel:"+pd.invenOpenLevel);
//		for (int i=0; i<pd.invenOpenLevel; i++) {
//			setItemValue(i);
//		}

		totalCostLabel = GameObject.Find ("totalCostLabel");
		goldLabel = GameObject.Find ("goldLabel");
	}

	void setBg()
	{

		GameObject go = null;
		for (int i=0; i<9; i++) 
		{
			go = GameObject.Find("BgTile"+i);
			go.GetComponent<UISprite>().spriteName = GameCon.bgName;
		}
		
		UISprite uis = GameObject.Find ("BotBackground").GetComponent<UISprite>();
		uis.width = Screen.width+100;
		
		uis = GameObject.Find ("TopBackground").GetComponent<UISprite>();
		uis.width = Screen.width+100;

		Vector3 v3pos = GameObject.Find ("Anchor_TopRight").transform.localPosition;
		GameObject.Find ("PipeScrollViewPanel").GetComponent<UIPanel> ().baseClipRegion = new Vector4 (0, 0, (v3pos.x*2), 361);
	}

	private void setItemValue(int _index)
	{

		// pipe container
		itemContainer[_index] = item [_index].transform.FindChild ("UISlicedSprite").gameObject;

		//set pipe atlas change...
		itemContainer [_index].transform.FindChild ("Image").GetComponent<UISprite> ().atlas = uia;
		//itemContainer[selected_itemIndex].transform.FindChild("Image").GetComponent<UISprite>().spriteName = ;

		posCount [_index] = itemContainer [_index].transform.FindChild ("text_posCount").gameObject;
		posMaxCount [_index] = itemContainer [_index].transform.FindChild ("text_posMax").gameObject;
		
		posCount[_index].GetComponent<UILabel>().text = pd.pipes[_index].ToString();
		posMaxCount[_index].GetComponent<UILabel>().text = "/"+pd.invenMax.ToString();
		// pipe cost
		
		costLabel[_index] = itemContainer [_index].transform.FindChild ("text_price").gameObject;
		costLabel[_index].GetComponent<UILabel>().text = price[_index].ToString();

		itemContainer [_index].transform.FindChild ("text_pipeName").GetComponent<UILabel>().text = ts.PName[nameIndex[_index]];
	}

	int[] nameIndex = 
	{
		10, 11, 12, 13, 14, 15, 16, 70, 71, 72, 73, 74, 75, 76, 77, 80, 81, 82, 83, 84, 85, 86, 87 
	};

	// Update is called once per frame
	void Update () {
		//refresh_totlaCost ();


		GameObject.Find ("text_gemValue").GetComponent<UILabel>().text = string.Format("{0:N0}", pd.gem);
		GameObject.Find ("text_goldValue").GetComponent<UILabel>().text = string.Format("{0:N0}", pd.gold);
	}




	







	string makeMonetaryNumber(int _value)
	{
		return _value.ToString ("0,0", CultureInfo.InvariantCulture);
	}

	void setCursorPos()
	{
		for (int i=0; i<pd.invenOpenLevel; i++) {
			if(btnIndex == i)
			{
				cursorObj[i].SetActive(true);
			}
			else{
				cursorObj[i].SetActive(false);
			}
		}
	}
	
	void Close_Popup_Buy()
	{
		efm.Play(0);
		// enable pipes
		togglePipeActive(true);
		
		// close buy pop-up
		Popup_Panel_Buy_Obj.SetActive(false);
	}

	void Close_Popup_Info()
	{
		efm.Play(0);
		// enable pipes
		togglePipeActive(true);

		Popup_Panel_Obj.SetActive (false);
		popupState = 0;	//no popup
	}

	void Close_Popup_Info_OK()
	{
		efm.Play(0);

		togglePipeActive(true);
		
		Popup_Panel_OK_Obj.SetActive (false);
		popupState = 0;	//no popup
	}

	void Click_MoveToItemShop()
	{
		if (popupState == 1) //gold is insufficient
		{
			Application.LoadLevel("ShopGold");
		} else if (popupState == 2) //overflow pipe item max
		{
			Application.LoadLevel("ShopConsumeItem");	
		}

	}
	

	void togglePipeActive(bool toggle = false)
	{
		for (int i=0; i<pd.invenOpenLevel; i++) {
			if (toggle)
			{
				GameObject.Find ("Item" + i).GetComponent<BoxCollider>().enabled = true;
			}
			else
			{
				GameObject.Find ("Item" + i).GetComponent<BoxCollider>().enabled = false;
			}
		}

		if (toggle)
		{
			scrollBar.SetActive(true);
		}
		else
		{
			scrollBar.SetActive(false);
		}
	}
	
	int pipeID;
	GameObject moreButton;
	GameObject lessButton;
	
	int tmpBuyCount = 0;
	int selected_itemIndex = 0;

	void setInitPopup()
	{
		totalCost = 0;
		tmpBuyCount = 1;

		GameObject.Find ("text_SelectedPipeName").GetComponent<UILabel>().text = itemContainer[selected_itemIndex].transform.FindChild("text_pipeName").GetComponent<UILabel>().text;

		GameObject.Find ("selected_PipeImg").GetComponent<UISprite>().atlas = uia;
		GameObject.Find ("selected_PipeImg").GetComponent<UISprite>().spriteName = itemContainer[selected_itemIndex].transform.FindChild("Image").GetComponent<UISprite>().spriteName;
		GameObject.Find ("selected_Arrow").GetComponent<UISprite>().spriteName = itemContainer[selected_itemIndex].transform.FindChild("Arrow").GetComponent<UISprite>().spriteName;

		showBuyCount ();
		showTotalCost ();
		showPossession ();

		efm.Play(8);

		TweenPosition twPosition = TweenPosition.Begin(Popup_Panel_Buy_Obj, 0.3f, new Vector3(0, 0, 0));
		twPosition.method = UITweener.Method.BounceIn;
	}

	private void showBuyCount()
	{
		//count
		GameObject.Find ("text_buyQuantity").GetComponent<UILabel> ().text = tmpBuyCount.ToString ();
	}

	private void showTotalCost()
	{
		totalCost = tmpBuyCount * price[selected_itemIndex];
		GameObject.Find ("text_TotalCostValue").GetComponent<UILabel> ().text = totalCost.ToString ();
	}

	private void showPossession()
	{
		GameObject.Find ("text_popup_posMax").GetComponent<UILabel> ().text = "/"+pd.invenMax.ToString ();//itemContainer[_itemIndex].transform.FindChild("text_posMax").GetComponent<UILabel>().text;
		GameObject.Find ("text_popup_posCount").GetComponent<UILabel> ().text = pd.pipes [selected_itemIndex].ToString();//itemContainer[_itemIndex].transform.FindChild("text_posCount").GetComponent<UILabel>().text;
	}

	private void Click_Plus()
	{
		tmpBuyCount++;
		if (tmpBuyCount >= pd.invenMax - pd.pipes [selected_itemIndex]) {
			tmpBuyCount = pd.invenMax - pd.pipes [selected_itemIndex];
		}

		showBuyCount();
		showTotalCost();

	}

	private void Click_Minus()
	{
		tmpBuyCount--;
		if (tmpBuyCount <= 1) 
		{
			tmpBuyCount = 1;
		}
		showBuyCount();
		showTotalCost();
	}

	void setPopupCommon()
	{
		if (pd.invenMax <= pd.pipes [selected_itemIndex]) 
		{
			togglePipeActive ();		// disable pipes
			setPopupText (2);
		} else {
			togglePipeActive ();		// disable pipes
			Popup_Panel_Buy_Obj.transform.localPosition = new Vector3(0, 800, 0);
			Popup_Panel_Buy_Obj.SetActive (true);
			setInitPopup ();
		}
	}

	void Show_Pipe0()
	{
		pipeID = 10;
		selected_itemIndex = 0;
		setPopupCommon ();
	}
	
	void Show_Pipe1()
	{
		pipeID = 11;
		selected_itemIndex = 1;
		setPopupCommon ();
	}

	void Show_Pipe2()
	{
		pipeID = 12;
		selected_itemIndex = 2;
		setPopupCommon ();
	}

	void Show_Pipe3()
	{
		pipeID = 13;
		selected_itemIndex = 3;
		setPopupCommon ();
	}

	void Show_Pipe4()
	{
		pipeID = 14;
		selected_itemIndex = 4;
		setPopupCommon ();
	}

	void Show_Pipe5()
	{
		pipeID = 15;
		selected_itemIndex = 5;
		setPopupCommon ();
	}

	void Show_Pipe6()
	{
		pipeID = 16;
		selected_itemIndex = 6;
		setPopupCommon ();
	}

	void Show_Pipe7()
	{
		pipeID = 70;
		selected_itemIndex = 7;
		setPopupCommon ();
	}

	void Show_Pipe8()
	{
		pipeID = 71;
		selected_itemIndex = 8;
		setPopupCommon ();
	}

	void Show_Pipe9()
	{
		pipeID = 72;
		selected_itemIndex = 9;
		setPopupCommon ();
	}

	void Show_Pipe10()
	{
		pipeID = 73;
		selected_itemIndex = 10;
		setPopupCommon ();
	}





	void Show_Pipe11()
	{
		pipeID = 74;
		selected_itemIndex = 11;
		setPopupCommon ();
	}

	void Show_Pipe12()
	{
		pipeID = 75;
		selected_itemIndex = 12;
		setPopupCommon ();
	}

	void Show_Pipe13()
	{
		pipeID = 76;
		selected_itemIndex = 13;
		setPopupCommon ();
	}

	void Show_Pipe14()
	{
		pipeID = 77;
		selected_itemIndex = 14;
		setPopupCommon ();
	}




	void Show_Pipe15()
	{
		pipeID = 80;
		selected_itemIndex = 15;
		setPopupCommon ();
	}
	
	void Show_Pipe16()
	{
		pipeID = 81;
		selected_itemIndex = 16;
		setPopupCommon ();
	}
	
	void Show_Pipe17()
	{
		pipeID = 82;
		selected_itemIndex = 17;
		setPopupCommon ();
	}
	
	void Show_Pipe18()
	{
		pipeID = 83;
		selected_itemIndex = 18;
		setPopupCommon ();
	}


	void Show_Pipe19()
	{
		pipeID = 84;
		selected_itemIndex = 19;
		setPopupCommon ();
	}
	
	void Show_Pipe20()
	{
		pipeID = 85;
		selected_itemIndex = 20;
		setPopupCommon ();
	}
	
	void Show_Pipe21()
	{
		pipeID = 86;
		selected_itemIndex = 21;
		setPopupCommon ();
	}
	
	void Show_Pipe22()
	{
		pipeID = 87;
		selected_itemIndex = 22;
		setPopupCommon ();
	}



	
	void setPopupText(int _state)
	{
		Popup_Panel_OK_Obj.transform.localPosition = new Vector3(0, 800, 0);

		if (_state == 0) 
		{
			popupState = _state;
			Popup_Panel_Obj.SetActive (false);
			Popup_Panel_OK_Obj.SetActive (false);
		}
		else if(_state == 1)
		{
			popupState = _state;	//gold is insufficient
			Popup_Panel_OK_Obj.SetActive (true);
			Popup_Panel_OK_Obj.transform.FindChild("text_explain").GetComponent<UILabel>().text = "Can't buy item. Gold is [FFAA66]insufficient[-]. \nDo you want [FFAA66]buy gold[-]?";
			GameObject.Find("text_moveToItemShop").GetComponent<UILabel>().text = "Move to\nGold Shop";
		}
		else if(_state == 2)
		{
			popupState = _state;	//gold is insufficient
			Popup_Panel_OK_Obj.SetActive (true);


			Popup_Panel_OK_Obj.transform.FindChild("text_explain").GetComponent<UILabel>().text = "Can't buy this item anymore. \nif you want [FFAA66]buy more[-], you have to expend item space with [FFAA66]expend item[-].\nDo you want [FFAA66]buy expend item[-]?";
			GameObject.Find("text_moveToItemShop").GetComponent<UILabel>().text = "Move to\nItem Shop";
		}

		efm.Play(8);

		TweenPosition twPosition = TweenPosition.Begin(Popup_Panel_OK_Obj, 0.3f, new Vector3(0, 0, 0));
		twPosition.method = UITweener.Method.BounceIn;
	}

	void Click_Buy()//buy
	{
		if (pd.gold >= totalCost) 
		{
			pd.gold -= totalCost;

			pd.pipes[selected_itemIndex] += tmpBuyCount;

			setItemValue(selected_itemIndex);

			pd.saveData();

			Close_Popup_Buy();

		} else {
			setPopupText(1);
		}
	}


	void OnClick_Back()
	{
		efm.Play(0);

		StartCoroutine(LoadAfterDelay("ShopMain"));
		//Application.LoadLevel ("ShopMain");
	}

	IEnumerator LoadAfterDelay(string levelName){
		//yield return new WaitForSeconds(2.5f); // wait 1 seconds
		yield return new WaitForSeconds(efm.audioClip.length); //Waits till Audio is done playing
		Application.LoadLevel(levelName);
		
	}






	void set_Common(int _index)
	{
		GameObject.Find ("text_helpPipeName").GetComponent<UILabel>().text = ts.PName[_index];

		//DisableControls(go_HState[helpState]);
		HelpPopupCon.help_PipeIndex  = _index;
		helpPopupCon.setHelpPipe(HelpPopupCon.help_PipeIndex);
		//helpPopupCon.setHelpPipe(_index);

		if(_index >= 170 && _index <= 173)
		{
			GameObject.Find("text_explain").GetComponent<UILabel>().text = ts.HStr[170];
		}
		else
		{
			GameObject.Find("text_explain").GetComponent<UILabel>().text = ts.HStr[_index];
		}
		
		efm.Play(8);
		
		go_Popup_Help.transform.localPosition = new Vector3(0, 800, 0);
		TweenPosition twPosition = TweenPosition.Begin(go_Popup_Help, 0.3f, new Vector3(0, 0, 0));
		twPosition.method = UITweener.Method.BounceIn;
	}

	void Close_AniPopup()
	{
		efm.Play(0);

		TweenPosition twPosition = TweenPosition.Begin(go_Popup_Help, 0.3f, new Vector3(0, -800, 0));
		twPosition.method = UITweener.Method.BounceIn;
	}

	void Show_PipeAni10(){	set_Common(10);	}
	void Show_PipeAni11(){	set_Common(11);	}
	void Show_PipeAni12(){	set_Common(12);	}
	void Show_PipeAni13(){	set_Common(13);	}
	void Show_PipeAni14(){	set_Common(14);	}
	void Show_PipeAni15(){	set_Common(15);	}
	void Show_PipeAni16(){	set_Common(16);	}
	void Show_PipeAni70(){	set_Common(70);	}
	void Show_PipeAni71(){	set_Common(71);	}
	void Show_PipeAni72(){	set_Common(72);	}
	void Show_PipeAni73(){	set_Common(73);	}
	void Show_PipeAni74(){	set_Common(74);	}
	void Show_PipeAni75(){	set_Common(75);	}
	void Show_PipeAni76(){	set_Common(76);	}
	void Show_PipeAni77(){	set_Common(77);	}
	void Show_PipeAni80(){	set_Common(80);	}
	void Show_PipeAni81(){	set_Common(81);	}
	void Show_PipeAni82(){	set_Common(82);	}
	void Show_PipeAni83(){	set_Common(83);	}
	void Show_PipeAni84(){	set_Common(84);	}
	void Show_PipeAni85(){	set_Common(85);	}
	void Show_PipeAni86(){	set_Common(86);	}
	void Show_PipeAni87(){	set_Common(87);	}

}
