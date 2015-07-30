using UnityEngine;
using System.Collections;

public class ConveyorPipeScript : MonoBehaviour {

	GameCon gameCon;
	public int pipeID;
	public bool bMove;

	// Use this for initialization
	void Awake () {
		gameCon = GameObject.Find ("GameCon").GetComponent<GameCon>();
		bMove = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setPipe(int _pipeID)
	{
		pipeID = _pipeID;

		GameCon.setAtlasSet (GlobalData.baseTileNum);


		UISprite uis = this.transform.FindChild("Layer2").GetComponent<UISprite>();
		uis.atlas = Resources.Load<UIAtlas> (GameCon.basePipeAtlasPath);
		UISprite uisArrow = this.transform.FindChild("Arrow").GetComponent<UISprite>();

		//set base
		switch(pipeID)
		{
		case 10:	case 20:	case 30:
			uis.spriteName = "10b";
			break;

		case 11:	case 21:	case 31:
			uis.spriteName = "11b";
			break;

		case 12:	case 22:	case 32:
			uis.spriteName = "12b";
			break;

		case 13:	case 23:	case 33:
			uis.spriteName = "13b";
			break;

		case 14:	case 24:	case 34:
			uis.spriteName = "14b";
			break;

		case 15:	case 25:	case 35:
			uis.spriteName = "15b";
			break;

		case 16:
			uis.spriteName = "16b";
			break;

		case 70:	case 74:
			uis.spriteName = "70b";
			break;

		case 71:	case 75:
			uis.spriteName = "71b";
			break;

		case 72:	case 76:
			uis.spriteName = "72b";
			break;

		case 73:	case 77:
			uis.spriteName = "73b";
			break;

		case 80:	case 84:
		case 81:	case 85:
		case 82:	case 86:
		case 83:	case 87:
			uis.spriteName = "80b";
			break;
		}


//		Debug.Log("pipeID:   "+pipeID);
		//set arrow
		if (pipeID >= 10 && pipeID <= 16) 
		{

			uisArrow.alpha = 0;
//			Debug.Log("uisArrow.alpha:   "+uisArrow.alpha);
		} else {

			uisArrow.alpha = 1;
			if(pipeID >= 20 && pipeID <= 25)
			{
				uisArrow.atlas = Resources.Load<UIAtlas> ("Atlases/TopClockTiles");
				uisArrow.spriteName = (pipeID-10)+"t";
			}
			else if(pipeID >= 30 && pipeID <= 35)
			{
				uisArrow.atlas = Resources.Load<UIAtlas> ("Atlases/TopCounterTiles");
				uisArrow.spriteName = (pipeID-20)+"t";
			}
			else if((pipeID >= 70 && pipeID <= 73) || (pipeID >= 80 && pipeID <= 83))
			{
				uisArrow.atlas = Resources.Load<UIAtlas> ("Atlases/TopClockTiles");
				uisArrow.spriteName = (pipeID)+"t";
			}
			else if((pipeID >= 74 && pipeID <= 77) || (pipeID >= 84 && pipeID <= 87))
			{
				uisArrow.atlas = Resources.Load<UIAtlas> ("Atlases/TopCounterTiles");
				uisArrow.spriteName = (pipeID)+"t";
			}
		}
	}



	public void move(bool _skipThisPipe)
	{
		gameCon.skipPipe = _skipThisPipe;

		Vector3 _v3pos_LeftTop = GameObject.Find ("Anchor_BottomLeft").transform.localPosition;
		Vector3 _v3pos_Panel_ScrollView = gameCon.go_Panel_ScrollView.transform.localPosition;
		Vector3 _v3scale_zoomObj = gameCon.zoomPanObj.transform.localScale;
		bMove = true;
		Vector3 pos = this.transform.localPosition;

		if (pos.y > 735) 
		{
			if(_skipThisPipe)
			{
				pos.x -= 500;
			}
			else{
				//pos.y += 500;
				//first cal transfer center 0, 0
				pos.x = -_v3pos_LeftTop.x+_v3pos_Panel_ScrollView.x+(gameCon.v3pos_nowPipe.x*_v3scale_zoomObj.x);
				pos.y = -_v3pos_LeftTop.y+_v3pos_Panel_ScrollView.y+(gameCon.v3pos_nowPipe.y*_v3scale_zoomObj.y);
			}

			gameCon.efm.Play (4);

			TweenScale twScale = TweenScale.Begin(this.gameObject, 0.3f, _v3scale_zoomObj);
			EventDelegate.Add( twScale.onFinished, callback_move_top_scale, true);
			TweenPosition twPosition = TweenPosition.Begin( this.gameObject, 0.3f, pos); //VenderCon.VENDER_SPEED
			twPosition.method = UITweener.Method.EaseIn;
			EventDelegate.Add( twPosition.onFinished, callback_move_top, true);
		} else {
			pos.y += 105;
//			Debug.Log("gameCon.conveyorSpeed:   "+gameCon.conveyorSpeed);
			TweenPosition twPosition = TweenPosition.Begin( this.gameObject, gameCon.conveyorSpeed, pos ); //VenderCon.VENDER_SPEED
			twPosition.method = UITweener.Method.Linear;
			EventDelegate.Add( twPosition.onFinished, callback_move_default, true);
			//this.transform.localPosition = pos;
		}
	}

	private void callback_move_top_scale()
	{
		this.gameObject.transform.localScale = new Vector3(1, 1, 1);
	}

	private void callback_move_top()
	{
		//Debug.Log ("7");
		//Debug.Log (gameCon.nowFireID);
		if (!gameCon.skipPipe) 
		{
			gameCon.changePref_Tile_fromConveyorBelt (gameCon.nowPipeIndex, gameCon.nowFireID); 


			gameCon.createTileEffect(gameCon.v3pos_nowPipe.x, gameCon.v3pos_nowPipe.y);

			//check bomb, and then if pipe is correct, remove bomb

			int id_eatItem = gameCon.getID_EatItem (gameCon.pos_eatItem);
			if (id_eatItem >= 50 && id_eatItem <= 54) 
			{
				if (gameCon.nowFireID == gameCon.getPerfectPlanID (gameCon.pos_eatItem)) 
				{
					gameCon.checkEatItem (gameCon.pos_eatItem.x, gameCon.pos_eatItem.y);
				}
			}
			else if (id_eatItem >= 60 && id_eatItem <= 75) 
			{
				if (gameCon.nowFireID == gameCon.getPerfectPlanID (gameCon.pos_eatItem)) 
				{
					gameCon.checkPlusItem (gameCon.pos_eatItem.x, gameCon.pos_eatItem.y);
				}
			}
		}



		Vector3 v3pos = this.transform.localPosition;
		v3pos.y = -205;
		v3pos.x = 90;
		this.transform.localPosition = v3pos;
		this.transform.localScale = new Vector3 (1, 1, 1);

		setPipe (gameCon.getCreatePipeID ());

//		Debug.Log(this.name);
//		Debug.Log(this.transform.localScale);

		bMove = false;
		//EventDelegate.Remove(callback_move);
	}

	private void callback_move_default()
	{
		bMove = false;
		gameCon.efm.stop(5);
	}



	//////



}
