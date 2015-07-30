using UnityEngine;
using System.Collections;
using System.Globalization;

public class ShopConsumeItemController : MonoBehaviour {

	PlayerData pd;
	
	
	GameObject Popup_Panel_Obj;



	int popupState;
	
	int selected_itemIndex;
	
	GameObject[] item;// = GameObject.Find("Item0");
	GameObject[] itemContainer;
	GameObject[] itemPlate;
//	GameObject[] tempLabel;
//	GameObject[] cursorObj;
//	int[] tempCount;
//	int tempCost;
//	UILabel explainLabel;
	
	// buy pop up
	GameObject Popup_Panel_Buy_Obj;
	GameObject Popup_Panel_Buy5_Obj;
	GameObject Popup_Panel_Buy1_Obj;
	GameObject Popup_Panel_Buy2_Obj;
	GameObject Popup_Panel_Buy3_Obj;
	GameObject Popup_Panel_Buy4_Obj;
	GameObject go_timeBase6Hour;
	GameObject go_timeBase24Hour;
	GameObject Popup_Panel_OK_Obj;


	// pipe image pop up
	GameObject Popup_Pipe_Panel_Obj;
	// pipe container

	
	//GameObject cursorObj;
	//GameObject scrollViewPanObj;
	
	public UIAtlas atlasSRC;
	public string spriteName = "conveyor&Inventory-2072";
	
	int[] price = {
		1000, 	//gem
		1000, 	//gold
		500, 	//gem
		50, 	//gem
		1000, 	//gold
		1000,	//gold
		2000, 	//gold
		100, 	//gem
		200, 	//gem
		500		//gem

	};

	int[] itemMax = 
	{
		99, //water time max
		99, //conveyor booster	
		99, //inven box	
		99, //inven booster	
		99, //perfect plan
		99,	//turn on water
		99, //water time extention
		0, 
		0, 
		0 

	};

	int[] priceWaterTimeMax = 
	{
		500, //120sec -> 150sec
		1000, //150sec	-> 180sec
		2000, //180sec	-> 210sec
		4000, //210sec	->240sec
		8000, //240sec	-> 270sec
		16000, //270sec	->300sec
	};

	int[] priceInvenMax = 
	{
		500, 1000, 1500, 2000, 2500, 
		3000, 3500, 4000, 4500, 5000, 
		5500, 6000, 6500, 7000, 7500, 
		8000, 8500, 9000, 9500, 10000 
	};

	UIAtlas uia;

	GameObject scrollBar;

	public EffectSoundManagerScript efm;

