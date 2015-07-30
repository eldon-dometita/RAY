using UnityEngine;
using System.Collections;

public class shopMainController : MonoBehaviour {
	
	PlayerData pd;
	public static int whereFrom = 0;	//0: Planet 	1:Stage

	public string spriteName = "conveyor&Inventory-2007";

	public EffectSoundManagerScript efm;

	// Use this for initialization
	void Start () {

		pd = PlayerData.Instance;

		efm = EffectSoundManagerScript.Instance;

		GameCon.bgNum = Random.Range (0, 7);
		GameCon.setBgAtlasSet (GameCon.bgNum);
		setBg ();



		GameObject.Find ("text_gemValue").GetComponent<UILabel>().text = string.Format("{0:N0}", pd.gem);
		GameObject.Find ("text_goldValue").GetComponent<UILabel>().text = string.Format("{0:N0}", pd.gold);
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
		//Debug.Log ("uis.width: "+uis.width);
		
		uis = GameObject.Find ("TopBackground").GetComponent<UISprite>();
		uis.width = Screen.width+100;
	}

	// Update is called once per frame
	void Update () {

	}
	


	void OnClick_Gold()
	{
		efm.Play(0);
		StartCoroutine(LoadAfterDelay("ShopGold"));    
		//Application.LoadLevel ("ShopGold");

	}

	void OnClick_Pipe()
	{
		efm.Play(0);
		StartCoroutine(LoadAfterDelay("ShopPipes"));    
		//Application.LoadLevel ("ShopPipes");
	}

//	void OnClick_EternalItem()
//	{
//		efm.Play(0);
//		Application.LoadLevel ("ShopEternalItem");
//	}

	void OnClick_ConsumeItem()
	{
		efm.Play(0);
		StartCoroutine(LoadAfterDelay("ShopConsumeItem"));    
		//Application.LoadLevel ("ShopConsumeItem");
	}

	void OnClick_Back()
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
}
