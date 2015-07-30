using UnityEngine;
using System.Collections;

public class RewardItemScript : MonoBehaviour {

	UISprite spr_Icon;
	UISprite spr_Arrow;
	UILabel lable_Count;
	UIAtlas atlas_pipe;
	UIAtlas atlas_ArrowClock;
	UIAtlas atlas_ArrowCount;
	UIAtlas atlas_item;
	UIAtlas atlas_shop;

	GameCon gameCon;


	// Use this for initialization
	void Awake () {

		gameCon = GameObject.Find ("GameCon").GetComponent<GameCon> ();

		spr_Icon = this.transform.FindChild ("Layer2").GetComponent<UISprite> ();
		spr_Arrow = this.transform.FindChild ("Arrow").GetComponent<UISprite> ();
		lable_Count = this.transform.FindChild ("Count").GetComponent<UILabel> ();



	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setItem(int _index, int _Count)
	{

		switch(_index)
		{

		case 0:
			//nothing
			spr_Icon.atlas = Resources.Load<UIAtlas> ("Atlases/Shop");
			spr_Arrow.atlas = Resources.Load<UIAtlas> ("Atlases/Shop");
			spr_Icon.spriteName = "empty";
			spr_Arrow.spriteName = "empty";
			lable_Count.text = " ";
			break;

		case 10:
		case 11:
		case 12:
		case 13:
		case 14:
		case 15:
		case 16:
			//default pipe
			GameCon.setAtlasSet (GlobalData.baseTileNum);
			spr_Icon.atlas = Resources.Load<UIAtlas> (GameCon.basePipeAtlasPath);
			spr_Icon.spriteName = _index+"b";
			spr_Arrow.atlas = Resources.Load<UIAtlas> ("Atlases/Shop");
			spr_Arrow.spriteName = "empty";
			lable_Count.text = ""+_Count;
			break;

		case 70:
		case 71:
		case 72:
		case 73:
			//1to2
			GameCon.setAtlasSet (GlobalData.baseTileNum);
			spr_Icon.atlas = Resources.Load<UIAtlas> (GameCon.basePipeAtlasPath);
			spr_Icon.spriteName = _index+"b";
			spr_Arrow.atlas = Resources.Load<UIAtlas> ("Atlases/TopClockTiles");
			spr_Arrow.spriteName = _index+"t";
			lable_Count.text = ""+_Count;
			break;

		case 74:
		case 75:
		case 76:
		case 77:
			//1to2
			GameCon.setAtlasSet (GlobalData.baseTileNum);
			spr_Icon.atlas = Resources.Load<UIAtlas> (GameCon.basePipeAtlasPath);
			spr_Icon.spriteName = (_index-4)+"b";
			spr_Arrow.atlas = Resources.Load<UIAtlas> ("Atlases/TopCounterTiles");
			spr_Arrow.spriteName = _index+"t";
			lable_Count.text = ""+_Count;
			break;

		case 80:
		case 81:
		case 82:
		case 83:
			//1to3
			GameCon.setAtlasSet (GlobalData.baseTileNum);
			spr_Icon.atlas = Resources.Load<UIAtlas> (GameCon.basePipeAtlasPath);
			spr_Icon.spriteName = "80b";
			spr_Arrow.atlas = Resources.Load<UIAtlas> ("Atlases/TopClockTiles");
			spr_Arrow.spriteName = _index+"t";
			lable_Count.text = ""+_Count;
			break;
		case 84:
		case 85:
		case 86:
		case 87:
			//1to3
			GameCon.setAtlasSet (GlobalData.baseTileNum);
			spr_Icon.atlas = Resources.Load<UIAtlas> (GameCon.basePipeAtlasPath);
			spr_Icon.spriteName = "80b";
			spr_Arrow.atlas = Resources.Load<UIAtlas> ("Atlases/TopCounterTiles");
			spr_Arrow.spriteName = _index+"t";
			lable_Count.text = ""+_Count;
			break;

		case 300:
			//gold
			spr_Icon.atlas = Resources.Load<UIAtlas> ("Atlases/GameItemAtlas");
			spr_Icon.spriteName = "ItemSprite002";
			spr_Icon.width = 94;
			spr_Icon.height = 92;
			spr_Arrow.atlas = Resources.Load<UIAtlas> ("Atlases/Shop");
			spr_Arrow.spriteName = "empty";
			lable_Count.text = ""+_Count;
			break;

		case 310:
			//gem
			spr_Icon.atlas = Resources.Load<UIAtlas> ("Atlases/GameItemAtlas");
			spr_Icon.spriteName = "ItemSprite000";
			spr_Icon.width = 94;
			spr_Icon.height = 92;
			spr_Arrow.atlas = Resources.Load<UIAtlas> ("Atlases/Shop");
			spr_Arrow.spriteName = "empty";
			lable_Count.text = ""+_Count;
			break;

		case 400:
			//time expand 30s item
			spr_Icon.atlas = Resources.Load<UIAtlas> ("Atlases/Shop");
			spr_Icon.spriteName = "Shop075";
			spr_Icon.width = 107;
			spr_Icon.height = 75;
			spr_Arrow.atlas = Resources.Load<UIAtlas> ("Atlases/Shop");
			spr_Arrow.spriteName = "empty";
			lable_Count.text = ""+_Count;
			break;

		case 401:
			//perfect plan item
			spr_Icon.atlas = Resources.Load<UIAtlas> ("Atlases/Shop");
			spr_Icon.spriteName = "Shop077";
			spr_Icon.width = 97;
			spr_Icon.height = 99;
			spr_Arrow.atlas = Resources.Load<UIAtlas> ("Atlases/Shop");
			spr_Arrow.spriteName = "empty";
			lable_Count.text = ""+_Count;
			break;

		case 402:
			//turn on water
			spr_Icon.atlas = Resources.Load<UIAtlas> ("Atlases/Shop");
			spr_Icon.spriteName = "wateron";
			spr_Icon.width = 97;
			spr_Icon.height = 99;
			spr_Arrow.atlas = Resources.Load<UIAtlas> ("Atlases/Shop");
			spr_Arrow.spriteName = "empty";
			lable_Count.text = ""+_Count;
			break;
		}

	}
}