	// Use this for initialization
	void Awake () 
	{
		efm = EffectSoundManagerScript.Instance;

		scrollBar = GameObject.Find ("Scroll Bar");

//		GoldImg = GameObject.Find ("GoldImg");
//		GemImg = GameObject.Find ("GemImg");

		uia = Resources.Load<UIAtlas> ("Atlases/Shop");


		go_timeBase6Hour = GameObject.Find ("TimeBase6Hour");
		go_timeBase6Hour.SetActive (false);
		go_timeBase24Hour = GameObject.Find ("TimeBase24Hour");
		go_timeBase24Hour.SetActive (false);


		GameCon.bgNum = Random.Range (0, 7);
		GameCon.setBgAtlasSet (GameCon.bgNum);
		setBg ();

		// buy pop-up
		Popup_Panel_Buy_Obj = GameObject.Find ("Popup_Panel_Buy");
		Popup_Panel_Buy_Obj.SetActive (false);

		Popup_Panel_Buy5_Obj = GameObject.Find ("Popup_Panel_Buy5");
		Popup_Panel_Buy5_Obj.SetActive (false);

		Popup_Panel_Buy1_Obj = GameObject.Find ("Popup_Panel_Buy1");
		Popup_Panel_Buy1_Obj.SetActive (false);

		Popup_Panel_Buy2_Obj = GameObject.Find ("Popup_Panel_Buy2");
		Popup_Panel_Buy2_Obj.SetActive (false);

		Popup_Panel_Buy3_Obj = GameObject.Find ("Popup_Panel_Buy3");
		Popup_Panel_Buy3_Obj.SetActive (false);

		Popup_Panel_Buy4_Obj = GameObject.Find ("Popup_Panel_Buy4");
		Popup_Panel_Buy4_Obj.SetActive (false);

		Popup_Panel_OK_Obj = GameObject.Find ("Popup_Info_OK");
		Popup_Panel_OK_Obj.SetActive (false);
		
		pd = PlayerData.Instance;

		selected_itemIndex = 0;
		
		//Popup_Panel_Obj = GameObject.Find ("Popup_Panel");
		//infoTextLabel_Obj = Popup_Panel_Obj.transform.FindChild ("infoTextLabel").gameObject;
		//Popup_Panel_Obj.SetActive (false);
		popupState = 0;	//no popup
		
		//Popup_Panel_OK_Obj = GameObject.Find ("Popup_Panel_OK");
		//infoTextLabel_OK_Obj = Popup_Panel_OK_Obj.transform.FindChild ("infoTextLabel").gameObject;
		//Popup_Panel_OK_Obj.SetActive (false);
		

		item = new GameObject[PlayerData.CONSUME_ITEM_MAX];
		itemContainer = new GameObject[PlayerData.CONSUME_ITEM_MAX];
		itemPlate = new GameObject[PlayerData.CONSUME_ITEM_MAX]; 

//		costLabel = new GameObject[PlayerData.CONSUME_ITEM_MAX];
//		posLabel = new GameObject[PlayerData.CONSUME_ITEM_MAX];
//		tempLabel = new GameObject[PlayerData.CONSUME_ITEM_MAX];
//		cursorObj = new GameObject[PlayerData.CONSUME_ITEM_MAX];
		

		Vector3 v3pos;
		for (int i=0; i<PlayerData.CONSUME_ITEM_MAX; i++) 
		{
			item [i] = GameObject.Find ("Item" + i);

			v3pos = item[i].transform.localPosition;
			v3pos.x = 30 + 390*i;
			item[i].transform.localPosition = v3pos;

			setItemValue(i);
		}
		
//		tempCount = new int[PlayerData.CONSUME_ITEM_MAX];
//
//		for (int i=0; i<PlayerData.CONSUME_ITEM_MAX; i++) {
//			tempCount[i] = 0;
//				}
//
//		tempCost = 0;
	}

	private void setItemValue(int _index)
	{
		//item [_index] = GameObject.Find ("Item" + _index);
		
		itemContainer[_index] = item [_index].transform.FindChild ("UISlicedSprite").gameObject;
		itemPlate[_index] = itemContainer[_index].transform.FindChild("Plate").gameObject;
		
		setPossession(_index);
		setPriceValue (_index);

//		tempLabel[_index] = itemContainer[_index].transform.FindChild("tempLabel").gameObject;
	}

