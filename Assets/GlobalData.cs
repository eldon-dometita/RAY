using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalData : MonoBehaviour {
	

	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// static variable.....
	public UIAtlas atlas_bgTiles;
	public UIAtlas atlas_clockTiles;
	public UIAtlas atlas_counterTiles;
	public UIAtlas atlas_textTiles;
	public UIAtlas atlas_tile;
	//public static string basePipeAtlasPath = "Atlases/Atlas_Tile1";

	public static int baseTileNum = 0;
	public static int perfectTileNum = 4;

	private static GlobalData _instance;

	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// 
	// Static singleton property
	public static GlobalData Instance
	{
		// Here we use the ?? operator, to return 'instance' if 'instance' does not equal null
		// otherwise we assign instance to a new component and return that
		get 
		{ 
			if(_instance == null)
			{
				GameObject obj = new GameObject ();
				obj.hideFlags = HideFlags.HideAndDontSave;
				_instance = obj.AddComponent<GlobalData> ();
				//DontDestroyOnLoad(obj);
				_instance.init();
			}
			
			return _instance;
		}
	}

	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void init()
	{
		atlas_tile = Resources.Load<UIAtlas> (GameCon.basePipeAtlasPath);
		atlas_bgTiles = Resources.Load<UIAtlas> ("Atlases/BackgroundTiles");
		atlas_clockTiles = Resources.Load<UIAtlas> ("Atlases/TopClockTiles");
		atlas_counterTiles = Resources.Load<UIAtlas> ("Atlases/TopCounterTiles");
		atlas_textTiles = Resources.Load<UIAtlas> ("Atlases/TopTextTiles");
	}

	public void setTileNum(int _baseTileNum, bool _bRandom)
	{
		if(_bRandom)
		{
			baseTileNum = Random.Range(0, 7);
		}
		else
		{
			baseTileNum = _baseTileNum;
		}

		switch(baseTileNum)
		{
		case 0:
			perfectTileNum = 4;
			break;
		case 1:
			perfectTileNum = 5;
			break;
		case 2:
			perfectTileNum = 3;
			break;
		case 3:
			perfectTileNum = 2;
			break;
		case 4:
			perfectTileNum = 1;
			break;
		case 5:
			perfectTileNum = 6;
			break;
		case 6:
			perfectTileNum = 2;
			break;
		}
	}

	public void setAtlas_Tile(int _setNum)
	{
		GameCon.basePipeAtlasPath = "Atlases/Atlas_Tile"+(_setNum+1);

		atlas_tile = Resources.Load<UIAtlas> (GameCon.basePipeAtlasPath);
	}

}
