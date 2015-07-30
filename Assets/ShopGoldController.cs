using UnityEngine;
using System.Collections;
using System.Globalization;

public class ShopGoldController : MonoBehaviour {

	public static int startPosition = 0;

	//const int ITEM_MAX = 4;
	PlayerData pd;

	int selected_itemIndex;
	int popupState;
	GameObject Popup_Panel_OK_Obj;
	//GameObject infoTextLabel_Obj;

	GameObject[] item = null;
	GameObject[] itemContainer = null;

	GameObject PipeScrollViewPanelObj = null;
	GameObject scrollBar;

	float[] price = 
	{
		//0.45f, 1.32f, 2.49f, 4.79f, 
		0.99f, 1.99f, 2.99f, 4.99f, 
		200f, 400f, 2000f, 4000f
	};

	int[] give_gem = {
		300, 
		700, 
		1500, 
		3500, 
	};
	
	int[] give_gold = {
		0, 0, 0, 0, 
		10000, 
		30000, 
		100000, 
		300000, 
	};
	
	int[] price_gold = {
		200, 
		400, 
		2000, 
		4000, 
	};
	
	public UIAtlas atlasSRC;
	public string spriteName = "conveyor&Inventory-2072";

	public EffectSoundManagerScript efm;
	
	//	GameObject[] prefab_pipes;
	//	bool[] bLoadPrefab_pipes;
	
	
	// Use this for initialization
	void Awake () {

		efm = EffectSoundManagerScript.Instance;

		scrollBar = GameObject.Find ("Scroll Bar");
		PipeScrollViewPanelObj = GameObject.Find ("PipeScrollViewPanel");

		if (startPosition == 1) 
		{

			UIScrollBar uisb = GameObject.Find("Scroll Bar").GetComponent<UIScrollBar>();
			uisb.value = 0.79f;
		}

		GameCon.bgNum = Random.Range (0, 7);
		GameCon.setBgAtlasSet (GameCon.bgNum);
		setBg ();


		selected_itemIndex = 0;

		//set popup initialize........
		Popup_Panel_OK_Obj = GameObject.Find ("Popup_Info_OK");
		Popup_Panel_OK_Obj.SetActive (false);
		popupState = 0;	//no popup
		//end set popup initialize........

		pd = PlayerData.Instance;

		item = new GameObject[PlayerData.GOLD_SHOP_ITEM_MAX];
		itemContainer = new GameObject[PlayerData.GOLD_SHOP_ITEM_MAX];

		Vector3 v3pos;
		for (int i=0; i<PlayerData.GOLD_SHOP_ITEM_MAX; i++) 
		{
			item[i] = GameObject.Find ("Item" + i);

			v3pos = item[i].transform.localPosition;
			v3pos.x = 30 + 390*i;
			item[i].transform.localPosition = v3pos;

			itemContainer[i] = item [i].transform.FindChild ("UISlicedSprite").gameObject;

			if(i < 4)
			{
				itemContainer[i].transform.FindChild ("text_cost").GetComponent<UILabel>().text = price[i]+" $";
			}
			else
			{
				itemContainer[i].transform.FindChild ("text_cost").GetComponent<UILabel>().text = price[i]+ " Gems";
			}
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
	
	
	// Update is called once per frame
	void Update () {
		//refresh_Gold ();

		GameObject.Find ("text_gemValue").GetComponent<UILabel>().text = string.Format("{0:N0}", pd.gem);
		GameObject.Find ("text_goldValue").GetComponent<UILabel>().text = string.Format("{0:N0}", pd.gold);
	}

	void setCursorPos()
	{
//		for (int i=0; i<PlayerData.GOLD_ITEM_MAX; i++) {
//			if(btnIndex == i)
//			{
//				cursorObj[i].SetActive(true);
//			}
//			else{
//				cursorObj[i].SetActive(false);
//			}
//		}
	}
		
	void toggleItemActive(bool toggle = false)
	{
		for (int i=0; i<PlayerData.GOLD_SHOP_ITEM_MAX; i++) {
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
	
		
	void Buy_Gems1()
	{
		selected_itemIndex = 0;
		toggleItemActive();
		setPopupText (1);	//this is cash item. move to buy cash item 
	}
	
	void Buy_Gems2()
	{
		selected_itemIndex = 1;
		toggleItemActive();
		setPopupText (1);	//this is cash item. move to buy cash item 
	}
	
	void Buy_Gems3()
	{
		selected_itemIndex = 2;
		toggleItemActive();
		setPopupText (1);	//this is cash item. move to buy cash item 
	}
	
	void Buy_Gems4()
	{
		selected_itemIndex = 3;
		toggleItemActive();
		setPopupText (1);	//this is cash item. move to buy cash item 
	}

	//			pd.gem -= (int)(price [selected_itemIndex]);
	//
	//			pd.saveData ();


	void Buy_Gold1()
	{
		selected_itemIndex = 4;
		toggleItemActive();
		if (pd.gem >= price [selected_itemIndex]) 
		{
			setPopupText (3);
		} else 
		{
			setPopupText (2);
		}
		

	}
	void Buy_Gold2()
	{
		selected_itemIndex = 5;
		toggleItemActive();
		if (pd.gem >= price [selected_itemIndex]) 
		{
			setPopupText (3);
		} else 
		{
			setPopupText (2);
		}
	}
	void Buy_Gold3()
	{
		selected_itemIndex = 6;
		toggleItemActive();
		if (pd.gem >= price [selected_itemIndex]) 
		{
			setPopupText (3);
		} else 
		{
			setPopupText (2);
		}
	}
	void Buy_Gold4()
	{
		selected_itemIndex = 7;
		toggleItemActive();
		if (pd.gem >= price [selected_itemIndex]) 
		{
			setPopupText (3);
		} else 
		{
			setPopupText (2);
		}
	}
	



	void OnClick_Buy()
	{
		setPopupText (1);
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

	void setPopupText(int _state)
	{
		Popup_Panel_OK_Obj.transform.localPosition = new Vector3(0, 800, 0);

		if (_state == 0) 
		{
			popupState = _state;
			Popup_Panel_OK_Obj.SetActive (false);
		} 
		else if (_state == 1) 
		{
			//go to billing infomation window, do you want buy gold?
			popupState = _state;	//
			Popup_Panel_OK_Obj.SetActive (true);
			GameObject.Find ("text_explain").GetComponent<UILabel> ().text = "this item is [FF0000]cash item[-]! \ndo you want buy really?\nif you touch buy button, go to billing process!";
			GameObject.Find("text_moveToItemShop").GetComponent<UILabel>().text = "Buy";
		} 
		else if (_state == 2) 
		{
			//go to billing infomation window, do you want buy gold?
			popupState = _state;	//
			Popup_Panel_OK_Obj.SetActive (true);
			Popup_Panel_OK_Obj.transform.FindChild("text_explain").GetComponent<UILabel>().text = "Can't buy item. Gems are [FFAA66]insufficient[-]. \nDo you want [FFAA66]buy Gems[-]?";
			GameObject.Find("text_moveToItemShop").GetComponent<UILabel>().text = "Move to\nBuy Gems";
		}
		else if (_state == 3) 
		{
			//go to billing infomation window, do you want buy gold?
			popupState = _state;	//
			Popup_Panel_OK_Obj.SetActive (true);
			Popup_Panel_OK_Obj.transform.FindChild("text_explain").GetComponent<UILabel>().text = "Do you want [FFAA66]buy Gold[-] really?";
			GameObject.Find("text_moveToItemShop").GetComponent<UILabel>().text = "Buy";
		}

		efm.Play(8);

		TweenPosition twPosition = TweenPosition.Begin(Popup_Panel_OK_Obj, 0.3f, new Vector3(0, 0, 0));
		twPosition.method = UITweener.Method.BounceIn;
	}

	void Close_Popup_Info_OK()
	{
		efm.Play(0);
		// enable pipes
		toggleItemActive(true);
		
		Popup_Panel_OK_Obj.SetActive (false);
		popupState = 0;	//no popup
	}

	void Click_Confirm()
	{
		if (popupState == 0) 
		{
		}
		else if (popupState == 1) 
		{
			//go to billing process


			//this is temporary...
			//buy gem

			pd.gem += give_gem[selected_itemIndex];
			pd.saveData();
			
			Close_Popup_Info_OK();
		}
		else if (popupState == 2) 
		{
			//focus item 0 (buy gems)
			Vector3 v3pos = PipeScrollViewPanelObj.transform.localPosition;
			v3pos.x = -10;
			PipeScrollViewPanelObj.transform.localPosition = v3pos;

			Close_Popup_Info_OK();
		}
		else if (popupState == 3) 
		{
			//buy gold
			pd.gem -= (int)(price[selected_itemIndex]);

			pd.gold += give_gold[selected_itemIndex];
			pd.saveData();

			Close_Popup_Info_OK();
		}
	}
//		if (popupState == 1) //
//		{
//		
//			if (pd.gold < give_gold[btnIndex] ||
//			    pd.gem < price_gold[btnIndex])
//			{
//			   print ("insufficient funds!");
//			}
//			else
//			{
//				pd.gold += give_gold[btnIndex];
//				pd.gem -= price_gold[btnIndex];
//			}
//			
//		} else if (popupState == 2) //
//		{
//		}
//	}
//	
//
//	void Click_Yes()
//	{
//		if (popupState == 1) //
//		{
//			//later hero implementation billing connect
//			//below is temporary....
//			//pd.gold += price[btnIndex];
//			pd.gem += give_gem[btnIndex];
//		
//
//
//		} else if (popupState == 2) //
//		{
//		}
//	}
//	
//	void Click_No()
//	{
//		if (popupState == 1) //
//		{
//			setPopupText(0);
//			toggleItemActive(true);
//		} else if (popupState == 2) //
//		{
//
//		}
//	}
}