	private void setPriceValue(int _index)
	{
		switch(_index)
		{
		case 0:
			if(pd.waterTimePriceIndex > 5)
			{
				itemContainer [_index].transform.FindChild ("IconImg").GetComponent<UISprite> ().spriteName = "empty";
				itemContainer [_index].transform.FindChild ("text_price").GetComponent<UILabel> ().text = " ";
				itemContainer [_index].transform.FindChild ("text_SoldOut").GetComponent<UILabel> ().text = "SOLD OUT!";
			}
			else
			{
				if(pd.waterTimePriceIndex == 5)
				{
					itemContainer [_index].transform.FindChild ("text_price").GetComponent<UILabel> ().text = "16K";
				}
				else
				{
					itemContainer [_index].transform.FindChild ("text_price").GetComponent<UILabel> ().text = priceWaterTimeMax [pd.waterTimePriceIndex].ToString ();
				}
			}
			break;
		
		case 2:
			if(pd.invenMax >= 99)
			{
				itemContainer [_index].transform.FindChild ("IconImg").GetComponent<UISprite> ().spriteName = "empty";
				itemContainer [_index].transform.FindChild ("text_price").GetComponent<UILabel> ().text = " ";
				itemContainer [_index].transform.FindChild ("text_SoldOut").GetComponent<UILabel> ().text = "SOLD OUT!";
			}
			else
			{
				if(priceInvenMax[ pd.invenMax/5] /1000 >= 10)
				{
					itemContainer [_index].transform.FindChild ("text_price").GetComponent<UILabel> ().text = (priceInvenMax[ pd.invenMax/5] /1000)+"K";
				}
				else
				{
					itemContainer [_index].transform.FindChild ("text_price").GetComponent<UILabel> ().text = priceInvenMax[ pd.invenMax/5].ToString();
				}
			}
			break;

		case 1:
		case 3:
		case 4:
		case 6:
		case 5:
			if(pd.consumeItem[_index] >= 99)
			{
				itemContainer [_index].transform.FindChild ("IconImg").GetComponent<UISprite> ().spriteName = "empty";
				itemContainer [_index].transform.FindChild ("text_price").GetComponent<UILabel> ().text = " ";

				itemContainer [_index].transform.FindChild ("text_Max").GetComponent<UILabel> ().text = "MAX VALUE!";
			}
			else
			{
				itemContainer [_index].transform.FindChild ("text_price").GetComponent<UILabel> ().text = price [_index].ToString ();
			}
			break;
			
		case 7:
			if(pd.life >= 99)
			{
				itemContainer [_index].transform.FindChild ("IconImg").GetComponent<UISprite> ().spriteName = "empty";
				itemContainer [_index].transform.FindChild ("text_price").GetComponent<UILabel> ().text = " ";
				itemContainer [_index].transform.FindChild ("text_Max").GetComponent<UILabel> ().text = "MAX VALUE!";
			}
			else
			{
				itemContainer [_index].transform.FindChild ("text_price").GetComponent<UILabel> ().text = price [_index].ToString ();
			}
			break;

		case 8:
			if(pd.bUnlimitTime == 1)
			{
				itemContainer [_index].transform.FindChild ("IconImg").GetComponent<UISprite> ().spriteName = "empty";
				itemContainer [_index].transform.FindChild ("text_price").GetComponent<UILabel> ().text = " ";
				

				go_timeBase6Hour.SetActive (true);
				go_timeBase24Hour.SetActive (false);

				itemContainer [_index].transform.FindChild ("text_CoolTime6Hour").GetComponent<UILabel> ().text = "Cool Time!";
			}
			else if(pd.bUnlimitTime == 2)
			{
				itemContainer [_index].transform.FindChild ("IconImg").GetComponent<UISprite> ().spriteName = "empty";
				itemContainer [_index].transform.FindChild ("text_price").GetComponent<UILabel> ().text = " ";
				
				itemContainer [_index].transform.FindChild ("text_CoolTime6Hour").GetComponent<UILabel> ().text = "Cool Time!";
			}
			else
			{
				go_timeBase6Hour.SetActive (false);
				go_timeBase24Hour.SetActive (false);
				
				itemContainer [_index].transform.FindChild ("text_CoolTime6Hour").GetComponent<UILabel> ().text = " ";

				itemContainer [_index].transform.FindChild ("IconImg").GetComponent<UISprite> ().spriteName = "Shop004";
				itemContainer [_index].transform.FindChild ("text_price").GetComponent<UILabel> ().text = price [_index].ToString ();
			}
			break;

		case 9:
			if(pd.bUnlimitTime == 1)
			{
				itemContainer [_index].transform.FindChild ("IconImg").GetComponent<UISprite> ().spriteName = "empty";
				itemContainer [_index].transform.FindChild ("text_price").GetComponent<UILabel> ().text = " ";

				itemContainer [_index].transform.FindChild ("text_CoolTime24Hour").GetComponent<UILabel> ().text = "Cool Time!";
			}
			else if(pd.bUnlimitTime == 2)
			{
				itemContainer [_index].transform.FindChild ("IconImg").GetComponent<UISprite> ().spriteName = "empty";
				itemContainer [_index].transform.FindChild ("text_price").GetComponent<UILabel> ().text = " ";

				go_timeBase6Hour.SetActive (false);
				go_timeBase24Hour.SetActive (true);

				itemContainer [_index].transform.FindChild ("text_CoolTime24Hour").GetComponent<UILabel> ().text = "Cool Time!";
			}
			else
			{
				go_timeBase6Hour.SetActive (false);
				go_timeBase24Hour.SetActive (false);
				
				itemContainer [_index].transform.FindChild ("text_CoolTime24Hour").GetComponent<UILabel> ().text = " ";

				itemContainer [_index].transform.FindChild ("IconImg").GetComponent<UISprite> ().spriteName = "Shop004";
				itemContainer [_index].transform.FindChild ("text_price").GetComponent<UILabel> ().text = price [_index].ToString ();
			}
			break;
			
		}
	}

