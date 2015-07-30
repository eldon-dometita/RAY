using UnityEngine;
using System.Collections;

public class PipeScript : MonoBehaviour {
	
	
	GameCon gameCon;
	
	
	public bool bChange;
	public int xpos;
	public int ypos;
	public int ID;
	public int idx;
	//public POS pos;
	//public POSINFO posInfo;
	
	
	/// <summary>
	/// The direction. 
	/// 0 is North
	/// 1 is East
	/// 2 is South
	/// 3 is West
	/// </summary>
	int direction;		
	int[] outDirection;
	bool[] inFinish;
	
	/// 1 to 3
	/// 28 enter west
	/// 27 enter south
	/// 26 enter east
	/// 25 enter north 
	/// </summary>
	int type;	//
	
	bool[] bStartAni;
	bool bEatItem;
	float[] timer;
	Transform go_uis1;
	Transform go_uis2;
	Transform go_uis3;
	Transform go_uis4;
	
	UISprite uis1;
	UISprite uis2;
	UISprite uis3;
	UISprite uis4;
	UISprite uis_Base;
	UISprite uis_Base1;
	UISprite uis_Tile;
	UISprite uis_Top;
	UISprite uis_TopText;
	UISprite uis_Object;
	
	GlobalData gd;
	
	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Use this for initialization
	void Awake () 
	{
		gd = GlobalData.Instance;
		
		gameCon = GameCon.Instance;//GameObject.Find ("GameCon").GetComponent<GameCon>();
		bChange = false;
		//pos = new POS ();
		
		
		direction = 0;
		outDirection = new int[4];
		for (int i=0; i<4; i++) 
		{
			outDirection [i] = 0;
		}
		
		inFinish = new bool[4];
		for (int i=0; i<4; i++) 
		{
			inFinish [i] = false;
		}
		
		type = 0;
		//아이디 가지고 바꿀수 있는 것과 바꿀 수 없는 것을 설정해 줘야 한다.
		//		if ( this.name == "P100(Clone)")
		//		{
		//			GetComponent<PM> ().bChange = false;
		//		} else {
		//			GetComponent<PM>().bChange = true;
		//		}
		
		
		
		//setting Ani
		bStartAni = new bool[4];
		timer = new float[4];
		for (int i=0; i<4; i++) 
		{
			bStartAni[i] = false;
			timer[i] = 0;
		}
		bEatItem = false;
		
		go_uis1 = this.gameObject.transform.FindChild("Layer10");
		go_uis1.localRotation =  Quaternion.Euler (0, 0, 0);
		uis1 = go_uis1.GetComponent<UISprite> ();	
		uis1.fillAmount = 0;
		
		go_uis2 = this.gameObject.transform.FindChild("Layer11");
		go_uis2.localRotation =  Quaternion.Euler (0, 0, 0);
		uis2 = go_uis2.GetComponent<UISprite> ();	
		uis2.fillAmount = 0; 
		
		go_uis3 = this.gameObject.transform.FindChild("Layer12");
		go_uis3.localRotation =  Quaternion.Euler (0, 0, 0);
		uis3 = go_uis3.GetComponent<UISprite> ();	
		uis3.fillAmount = 0;
		
		go_uis4 = this.gameObject.transform.FindChild("Layer13");
		go_uis4.localRotation =  Quaternion.Euler (0, 0, 0);
		uis4 = go_uis4.GetComponent<UISprite> ();	
		uis4.fillAmount = 0;
		
		uis_Base = this.gameObject.transform.FindChild("Layer0").GetComponent<UISprite> ();	
		uis_Base1 = this.gameObject.transform.FindChild("Layer1").GetComponent<UISprite> ();	
		uis_Tile = this.gameObject.transform.FindChild("Layer2").GetComponent<UISprite> ();	
		uis_Top = this.gameObject.transform.FindChild("Layer20").GetComponent<UISprite> ();	
		uis_TopText = this.gameObject.transform.FindChild("Layer30").GetComponent<UISprite> ();	
		uis_Object = this.gameObject.transform.FindChild("Layer40").GetComponent<UISprite> ();	
		
		InitSetup ();
	}
	
	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void setPos(int _xpos, int _ypos)
	{
		xpos = _xpos;
		ypos = _ypos;
	}
	
	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void InitSetup()
	{
		//UIAtlas uia = Resources.Load<UIAtlas> (GameCon.basePipeAtlasPath);
		
		
		uis_Base.atlas = gd.atlas_bgTiles;
		uis_Base.spriteName = "empty";
		
		
		uis_Base1.atlas = gd.atlas_bgTiles;
		uis_Base1.spriteName = "empty";
		
		uis_Tile.atlas = gd.atlas_tile;
		uis_Tile.spriteName = "empty";
		
		uis1.atlas = gd.atlas_tile;
		uis1.spriteName = "empty";
		uis1.fillDirection = UIBasicSprite.FillDirection.Horizontal;
		uis1.invert = false;
		
		uis2.atlas = gd.atlas_tile;
		uis2.spriteName = "empty";
		uis2.fillDirection = UIBasicSprite.FillDirection.Horizontal;
		uis2.invert = false;
		
		uis3.atlas = gd.atlas_tile;
		uis3.spriteName = "empty";
		uis3.fillDirection = UIBasicSprite.FillDirection.Horizontal;
		uis3.invert = false;
		
		uis4.atlas = gd.atlas_tile;
		uis4.spriteName = "empty";
		uis4.fillDirection = UIBasicSprite.FillDirection.Horizontal;
		uis4.invert = false;
		
		uis_Top.atlas = gd.atlas_textTiles;
		uis_Top.spriteName = "empty";
		
		uis_TopText.atlas = gd.atlas_textTiles;
		uis_TopText.spriteName = "empty";
		
		uis_Object.atlas = gd.atlas_bgTiles;
		uis_Object.spriteName = "empty";
		
		
		
		
	}
	
	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void setIndex(int _idx)
	{
		idx = _idx;
	}
	
	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void setID(int _id, int _color)
	{
		ID = _id;
		
		setImage(_color);
	}
	
	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void setImage(int _color)
	{
		if(_color == 0)
		{
			gd.setAtlas_Tile(GlobalData.baseTileNum);
		}
		else
		{
			gd.setAtlas_Tile(GlobalData.perfectTileNum);
		}
		
		uis_Tile.atlas = gd.atlas_tile;
		uis1.atlas = gd.atlas_tile;
		uis2.atlas = gd.atlas_tile;
		uis3.atlas = gd.atlas_tile;
		uis4.atlas = gd.atlas_tile;
		
		go_uis1.localRotation =  Quaternion.Euler (0, 0, 0);
		uis1.fillDirection = UIBasicSprite.FillDirection.Horizontal;
		uis1.invert = false;
		uis1.fillAmount = 0;
		
		go_uis2.localRotation =  Quaternion.Euler (0, 0, 0);
		uis2.fillDirection = UIBasicSprite.FillDirection.Horizontal;
		uis2.invert = false;
		uis2.fillAmount = 0; 
		
		go_uis3.localRotation =  Quaternion.Euler (0, 0, 0);
		uis3.fillDirection = UIBasicSprite.FillDirection.Horizontal;
		uis3.invert = false;
		uis3.fillAmount = 0;
		
		go_uis4.localRotation =  Quaternion.Euler (0, 0, 0);
		uis4.fillDirection = UIBasicSprite.FillDirection.Horizontal;
		uis4.invert = false;
		uis4.fillAmount = 0;
		
		switch(ID)
		{
		case 0:
			bChange = false;
			uis_Base.spriteName = "empty";
			uis_Base1.spriteName = "empty";
			uis_Tile.spriteName = "empty";
			uis1.spriteName = "empty";
			uis2.spriteName = "empty";
			uis3.spriteName = "empty";
			uis4.spriteName = "empty";
			uis_Top.spriteName = "empty";
			uis_TopText.spriteName = "empty";
			uis_Object.spriteName = "empty";
			break;
			
		case 1:
			bChange = true;
			
			uis_Base.spriteName = "_back";
			uis_Base1.spriteName = "empty";
			uis_Tile.spriteName = "empty";
			uis1.spriteName = "empty";
			uis2.spriteName = "empty";
			uis3.spriteName = "empty";
			uis4.spriteName = "empty";
			uis_Top.spriteName = "empty";
			uis_TopText.spriteName = "empty";
			uis_Object.spriteName = "empty";
			break;
			
		case 2:
			bChange = true;
			
			uis_Base.spriteName = "empty";
			uis_Base1.spriteName = "gray";
			uis_Tile.spriteName = "empty";
			uis1.spriteName = "empty";
			uis2.spriteName = "empty";
			uis3.spriteName = "empty";
			uis4.spriteName = "empty";
			uis_Top.spriteName = "empty";
			uis_TopText.spriteName = "empty";
			uis_Object.spriteName = "empty";
			break;
			
		case 3:
			bChange = false;
			uis_Base.spriteName = "empty";
			uis_Base1.spriteName = "empty";
			uis_Tile.spriteName = "empty";
			uis1.spriteName = "empty";
			uis2.spriteName = "empty";
			uis3.spriteName = "empty";
			uis4.spriteName = "empty";
			uis_Top.spriteName = "empty";
			uis_TopText.spriteName = "empty";
			uis_Object.spriteName = "3";
			break;
			
		case 10:
		case 20:
		case 30:
		case 40:
		case 50:
		case 60:
			bChange = true;
			uis_Base.spriteName = "empty";
			uis_Base1.spriteName = "empty";
			if(ID >= 40)
			{
				bChange = false;
				uis_Base1.spriteName = "gray";
			}
			uis_Tile.spriteName = "10b";
			uis1.spriteName = "10a";
			uis1.fillDirection = UIBasicSprite.FillDirection.Radial90;
			uis1.invert = true;
			uis2.spriteName = "10a";
			uis2.fillDirection = UIBasicSprite.FillDirection.Radial90;
			uis2.invert = false;
			uis3.spriteName = "empty";
			uis4.spriteName = "empty";
			uis_Top.spriteName = "empty";
			
			uis_TopText.spriteName = "empty";
			if(ID == 20 || ID == 50)
			{
				uis_TopText.atlas = gd.atlas_clockTiles;
				uis_TopText.spriteName = "10t";
			}
			else if(ID == 30 || ID == 60)
			{
				uis_TopText.atlas = gd.atlas_counterTiles;
				uis_TopText.spriteName = "10t";
			}
			
			uis_Object.spriteName = "empty";
			break;
			
		case 11:
		case 21:
		case 31:
		case 41:
		case 51:
		case 61:
			bChange = true;
			uis_Base.spriteName = "empty";
			uis_Base1.spriteName = "empty";
			if(ID >= 40)
			{
				bChange = false;
				uis_Base1.spriteName = "gray";
			}
			uis_Tile.spriteName = "11b";
			uis1.spriteName = "11a";
			
			uis1.fillDirection = UIBasicSprite.FillDirection.Radial90;
			uis1.invert = true;
			go_uis1.localRotation =  Quaternion.Euler (0, 0, -90);
			
			uis2.spriteName = "11a";
			uis2.fillDirection = UIBasicSprite.FillDirection.Radial90;
			uis2.invert = false;
			go_uis2.localRotation = Quaternion.Euler (0, 0, -90);
			
			uis3.spriteName = "empty";
			uis4.spriteName = "empty";
			uis_Top.spriteName = "empty";
			
			uis_TopText.spriteName = "empty";
			if(ID == 21 || ID == 51)
			{
				uis_TopText.atlas = gd.atlas_clockTiles;
				uis_TopText.spriteName = "11t";
			}
			else if(ID == 31 || ID == 61)
			{
				uis_TopText.atlas = gd.atlas_counterTiles;
				uis_TopText.spriteName = "11t";
			}
			
			uis_Object.spriteName = "empty";
			break;
			
		case 12:
		case 22:
		case 32:
		case 42:
		case 52:
		case 62:
			bChange = true;
			uis_Base.spriteName = "empty";
			uis_Base1.spriteName = "empty";
			if(ID >= 40)
			{
				bChange = false;
				uis_Base1.spriteName = "gray";
			}
			uis_Tile.spriteName = "12b";
			uis1.spriteName = "12a";
			
			uis1.fillDirection = UIBasicSprite.FillDirection.Radial90;
			uis1.invert = true;
			go_uis1.localRotation =  Quaternion.Euler (0, 0, -180);
			
			uis2.spriteName = "12a";
			uis2.fillDirection = UIBasicSprite.FillDirection.Radial90;
			uis2.invert = false;
			go_uis2.localRotation = Quaternion.Euler (0, 0, -180);
			
			uis3.spriteName = "empty";
			uis4.spriteName = "empty";
			uis_Top.spriteName = "empty";
			
			uis_TopText.spriteName = "empty";
			if(ID == 22 || ID == 52)
			{
				uis_TopText.atlas = gd.atlas_clockTiles;
				uis_TopText.spriteName = "12t";
			}
			else if(ID == 32 || ID == 62)
			{
				uis_TopText.atlas = gd.atlas_counterTiles;
				uis_TopText.spriteName = "12t";
			}
			
			uis_Object.spriteName = "empty";
			break;
			
		case 13:
		case 23:
		case 33:
		case 43:
		case 53:
		case 63:
			bChange = true;
			uis_Base.spriteName = "empty";
			uis_Base1.spriteName = "empty";
			if(ID >= 40)
			{
				bChange = false;
				uis_Base1.spriteName = "gray";
			}
			uis_Tile.spriteName = "13b";
			uis1.spriteName = "13a";
			
			uis1.fillDirection = UIBasicSprite.FillDirection.Radial90;
			uis1.invert = true;
			go_uis1.localRotation =  Quaternion.Euler (0, 0, 90);
			
			uis2.spriteName = "13a";
			uis2.fillDirection = UIBasicSprite.FillDirection.Radial90;
			uis2.invert = false;
			go_uis2.localRotation = Quaternion.Euler (0, 0, 90);
			
			uis3.spriteName = "empty";
			uis4.spriteName = "empty";
			uis_Top.spriteName = "empty";
			
			uis_TopText.spriteName = "empty";
			if(ID == 23 || ID == 53)
			{
				uis_TopText.atlas = gd.atlas_clockTiles;
				uis_TopText.spriteName = "13t";
			}
			else if(ID == 33 || ID == 63)
			{
				uis_TopText.atlas = gd.atlas_counterTiles;
				uis_TopText.spriteName = "13t";
			}
			
			uis_Object.spriteName = "empty";
			break;
			
		case 14:
		case 24:
		case 34:
		case 44:
		case 54:
		case 64:
			bChange = true;
			uis_Base.spriteName = "empty";
			uis_Base1.spriteName = "empty";
			if(ID >= 40)
			{
				bChange = false;
				uis_Base1.spriteName = "gray";
			}
			uis_Tile.spriteName = "14b";
			uis1.spriteName = "14a";
			uis1.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis1.invert = false;
			
			uis2.spriteName = "14a";
			uis2.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis2.invert = true;
			
			uis3.spriteName = "empty";
			uis4.spriteName = "empty";
			uis_Top.spriteName = "empty";
			
			uis_TopText.spriteName = "empty";
			if(ID == 24 || ID == 54)
			{
				uis_TopText.atlas = gd.atlas_clockTiles;
				uis_TopText.spriteName = "14t";
			}
			else if(ID == 34 || ID == 64)
			{
				uis_TopText.atlas = gd.atlas_counterTiles;
				uis_TopText.spriteName = "14t";
			}
			
			uis_Object.spriteName = "empty";
			break;
			
		case 15:
		case 25:
		case 35:
		case 45:
		case 55:
		case 65:
			bChange = true;
			uis_Base.spriteName = "empty";
			uis_Base1.spriteName = "empty";
			if(ID >= 40)
			{
				bChange = false;
				uis_Base1.spriteName = "gray";
			}
			uis_Tile.spriteName = "15b";
			uis1.spriteName = "15a";
			uis1.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis1.invert = false;
			
			uis2.spriteName = "15a";
			uis2.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis2.invert = true;
			
			uis3.spriteName = "empty";
			uis4.spriteName = "empty";
			uis_Top.spriteName = "empty";
			
			uis_TopText.spriteName = "empty";
			if(ID == 25 || ID == 55)
			{
				uis_TopText.atlas = gd.atlas_clockTiles;
				uis_TopText.spriteName = "15t";
			}
			else if(ID == 35 || ID == 65)
			{
				uis_TopText.atlas = gd.atlas_counterTiles;
				uis_TopText.spriteName = "15t";
			}
			
			uis_Object.spriteName = "empty";
			break;
			
		case 16:
		case 46:
			bChange = true;
			uis_Base.spriteName = "empty";
			uis_Base1.spriteName = "empty";
			if(ID >= 40)
			{
				bChange = false;
				uis_Base1.spriteName = "gray";
			}
			uis_Tile.spriteName = "16b";
			uis1.spriteName = "16a1";
			uis1.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis1.invert = false;
			
			uis2.spriteName = "16a1";
			uis2.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis2.invert = true;
			
			uis3.spriteName = "15a";
			uis3.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis3.invert = false;
			
			uis4.spriteName = "15a";
			uis4.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis4.invert = true;
			
			uis_Top.spriteName = "empty";
			uis_TopText.spriteName = "empty";
			uis_Object.spriteName = "empty";
			break;
			
		case 70:
		case 90:
			bChange = true;
			uis_Base.spriteName = "empty";
			uis_Base1.spriteName = "empty";
			if(ID >= 90)
			{
				bChange = false;
				uis_Base1.spriteName = "gray";
			}
			uis_Tile.spriteName = "70b";
			uis1.spriteName = "70a1";
			uis1.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis1.invert = true;
			
			uis2.spriteName = "70a3";
			uis2.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis2.invert = false;
			
			uis3.spriteName = "empty";
			
			uis4.spriteName = "70a2";
			uis4.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis4.invert = true;
			
			uis_Top.spriteName = "empty";
			
			uis_TopText.atlas = gd.atlas_clockTiles;
			uis_TopText.spriteName = "70t";
			
			uis_Object.spriteName = "empty";
			break;
			
		case 71:
		case 91:
			bChange = true;
			uis_Base.spriteName = "empty";
			uis_Base1.spriteName = "empty";
			if(ID >= 90)
			{
				bChange = false;
				uis_Base1.spriteName = "gray";
			}
			uis_Tile.spriteName = "71b";
			uis1.spriteName = "71a3";
			uis1.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis1.invert = false;
			
			uis2.spriteName = "71a1";
			uis2.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis2.invert = true;
			
			uis3.spriteName = "71a2";
			uis3.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis3.invert = true;
			
			uis4.spriteName = "empty";
			//			uis4.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			//			uis4.invert = true;
			
			uis_Top.spriteName = "empty";
			
			uis_TopText.atlas = gd.atlas_clockTiles;
			uis_TopText.spriteName = "71t";
			
			uis_Object.spriteName = "empty";
			break;
			
		case 72:
		case 92:
			bChange = true;
			uis_Base.spriteName = "empty";
			uis_Base1.spriteName = "empty";
			if(ID >= 90)
			{
				bChange = false;
				uis_Base1.spriteName = "gray";
			}
			uis_Tile.spriteName = "72b";
			uis1.spriteName = "empty";
			//			uis1.fillDirection = UIBasicSprite.FillDirection.Vertical;
			//			uis1.invert = false;
			
			uis2.spriteName = "72a2";
			uis2.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis2.invert = false;
			
			uis3.spriteName = "72a3";
			uis3.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis3.invert = false;
			
			uis4.spriteName = "72a1";
			uis4.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis4.invert = true;
			
			uis_Top.spriteName = "empty";
			
			uis_TopText.atlas = gd.atlas_clockTiles;
			uis_TopText.spriteName = "72t";
			
			uis_Object.spriteName = "empty";
			break;
			
		case 73:
		case 93:
			bChange = true;
			uis_Base.spriteName = "empty";
			uis_Base1.spriteName = "empty";
			if(ID >= 90)
			{
				bChange = false;
				uis_Base1.spriteName = "gray";
			}
			uis_Tile.spriteName = "73b";
			uis1.spriteName = "73a1";
			uis1.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis1.invert = false;
			
			uis2.spriteName = "empty";
			//			uis2.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			//			uis2.invert = false;
			
			uis3.spriteName = "73a2";
			uis3.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis3.invert = true;
			
			uis4.spriteName = "73a3";
			uis4.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis4.invert = false;
			
			uis_Top.spriteName = "empty";
			
			uis_TopText.atlas = gd.atlas_clockTiles;
			uis_TopText.spriteName = "73t";
			
			uis_Object.spriteName = "empty";
			break;
			
		case 74:
		case 94:
			bChange = true;
			uis_Base.spriteName = "empty";
			uis_Base1.spriteName = "empty";
			if(ID >= 90)
			{
				bChange = false;
				uis_Base1.spriteName = "gray";
			}
			uis_Tile.spriteName = "70b";
			uis1.spriteName = "70a1";
			uis1.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis1.invert = false;
			
			uis2.spriteName = "70a3";
			uis2.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis2.invert = true;
			
			uis3.spriteName = "empty";
			
			uis4.spriteName = "70a2";
			uis4.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis4.invert = false;
			
			uis_Top.spriteName = "empty";
			
			uis_TopText.atlas = gd.atlas_counterTiles;
			uis_TopText.spriteName = "74t";
			
			uis_Object.spriteName = "empty";
			
			inFinish [0] = true;		
			inFinish [2] = true;		
			break;
			
		case 75:
		case 95:
			bChange = true;
			uis_Base.spriteName = "empty";
			uis_Base1.spriteName = "empty";
			if(ID >= 90)
			{
				bChange = false;
				uis_Base1.spriteName = "gray";
			}
			uis_Tile.spriteName = "71b";
			uis1.spriteName = "71a3";
			uis1.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis1.invert = true;
			
			uis2.spriteName = "71a1";
			uis2.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis2.invert = false;
			
			uis3.spriteName = "71a2";
			uis3.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis3.invert = false;
			
			uis4.spriteName = "empty";
			//			uis4.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			//			uis4.invert = true;
			
			uis_Top.spriteName = "empty";
			
			uis_TopText.atlas = gd.atlas_counterTiles;
			uis_TopText.spriteName = "75t";
			
			uis_Object.spriteName = "empty";
			
			inFinish [1] = true;		
			inFinish [3] = true;		
			break;
			
		case 76:
		case 96:
			bChange = true;
			uis_Base.spriteName = "empty";
			uis_Base1.spriteName = "empty";
			if(ID >= 90)
			{
				bChange = false;
				uis_Base1.spriteName = "gray";
			}
			uis_Tile.spriteName = "72b";
			uis1.spriteName = "empty";
			//			uis1.fillDirection = UIBasicSprite.FillDirection.Vertical;
			//			uis1.invert = false;
			
			uis2.spriteName = "72a2";
			uis2.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis2.invert = true;
			
			uis3.spriteName = "72a3";
			uis3.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis3.invert = true;
			
			uis4.spriteName = "72a1";
			uis4.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis4.invert = false;
			
			uis_Top.spriteName = "empty";
			
			uis_TopText.atlas = gd.atlas_counterTiles;
			uis_TopText.spriteName = "76t";
			
			uis_Object.spriteName = "empty";
			
			inFinish [0] = true;		
			inFinish [2] = true;	
			break;
			
		case 77:
		case 97:
			bChange = true;
			uis_Base.spriteName = "empty";
			uis_Base1.spriteName = "empty";
			if(ID >= 90)
			{
				bChange = false;
				uis_Base1.spriteName = "gray";
			}
			uis_Tile.spriteName = "73b";
			uis1.spriteName = "73a1";
			uis1.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis1.invert = true;
			
			uis2.spriteName = "empty";
			//			uis2.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			//			uis2.invert = false;
			
			uis3.spriteName = "73a2";
			uis3.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis3.invert = false;
			
			uis4.spriteName = "73a3";
			uis4.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis4.invert = true;
			
			uis_Top.spriteName = "empty";
			
			uis_TopText.atlas = gd.atlas_counterTiles;
			uis_TopText.spriteName = "77t";
			
			uis_Object.spriteName = "empty";
			
			inFinish [1] = true;		
			inFinish [3] = true;		
			break;
			
		case 80:
		case 100:
			bChange = true;
			uis_Base.spriteName = "empty";
			uis_Base1.spriteName = "empty";
			if(ID >= 90)
			{
				bChange = false;
				uis_Base1.spriteName = "gray";
			}
			uis_Tile.spriteName = "80b";
			uis1.spriteName = "80a1";
			uis1.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis1.invert = true;
			
			uis2.spriteName = "80a3";
			uis2.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis2.invert = false;
			
			uis3.spriteName = "80a4";
			uis3.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis3.invert = true;
			
			uis4.spriteName = "80a2";
			uis4.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis4.invert = true;
			
			uis_Top.spriteName = "empty";
			
			uis_TopText.atlas = gd.atlas_clockTiles;
			uis_TopText.spriteName = "80t";
			
			uis_Object.spriteName = "empty";
			break;
			
		case 81:
		case 101:
			bChange = true;
			uis_Base.spriteName = "empty";
			uis_Base1.spriteName = "empty";
			if(ID >= 90)
			{
				bChange = false;
				uis_Base1.spriteName = "gray";
			}
			uis_Tile.spriteName = "80b";
			uis1.spriteName = "80a1";
			uis1.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis1.invert = false;
			
			uis2.spriteName = "80a3";
			uis2.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis2.invert = true;
			
			uis3.spriteName = "80a4";
			uis3.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis3.invert = true;
			
			uis4.spriteName = "80a2";
			uis4.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis4.invert = true;
			
			uis_Top.spriteName = "empty";
			
			uis_TopText.atlas = gd.atlas_clockTiles;
			uis_TopText.spriteName = "81t";
			
			uis_Object.spriteName = "empty";
			break;
			
		case 82:
		case 102:
			bChange = true;
			uis_Base.spriteName = "empty";
			uis_Base1.spriteName = "empty";
			if(ID >= 90)
			{
				bChange = false;
				uis_Base1.spriteName = "gray";
			}
			uis_Tile.spriteName = "80b";
			uis1.spriteName = "80a1";
			uis1.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis1.invert = false;
			
			uis2.spriteName = "80a3";
			uis2.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis2.invert = false;
			
			uis3.spriteName = "80a4";
			uis3.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis3.invert = false;
			
			uis4.spriteName = "80a2";
			uis4.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis4.invert = true;
			
			uis_Top.spriteName = "empty";
			
			uis_TopText.atlas = gd.atlas_clockTiles;
			uis_TopText.spriteName = "82t";
			
			uis_Object.spriteName = "empty";
			break;
			
		case 83:
		case 103:
			bChange = true;
			uis_Base.spriteName = "empty";
			uis_Base1.spriteName = "empty";
			if(ID >= 90)
			{
				bChange = false;
				uis_Base1.spriteName = "gray";
			}
			uis_Tile.spriteName = "80b";
			uis1.spriteName = "80a1";
			uis1.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis1.invert = false;
			
			uis2.spriteName = "80a3";
			uis2.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis2.invert = false;
			
			uis3.spriteName = "80a4";
			uis3.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis3.invert = true;
			
			uis4.spriteName = "80a2";
			uis4.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis4.invert = false;
			
			uis_Top.spriteName = "empty";
			
			uis_TopText.atlas = gd.atlas_clockTiles;
			uis_TopText.spriteName = "83t";
			
			uis_Object.spriteName = "empty";
			break;
			
		case 84:
		case 104:
			bChange = true;
			uis_Base.spriteName = "empty";
			uis_Base1.spriteName = "empty";
			if(ID >= 90)
			{
				bChange = false;
				uis_Base1.spriteName = "gray";
			}
			uis_Tile.spriteName = "80b";
			uis1.spriteName = "80a1";
			uis1.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis1.invert = false;
			
			uis2.spriteName = "80a3";
			uis2.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis2.invert = true;
			
			uis3.spriteName = "80a4";
			uis3.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis3.invert = false;
			
			uis4.spriteName = "80a2";
			uis4.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis4.invert = false;
			
			uis_Top.spriteName = "empty";
			
			uis_TopText.atlas = gd.atlas_counterTiles;
			uis_TopText.spriteName = "84t";
			
			uis_Object.spriteName = "empty";
			
			inFinish [0] = true;		
			break;
			
		case 85:
		case 105:
			bChange = true;
			uis_Base.spriteName = "empty";
			uis_Base1.spriteName = "empty";
			if(ID >= 90)
			{
				bChange = false;
				uis_Base1.spriteName = "gray";
			}
			uis_Tile.spriteName = "80b";
			uis1.spriteName = "80a1";
			uis1.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis1.invert = true;
			
			uis2.spriteName = "80a3";
			uis2.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis2.invert = false;
			
			uis3.spriteName = "80a4";
			uis3.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis3.invert = false;
			
			uis4.spriteName = "80a2";
			uis4.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis4.invert = false;
			
			uis_Top.spriteName = "empty";
			
			uis_TopText.atlas = gd.atlas_counterTiles;
			uis_TopText.spriteName = "85t";
			
			uis_Object.spriteName = "empty";
			
			inFinish [1] = true;		
			break;
			
		case 86:
		case 106:
			bChange = true;
			uis_Base.spriteName = "empty";
			uis_Base1.spriteName = "empty";
			if(ID >= 90)
			{
				bChange = false;
				uis_Base1.spriteName = "gray";
			}
			uis_Tile.spriteName = "80b";
			uis1.spriteName = "80a1";
			uis1.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis1.invert = true;
			
			uis2.spriteName = "80a3";
			uis2.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis2.invert = true;
			
			uis3.spriteName = "80a4";
			uis3.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis3.invert = true;
			
			uis4.spriteName = "80a2";
			uis4.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis4.invert = false;
			
			uis_Top.spriteName = "empty";
			
			uis_TopText.atlas = gd.atlas_counterTiles;
			uis_TopText.spriteName = "86t";
			
			uis_Object.spriteName = "empty";
			
			inFinish [2] = true;		
			break;
			
		case 87:
		case 107:
			bChange = true;
			uis_Base.spriteName = "empty";
			uis_Base1.spriteName = "empty";
			if(ID >= 90)
			{
				bChange = false;
				uis_Base1.spriteName = "gray";
			}
			uis_Tile.spriteName = "80b";
			uis1.spriteName = "80a1";
			uis1.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis1.invert = true;
			
			uis2.spriteName = "80a3";
			uis2.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis2.invert = true;
			
			uis3.spriteName = "80a4";
			uis3.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis3.invert = false;
			
			uis4.spriteName = "80a2";
			uis4.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis4.invert = true;
			
			uis_Top.spriteName = "empty";
			
			uis_TopText.atlas = gd.atlas_counterTiles;
			uis_TopText.spriteName = "87t";
			
			uis_Object.spriteName = "empty";
			
			inFinish [3] = true;		
			break;
			
			/////////////////////////////////////////////////////////////////////////////////////////////////
		case 110:
		case 134:
		case 144:
		case 154:
			bChange = false;
			uis_Base.spriteName = "_back";
			if(ID == 110)
			{
				uis_Base1.spriteName = "blue";
			}
			else if(ID == 134)
			{
				uis_Base1.spriteName = "yellow";
			}
			else if(ID == 144)
			{
				uis_Base1.spriteName = "green";
			}
			else if(ID == 154)
			{
				uis_Base1.spriteName = "red";
			}
			uis_Tile.spriteName = "110b";
			uis1.spriteName = "110a";
			uis1.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis1.invert = true;
			
			uis2.spriteName = "empty";
			uis3.spriteName = "empty";
			uis4.spriteName = "empty";
			
			uis_Top.atlas = gd.atlas_textTiles;
			uis_Top.spriteName = "110s";
			
			uis_TopText.atlas = gd.atlas_textTiles;
			if(ID == 110)
			{
				uis_TopText.spriteName = "110t";
			}
			else 
			{
				uis_TopText.spriteName = "134t";
			}
			
			uis_Object.spriteName = "empty";
			break;
			
		case 111:
		case 135:
		case 145:
		case 155:
			bChange = false;
			uis_Base.spriteName = "_back";
			if(ID == 111)
			{
				uis_Base1.spriteName = "blue";
			}
			else if(ID == 135)
			{
				uis_Base1.spriteName = "yellow";
			}
			else if(ID == 145)
			{
				uis_Base1.spriteName = "green";
			}
			else if(ID == 155)
			{
				uis_Base1.spriteName = "red";
			}
			uis_Tile.spriteName = "111b";
			uis1.spriteName = "111a";
			uis1.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis1.invert = false;
			
			uis2.spriteName = "empty";
			uis3.spriteName = "empty";
			uis4.spriteName = "empty";
			
			uis_Top.atlas = gd.atlas_textTiles;
			uis_Top.spriteName = "111s";
			
			uis_TopText.atlas = gd.atlas_textTiles;
			if(ID == 111)
			{
				uis_TopText.spriteName = "111t";
			}
			else 
			{
				uis_TopText.spriteName = "135t";
			}
			
			uis_Object.spriteName = "empty";
			break;
			
		case 112:
		case 136:
		case 146:
		case 156:
			bChange = false;
			uis_Base.spriteName = "_back";
			if(ID == 112)
			{
				uis_Base1.spriteName = "blue";
			}
			else if(ID == 136)
			{
				uis_Base1.spriteName = "yellow";
			}
			else if(ID == 146)
			{
				uis_Base1.spriteName = "green";
			}
			else if(ID == 156)
			{
				uis_Base1.spriteName = "red";
			}
			uis_Tile.spriteName = "112b";
			uis1.spriteName = "112a";
			uis1.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis1.invert = true;
			
			uis2.spriteName = "empty";
			uis3.spriteName = "empty";
			uis4.spriteName = "empty";
			
			uis_Top.atlas = gd.atlas_textTiles;
			uis_Top.spriteName = "112s";
			
			uis_TopText.atlas = gd.atlas_textTiles;
			if(ID == 112)
			{
				uis_TopText.spriteName = "112t";
			}
			else 
			{
				uis_TopText.spriteName = "136t";
			}
			
			uis_Object.spriteName = "empty";
			break;
			
		case 113:
		case 137:
		case 147:
		case 157:
			bChange = false;
			uis_Base.spriteName = "_back";
			if(ID == 113)
			{
				uis_Base1.spriteName = "blue";
			}
			else if(ID == 137)
			{
				uis_Base1.spriteName = "yellow";
			}
			else if(ID == 147)
			{
				uis_Base1.spriteName = "green";
			}
			else if(ID == 157)
			{
				uis_Base1.spriteName = "red";
			}
			uis_Tile.spriteName = "113b";
			uis1.spriteName = "113a";
			uis1.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis1.invert = false;
			
			uis2.spriteName = "empty";
			uis3.spriteName = "empty";
			uis4.spriteName = "empty";
			
			uis_Top.atlas = gd.atlas_textTiles;
			uis_Top.spriteName = "113s";
			
			uis_TopText.atlas = gd.atlas_textTiles;
			if(ID == 113)
			{
				uis_TopText.spriteName = "113t";
			}
			else 
			{
				uis_TopText.spriteName = "137t";
			}
			
			uis_Object.spriteName = "empty";
			break;
			
			////////////////////////////////////////////////////////////////////////////////////////////////
		case 120:
		case 130:
		case 140:
		case 150:
			bChange = false;
			uis_Base.spriteName = "_back";
			if(ID == 120)
			{
				uis_Base1.spriteName = "orange";
			}
			else if(ID == 130)
			{
				uis_Base1.spriteName = "yellow";
			}
			else if(ID == 140)
			{
				uis_Base1.spriteName = "green";
			}
			else if(ID == 150)
			{
				uis_Base1.spriteName = "red";
			}
			uis_Tile.spriteName = "110b";
			uis1.spriteName = "110a";
			uis1.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis1.invert = false;
			
			uis2.spriteName = "empty";
			uis3.spriteName = "empty";
			uis4.spriteName = "empty";
			
			uis_Top.atlas = gd.atlas_textTiles;
			uis_Top.spriteName = "120s";
			
			uis_TopText.atlas = gd.atlas_textTiles;
			if(ID == 120)
			{
				uis_TopText.spriteName = "120t";
			}
			else 
			{
				uis_TopText.spriteName = "130t";
			}
			
			uis_Object.spriteName = "empty";
			break;
			
		case 121:
		case 131:
		case 141:
		case 151:
			bChange = false;
			uis_Base.spriteName = "_back";
			if(ID == 121)
			{
				uis_Base1.spriteName = "orange";
			}
			else if(ID == 131)
			{
				uis_Base1.spriteName = "yellow";
			}
			else if(ID == 141)
			{
				uis_Base1.spriteName = "green";
			}
			else if(ID == 151)
			{
				uis_Base1.spriteName = "red";
			}
			uis_Tile.spriteName = "111b";
			uis1.spriteName = "111a";
			uis1.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis1.invert = true;
			
			uis2.spriteName = "empty";
			uis3.spriteName = "empty";
			uis4.spriteName = "empty";
			
			uis_Top.atlas = gd.atlas_textTiles;
			uis_Top.spriteName = "121s";
			
			uis_TopText.atlas = gd.atlas_textTiles;
			if(ID == 121)
			{
				uis_TopText.spriteName = "121t";
			}
			else 
			{
				uis_TopText.spriteName = "131t";
			}
			
			uis_Object.spriteName = "empty";
			break;
			
		case 122:
		case 132:
		case 142:
		case 152:
			bChange = false;
			uis_Base.spriteName = "_back";
			if(ID == 122)
			{
				uis_Base1.spriteName = "orange";
			}
			else if(ID == 132)
			{
				uis_Base1.spriteName = "yellow";
			}
			else if(ID == 142)
			{
				uis_Base1.spriteName = "green";
			}
			else if(ID == 152)
			{
				uis_Base1.spriteName = "red";
			}
			uis_Tile.spriteName = "112b";
			uis1.spriteName = "112a";
			uis1.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis1.invert = false;
			
			uis2.spriteName = "empty";
			uis3.spriteName = "empty";
			uis4.spriteName = "empty";
			
			uis_Top.atlas = gd.atlas_textTiles;
			uis_Top.spriteName = "122s";
			
			uis_TopText.atlas = gd.atlas_textTiles;
			if(ID == 122)
			{
				uis_TopText.spriteName = "122t";
			}
			else 
			{
				uis_TopText.spriteName = "132t";
			}
			
			uis_Object.spriteName = "empty";
			break;
			
		case 123:
		case 133:
		case 143:
		case 153:
			bChange = false;
			uis_Base.spriteName = "_back";
			if(ID == 123)
			{
				uis_Base1.spriteName = "orange";
			}
			else if(ID == 133)
			{
				uis_Base1.spriteName = "yellow";
			}
			else if(ID == 143)
			{
				uis_Base1.spriteName = "green";
			}
			else if(ID == 153)
			{
				uis_Base1.spriteName = "red";
			}
			uis_Tile.spriteName = "113b";
			uis1.spriteName = "113a";
			uis1.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis1.invert = true;
			
			uis2.spriteName = "empty";
			uis3.spriteName = "empty";
			uis4.spriteName = "empty";
			
			uis_Top.atlas = gd.atlas_textTiles;
			uis_Top.spriteName = "123s";
			
			uis_TopText.atlas = gd.atlas_textTiles;
			if(ID == 123)
			{
				uis_TopText.spriteName = "123t";
			}
			else 
			{
				uis_TopText.spriteName = "133t";
			}
			
			uis_Object.spriteName = "empty";
			break;
			
			
		case 160:
		case 161:
		case 162:
		case 163:
		case 164:
		case 165:
		case 166:
		case 167:
		case 174:
		case 175:
		case 176:
		case 177:
		case 180: 
		case 181:
		case 182: 
		case 183:
		case 185:
		case 186:
		case 187:
		case 188:
		case 189: 
		case 190: 
		case 191:
			bChange = false;
			uis_Base.spriteName = ""+ID;
			uis_Base1.spriteName = "empty";
			uis_Tile.spriteName = "empty";
			uis1.spriteName = "empty";
			uis2.spriteName = "empty";
			uis3.spriteName = "empty";
			uis4.spriteName = "empty";
			uis_Top.spriteName = "empty";
			uis_TopText.spriteName = "empty";
			uis_Object.spriteName = "empty";
			break;
			
		case 170:
			bChange = false;
			uis_Base.spriteName = "160";
			uis_Base1.spriteName = "empty";
			uis_Tile.spriteName = "170b";
			
			uis1.spriteName = "170a";
			uis1.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis1.invert = false;
			
			uis2.spriteName = "170a";
			uis2.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis2.invert = true;
			
			uis3.spriteName = "empty";
			uis4.spriteName = "empty";
			uis_Top.spriteName = "empty";
			uis_TopText.spriteName = "170t";
			uis_Object.spriteName = "empty";
			break;
			
		case 171:
			bChange = false;
			uis_Base.spriteName = "161";
			uis_Base1.spriteName = "empty";
			uis_Tile.spriteName = "171b";
			
			uis1.spriteName = "172a";
			uis1.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis1.invert = false;
			
			uis2.spriteName = "172a";
			uis2.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis2.invert = true;
			
			uis3.spriteName = "empty";
			uis4.spriteName = "empty";
			uis_Top.spriteName = "empty";
			uis_TopText.spriteName = "171t";
			uis_Object.spriteName = "empty";
			break;
			
		case 172:
			bChange = false;
			uis_Base.spriteName = "162";
			uis_Base1.spriteName = "empty";
			uis_Tile.spriteName = "172b";
			
			uis1.spriteName = "171a";
			uis1.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis1.invert = false;
			
			uis2.spriteName = "171a";
			uis2.fillDirection = UIBasicSprite.FillDirection.Vertical;
			uis2.invert = true;
			
			uis3.spriteName = "empty";
			uis4.spriteName = "empty";
			uis_Top.spriteName = "empty";
			uis_TopText.spriteName = "172t";
			uis_Object.spriteName = "empty";
			break;
			
		case 173:
			bChange = false;
			uis_Base.spriteName = "163";
			uis_Base1.spriteName = "empty";
			uis_Tile.spriteName = "173b";
			
			uis1.spriteName = "173a";
			uis1.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis1.invert = false;
			
			uis2.spriteName = "173a";
			uis2.fillDirection = UIBasicSprite.FillDirection.Horizontal;
			uis2.invert = true;
			
			uis3.spriteName = "empty";
			uis4.spriteName = "empty";
			uis_Top.spriteName = "empty";
			uis_TopText.spriteName = "173t";
			uis_Object.spriteName = "empty";
			break;
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		}
	}
	
	
	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void Start () {
	}
	
	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Update is called once per frame
	void Update () {
		
		switch(ID)
		{
		case 10: case 20: case 30: case 40: case 50: case 60:
			ani_10 ();
			break;
			
		case 11: case 21: case 31: case 41: case 51: case 61:
			ani_11 ();
			break;
			
		case 12: case 22: case 32: case 42: case 52: case 62: 
			ani_12 ();
			break;
			
		case 13: case 23: case 33: case 43: case 53: case 63:
			ani_13 ();
			break;
			
		case 14: case 24: case 34: case 44: case 54: case 64: 
			ani_14 ();
			break;
			
		case 15: case 25: case 35: case 45: case 55: case 65: 
			ani_15 ();
			break;
			
		case 16: case 46: 
			ani_16 ();
			break;
			
		case 70: case 90:
			ani_70 ();
			break;
			
		case 71: case 91:
			ani_71 ();
			break;
			
		case 72: case 92:
			ani_72 ();
			break;
			
		case 73: case 93:
			ani_73 ();
			break;
			
		case 74: case 94:
			ani_74 ();
			break;
			
		case 75: case 95:
			ani_75 ();
			break;
			
		case 76: case 96:
			ani_76 ();
			break;
			
		case 77: case 97:
			ani_77 ();
			break;
			
		case 80: case 100:
			ani_80();
			break;
			
		case 81: case 101:
			ani_81();
			break;
			
		case 82: case 102:
			ani_82();
			break;
			
		case 83: case 103:
			ani_83();
			break;
			
		case 84: case 104:
			ani_84();
			break;
			
		case 85: case 105:
			ani_85();
			break;
			
		case 86: case 106:
			ani_86();
			break;
			
		case 87: case 107:
			ani_87();
			break;
			
		case 110: case 111: case 112: case 113:
		case 134: case 135: case 136: case 137:
		case 144: case 145: case 146: case 147:
		case 154: case 155: case 156: case 157:
			ani_start();
			break;
			
		case 120: case 121: case 122: case 123: 
			ani_end();
			break;
			
		case 130: case 131: case 132: case 133:
		case 140: case 141: case 142: case 143:
		case 150: case 151: case 152: case 153:
			ani_in ();
			break;
			
		case 170:
			ani_170 ();
			break;
			
		case 171:
			ani_171 ();
			break;
			
		case 172:
			ani_172 ();
			break;
			
		case 173:
			ani_173 ();
			break;
			
			
			
		}
	}
	
	
	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void OnClick()
	{
		//Debug.Log("gameCon.bFailMark: "+gameCon.bLock_All_Trigger);
		
		if(!gameCon.bGameStart)
		{
			return;
		}
		
		if (gameCon.bLock_All_Trigger) {
			
			return;
		}
		
		if (gameCon.useWherePipe == 0) 
		{
			if(gameCon.checkReadyMove())
			{
				if (bChange) 
				{
					gameCon.pos_eatItem = new POS();
					gameCon.pos_eatItem.x = xpos;
					gameCon.pos_eatItem.y = ypos;
					gameCon.nowPipeIndex = idx;
					gameCon.nowPipeID = ID;
					gameCon.nowFireID = gameCon.getFireReadyPipe();
					gameCon.v3pos_nowPipe = GameObject.Find("Pipe"+idx).transform.localPosition;
					
					if(ID == 2)
					{
						gameCon.changeFireID();
					}

					gameCon.moveConveyor(false);
				}
			}
		} else if (gameCon.useWherePipe == 1) 
		{
			if(gameCon.checkReadyUseInventory())
			{
				if (bChange) 
				{
					gameCon.pos_eatItem = new POS();
					gameCon.pos_eatItem.x = xpos;
					gameCon.pos_eatItem.y = ypos;
					gameCon.nowPipeIndex = idx;
					gameCon.nowPipeID = ID;
					gameCon.nowFireID = gameCon.invenPipeID;
					gameCon.v3pos_nowPipe = GameObject.Find("Pipe"+idx).transform.localPosition;
					
					if(ID == 2)
					{
						gameCon.changeFireID();
					}
					gameCon.createNmoveInventoryPipe();
					
					gameCon.uis_InventoryItem [gameCon.invenCursor].fillAmount = 0;
					gameCon.timer_InventoryItem = 0;
					gameCon.bAni_InventoryItem = true;
				}
			}
		}
		
	}
	
	
	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void ani_10()
	{
		if (bStartAni[0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			if(type == 1)
			{
				uis1.fillAmount = timer[0] / GameCon.waterSpeed;
				if(uis1.fillAmount >= 0.5f && !bEatItem)
				{
					gameCon.checkEatItem(xpos, ypos);
					bEatItem = true;
				}
				
				if(uis1.fillAmount >= 1)
				{
					timer[0] = 0;
					bStartAni[0] = false;
					gameCon.set_bFinish(xpos, ypos, direction, true);
					gameCon.addEndCount(xpos, ypos);
					//gameCon.set_bFinish(pos, direction, true);
					GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
				}
			}
			else if(type == 2)
			{
				uis2.fillAmount = timer[0] / GameCon.waterSpeed;
				if(uis2.fillAmount >= 0.5f && !bEatItem)
				{
					gameCon.checkEatItem(xpos, ypos);
					bEatItem = true;
				}
				
				if(uis2.fillAmount >= 1)
				{
					timer[0] = 0;
					bStartAni[0] = false;
					gameCon.set_bFinish(xpos, ypos, direction, true);
					gameCon.addEndCount(xpos, ypos);
					//gameCon.set_bFinish(pos, direction, true);
					GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
				}
			}
		}
	}
	
	void ani_11()
	{
		if (bStartAni[0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			if(type == 3)
			{
				uis1.fillAmount = timer[0] / GameCon.waterSpeed;
				if(uis1.fillAmount >= 0.5f && !bEatItem)
				{
					gameCon.checkEatItem(xpos, ypos);
					bEatItem = true;
				}
				
				if(uis1.fillAmount >= 1)
				{
					timer[0] = 0;
					bStartAni[0] = false;
					gameCon.set_bFinish(xpos, ypos, direction, true);
					gameCon.addEndCount(xpos, ypos);
					//gameCon.set_bFinish(pos, direction, true);
					GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
				}
			}
			else if(type == 4)
			{
				uis2.fillAmount = timer[0] / GameCon.waterSpeed;
				if(uis2.fillAmount >= 0.5f && !bEatItem)
				{
					gameCon.checkEatItem(xpos, ypos);
					bEatItem = true;
				}
				
				if(uis2.fillAmount >= 1)
				{
					timer[0] = 0;
					bStartAni[0] = false;
					gameCon.set_bFinish(xpos, ypos, direction, true);
					gameCon.addEndCount(xpos, ypos);
					//gameCon.set_bFinish(pos, direction, true);
					GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
				}
			}
		}
	}
	
	void ani_12()
	{
		if (bStartAni[0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			
			if(type == 5)
			{
				uis1.fillAmount = timer[0] / GameCon.waterSpeed;
				if(uis1.fillAmount >= 0.5f && !bEatItem)
				{
					gameCon.checkEatItem(xpos, ypos);
					bEatItem = true;
				}
				
				if(uis1.fillAmount >= 1)
				{
					timer[0] = 0;
					bStartAni[0] = false;
					gameCon.set_bFinish(xpos, ypos, direction, true);
					gameCon.addEndCount(xpos, ypos);
					//gameCon.set_bFinish(pos, direction, true);
					GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
				}
			}
			else if(type == 6)
			{
				uis2.fillAmount = timer[0] / GameCon.waterSpeed;
				if(uis2.fillAmount >= 0.5f && !bEatItem)
				{
					gameCon.checkEatItem(xpos, ypos);
					bEatItem = true;
				}
				
				if(uis2.fillAmount >= 1)
				{
					timer[0] = 0;
					bStartAni[0] = false;
					gameCon.set_bFinish(xpos, ypos, direction, true);
					gameCon.addEndCount(xpos, ypos);
					//gameCon.set_bFinish(pos, direction, true);
					GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
				}
			}
		}
	}
	
	void ani_13()
	{
		if (bStartAni[0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			
			if(type == 7)
			{
				uis1.fillAmount = timer[0] / GameCon.waterSpeed;
				if(uis1.fillAmount >= 0.5f && !bEatItem)
				{
					gameCon.checkEatItem(xpos, ypos);
					bEatItem = true;
				}
				
				if(uis1.fillAmount >= 1)
				{
					timer[0] = 0;
					bStartAni[0] = false;
					gameCon.set_bFinish(xpos, ypos, direction, true);
					gameCon.addEndCount(xpos, ypos);
					GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
				}
			}
			else if(type == 8)
			{
				uis2.fillAmount = timer[0] / GameCon.waterSpeed;
				if(uis2.fillAmount >= 0.5f && !bEatItem)
				{
					gameCon.checkEatItem(xpos, ypos);
					bEatItem = true;
				}
				
				if(uis2.fillAmount >= 1)
				{
					timer[0] = 0;
					bStartAni[0] = false;
					gameCon.set_bFinish(xpos, ypos, direction, true);
					gameCon.addEndCount(xpos, ypos);
					GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
				}
			}
		}
	}
	
	void ani_14()
	{
		if (bStartAni[0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			
			if(type == 9)
			{
				uis1.fillAmount = timer[0] / GameCon.waterSpeed;
				if(uis1.fillAmount >= 0.5f && !bEatItem)
				{
					gameCon.checkEatItem(xpos, ypos);
					bEatItem = true;
				}
				
				if(uis1.fillAmount >= 1)
				{
					timer[0] = 0;
					bStartAni[0] = false;
					gameCon.set_bFinish(xpos, ypos, direction, true);
					gameCon.addEndCount(xpos, ypos);
					GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
				}
			}
			else if(type == 10)
			{
				uis2.fillAmount = timer[0] / GameCon.waterSpeed;
				
				if(uis2.fillAmount >= 0.5f && !bEatItem)
				{
					gameCon.checkEatItem(xpos, ypos);
					bEatItem = true;
				}
				
				if(uis2.fillAmount >= 1)
				{
					timer[0] = 0;
					bStartAni[0] = false;
					gameCon.set_bFinish(xpos, ypos, direction, true);
					gameCon.addEndCount(xpos, ypos);
					GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
				}
			}
		}
	}
	
	void ani_15()
	{
		if (bStartAni[0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			
			if(type == 11)
			{
				uis1.fillAmount = timer[0] / GameCon.waterSpeed;
				if(uis1.fillAmount >= 0.5f && !bEatItem)
				{
					gameCon.checkEatItem(xpos, ypos);
					bEatItem = true;
				}
				
				if(uis1.fillAmount >= 1)
				{
					timer[0] = 0;
					bStartAni[0] = false;
					gameCon.set_bFinish(xpos, ypos, direction, true);
					gameCon.addEndCount(xpos, ypos);
					GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
				}
			}
			else if(type == 12)
			{
				uis2.fillAmount = timer[0] / GameCon.waterSpeed;
				if(uis2.fillAmount >= 0.5f && !bEatItem)
				{
					gameCon.checkEatItem(xpos, ypos);
					bEatItem = true;
				}
				
				if(uis2.fillAmount >= 1)
				{
					timer[0] = 0;
					bStartAni[0] = false;
					gameCon.set_bFinish(xpos, ypos, direction, true);
					gameCon.addEndCount(xpos, ypos);
					GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
				}
			}
		}
	}
	
	void ani_16()
	{
		if (bStartAni [0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			
			uis1.fillAmount = timer[0] / GameCon.waterSpeed;
			if(uis1.fillAmount >= 0.5f && !bEatItem)
			{
				gameCon.checkEatItem(xpos, ypos);
				bEatItem = true;
			}
			
			if (uis1.fillAmount >= 1) {
				timer[0] = 0;
				bStartAni[0] = false;
				gameCon.set_direction(xpos, ypos, 1);
				gameCon.set_bFinish(xpos, ypos, direction, true);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe (xpos, ypos);
			}
		}
		
		if (bStartAni [1] && GameCon.bAniCon) 
		{
			timer[1] += Time.deltaTime;
			
			uis2.fillAmount = timer[1] / GameCon.waterSpeed;
			if(uis2.fillAmount >= 0.5f && !bEatItem)
			{
				gameCon.checkEatItem(xpos, ypos);
				bEatItem = true;
			}
			
			if (uis2.fillAmount >= 1) {
				timer[1] = 0;
				bStartAni[1] = false;
				gameCon.set_direction(xpos, ypos, 3);
				gameCon.set_bFinish(xpos, ypos, direction, true);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe (xpos, ypos);
			}
		}
		
		if (bStartAni [2] && GameCon.bAniCon) 
		{
			timer[2] += Time.deltaTime;
			
			uis3.fillAmount = timer[2] / GameCon.waterSpeed;
			if(uis3.fillAmount >= 0.5f && !bEatItem)
			{
				gameCon.checkEatItem(xpos, ypos);
				bEatItem = true;
			}
			
			if (uis3.fillAmount >= 1) {
				timer[2] = 0;
				bStartAni[2] = false;
				gameCon.set_direction(xpos, ypos, 0);
				gameCon.set_bFinish(xpos, ypos, direction, true);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe (xpos, ypos);
			}
		}
		
		if (bStartAni [3] && GameCon.bAniCon) 
		{
			//			Debug.Log("P16 down");
			timer[3] += Time.deltaTime;
			
			uis4.fillAmount = timer[3] / GameCon.waterSpeed;
			if(uis4.fillAmount >= 0.5f && !bEatItem)
			{
				gameCon.checkEatItem(xpos, ypos);
				bEatItem = true;
			}
			
			if (uis4.fillAmount >= 1) {
				timer[3] = 0;
				bStartAni[3] = false;
				gameCon.set_direction(xpos, ypos, 2);
				gameCon.set_bFinish(xpos, ypos, direction, true);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe (xpos, ypos);
			}
		}
	}
	
	void ani_70()
	{
		if (bStartAni [0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			uis1.fillAmount = timer[0] / GameCon.waterSpeed;
			if(uis1.fillAmount >= 0.5f && !bEatItem)
			{
				gameCon.checkEatItem(xpos, ypos);
				bEatItem = true;
			}
			
			if(uis1.fillAmount >= 1)
			{
				timer[0] = 0;
				bStartAni[0] = false;
				gameCon.set_bFinish(xpos, ypos, 0, true);
				
				//				gameCon.set_direction(xpos, ypos, outDirection[0]);
				//				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		if (bStartAni [1] && GameCon.bAniCon) 
		{
			timer[1] += Time.deltaTime;
			uis2.fillAmount = timer[1] / GameCon.waterSpeed;
			if(uis2.fillAmount >= 1)
			{
				timer[1] = 0;
				bStartAni[1] = false;
				gameCon.set_bFinish(xpos, ypos, 1, true);
				gameCon.set_direction(xpos, ypos, outDirection[1]);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		//		if (bStartAni [2]) 
		//		{
		//			timer[2] += Time.deltaTime;
		//			uis3.fillAmount = timer[2] / GameCon.waterSpeed;
		//			if(uis3.fillAmount >= 1)
		//			{
		//				timer[2] = 0;
		//				bStartAni[2] = false;
		//				gameCon.set_bFinish(xpos, ypos, 2, true);
		//				gameCon.set_direction(xpos, ypos, outDirection[1]);
		//				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
		//			}
		//		}
		
		if (bStartAni [3] && GameCon.bAniCon) 
		{
			timer[3] += Time.deltaTime;
			uis4.fillAmount = timer[3] / GameCon.waterSpeed;
			if(uis4.fillAmount >= 1)
			{
				timer[3] = 0;
				bStartAni[3] = false;
				gameCon.set_bFinish(xpos, ypos, 3, true);
				gameCon.set_direction(xpos, ypos, outDirection[3]);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
	}
	
	void ani_71()
	{
		if (bStartAni [0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			uis1.fillAmount = timer[0] / GameCon.waterSpeed;
			if(uis1.fillAmount >= 1)
			{
				timer[0] = 0;
				bStartAni[0] = false;
				gameCon.set_bFinish(xpos, ypos, 0, true);
				gameCon.set_direction(xpos, ypos, outDirection[0]);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		if (bStartAni [1] && GameCon.bAniCon) 
		{
			timer[1] += Time.deltaTime;
			uis2.fillAmount = timer[1] / GameCon.waterSpeed;
			if(uis2.fillAmount >= 0.5f && !bEatItem)
			{
				gameCon.checkEatItem(xpos, ypos);
				bEatItem = true;
			}
			
			if(uis2.fillAmount >= 1)
			{
				timer[1] = 0;
				bStartAni[1] = false;
				gameCon.set_bFinish(xpos, ypos, 1, true);
				//				gameCon.set_direction(xpos, ypos, outDirection[1]);
				//				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		if (bStartAni [2] && GameCon.bAniCon) 
		{
			timer[2] += Time.deltaTime;
			uis3.fillAmount = timer[2] / GameCon.waterSpeed;
			if(uis3.fillAmount >= 1)
			{
				timer[2] = 0;
				bStartAni[2] = false;
				gameCon.set_bFinish(xpos, ypos, 2, true);
				gameCon.set_direction(xpos, ypos, outDirection[2]);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		//		if (bStartAni [3]) 
		//		{
		//			timer[3] += Time.deltaTime;
		//			uis4.fillAmount = timer[3] / GameCon.waterSpeed;
		//			if(uis4.fillAmount >= 1)
		//			{
		//				timer[3] = 0;
		//				bStartAni[3] = false;
		//				gameCon.set_bFinish(xpos, ypos, 3, true);
		//				gameCon.set_direction(xpos, ypos, outDirection[3]);
		//				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
		//			}
		//		}
	}
	
	void ani_72()
	{
		//if (bStartAni [0]) 
		//		{
		//			timer[0] += Time.deltaTime;
		//			uis1.fillAmount = timer[0] / GameCon.waterSpeed;
		//			if(uis1.fillAmount >= 1)
		//			{
		//				timer[0] = 0;
		//				bStartAni[0] = false;
		//				gameCon.set_bFinish(pos, 0, true);
		//				gameCon.set_direction(pos, outDirection[0]);
		//				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(pos);
		//			}
		//		}
		
		if (bStartAni [1] && GameCon.bAniCon) 
		{
			timer[1] += Time.deltaTime;
			uis2.fillAmount = timer[1] / GameCon.waterSpeed;
			if(uis2.fillAmount >= 1)
			{
				timer[1] = 0;
				bStartAni[1] = false;
				gameCon.set_bFinish(xpos, ypos, 1, true);
				gameCon.set_direction(xpos, ypos, outDirection[1]);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		if (bStartAni [2] && GameCon.bAniCon) 
		{
			timer[2] += Time.deltaTime;
			uis3.fillAmount = timer[2] / GameCon.waterSpeed;
			if(uis3.fillAmount >= 0.5f && !bEatItem)
			{
				gameCon.checkEatItem(xpos, ypos);
				bEatItem = true;
			}
			
			if(uis3.fillAmount >= 1)
			{
				timer[2] = 0;
				bStartAni[2] = false;
				gameCon.set_bFinish(xpos, ypos, 2, true);
				//				gameCon.set_direction(xpos, ypos, outDirection[2]);
				//				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		if (bStartAni [3] && GameCon.bAniCon) 
		{
			timer[3] += Time.deltaTime;
			uis4.fillAmount = timer[3] / GameCon.waterSpeed;
			if(uis4.fillAmount >= 1)
			{
				timer[3] = 0;
				bStartAni[3] = false;
				gameCon.set_bFinish(xpos, ypos, 3, true);
				gameCon.set_direction(xpos, ypos, outDirection[3]);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
	}
	
	void ani_73()
	{
		if (bStartAni [0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			uis1.fillAmount = timer[0] / GameCon.waterSpeed;
			if(uis1.fillAmount >= 1)
			{
				timer[0] = 0;
				bStartAni[0] = false;
				gameCon.set_bFinish(xpos, ypos, 0, true);
				gameCon.set_direction(xpos, ypos, outDirection[0]);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		//		if (bStartAni [1]) 
		//		{
		//			timer[1] += Time.deltaTime;
		//			uis2.fillAmount = timer[1] / GameCon.waterSpeed;
		//			if(uis2.fillAmount >= 1)
		//			{
		//				timer[1] = 0;
		//				bStartAni[1] = false;
		//				gameCon.set_bFinish(xpos, ypos, 1, true);
		//				gameCon.set_direction(xpos, ypos, outDirection[1]);
		//				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
		//			}
		//		}
		
		if (bStartAni [2] && GameCon.bAniCon) 
		{
			timer[2] += Time.deltaTime;
			uis3.fillAmount = timer[2] / GameCon.waterSpeed;
			if(uis3.fillAmount >= 1)
			{
				timer[2] = 0;
				bStartAni[2] = false;
				gameCon.set_bFinish(xpos, ypos, 2, true);
				gameCon.set_direction(xpos, ypos, outDirection[2]);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		if (bStartAni [3] && GameCon.bAniCon) 
		{
			timer[3] += Time.deltaTime;
			uis4.fillAmount = timer[3] / GameCon.waterSpeed;
			if(uis4.fillAmount >= 0.5f && !bEatItem)
			{
				gameCon.checkEatItem(xpos, ypos);
				bEatItem = true;
			}
			
			if(uis4.fillAmount >= 1)
			{
				timer[3] = 0;
				bStartAni[3] = false;
				gameCon.set_bFinish(xpos, ypos, 3, true);
				//				gameCon.set_direction(xpos, ypos, outDirection[3]);
				//				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
	}
	
	void ani_74()
	{
		if (bStartAni [0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			uis1.fillAmount = timer[0] / GameCon.waterSpeed +0.5f;
			if(uis1.fillAmount >= 0.5f && !bEatItem)
			{
				gameCon.checkEatItem(xpos, ypos);
				bEatItem = true;
			}
			
			if(uis1.fillAmount >= 1)
			{
				timer[0] = 0;
				bStartAni[0] = false;
				gameCon.set_bFinish(xpos, ypos, 0, true);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		if (bStartAni [1] && GameCon.bAniCon) 
		{
			timer[1] += Time.deltaTime;
			uis2.fillAmount = timer[1] / GameCon.waterSpeed;
			if(uis2.fillAmount >= 0.5f)
			{
				timer[1] = 0;
				bStartAni[1] = false;
				gameCon.set_bFinish(xpos, ypos, 1, true);
				inFinish[1] = true;
				if (!bStartAni [0]) 
				{
					if(checkAllIn())
					{
						timer[0] = 0;
						bStartAni[0] = true;
					}
				}
			}
		}
		
		//		if (bStartAni [2]) 
		//		{
		//			timer[2] += Time.deltaTime;
		//			uis3.fillAmount = timer[2] / GameCon.waterSpeed;
		//			if(uis3.fillAmount >= 0.5f)
		//			{
		//				timer[2] = 0;
		//				bStartAni[2] = false;
		//				gameCon.set_bFinish(xpos, ypos, 2, true);
		//				inFinish[2] = true;
		//				
		//				if (!bStartAni [0]) 
		//				{
		//					if(checkAllIn())
		//					{
		//						timer[0] = 0;
		//						bStartAni[0] = true;
		//					}
		//				}
		//			}
		//		}
		
		if (bStartAni [3] && GameCon.bAniCon) 
		{
			timer[3] += Time.deltaTime;
			uis4.fillAmount = timer[3] / GameCon.waterSpeed;
			if(uis4.fillAmount >= 0.5f)
			{
				timer[3] = 0;
				bStartAni[3] = false;
				gameCon.set_bFinish(xpos, ypos, 3, true);
				inFinish[3] = true;
				
				if (!bStartAni [0]) 
				{
					if(checkAllIn())
					{
						timer[0] = 0;
						bStartAni[0] = true;
					}
				}
			}
		}
	}
	
	void ani_75()
	{
		if (bStartAni [0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			uis1.fillAmount = timer[0] / GameCon.waterSpeed;
			if(uis1.fillAmount >= 0.5f)
			{
				timer[0] = 0;
				bStartAni[0] = false;
				gameCon.set_bFinish(xpos, ypos, 0, true);
				inFinish[0] = true;
				if (!bStartAni [1]) 
				{
					if(checkAllIn())
					{
						timer[1] = 0;
						bStartAni[1] = true;
					}
				}
				
				
			}
		}
		
		if (bStartAni [1] && GameCon.bAniCon) 
		{
			timer[1] += Time.deltaTime;
			uis2.fillAmount = timer[1] / GameCon.waterSpeed+0.5f;
			if(uis2.fillAmount >= 0.5f && !bEatItem)
			{
				gameCon.checkEatItem(xpos, ypos);
				bEatItem = true;
			}
			
			if(uis2.fillAmount >= 1)
			{
				timer[1] = 0;
				bStartAni[1] = false;
				gameCon.set_bFinish(xpos, ypos, 1, true);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		if (bStartAni [2] && GameCon.bAniCon) 
		{
			timer[2] += Time.deltaTime;
			uis3.fillAmount = timer[2] / GameCon.waterSpeed;
			if(uis3.fillAmount >= 0.5f)
			{
				timer[2] = 0;
				bStartAni[2] = false;
				gameCon.set_bFinish(xpos, ypos, 2, true);
				inFinish[2] = true;
				
				if (!bStartAni [1]) 
				{
					if(checkAllIn())
					{
						timer[1] = 0;
						bStartAni[1] = true;
					}
				}
			}
		}
		
		//		if (bStartAni [3]) 
		//		{
		//			timer[3] += Time.deltaTime;
		//			uis4.fillAmount = timer[3] / GameCon.waterSpeed;
		//			if(uis4.fillAmount >=  0.5f)
		//			{
		//				timer[3] = 0;
		//				bStartAni[3] = false;
		//				gameCon.set_bFinish(pos, 3, true);
		//				inFinish[3] = true;
		//				
		//				if (!bStartAni [1]) 
		//				{
		//					if(checkAllIn())
		//					{
		//						timer[1] = 0;
		//						bStartAni[1] = true;
		//					}
		//				}
		//			}
		//		}
	}
	
	void ani_76()
	{
		//		if (bStartAni [0]) 
		//		{
		//			timer[0] += Time.deltaTime;
		//			uis1.fillAmount = timer[0] / GameCon.waterSpeed;
		//			if(uis1.fillAmount >= 0.5f)
		//			{
		//				timer[0] = 0;
		//				bStartAni[0] = false;
		//				gameCon.set_bFinish(xpos, ypos, 0, true);
		//				inFinish[0] = true;
		//				if (!bStartAni [2]) 
		//				{
		//					if(checkAllIn())
		//					{
		//						timer[2] = 0;
		//						bStartAni[2] = true;
		//					}
		//				}
		//				
		//				
		//			}
		//		}
		
		if (bStartAni [1] && GameCon.bAniCon) 
		{
			timer[1] += Time.deltaTime;
			uis2.fillAmount = timer[1] / GameCon.waterSpeed;
			if(uis2.fillAmount >= 0.5f)
			{
				timer[1] = 0;
				bStartAni[1] = false;
				gameCon.set_bFinish(xpos, ypos, 1, true);
				inFinish[1] = true;
				
				if (!bStartAni [2]) 
				{
					if(checkAllIn())
					{
						timer[2] = 0;
						bStartAni[2] = true;
					}
				}
				
			}
		}
		
		if (bStartAni [2] && GameCon.bAniCon) 
		{
			timer[2] += Time.deltaTime;
			uis3.fillAmount = timer[2] / GameCon.waterSpeed+0.5f;
			if(uis3.fillAmount >= 0.5f && !bEatItem)
			{
				gameCon.checkEatItem(xpos, ypos);
				bEatItem = true;
			}
			
			if(uis3.fillAmount >= 1)
			{
				timer[2] = 0;
				bStartAni[2] = false;
				gameCon.set_bFinish(xpos, ypos, 2, true);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		if (bStartAni [3] && GameCon.bAniCon) 
		{
			timer[3] += Time.deltaTime;
			uis4.fillAmount = timer[3] / GameCon.waterSpeed;
			if(uis4.fillAmount >= 0.5f)
			{
				timer[3] = 0;
				bStartAni[3] = false;
				gameCon.set_bFinish(xpos, ypos, 3, true);
				inFinish[3] = true;
				
				if (!bStartAni [2]) 
				{
					if(checkAllIn())
					{
						timer[2] = 0;
						bStartAni[2] = true;
					}
				}
			}
		}
	}
	
	void ani_77()
	{
		if (bStartAni [0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			uis1.fillAmount = timer[0] / GameCon.waterSpeed;
			if(uis1.fillAmount >= 0.5f)
			{
				timer[0] = 0;
				bStartAni[0] = false;
				gameCon.set_bFinish(xpos, ypos, 0, true);
				inFinish[0] = true;
				if (!bStartAni [3]) 
				{
					if(checkAllIn())
					{
						timer[3] = 0;
						bStartAni[3] = true;
					}
				}
				
				
			}
		}
		
		//		if (bStartAni [1]) 
		//		{
		//			timer[1] += Time.deltaTime;
		//			uis2.fillAmount = timer[1] / GameCon.waterSpeed;
		//			if(uis2.fillAmount >= 0.5f)
		//			{
		//				timer[1] = 0;
		//				bStartAni[1] = false;
		//				gameCon.set_bFinish(xpos, ypos, 1, true);
		//				inFinish[1] = true;
		//				
		//				if (!bStartAni [3]) 
		//				{
		//					if(checkAllIn())
		//					{
		//						timer[3] = 0;
		//						bStartAni[3] = true;
		//					}
		//				}
		//				
		//			}
		//		}
		
		if (bStartAni [2] && GameCon.bAniCon) 
		{
			timer[2] += Time.deltaTime;
			uis3.fillAmount = timer[2] / GameCon.waterSpeed;
			if(uis3.fillAmount >= 0.5f)
			{
				timer[2] = 0;
				bStartAni[2] = false;
				gameCon.set_bFinish(xpos, ypos, 2, true);
				inFinish[2] = true;
				
				if (!bStartAni [3]) 
				{
					if(checkAllIn())
					{
						timer[3] = 0;
						bStartAni[3] = true;
					}
				}
			}
		}
		
		if (bStartAni [3] && GameCon.bAniCon) 
		{
			timer[3] += Time.deltaTime;
			uis4.fillAmount = timer[3] / GameCon.waterSpeed+0.5f;
			if(uis4.fillAmount >= 0.5f && !bEatItem)
			{
				gameCon.checkEatItem(xpos, ypos);
				bEatItem = true;
			}
			
			if(uis4.fillAmount >= 1)
			{
				timer[3] = 0;
				bStartAni[3] = false;
				gameCon.set_bFinish(xpos, ypos, 3, true);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
				
				
			}
		}
	}
	
	void ani_80()
	{
		if (bStartAni [0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			uis1.fillAmount = timer[0] / GameCon.waterSpeed;
			if(uis1.fillAmount >= 0.5f && !bEatItem)
			{
				gameCon.checkEatItem(xpos, ypos);
				bEatItem = true;
			}
			
			if(uis1.fillAmount >= 1)
			{
				timer[0] = 0;
				bStartAni[0] = false;
				gameCon.set_bFinish(xpos, ypos, 0, true);
				//				gameCon.set_direction(xpos, ypos, outDirection[0]);
				//				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		if (bStartAni [1] && GameCon.bAniCon) 
		{
			timer[1] += Time.deltaTime;
			uis2.fillAmount = timer[1] / GameCon.waterSpeed;
			if(uis2.fillAmount >= 1)
			{
				timer[1] = 0;
				bStartAni[1] = false;
				gameCon.set_bFinish(xpos, ypos, 1, true);
				gameCon.set_direction(xpos, ypos, outDirection[0]);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		if (bStartAni [2] && GameCon.bAniCon) 
		{
			timer[2] += Time.deltaTime;
			uis3.fillAmount = timer[2] / GameCon.waterSpeed;
			if(uis3.fillAmount >= 1)
			{
				timer[2] = 0;
				bStartAni[2] = false;
				gameCon.set_bFinish(xpos, ypos, 2, true);
				gameCon.set_direction(xpos, ypos, outDirection[1]);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		if (bStartAni [3] && GameCon.bAniCon) 
		{
			timer[3] += Time.deltaTime;
			uis4.fillAmount = timer[3] / GameCon.waterSpeed;
			if(uis4.fillAmount >= 1)
			{
				timer[3] = 0;
				bStartAni[3] = false;
				gameCon.set_bFinish(xpos, ypos, 3, true);
				gameCon.set_direction(xpos, ypos, outDirection[2]);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
	}
	
	void ani_81()
	{
		if (bStartAni [0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			uis1.fillAmount = timer[0] / GameCon.waterSpeed;
			if(uis1.fillAmount >= 1)
			{
				timer[0] = 0;
				bStartAni[0] = false;
				gameCon.set_bFinish(xpos, ypos, 0, true);
				gameCon.set_direction(xpos, ypos, outDirection[0]);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		if (bStartAni [1] && GameCon.bAniCon) 
		{
			timer[1] += Time.deltaTime;
			uis2.fillAmount = timer[1] / GameCon.waterSpeed;
			if(uis2.fillAmount >= 0.5f && !bEatItem)
			{
				gameCon.checkEatItem(xpos, ypos);
				bEatItem = true;
			}
			
			if(uis2.fillAmount >= 1)
			{
				timer[1] = 0;
				bStartAni[1] = false;
				gameCon.set_bFinish(xpos, ypos, 1, true);
				//				gameCon.set_direction(xpos, ypos, outDirection[1]);
				//				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		if (bStartAni [2] && GameCon.bAniCon) 
		{
			timer[2] += Time.deltaTime;
			uis3.fillAmount = timer[2] / GameCon.waterSpeed;
			if(uis3.fillAmount >= 1)
			{
				timer[2] = 0;
				bStartAni[2] = false;
				gameCon.set_bFinish(xpos, ypos, 2, true);
				gameCon.set_direction(xpos, ypos, outDirection[1]);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		if (bStartAni [3] && GameCon.bAniCon) 
		{
			timer[3] += Time.deltaTime;
			uis4.fillAmount = timer[3] / GameCon.waterSpeed;
			if(uis4.fillAmount >= 1)
			{
				timer[3] = 0;
				bStartAni[3] = false;
				gameCon.set_bFinish(xpos, ypos, 3, true);
				gameCon.set_direction(xpos, ypos, outDirection[2]);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
	}
	
	void ani_82()
	{
		if (bStartAni [0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			uis1.fillAmount = timer[0] / GameCon.waterSpeed;
			if(uis1.fillAmount >= 1)
			{
				timer[0] = 0;
				bStartAni[0] = false;
				gameCon.set_bFinish(xpos, ypos, 0, true);
				gameCon.set_direction(xpos, ypos, outDirection[0]);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		if (bStartAni [1] && GameCon.bAniCon) 
		{
			timer[1] += Time.deltaTime;
			uis2.fillAmount = timer[1] / GameCon.waterSpeed;
			if(uis2.fillAmount >= 1)
			{
				timer[1] = 0;
				bStartAni[1] = false;
				gameCon.set_bFinish(xpos, ypos, 1, true);
				gameCon.set_direction(xpos, ypos, outDirection[1]);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		if (bStartAni [2] && GameCon.bAniCon) 
		{
			timer[2] += Time.deltaTime;
			uis3.fillAmount = timer[2] / GameCon.waterSpeed;
			if(uis3.fillAmount >= 0.5f && !bEatItem)
			{
				gameCon.checkEatItem(xpos, ypos);
				bEatItem = true;
			}
			
			if(uis3.fillAmount >= 1)
			{
				timer[2] = 0;
				bStartAni[2] = false;
				gameCon.set_bFinish(xpos, ypos, 2, true);
				//				gameCon.set_direction(xpos, ypos, outDirection[2]);
				//				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		if (bStartAni [3] && GameCon.bAniCon) 
		{
			timer[3] += Time.deltaTime;
			uis4.fillAmount = timer[3] / GameCon.waterSpeed;
			if(uis4.fillAmount >= 1)
			{
				timer[3] = 0;
				bStartAni[3] = false;
				gameCon.set_bFinish(xpos, ypos, 3, true);
				gameCon.set_direction(xpos, ypos, outDirection[2]);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
	}
	
	void ani_83()
	{
		if (bStartAni [0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			uis1.fillAmount = timer[0] / GameCon.waterSpeed;
			if(uis1.fillAmount >= 1)
			{
				timer[0] = 0;
				bStartAni[0] = false;
				gameCon.set_bFinish(xpos, ypos, 0, true);
				gameCon.set_direction(xpos, ypos, outDirection[0]);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		if (bStartAni [1] && GameCon.bAniCon) 
		{
			timer[1] += Time.deltaTime;
			uis2.fillAmount = timer[1] / GameCon.waterSpeed;
			if(uis2.fillAmount >= 1)
			{
				timer[1] = 0;
				bStartAni[1] = false;
				gameCon.set_bFinish(xpos, ypos, 1, true);
				gameCon.set_direction(xpos, ypos, outDirection[1]);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		if (bStartAni [2] && GameCon.bAniCon) 
		{
			timer[2] += Time.deltaTime;
			uis3.fillAmount = timer[2] / GameCon.waterSpeed;
			if(uis3.fillAmount >= 1)
			{
				timer[2] = 0;
				bStartAni[2] = false;
				gameCon.set_bFinish(xpos, ypos, 2, true);
				gameCon.set_direction(xpos, ypos, outDirection[2]);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		if (bStartAni [3] && GameCon.bAniCon) 
		{
			timer[3] += Time.deltaTime;
			uis4.fillAmount = timer[3] / GameCon.waterSpeed;
			if(uis4.fillAmount >= 0.5f && !bEatItem)
			{
				gameCon.checkEatItem(xpos, ypos);
				bEatItem = true;
			}
			
			if(uis4.fillAmount >= 1)
			{
				timer[3] = 0;
				bStartAni[3] = false;
				gameCon.set_bFinish(xpos, ypos, 3, true);
			}
		}
	}
	
	void ani_84()
	{
		if (bStartAni [0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			uis1.fillAmount = timer[0] / GameCon.waterSpeed +0.5f;
			if(uis1.fillAmount >= 0.5f && !bEatItem)
			{
				gameCon.checkEatItem(xpos, ypos);
				bEatItem = true;
			}
			
			if(uis1.fillAmount >= 1)
			{
				timer[0] = 0;
				bStartAni[0] = false;
				gameCon.set_bFinish(xpos, ypos, 0, true);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		if (bStartAni [1] && GameCon.bAniCon) 
		{
			timer[1] += Time.deltaTime;
			uis2.fillAmount = timer[1] / GameCon.waterSpeed;
			if(uis2.fillAmount >= 0.5f)
			{
				timer[1] = 0;
				bStartAni[1] = false;
				gameCon.set_bFinish(xpos, ypos, 1, true);
				inFinish[1] = true;
				if (!bStartAni [0]) 
				{
					if(checkAllIn())
					{
						timer[0] = 0;
						bStartAni[0] = true;
					}
				}
			}
		}
		
		if (bStartAni [2] && GameCon.bAniCon) 
		{
			timer[2] += Time.deltaTime;
			uis3.fillAmount = timer[2] / GameCon.waterSpeed;
			if(uis3.fillAmount >= 0.5f)
			{
				timer[2] = 0;
				bStartAni[2] = false;
				gameCon.set_bFinish(xpos, ypos, 2, true);
				inFinish[2] = true;
				
				if (!bStartAni [0]) 
				{
					if(checkAllIn())
					{
						timer[0] = 0;
						bStartAni[0] = true;
					}
				}
			}
		}
		
		if (bStartAni [3] && GameCon.bAniCon) 
		{
			timer[3] += Time.deltaTime;
			uis4.fillAmount = timer[3] / GameCon.waterSpeed;
			if(uis4.fillAmount >= 0.5f)
			{
				timer[3] = 0;
				bStartAni[3] = false;
				gameCon.set_bFinish(xpos, ypos, 3, true);
				inFinish[3] = true;
				
				if (!bStartAni [0]) 
				{
					if(checkAllIn())
					{
						timer[0] = 0;
						bStartAni[0] = true;
					}
				}
			}
		}
	}
	
	void ani_85()
	{
		if (bStartAni [0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			uis1.fillAmount = timer[0] / GameCon.waterSpeed;
			if(uis1.fillAmount >= 0.5f)
			{
				timer[0] = 0;
				bStartAni[0] = false;
				gameCon.set_bFinish(xpos, ypos, 0, true);
				inFinish[0] = true;
				if (!bStartAni [1]) 
				{
					if(checkAllIn())
					{
						timer[1] = 0;
						bStartAni[1] = true;
					}
				}
				
				
			}
		}
		
		if (bStartAni [1] && GameCon.bAniCon) 
		{
			timer[1] += Time.deltaTime;
			uis2.fillAmount = timer[1] / GameCon.waterSpeed+0.5f;
			if(uis2.fillAmount >= 0.5f && !bEatItem)
			{
				gameCon.checkEatItem(xpos, ypos);
				bEatItem = true;
			}
			
			if(uis2.fillAmount >= 1)
			{
				timer[1] = 0;
				bStartAni[1] = false;
				gameCon.set_bFinish(xpos, ypos, 1, true);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		if (bStartAni [2] && GameCon.bAniCon) 
		{
			timer[2] += Time.deltaTime;
			uis3.fillAmount = timer[2] / GameCon.waterSpeed;
			if(uis3.fillAmount >= 0.5f)
			{
				timer[2] = 0;
				bStartAni[2] = false;
				gameCon.set_bFinish(xpos, ypos, 2, true);
				inFinish[2] = true;
				
				if (!bStartAni [1]) 
				{
					if(checkAllIn())
					{
						timer[1] = 0;
						bStartAni[1] = true;
					}
				}
			}
		}
		
		if (bStartAni [3] && GameCon.bAniCon) 
		{
			timer[3] += Time.deltaTime;
			uis4.fillAmount = timer[3] / GameCon.waterSpeed;
			if(uis4.fillAmount >=  0.5f)
			{
				timer[3] = 0;
				bStartAni[3] = false;
				gameCon.set_bFinish(xpos, ypos, 3, true);
				inFinish[3] = true;
				
				if (!bStartAni [1]) 
				{
					if(checkAllIn())
					{
						timer[1] = 0;
						bStartAni[1] = true;
					}
				}
			}
		}
	}
	
	void ani_86()
	{
		if (bStartAni [0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			uis1.fillAmount = timer[0] / GameCon.waterSpeed;
			if(uis1.fillAmount >= 0.5f)
			{
				timer[0] = 0;
				bStartAni[0] = false;
				gameCon.set_bFinish(xpos, ypos, 0, true);
				inFinish[0] = true;
				if (!bStartAni [2]) 
				{
					if(checkAllIn())
					{
						timer[2] = 0;
						bStartAni[2] = true;
					}
				}
				
				
			}
		}
		
		if (bStartAni [1] && GameCon.bAniCon) 
		{
			timer[1] += Time.deltaTime;
			uis2.fillAmount = timer[1] / GameCon.waterSpeed;
			if(uis2.fillAmount >= 0.5f)
			{
				timer[1] = 0;
				bStartAni[1] = false;
				gameCon.set_bFinish(xpos, ypos, 1, true);
				inFinish[1] = true;
				
				if (!bStartAni [2]) 
				{
					if(checkAllIn())
					{
						timer[2] = 0;
						bStartAni[2] = true;
					}
				}
				
			}
		}
		
		if (bStartAni [2] && GameCon.bAniCon) 
		{
			timer[2] += Time.deltaTime;
			uis3.fillAmount = timer[2] / GameCon.waterSpeed+0.5f;
			if(uis3.fillAmount >= 0.5f && !bEatItem)
			{
				gameCon.checkEatItem(xpos, ypos);
				bEatItem = true;
			}
			
			if(uis3.fillAmount >= 1)
			{
				timer[2] = 0;
				bStartAni[2] = false;
				gameCon.set_bFinish(xpos, ypos, 2, true);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
			}
		}
		
		if (bStartAni [3] && GameCon.bAniCon) 
		{
			timer[3] += Time.deltaTime;
			uis4.fillAmount = timer[3] / GameCon.waterSpeed;
			if(uis4.fillAmount >= 0.5f)
			{
				timer[3] = 0;
				bStartAni[3] = false;
				gameCon.set_bFinish(xpos, ypos, 3, true);
				inFinish[3] = true;
				
				if (!bStartAni [2]) 
				{
					if(checkAllIn())
					{
						timer[2] = 0;
						bStartAni[2] = true;
					}
				}
			}
		}
	}
	
	void ani_87()
	{
		if (bStartAni [0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			uis1.fillAmount = timer[0] / GameCon.waterSpeed;
			if(uis1.fillAmount >= 0.5f)
			{
				timer[0] = 0;
				bStartAni[0] = false;
				gameCon.set_bFinish(xpos, ypos, 0, true);
				inFinish[0] = true;
				if (!bStartAni [3]) 
				{
					if(checkAllIn())
					{
						timer[3] = 0;
						bStartAni[3] = true;
					}
				}
				
				
			}
		}
		
		if (bStartAni [1] && GameCon.bAniCon) 
		{
			timer[1] += Time.deltaTime;
			uis2.fillAmount = timer[1] / GameCon.waterSpeed;
			if(uis2.fillAmount >= 0.5f)
			{
				timer[1] = 0;
				bStartAni[1] = false;
				gameCon.set_bFinish(xpos, ypos, 1, true);
				inFinish[1] = true;
				
				if (!bStartAni [3]) 
				{
					if(checkAllIn())
					{
						timer[3] = 0;
						bStartAni[3] = true;
					}
				}
				
			}
		}
		
		if (bStartAni [2] && GameCon.bAniCon) 
		{
			timer[2] += Time.deltaTime;
			uis3.fillAmount = timer[2] / GameCon.waterSpeed;
			if(uis3.fillAmount >= 0.5f)
			{
				timer[2] = 0;
				bStartAni[2] = false;
				gameCon.set_bFinish(xpos, ypos, 2, true);
				inFinish[2] = true;
				
				if (!bStartAni [3]) 
				{
					if(checkAllIn())
					{
						timer[3] = 0;
						bStartAni[3] = true;
					}
				}
			}
		}
		
		if (bStartAni [3] && GameCon.bAniCon) 
		{
			timer[3] += Time.deltaTime;
			uis4.fillAmount = timer[3] / GameCon.waterSpeed+0.5f;
			if(uis4.fillAmount >= 0.5f && !bEatItem)
			{
				gameCon.checkEatItem(xpos, ypos);
				bEatItem = true;
			}
			
			if(uis4.fillAmount >= 1)
			{
				timer[3] = 0;
				bStartAni[3] = false;
				gameCon.set_bFinish(xpos, ypos, 3, true);
				gameCon.addEndCount(xpos, ypos);
				GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
				
				
			}
		}
	}
	
	void ani_start()
	{
		if (bStartAni[0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			
			if(type >= 17 && type <= 20)
			{
				uis1.fillAmount = timer[0] / GameCon.waterSpeed;
				
				if(uis1.fillAmount >= 0.5f && !bEatItem)
				{
					gameCon.checkEatItem(xpos, ypos);
					bEatItem = true;
				}
				
				if(uis1.fillAmount >= 1)
				{
					timer[0] = 0;
					bStartAni[0] = false;
					gameCon.set_bFinish(xpos, ypos, direction, true);
					gameCon.addEndCount(xpos, ypos);
					//gameCon.set_bFinish(xpos, ypos, direction, true);
					GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
				}
			}
		}
	}
	
	void ani_end()
	{
		if (bStartAni[0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			
			if(type >= 21 && type <= 24)
			{
				uis1.fillAmount = timer[0] / GameCon.waterSpeed;
				if(uis1.fillAmount >= 1)
				{
					timer[0] = 0;
					bStartAni[0] = false;
					gameCon.set_bEndFinish(xpos, ypos, true);
					gameCon.addEndCount(xpos, ypos);
					
					if(gameCon.check_End_all())
					{
						//check limit condition........
						gameCon.efm.stop(6);	//water sound
						gameCon.check_limit_Condition_Norm();
					}
				}
			}
		}
	}
	
	void ani_in()
	{
		if (bStartAni[0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			
			if(type >= 21 && type <= 24)
			{
				uis1.fillAmount = timer[0] / GameCon.waterSpeed;
				if(uis1.fillAmount >= 0.5f && !bEatItem)
				{
					gameCon.checkEatItem(xpos, ypos);
					bEatItem = true;
				}
				
				if(uis1.fillAmount >= 1)
				{
					timer[0] = 0;
					bStartAni[0] = false;
					gameCon.addEndCount(xpos, ypos);
					GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe_OUT(xpos, ypos);
				}
			}
		}
	}
	
	
	void ani_170()
	{
		if (bStartAni[0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			
			if(type == 53)
			{
				uis1.fillAmount = timer[0] / GameCon.waterSpeed;
				if(uis1.fillAmount >= 1)
				{
					timer[0] = 0;
					bStartAni[0] = false;
					gameCon.set_bFinish(xpos, ypos, direction, true);
					gameCon.addEndCount(xpos, ypos);
					GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
				}
			}
			else if(type == 54)
			{
				uis2.fillAmount = timer[0] / GameCon.waterSpeed;
				if(uis2.fillAmount >= 1)
				{
					timer[0] = 0;
					bStartAni[0] = false;
					gameCon.set_bFinish(xpos, ypos, direction, true);
					gameCon.addEndCount(xpos, ypos);
					GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe_Wall(xpos, ypos);
				}
			}
		}
	}
	
	void ani_171()
	{
		if (bStartAni[0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			
			if(type == 57)
			{
				uis1.fillAmount = timer[0] / GameCon.waterSpeed;
				if(uis1.fillAmount >= 1)
				{
					timer[0] = 0;
					bStartAni[0] = false;
					gameCon.set_bFinish(xpos, ypos, direction, true);
					gameCon.addEndCount(xpos, ypos);
					GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe_Wall(xpos, ypos);
				}
			}
			else if(type == 58)
			{
				uis2.fillAmount = timer[0] / GameCon.waterSpeed;
				if(uis2.fillAmount >= 1)
				{
					timer[0] = 0;
					bStartAni[0] = false;
					gameCon.set_bFinish(xpos, ypos, direction, true);
					gameCon.addEndCount(xpos, ypos);
					GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
				}
			}
		}
	}
	
	void ani_172()
	{
		if (bStartAni[0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			
			if(type == 59)
			{
				uis1.fillAmount = timer[0] / GameCon.waterSpeed;
				if(uis1.fillAmount >= 1)
				{
					timer[0] = 0;
					bStartAni[0] = false;
					gameCon.set_bFinish(xpos, ypos, direction, true);
					gameCon.addEndCount(xpos, ypos);
					GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
				}
			}
			else if(type == 60)
			{
				uis2.fillAmount = timer[0] / GameCon.waterSpeed;
				if(uis2.fillAmount >= 1)
				{
					timer[0] = 0;
					bStartAni[0] = false;
					gameCon.set_bFinish(xpos, ypos, direction, true);
					gameCon.addEndCount(xpos, ypos);
					GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe_Wall(xpos, ypos);
				}
			}
		}
	}
	
	void ani_173()
	{
		if (bStartAni[0] && GameCon.bAniCon) 
		{
			timer[0] += Time.deltaTime;
			
			if(type == 55)
			{
				uis1.fillAmount = timer[0] / GameCon.waterSpeed;
				if(uis1.fillAmount >= 1)
				{
					timer[0] = 0;
					bStartAni[0] = false;
					gameCon.set_bFinish(xpos, ypos, direction, true);
					gameCon.addEndCount(xpos, ypos);
					GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe_Wall(xpos, ypos);
				}
			}
			else if(type == 56)
			{
				uis2.fillAmount = timer[0] / GameCon.waterSpeed;
				if(uis2.fillAmount >= 1)
				{
					timer[0] = 0;
					bStartAni[0] = false;
					gameCon.set_bFinish(xpos, ypos, direction, true);
					gameCon.addEndCount(xpos, ypos);
					GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
				}
			}
		}
	}
	
	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// 
	public void setType(int _type)
	{
		switch(ID)
		{
		case 10: case 20: case 30: case 40: case 50: case 60:
			if (_type == 1) 
			{
				type = _type;
				direction = 2;	//south
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 3, true);
				bStartAni[0] = true;
				bEatItem = false;
			}
			else if (_type == 2) 
			{
				type = _type;
				direction = 3;	//west
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 2, true);
				bStartAni[0] = true;
				bEatItem = false;
			}
			break;
			
		case 11: case 21: case 31: case 41: case 51: case 61:
			if (_type == 3) 
			{
				type = _type;
				direction = 3;	//west
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 0, true);
				bStartAni[0] = true;
				bEatItem = false;
			}
			else if (_type == 4) 
			{
				type = _type;
				direction = 0;	//north
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 3, true);
				bStartAni[0] = true;
				bEatItem = false;
			}
			break;
			
		case 12: case 22: case 32: case 42: case 52: case 62:
			if (_type == 5) 
			{
				type = _type;
				direction = 0;	//North
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 1, true);
				bStartAni[0] = true;
				bEatItem = false;
			}
			else if (_type == 6) 
			{
				type = _type;
				direction = 1;	//East
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 0, true);
				bStartAni[0] = true;
				bEatItem = false;
			}
			break;
			
		case 13: case 23: case 33: case 43: case 53: case 63:
			if (_type == 7) 
			{
				type = _type;
				direction = 1;	//East
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 2, true);
				bStartAni[0] = true;
				bEatItem = false;
			}
			else if (_type == 8) 
			{
				type = _type;
				direction = 2;	//South
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 1, true);
				bStartAni[0] = true;
				bEatItem = false;
			}
			break;
			
		case 14: case 24: case 34: case 44: case 54: case 64:
			if (_type == 9) 
			{
				type = _type;
				direction = 1;	//East
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 3, true);
				bStartAni[0] = true;
				bEatItem = false;
			}
			else if (_type == 10) 
			{
				type = _type;
				direction = 3;	//West
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 1, true);
				bStartAni[0] = true;
				bEatItem = false;
			}
			break;
			
		case 15: case 25: case 35: case 45: case 55: case 65:
			if (_type == 11) 
			{
				type = _type;
				direction = 0;	//North
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 2, true);
				bStartAni[0] = true;
				bEatItem = false;
			}
			else if (_type == 12) 
			{
				type = _type;
				direction = 2;	//South
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 0, true);
				bStartAni[0] = true;
				bEatItem = false;
			}
			break;
			
		case 16: case 46:
			if (_type == 13) 
			{
				type = _type;
				direction = 1;	//East
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 3, true);
				bStartAni[0] = true;
				//bEatItem = false;
			}
			else if (_type == 14) 
			{
				type = _type;
				direction = 3;	//West
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 1, true);
				bStartAni[1] = true;
				//bEatItem = false;
			}
			else if (_type == 15) 
			{
				type = _type;
				direction = 0;	//north
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 2, true);
				bStartAni[2] = true;
				//bEatItem = false;
			}
			else if (_type == 16) 
			{
				type = _type;
				direction = 2;	//south
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 0, true);
				bStartAni[3] = true;
				//bEatItem = false;
			}
			break;
			
		case 70: case 90:
			if (_type == 41) 
			{
				type = _type;
				gameCon.set_direction(xpos, ypos, direction);
				bStartAni[0] = true;
				bStartAni[1] = true;
				//			bStartAni[2] = true;
				bStartAni[3] = true;
				
				outDirection[1] = 1;
				outDirection[3] = 3;
				
				bEatItem = false;
				gameCon.set_bFinish(xpos, ypos, 0, true);
				
			}
			break;
			
		case 71: case 91:
			if (_type == 42) 
			{
				type = _type;
				gameCon.set_direction(xpos, ypos, direction);
				bStartAni[0] = true;
				bStartAni[1] = true;
				bStartAni[2] = true;
				
				outDirection[0] = 0;
				outDirection[2] = 2;
				//bStartAni[3] = true;
				bEatItem = false;
				gameCon.set_bFinish(xpos, ypos, 1, true);
				
			}
			break;
			
		case 72: case 92:
			if (_type == 43) 
			{
				type = _type;
				gameCon.set_direction(xpos, ypos, direction);
				//bStartAni[0] = true;
				bStartAni[1] = true;
				bStartAni[2] = true;
				bStartAni[3] = true;
				
				outDirection[1] = 1;
				outDirection[3] = 3;
				bEatItem = false;
				
				gameCon.set_bFinish(xpos, ypos, 2, true);
				
			}
			break;
			
		case 73: case 93:
			if (_type == 44) 
			{
				type = _type;
				gameCon.set_direction(xpos, ypos, direction);
				bStartAni[0] = true;
				//bStartAni[1] = true;
				bStartAni[2] = true;
				bStartAni[3] = true;
				
				outDirection[0] = 0;
				outDirection[2] = 2;
				bEatItem = false;
				
				gameCon.set_bFinish(xpos, ypos, 3, true);
				
			}
			break;
			
		case 74: case 94:
			if (_type == 45) 
			{
				type = _type;
				direction = 0;	//East
				gameCon.set_direction(xpos, ypos, direction);
				bStartAni[1] = true;
				gameCon.set_bFinish(xpos, ypos, 1, true);
				bEatItem = false;
			}
			else if (_type == 46) 
			{
				type = _type;
				direction = 0;	//East
				gameCon.set_direction(xpos, ypos, direction);
				bStartAni[3] = true;
				gameCon.set_bFinish(xpos, ypos, 3, true);
				bEatItem = false;
			}
			break;
			
		case 75: case 95:
			if (_type == 47) 
			{
				type = _type;
				direction = 1;	//East
				gameCon.set_direction(xpos, ypos, direction);
				bStartAni[0] = true;
				gameCon.set_bFinish(xpos, ypos, 0, true);
				bEatItem = false;
			}
			else if (_type == 48) 
			{
				type = _type;
				direction = 1;	//East
				gameCon.set_direction(xpos, ypos, direction);
				bStartAni[2] = true;
				gameCon.set_bFinish(xpos, ypos, 2, true);
				bEatItem = false;
			}
			break;
			
		case 76: case 96:
			if (_type == 49) 
			{
				type = _type;
				direction = 2;	//East
				gameCon.set_direction(xpos, ypos, direction);
				bStartAni[1] = true;
				gameCon.set_bFinish(xpos, ypos, 1, true);
				bEatItem = false;
			}
			else if (_type == 50) 
			{
				type = _type;
				direction = 2;	//East
				gameCon.set_direction(xpos, ypos, direction);
				bStartAni[3] = true;
				gameCon.set_bFinish(xpos, ypos, 3, true);
				bEatItem = false;
			}
			break;
			
		case 77: case 97:
			if (_type == 51) 
			{
				type = _type;
				direction = 3;	//East
				gameCon.set_direction(xpos, ypos, direction);
				bStartAni[0] = true;
				gameCon.set_bFinish(xpos, ypos, 0, true);
				bEatItem = false;
			}
			else if (_type == 52) 
			{
				type = _type;
				direction = 3;	//East
				gameCon.set_direction(xpos, ypos, direction);
				bStartAni[2] = true;
				gameCon.set_bFinish(xpos, ypos, 2, true);
				bEatItem = false;
			}
			break;
			
		case 80: case 100:
			if (_type == 25) 
			{
				type = _type;
				direction = 3;	//East
				gameCon.set_direction(xpos, ypos, direction);
				outDirection[0] = 1;
				outDirection[1] = 2;
				outDirection[2] = 3;
				
				bStartAni[0] = true;
				bStartAni[1] = true;
				bStartAni[2] = true;
				bStartAni[3] = true;
				bEatItem = false;
				
				gameCon.set_bFinish(xpos, ypos, 0, true);
				
			}
			break;
			
		case 81: case 101:
			if (_type == 26) 
			{
				type = _type;
				direction = 3;	//East
				gameCon.set_direction(xpos, ypos, direction);
				outDirection[0] = 0;
				outDirection[1] = 2;
				outDirection[2] = 3;
				
				bStartAni[0] = true;
				bStartAni[1] = true;
				bStartAni[2] = true;
				bStartAni[3] = true;
				bEatItem = false;
				
				gameCon.set_bFinish(xpos, ypos, 1, true);
				
			}
			break;
			
		case 82: case 102:
			if (_type == 27) 
			{
				type = _type;
				direction = 0;	//East
				gameCon.set_direction(xpos, ypos, direction);
				outDirection[0] = 0;
				outDirection[1] = 1;
				outDirection[2] = 3;
				
				bStartAni[0] = true;
				bStartAni[1] = true;
				bStartAni[2] = true;
				bStartAni[3] = true;
				bEatItem = false;
				
				gameCon.set_bFinish(xpos, ypos, 2, true);
				
			}
			break;
			
		case 83: case 103:
			if (_type == 28) 
			{
				type = _type;
				direction = 1;	//East
				gameCon.set_direction(xpos, ypos, direction);
				outDirection[0] = 0;
				outDirection[1] = 1;
				outDirection[2] = 2;
				
				bStartAni[0] = true;
				bStartAni[1] = true;
				bStartAni[2] = true;
				bStartAni[3] = true;
				bEatItem = false;
				gameCon.set_bFinish(xpos, ypos, 3, true);
				
			}
			break;
			
		case 84: case 104:
			if (_type == 29) 
			{
				type = _type;
				direction = 0;	//East
				gameCon.set_direction(xpos, ypos, direction);
				bStartAni[1] = true;
				gameCon.set_bFinish(xpos, ypos, 1, true);
				bEatItem = false;
			}
			else if (_type == 30) 
			{
				type = _type;
				direction = 0;	//East
				gameCon.set_direction(xpos, ypos, direction);
				bStartAni[2] = true;
				gameCon.set_bFinish(xpos, ypos, 2, true);
				bEatItem = false;
			}
			else if (_type == 31) 
			{
				type = _type;
				direction = 0;	//East
				gameCon.set_direction(xpos, ypos, direction);
				bStartAni[3] = true;
				gameCon.set_bFinish(xpos, ypos, 3, true);
				bEatItem = false;
			}
			break;
			
		case 85: case 105:
			if (_type == 32) 
			{
				//Debug.Log("_type == 31");
				type = _type;
				direction = 1;	//East
				gameCon.set_direction(xpos, ypos, direction);
				bStartAni[0] = true;
				gameCon.set_bFinish(xpos, ypos, 0, true);
				bEatItem = false;
			}
			else if (_type == 33) 
			{
				//Debug.Log("_type == 32");
				type = _type;
				direction = 1;	//East
				gameCon.set_direction(xpos, ypos, direction);
				bStartAni[2] = true;
				gameCon.set_bFinish(xpos, ypos, 2, true);
				bEatItem = false;
			}
			else if (_type == 34) 
			{
				//Debug.Log("_type == 33");
				type = _type;
				direction = 1;	//East
				gameCon.set_direction(xpos, ypos, direction);
				bStartAni[3] = true;
				gameCon.set_bFinish(xpos, ypos, 3, true);
				bEatItem = false;
			}
			break;
			
		case 86: case 106:
			if (_type == 35) 
			{
				type = _type;
				direction = 2;	//East
				gameCon.set_direction(xpos, ypos, direction);
				bStartAni[0] = true;
				gameCon.set_bFinish(xpos, ypos, 0, true);
				bEatItem = false;
			}
			else if (_type == 36) 
			{
				type = _type;
				direction = 2;	//East
				gameCon.set_direction(xpos, ypos, direction);
				bStartAni[1] = true;
				gameCon.set_bFinish(xpos, ypos, 1, true);
				bEatItem = false;
			}
			else if (_type == 37) 
			{
				type = _type;
				direction = 2;	//East
				gameCon.set_direction(xpos, ypos, direction);
				bStartAni[3] = true;
				gameCon.set_bFinish(xpos, ypos, 3, true);
				bEatItem = false;
			}
			break;
			
		case 87: case 107:
			if (_type == 38) 
			{
				type = _type;
				direction = 3;	//East
				gameCon.set_direction(xpos, ypos, direction);
				bStartAni[0] = true;
				gameCon.set_bFinish(xpos, ypos, 0, true);
				bEatItem = false;
				
			}
			else if (_type == 39) 
			{
				type = _type;
				direction = 3;	//East
				gameCon.set_direction(xpos, ypos, direction);
				bStartAni[1] = true;
				gameCon.set_bFinish(xpos, ypos, 1, true);
				bEatItem = false;
			}
			else if (_type == 40) 
			{
				type = _type;
				direction = 3;	//East
				gameCon.set_direction(xpos, ypos, direction);
				bStartAni[2] = true;
				gameCon.set_bFinish(xpos, ypos, 2, true);
				bEatItem = false;
			}
			break;
			
		case 110:
		case 111:
		case 112:
		case 113:
		case 134: case 135: case 136: case 137:
		case 144: case 145: case 146: case 147:
		case 154: case 155: case 156: case 157:
			if (_type == 17) 
			{
				type = _type;
				direction = 2;	//south
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 0, true);
				//			gameCon.set_direction(xpos, ypos, direction);
				//			gameCon.set_bFinish(xpos, ypos, 0, true);
				bStartAni[0] = true;
				bEatItem = false;
			}
			else if (_type == 18) 
			{
				type = _type;
				direction = 0;	//north
				gameCon.set_direction(xpos, ypos, direction);
				//gameCon.set_bFinish(xpos, ypos, 0, true);
				gameCon.set_bFinish(xpos, ypos, 2, true);
				bStartAni[0] = true;
				bEatItem = false;
			}
			else if (_type == 19) 
			{
				type = _type;
				direction = 3;	//west
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 1, true);
				bStartAni[0] = true;
				bEatItem = false;
			}
			else if (_type == 20) 
			{
				type = _type;
				direction = 1;	//east
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 3, true);
				bStartAni[0] = true;
				bEatItem = false;
			}
			break;
			
		case 120:
		case 121:
		case 122:
		case 123:
			if (_type == 21) 
			{
				type = _type;
				direction = 0;	//
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 2, true);
				bStartAni[0] = true;
			}
			else if (_type == 22) //121
			{
				type = _type;
				direction = 2;	//
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 0, true);
				bStartAni[0] = true;
			}
			else if (_type == 23) //122
			{
				type = _type;
				direction = 1;	//
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 3, true);
				bStartAni[0] = true;
			}
			else if (_type == 24) //123
			{
				type = _type;
				direction = 3;	//
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 1, true);
				bStartAni[0] = true;
			}
			break;
			
			
		case 130: case 131: case 132: case 133:
		case 140: case 141: case 142: case 143:
		case 150: case 151: case 152: case 153:
			if (_type == 21) 
			{
				type = _type;
				direction = 0;	//south
				gameCon.set_direction(xpos, ypos, direction);
				
				gameCon.set_bFinish(xpos, ypos, 2, true);
				bStartAni[0] = true;
				bEatItem = false;
			}
			else if (_type == 22) 
			{
				type = _type;
				direction = 2;	//north
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 0, true);
				bStartAni[0] = true;
				bEatItem = false;
			}
			else if (_type == 23) 
			{
				type = _type;
				direction = 1;	//west
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 3, true);
				bStartAni[0] = true;
				bEatItem = false;
			}
			else if (_type == 24) 
			{
				type = _type;
				direction = 3;	//east
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 1, true);
				bStartAni[0] = true;
				bEatItem = false;
			}
			break;
			
		case 170:
			if (_type == 53) 
			{
				type = _type;
				direction = 1;	//East
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 3, true);
				bStartAni[0] = true;
			}
			else if (_type == 54) 
			{
				type = _type;
				direction = 3;	//West
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 1, true);
				bStartAni[0] = true;
			}
			break;
			
		case 171:
			if (_type == 57) 
			{
				//Debug.Log("_type == 57");
				type = _type;
				direction = 0;	//North
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 2, true);
				bStartAni[0] = true;
			}
			else if (_type == 58) 
			{
				//Debug.Log("_type == 58");
				type = _type;
				direction = 2;	//South
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 0, true);
				bStartAni[0] = true;
			}
			break;
			
		case 172:
			if (_type == 59) 
			{
				type = _type;
				direction = 0;	//North
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 2, true);
				bStartAni[0] = true;
			}
			else if (_type == 60) 
			{
				type = _type;
				direction = 2;	//South
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 0, true);
				bStartAni[0] = true;
			}
			break;
			
		case 173:
			if (bStartAni[0] && GameCon.bAniCon) 
			{
				timer[0] += Time.deltaTime;
				
				if(type == 55)
				{
					uis1.fillAmount = timer[0] / GameCon.waterSpeed;
					if(uis1.fillAmount >= 1)
					{
						timer[0] = 0;
						bStartAni[0] = false;
						gameCon.set_bFinish(xpos, ypos, direction, true);
						gameCon.addEndCount(xpos, ypos);
						GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe_Wall(xpos, ypos);
					}
				}
				else if(type == 56)
				{
					uis2.fillAmount = timer[0] / GameCon.waterSpeed;
					if(uis2.fillAmount >= 1)
					{
						timer[0] = 0;
						bStartAni[0] = false;
						gameCon.set_bFinish(xpos, ypos, direction, true);
						gameCon.addEndCount(xpos, ypos);
						GameObject.Find ("GameCon").GetComponent<GameCon> ().StartNextPipe(xpos, ypos);
					}
				}
			}
			break;
			
		case 174:
			if (_type == 55) 
			{
				type = _type;
				direction = 1;	//East
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 3, true);
				bStartAni[0] = true;
			}
			else if (_type == 56) 
			{
				type = _type;
				direction = 3;	//West
				gameCon.set_direction(xpos, ypos, direction);
				gameCon.set_bFinish(xpos, ypos, 1, true);
				bStartAni[0] = true;
			}
			break;
		}
	}
	
	
	
	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	private bool checkAllIn()
	{
		bool returnValue = true;
		for (int i=0; i<4; i++) 
		{
			if(!inFinish[i])
			{
				returnValue = false;
			}
		}
		return returnValue;
	}
	
	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void calConnect(int _type_connect)
	{
		//pos = GetComponent<PM> ().pos;
		if (_type_connect == 17) 
		{
			//			type_connect = _type_connect;
			direction = 2;	//south
			gameCon.set_direction_connect(xpos, ypos, direction);
			gameCon.set_bFinish_connect(xpos, ypos, 0, true);
			gameCon.set_bFinish_connect(xpos, ypos, 2, true);
			GameCon.aliveStream++;
			
			gameCon.calConnect_NextPipe(xpos, ypos);
		}
		else if (_type_connect == 18) 
		{
			//			type_connect = _type_connect;
			direction = 0;	//north
			gameCon.set_direction_connect(xpos, ypos, direction);
			gameCon.set_bFinish_connect(xpos, ypos, 2, true);
			gameCon.set_bFinish_connect(xpos, ypos, 0, true);
			GameCon.aliveStream++;
			
			gameCon.calConnect_NextPipe(xpos, ypos);
			
		}
		else if (_type_connect == 19) 
		{
			//			type_connect = _type_connect;
			direction = 3;	//west
			gameCon.set_direction_connect(xpos, ypos, direction);
			gameCon.set_bFinish_connect(xpos, ypos, 1, true);
			gameCon.set_bFinish_connect(xpos, ypos, 3, true);
			GameCon.aliveStream++;
			
			gameCon.calConnect_NextPipe(xpos, ypos);
			
		}
		else if (_type_connect == 20) 
		{
			//			type_connect = _type_connect;
			direction = 1;	//east
			gameCon.set_direction_connect(xpos, ypos, direction);
			gameCon.set_bFinish_connect(xpos, ypos, 3, true);
			gameCon.set_bFinish_connect(xpos, ypos, 1, true);
			GameCon.aliveStream++;
			
			gameCon.calConnect_NextPipe (xpos, ypos);
		}
	}
	
	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