	void updateUnlimitTime()
	{

		if(pd.bUnlimitTime == 1)
		{
			go_timeBase6Hour.transform.FindChild("text_unlimitTime").GetComponent<UILabel>().text = getRemainUnlimitedTime(GameThread.second_6hour);
		}
		else if(pd.bUnlimitTime == 2)
		{
			go_timeBase24Hour.transform.FindChild("text_unlimitTime").GetComponent<UILabel>().text = getRemainUnlimitedTime(GameThread.second_24hour);
		}
		else if(pd.bUnlimitTime == 3)
		{
			pd.bUnlimitTime = 0;
			setItemValue(7);
			setItemValue(8);
		}

	}

	string getRemainUnlimitedTime(long _time)
	{
		string returnString = "00:00:00";
		long tmpTime = (_time - GameThread.runTime);
		if (tmpTime < 0) 
		{
			return returnString;
		}
		int hour = (int)(tmpTime / 3600);
		int minute = (int)((tmpTime % 3600)/60);
		int second = (int)(tmpTime % 60);
		returnString = string.Format("{0:D2}:", hour) + string.Format("{0:D2}:", minute)+string.Format("{0:D2}", second);

		return returnString;
	}


	void setPossession(int _index)
	{
		switch(_index)
		{
		case 0:
			itemContainer [_index].transform.FindChild ("text_posCount").GetComponent<UILabel> ().text = string.Format("{0:D2}", (int)(pd.flowDefaultTime/60)) + ":"+string.Format("{0:D2}", (int)(pd.flowDefaultTime%60));
			break;

		case 2:
			itemContainer [_index].transform.FindChild ("text_posCount").GetComponent<UILabel> ().text = string.Format("{0:D}", (pd.invenMax));
			break;

		case 1:
		case 3:
		case 4:
		case 5:
		case 6:
			itemContainer [_index].transform.FindChild ("text_posCount").GetComponent<UILabel> ().text = pd.consumeItem [_index].ToString ();
			itemContainer [_index].transform.FindChild ("text_posMax").GetComponent<UILabel> ().text = "/"+itemMax[_index].ToString();
			break;

		case 7:
			itemContainer [_index].transform.FindChild ("text_posCount").GetComponent<UILabel> ().text = pd.life.ToString();
			itemContainer [_index].transform.FindChild ("text_posMax").GetComponent<UILabel> ().text = " ";
			break;

		case 8:
		case 9:
			itemContainer [_index].transform.FindChild ("text_posCount").GetComponent<UILabel> ().text = " ";
			itemContainer [_index].transform.FindChild ("text_posMax").GetComponent<UILabel> ().text = " ";
			break;

		}

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

	void Close_Popup_Buy()
	{
		efm.Play(0);
		// enable pipes
		togglePipeActive(true);
		
		// close buy pop-up
		Popup_Panel_Buy_Obj.SetActive(false);
	}

	void Close_Popup_Info_OK()
	{
		efm.Play(0);
		// enable pipes
		togglePipeActive(true);
		
		// close buy pop-up
		Popup_Panel_OK_Obj.SetActive(false);
	}

	void Close_Popup_Buy1()
	{
		efm.Play(0);
		// enable pipes
		togglePipeActive(true);
		
		// close buy pop-up
		Popup_Panel_Buy1_Obj.SetActive(false);
	}

	void Close_Popup_Buy2()
	{
		efm.Play(0);
		// enable pipes
		togglePipeActive(true);
		
		// close buy pop-up
		Popup_Panel_Buy2_Obj.SetActive(false);
	}

	void Close_Popup_Buy3()
	{
		efm.Play(0);
		// enable pipes
		togglePipeActive(true);
		
		// close buy pop-up
		Popup_Panel_Buy3_Obj.SetActive(false);
	}

	void Close_Popup_Buy4()
	{
		efm.Play(0);
		// enable pipes
		togglePipeActive(true);
		
		// close buy pop-up
		Popup_Panel_Buy4_Obj.SetActive(false);
	}

	void Close_Popup_Buy5()
	{
		efm.Play(0);
		// enable pipes
		togglePipeActive(true);
		
		// close buy pop-up
		Popup_Panel_Buy5_Obj.SetActive(false);
	}

		
	// Update is called once per frame
	void Update () 
	{
		GameObject.Find ("text_gemValue").GetComponent<UILabel>().text = string.Format("{0:N0}", pd.gem);
		GameObject.Find ("text_goldValue").GetComponent<UILabel>().text = string.Format("{0:N0}", pd.gold);

		updateUnlimitTime ();
	}

	void togglePipeActive(bool toggle = false)
	{
		for (int i=0; i<PlayerData.CONSUME_ITEM_MAX; i++) {
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


	int tmpBuyCount = 0;
	int totalCost = 0;
//	GameObject GoldImg = null;
//	GameObject GemImg = null;
	void setInitPopup()
	{
		totalCost = 0;
		tmpBuyCount = 1;
		
		GameObject.Find ("text_SelectedPipeName").GetComponent<UILabel>().text = itemContainer[selected_itemIndex].transform.FindChild("text_itemName").GetComponent<UILabel>().text;
		GameObject.Find ("selected_itemImg").GetComponent<UISprite>().spriteName = itemPlate[selected_itemIndex].transform.FindChild("Image").GetComponent<UISprite>().spriteName;
		GameObject.Find ("selected_itemImg").GetComponent<UISprite>().width = itemPlate[selected_itemIndex].transform.FindChild("Image").GetComponent<UISprite>().width;
		GameObject.Find ("selected_itemImg").GetComponent<UISprite>().height = itemPlate[selected_itemIndex].transform.FindChild("Image").GetComponent<UISprite>().height;
		GameObject.Find ("selected_text_Amount").GetComponent<UILabel>().text = itemPlate[selected_itemIndex].transform.FindChild("text_Amount").GetComponent<UILabel>().text;

		showBuyCount ();
		showTotalCost ();
		showPossession ();



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
		GameObject.Find ("text_popup_posMax").GetComponent<UILabel> ().text = "/"+itemMax[selected_itemIndex].ToString ();//itemContainer[_itemIndex].transform.FindChild("text_posMax").GetComponent<UILabel>().text;
		GameObject.Find ("text_popup_posCount").GetComponent<UILabel> ().text = pd.consumeItem [selected_itemIndex].ToString();//itemContainer[_itemIndex].transform.FindChild("text_posCount").GetComponent<UILabel>().text;
	}





	
	int itemID;
	GameObject moreButton;
	GameObject lessButton;
	
	void setPopUpBuyPanel(int itemID, int no)
	{
		// pipe image pop-up
		Popup_Pipe_Panel_Obj = GameObject.Find ("PipeImg");
		moreButton = GameObject.Find ("More");
		lessButton = GameObject.Find ("Less");
		
		if (Popup_Pipe_Panel_Obj)
		{
			UISprite pipeImg = Popup_Pipe_Panel_Obj.GetComponent<UISprite>();
			UIButtonMessage more = moreButton.GetComponent<UIButtonMessage>();
			UIButtonMessage less = lessButton.GetComponent<UIButtonMessage>();
			
			pipeImg.spriteName = "Shop0" + itemID;
			more.functionName = "Click_Item"+no+"Plus";
			less.functionName = "Click_Item"+no+"Minus";
			
//			if (GameObject.Find("pop_up_possession"))
				//textToIMG(pd.consumeItem[no], GameObject.Find("pop_up_possession"), 23);
		}
	}
	
	void changeIMG(string find, string sname, int width, int height)
	{
		GameObject.Find(find).GetComponent<UISprite>().spriteName = sname;
		GameObject.Find(find).GetComponent<UISprite>().width = width;
		GameObject.Find(find).GetComponent<UISprite>().height = height;
	}

	//for gem
	void setPopupCommon()
	{
		if (itemMax[selected_itemIndex] <= pd.consumeItem [selected_itemIndex]) 
		{
			togglePipeActive ();		// disable pipes
			//setPopupText (2);
		} else {
			togglePipeActive ();		// disable pipes
			Popup_Panel_Buy_Obj.transform.localPosition = new Vector3(0, 800, 0);
			Popup_Panel_Buy_Obj.SetActive (true);
			setInitPopup ();

			efm.Play(8);

			TweenPosition twPosition = TweenPosition.Begin(Popup_Panel_Buy_Obj, 0.3f, new Vector3(0, 0, 0));
			twPosition.method = UITweener.Method.BounceIn;
		}
	}

	//for gold
	void setPopupCommon_gold()
	{
		if (itemMax[selected_itemIndex] <= pd.consumeItem [selected_itemIndex]) 
		{
			togglePipeActive ();		// disable pipes
			//setPopupText (2);
		} else {
			togglePipeActive ();		// disable pipes
			Popup_Panel_Buy5_Obj.transform.localPosition = new Vector3(0, 800, 0);
			Popup_Panel_Buy5_Obj.SetActive (true);
			setInitPopup ();

			efm.Play(8);

			TweenPosition twPosition = TweenPosition.Begin(Popup_Panel_Buy5_Obj, 0.3f, new Vector3(0, 0, 0));
			twPosition.method = UITweener.Method.BounceIn;
		}
	}




	void setWaterFlowTimeMaxPopup()
	{
		togglePipeActive ();		// disable pipes
		Popup_Panel_Buy1_Obj.transform.localPosition = new Vector3(0, 800, 0);

		Popup_Panel_Buy1_Obj.SetActive (true);
		int minute = (int)(pd.flowTime / 60);
		int second = (int)(pd.flowTime % 60);
		GameObject.Find ("text_beforeTime").GetComponent<UILabel> ().text = "Before " + string.Format("{0:D2}", minute) + " : "+string.Format("{0:D2}", second);

		minute = (int)((pd.flowTime + 30) / 60);
		second = (int)((pd.flowTime + 30) % 60);

		GameObject.Find ("text_afterTime").GetComponent<UILabel> ().text = "After [00FF00]" + string.Format("{0:D2}", minute) + " : "+string.Format("{0:D2}", second)+"[-]";
		totalCost = priceWaterTimeMax[pd.waterTimePriceIndex];
		GameObject.Find ("text_TotalCostValue_maxTime").GetComponent<UILabel> ().text = totalCost.ToString ();

		efm.Play(8);

		TweenPosition twPosition = TweenPosition.Begin(Popup_Panel_Buy1_Obj, 0.3f, new Vector3(0, 0, 0));
		twPosition.method = UITweener.Method.BounceIn;
	}


	int tmpAfterInvenMax = 0;
	void setInventoryBoxPopup()
	{
		togglePipeActive ();		// disable pipes
		Popup_Panel_Buy2_Obj.transform.localPosition = new Vector3(0, 800, 0);
		Popup_Panel_Buy2_Obj.SetActive (true);

		GameObject.Find ("text_beforeCapacity").GetComponent<UILabel> ().text = "Before capacity " + string.Format("{0:D2}", pd.invenMax);

		tmpAfterInvenMax = pd.invenMax + 5;
		if (tmpAfterInvenMax >= 99) 
		{
			tmpAfterInvenMax = 99;
		}
		GameObject.Find ("text_afterCapacity").GetComponent<UILabel> ().text = "After Capacity [00FF00]" +string.Format("{0:D2}", tmpAfterInvenMax)+"[-]";
		totalCost = priceInvenMax[pd.invenMax/5];
		GameObject.Find ("text_TotalCostValue_capacity").GetComponent<UILabel> ().text = totalCost.ToString ();

		efm.Play(8);

		TweenPosition twPosition = TweenPosition.Begin(Popup_Panel_Buy2_Obj, 0.3f, new Vector3(0, 0, 0));
		twPosition.method = UITweener.Method.BounceIn;
	}

	int tmpAfterLife = 0;
	void setLifePopup()
	{
		togglePipeActive ();		// disable pipes
		Popup_Panel_Buy3_Obj.transform.localPosition = new Vector3(0, 800, 0);
		Popup_Panel_Buy3_Obj.SetActive (true);
		
		GameObject.Find ("text_beforeLife").GetComponent<UILabel> ().text = "Before " + string.Format("{0:D2}", pd.life);
		
		tmpAfterLife = pd.life + 5;
		if (tmpAfterLife >= 99) 
		{
			tmpAfterLife = 99;
		}
		GameObject.Find ("text_afterLife").GetComponent<UILabel> ().text = "After [00FF00]" +string.Format("{0:D2}", tmpAfterLife)+"[-]";
		totalCost = price[selected_itemIndex];
		GameObject.Find ("text_TotalCostValue_life").GetComponent<UILabel> ().text = totalCost.ToString ();

		efm.Play(8);

		TweenPosition twPosition = TweenPosition.Begin(Popup_Panel_Buy3_Obj, 0.3f, new Vector3(0, 0, 0));
		twPosition.method = UITweener.Method.BounceIn;
	}

	void setLifeUnlimitPopup()
	{
		togglePipeActive ();		// disable pipes
		Popup_Panel_Buy4_Obj.transform.localPosition = new Vector3(0, 800, 0);
		Popup_Panel_Buy4_Obj.SetActive (true);

		string strTime = "06:00:00";
		if (selected_itemIndex == 9) 
		{
			strTime = "24:00:00";
		} 
		GameObject.Find ("text_unlimitTime").GetComponent<UILabel> ().text = strTime;
		totalCost = price[selected_itemIndex];
		GameObject.Find ("text_TotalCostValue_unlimitLife").GetComponent<UILabel> ().text = totalCost.ToString ();

		efm.Play(8);

		TweenPosition twPosition = TweenPosition.Begin(Popup_Panel_Buy4_Obj, 0.3f, new Vector3(0, 0, 0));
		twPosition.method = UITweener.Method.BounceIn;
	}

	void Show_Item0()
	{
		if (pd.waterTimePriceIndex < 6) 
		{
			selected_itemIndex = 0;
			setWaterFlowTimeMaxPopup ();
		}
	}

	void Show_Item1()
	{
		if (pd.consumeItem [1] < itemMax [1]) 
		{
			selected_itemIndex = 1;
			setPopupCommon_gold ();
		}
	}

	void Show_Item2()
	{
		if (pd.invenMax < 99) 
		{
			selected_itemIndex = 2;
			setInventoryBoxPopup ();
		}
	}

	void Show_Item3()
	{
		if (pd.consumeItem [3] < itemMax [3]) 
		{
			selected_itemIndex = 3;
			setPopupCommon ();
		}
	}

	void Show_Item4()
	{
		if (pd.consumeItem[4] < itemMax[4]) 
		{
			selected_itemIndex = 4;
			setPopupCommon_gold ();
		}
	}

	void Show_Item5()
	{
		if (pd.consumeItem [5] < itemMax[5]) 
		{
			selected_itemIndex = 5;
			setPopupCommon_gold ();
		}
	}

	void Show_Item6()
	{
		if (pd.consumeItem [6] < itemMax[6]) 
		{
			selected_itemIndex = 6;
			setPopupCommon_gold ();
		}
	}

	void Show_Item7()
	{
		if (pd.life < 99) 
		{
			selected_itemIndex = 7;
			setLifePopup ();
		}
	}

	void Show_Item8()
	{
		if (pd.bUnlimitTime == 0) 
		{
			selected_itemIndex = 8;
			setLifeUnlimitPopup ();
		}
	}

	void Show_Item9()
	{
		if (pd.bUnlimitTime == 0) 
		{
			selected_itemIndex = 9;
			setLifeUnlimitPopup ();
		}
	}



	private void Click_Plus()
	{
		tmpBuyCount++;
		if (tmpBuyCount >= itemMax[selected_itemIndex] - pd.consumeItem [selected_itemIndex]) {
			tmpBuyCount = itemMax[selected_itemIndex] - pd.consumeItem [selected_itemIndex];
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

	void Click_Buy()//buy
	{
		switch (selected_itemIndex) 
		{
		case 0:
			if (pd.gem >= totalCost) 
			{
				pd.gem -= totalCost;

				pd.waterTimePriceIndex++;
				pd.flowDefaultTime += 30;

//				if(pd.waterTimePriceIndex > 5)
//				{
//					//itemContainer [_index].transform.FindChild ("text_posCount").GetComponent<UILabel> ().text = string.Format("{0:D2}", (pd.flowTime/60)) + ":"+string.Format("{0:D2}", (pd.flowTime%60));
//				}
//				else
//				{
//					setItemValue(selected_itemIndex);
//				}

				setItemValue(selected_itemIndex);

				pd.saveData();
				
				Close_Popup_Buy1();

			} else {
				setPopupText(11);
			}
			break;

		case 2:
			if (pd.gem >= totalCost) 
			{
				pd.gem -= totalCost;
				
				pd.invenMax = tmpAfterInvenMax;


				setItemValue(selected_itemIndex);
				
				pd.saveData();
				
				Close_Popup_Buy2();
				
			} else {
				setPopupText(11);
			}
			break;

		case 1:
		case 4:
		case 5:
		case 6:
			if (pd.gold >= totalCost) 
			{
				pd.gold -= totalCost;
				
				pd.consumeItem[selected_itemIndex] += tmpBuyCount;
				
				setItemValue(selected_itemIndex);
				
				pd.saveData();
				
				Close_Popup_Buy5();
				
			} else {
				setPopupText(10);
			}
			break;

		case 3:
			if (pd.gem >= totalCost) 
			{
				pd.gem -= totalCost;
				
				pd.consumeItem[selected_itemIndex] += tmpBuyCount;
				
				setItemValue(selected_itemIndex);
				
				pd.saveData();
				
				Close_Popup_Buy();
				
			} else {
				setPopupText(11);
			}
			break;

		case 7:
			if (pd.gem >= totalCost) 
			{
				pd.gem -= totalCost;
				
				pd.life = tmpAfterLife;

				setItemValue(selected_itemIndex);
				
				pd.saveData();
				
				Close_Popup_Buy3();
				
			} else {
				setPopupText(11);
			}
			break;

		case 8:
			if (pd.gem >= totalCost) 
			{
				pd.gem -= totalCost;
				
				pd.bUnlimitTime = 1;
				pd.saveUnlimitTime = System.DateTime.UtcNow.ToFileTimeUtc();
				
				setItemValue(selected_itemIndex);
				setItemValue(8);
				
				pd.saveData();
				
				Close_Popup_Buy4();
				
			} else {
				setPopupText(11);
			}
			break;

		case 9:
			if (pd.gem >= totalCost) 
			{
				pd.gem -= totalCost;
				
				pd.bUnlimitTime = 2;
				pd.saveUnlimitTime = System.DateTime.UtcNow.ToFileTimeUtc();

				setItemValue(7);
				setItemValue(selected_itemIndex);
				
				pd.saveData();
				
				Close_Popup_Buy4();
				
			} else {
				setPopupText(11);
			}
			break;
		}

	}

	void Click_MoveToItemShop()
	{
		if (popupState == 10) //gold is insufficient
		{
			ShopGoldController.startPosition = 1;
			Application.LoadLevel("ShopGold");
		}
		else if (popupState == 11) //gold is insufficient
		{
			ShopGoldController.startPosition = 0;
			Application.LoadLevel("ShopGold");
		}
		
	}


	
//	void setExplainText(int _index)
//	{
//		if (_index == 0) 
//		{
//			explainLabel.text = "Hint item set a pipe correctly!";
//		} 
//		else if (_index == 1) 
//		{
//			explainLabel.text = "Perfect Plan item show how to use all space during 30 second";
//		}
//		else if (_index == 2) 
//		{
//			explainLabel.text = "Vender Booster item set to 0.5 second cooltime.";
//		}
//		else if (_index == 3) 
//		{
//			explainLabel.text = "Time expand item increase 30 second.";
//		}
//	}
	
	void setPopupText(int _state)
	{
		Popup_Panel_Buy_Obj.SetActive (false);
		Popup_Panel_Buy5_Obj.SetActive (false);
		Popup_Panel_Buy1_Obj.SetActive (false);
		Popup_Panel_Buy2_Obj.SetActive (false);
		Popup_Panel_Buy3_Obj.SetActive (false);
		Popup_Panel_Buy4_Obj.SetActive (false);
		Popup_Panel_OK_Obj.SetActive (false);

		Popup_Panel_OK_Obj.transform.localPosition = new Vector3(0, 800, 0);

		if (_state == 0) 
		{
			popupState = _state;
		}
		else if(_state == 10)
		{
			popupState = _state;	//gold is insufficient
			Popup_Panel_OK_Obj.SetActive (true);
			Popup_Panel_OK_Obj.transform.FindChild("text_explain").GetComponent<UILabel>().text = "Can't buy item. Gold is [FFAA66]insufficient[-]. \nDo you want [FFAA66]buy Gold[-]?";
			GameObject.Find("text_moveToItemShop").GetComponent<UILabel>().text = "Move to\nGold Shop";
		}
		else if(_state == 11)
		{
			popupState = _state;	//gold is insufficient
			Popup_Panel_OK_Obj.SetActive (true);
			Popup_Panel_OK_Obj.transform.FindChild("text_explain").GetComponent<UILabel>().text = "Can't buy item. Gem is [FFAA66]insufficient[-]. \nDo you want [FFAA66]buy Gem[-]?";
			GameObject.Find("text_moveToItemShop").GetComponent<UILabel>().text = "Move to\nGem Shop";
		}

		efm.Play(8);

		TweenPosition twPosition = TweenPosition.Begin(Popup_Panel_OK_Obj, 0.3f, new Vector3(0, 0, 0));
		twPosition.method = UITweener.Method.BounceIn;
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

}
