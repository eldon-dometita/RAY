using UnityEngine;
using System.Collections;
using System.IO;

public class GameCon : MonoBehaviour {
	
	bool DEBUG_CW = false;
	bool DEBUG_CHEAT_SETUP_MAP = false;
	
	const int NORTH = 0;
	const int EAST = 1;
	const int SOUTH = 2;
	const int WEST = 3;
	
	public int SCORE_OVERWRITE_NORMAL = -200;
	public int SCORE_OVERWRITE_CROSS = -200;
	public int SCORE_OVERWRITE_SPECIAL = -250;
	
	public int SCORE_SKIP_NORMAL = -50;
	public int SCORE_SKIP_CROSS = -60;
	public int SCORE_SKIP_SPECIAL = -70;
	
	public int SCORE_NEW_NORMAL = 100;
	public int SCORE_NEW_CROSS = 150;
	public int SCORE_NEW_SPECIAL = 200;
	
	public int SCORE_END_CROSS = 500;
	public int SCORE_END_DEFAULT = 100;
	public int SCORE_END_ONEWAY = 300;
	public int SCORE_END_INOUT = 1000;
	public int SCORE_END_1TO2 = 2000;
	public int SCORE_END_1TO3 = 3000;
	public int SCORE_END_WALL_INOUT = 700;
	public int SCORE_END_MISS = -700;
	public int SCORE_END_OVERUSE = 1000;
	
	public int PERFECT_BONUS = 10000;
	public int TIME_BONUS = 200;
	public int OVERUSE_BONUS = 1000;
	
	
	public static bool bAniCon;	
	public static bool bHelpAniCon;	

	
	//public int[][] mapBuf;	//for base layer 
	public POSINFO[][] map;
	public byte[][] mapBuf1;	//for perfect plan
	public byte[][] mapItem;	//for perfect plan
	public byte[][] objIdxBuf;	//for perfect plan
	public byte[][] perfectBuf;	//for perfect plan
	public GameObject[] gPipe;
	public PipeScript[] sPipe;
	
	public static int nCol;
	public static int nRow;
	int SX;
	int SY;
	
	GameObject go_parent;		//contrill pipe
	GameObject go_parent_base;	//base tile 
	GameObject go_parent_top;	//object tiles
	GameObject go_parent_perfectplan;	//pipe for perfectplan
	GameObject go_parent_Item;	//pipe for perfectplan
	
	GameObject go_ZoomPanObj;
	public GameObject go_Panel_ScrollView;
	public GameObject zoomPanObj;
	public bool bTurnOnWater;
	public bool bFail;
	public bool bFailMark;
	public bool bLock_All_Trigger;
	float timer_FailMark;
	GameObject go_popup_fail;
	GameObject go_Char_fail;
	GameObject go_GotoMenu_fail;
	public bool bGameStart;
	
	public int StartPipeCount;
	public int EndPipeCount;
	GameObject go_popup_score;
	GameObject go_popup_reward;
	
	GameObject go_Popup_CalScore;
	bool bCalScoreFrame;
	int stateScoreFrame;	//1: cross		2: default		3:special
	int currentScoreFrameIndex;
	string scoreStrPath;
	float scoreTimer;
	GameObject go_FillRectPipe;
	GameObject go_LeftPanel;
	GameObject go_RightPanel;
	GameObject go_BottomPanel;
	GameObject go_RightTopPanel;
	
	
	GameObject go_layer_calScore;
	
	int[] score_default;
	int[] score_cross;
	int[] score_oneway;
	int[] score_1To2;
	int[] score_1To3;
	int[] score_InOut;
	int[] score_WallInOut;
	int[] score_Miss;
	
	GameObject go_default;
	GameObject go_cross;
	GameObject go_special;
	GameObject go_overuse;
	GameObject go_timebonus;
	GameObject go_perfect;
	GameObject go_missing;
	GameObject go_totalscore;
	
	int ScoreValue_Default;
	int ScoreValue_Cross;
	int ScoreValue_Special;
	int ScoreValue_OverUse;
	int ScoreValue_TimeBonus;
	int ScoreValue_PerfectBonus;
	int ScoreValue_Missing;
	int ScoreValue_TotalScore;
	
	GameObject handObj;
	
	GameObject go_bigText;
	//GameObject go_parent_conveyorBelt;
	
	
	
	public int invenCursor = 0;
	public int invenPipeID = 10;
	
	public static float waterSpeed = 1f;
	
	public static int bgNum = 0;
	public static string bgName = "PatternBg0";
//	public static int baseTileNum = 6;
//	public static int perfectTileNum = 1;
	public static string basePipeAtlasPath = "Atlases/Atlas_Tile1";
	
	//public static string perfectplanPipeAtlasPath = "Atlases/Atlas_Tile2";
	public static UIAtlas atlasPipe;
	
	public int useWherePipe = 0;	//0 is conveyorBelt. 1 is inventory
	
	
	GameObject go_parent_conveyorBelt;		//contrill pipe
	public float conveyorSpeed = 1f;		//we can adjustment with speed item...
	GameObject[] go_conveyorItem;
	
	public bool bAni_InventoryItem;
	public float inventorySpeed = 1f;
	GameObject[] go_invenItem;
	
	public UISprite[] uis_InventoryItem;
	public float timer_InventoryItem;
	
	PlayerData pd;
	
	public static int connectCount;
	public static int aliveStream;
	
	
	bool bShowPerfectPlan;
	float time_ShowPerfectPlan;
	
	//int test_load_map_num = 1;
	
	
	int limitNorm = 0;
	public int limitTime = 0;
	public bool bCheckLimitTimeCondition;
	public bool bWaterFlow;
	//int map_maxScore = 0;
	public static int userScore = 0;
	public int tmpUserScore = 0;
	public int tmpUserScore1 = 0;
	public static int scoreMax = 0;
	
	//	int star1_Score = 0;
	//	int star2_Score = 0;
	//	int star3_Score = 0;
	
	
	
	int gameWidth;
	int gameHeight;
	
	int[][] nowConnectBuf;
	int[][] beforeConnectBuf;
	
	public int nowStarIndex;
	
	public bool bNewRecord;
	public bool bInventoryMaxPlusText;
	public int total_Star;
	
	
	
	
	bool bLoading;
	int loading_state;
	
	GameObject go_fade;
	bool bJustOneTimeCoroutine;
	
	UISlider uiSlider_Loading;
	
	
	public EffectSoundManagerScript efm;
	
	GameObject go_Temp_FromInven;// = Instantiate(Resources.Load ("cwPrefabs/TempInvenPipe"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
	UISprite uis1_Temp_FromInven;
	UISprite uis2_Temp_FromInven;
	
	GameObject go_TileEffect;

	GameObject[] go_PlusTileEffect;
	
	GlobalData gd;

	GameObject go_Popup_Help;
	HelpPopupCon helpPopupCon;
	TextScript ts;

	GameObject InventoryItemButtonObj;
	GameObject ConveyorItemButtonObj;
	GameObject Pan_inventoryScrollView;

	GameObject go_FillRect_Screen;
	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	
	//	GameObject[] prefab_pipes;
	//	bool[] bLoadPrefab_pipes;
	
	public static GameCon Instance;
	
	public GameCon getInstance()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		return Instance;
	}
	
	
	// Use this for initialization
	void Awake () {

		InventoryItemButtonObj = GameObject.Find("InventoryButton");
		InventoryItemButtonObj.SetActive(false);
		ConveyorItemButtonObj = GameObject.Find("conveyorButton");

		Pan_inventoryScrollView = GameObject.Find ("Panel_inventoryScrollView");

		go_FillRect_Screen = GameObject.Find("FillRect_Screen");
		go_FillRect_Screen.SetActive(false);

		float resolWidth = Screen.width;
//		Debug.Log("resolWidth:  "+resolWidth);
		float resolHeight = Screen.height;
//		Debug.Log("resolHeight:  "+resolHeight);

		float ratio1 = resolWidth/resolHeight;
//		Debug.Log("ratio1:  "+ratio1);
		float gameWidth = 800 * ratio1;
//		Debug.Log("gameWidth:  "+gameWidth);


		float ratio = gameWidth/1280;


		float tmpWidth = (gameWidth - 372*ratio);

		Vector4 range = new Vector4(0, 0, tmpWidth, 160);
		//Pan_inventoryScrollView.GetComponent<UIPanel>().clipping = UIDrawCall.Clipping.SoftClip;
		Pan_inventoryScrollView.GetComponent<UIPanel>().baseClipRegion = range;
		//Pan_inventoryScrollView.GetComponent<UIPanel>().drawCallClipRange = new Vector4(0, 0, Screen.width - 360, 160);

		gd = GlobalData.Instance;
		//		GameObject.Find("Prefab_Pipe").GetComponent<PipeScript>().setID(170);
		//		GameObject.Find("Prefab_Pipe1").GetComponent<PipeScript>().setID(171);
		//		GameObject.Find("Prefab_Pipe2").GetComponent<PipeScript>().setID(172);
		//		GameObject.Find("Prefab_Pipe3").GetComponent<PipeScript>().setID(173);
		//		GameObject.Find("Prefab_Pipe4").GetComponent<PipeScript>().setID(64);
		//		GameObject.Find("Prefab_Pipe5").GetComponent<PipeScript>().setID(65);
		//
		Instance = this;
		
		efm = EffectSoundManagerScript.Instance;

		pd = PlayerData.Instance;

		gd.setTileNum(0, true);

		go_Popup_Help = GameObject.Find("Popup_Help");
		helpPopupCon = GameObject.Find("Popup_Help").GetComponent<HelpPopupCon>();

		ts = TextScript.Instance;


		uiSlider_Loading = GameObject.Find ("loading_Slider").GetComponent<UISlider> ();
		uiSlider_Loading.value = 0;
		
		bJustOneTimeCoroutine = false;
		go_fade = GameObject.Find("Anchor_Center_Fade");
		
		bLoading = true;
		loading_state = 0;


		
	}

	byte[] tipArr = {
		10, 11, 12, 13, 14, 		15, 16, 20, 21, 22, 
		23, 24, 25, 30, 31, 		32, 33, 34, 35, 40, 
		41, 42, 43, 44, 45,			46, 50, 51, 52, 53, 
		54, 55, 60, 61, 62, 		63, 64, 65, 130, 131, 
		132, 133, 170, 171, 172, 	173, 70, 71, 72, 73, 
		74, 75, 76, 77, 80, 		81, 82, 83, 84, 85, 
		86, 87, 90, 91, 92, 		93, 94, 95, 96, 97, 
		100, 101, 102, 103, 104, 	105, 106, 107, 108
	};

	void setTip()
	{
		int tipIndex = 0;
		if(pd.lastStage < 20)
		{
			tipIndex = tipArr[Random.Range(0, 6)];
		}
		else if(pd.lastStage < 40)
		{
			tipIndex = tipArr[Random.Range(0, 7)];
		}
		else if(pd.lastStage < 60)
		{
			tipIndex = tipArr[Random.Range(0, 19)];
		}
		else if(pd.lastStage < 80)
		{
			tipIndex = tipArr[Random.Range(0, 25)];
		}
		else if(pd.lastStage < 100)
		{
			tipIndex = tipArr[Random.Range(0, 37)];
		}
		else if(pd.lastStage < 120)
		{
			tipIndex = tipArr[Random.Range(0, 41)];
		}
		else if(pd.lastStage < 140)
		{
			tipIndex = tipArr[Random.Range(0, 41)];
		}
		else if(pd.lastStage < 160)
		{
			tipIndex = tipArr[Random.Range(0, 41)];
		}
		else if(pd.lastStage < 180)
		{
			tipIndex = tipArr[Random.Range(0, 45)];
		}
		else if(pd.lastStage < 200)
		{
			tipIndex = tipArr[Random.Range(0, 53)];
		}
		else if(pd.lastStage < 220)
		{
			tipIndex = tipArr[Random.Range(0, 61)];
		}
		else if(pd.lastStage < 240)
		{
			tipIndex = tipArr[Random.Range(0, 69)];
		}
		else
		{
			tipIndex = tipArr[Random.Range(0, 78)];
		}

		//Debug.Log("tipIndex:   "+tipIndex);

		GameObject.Find ("text_helpPipeName").GetComponent<UILabel>().text = ts.PName[tipIndex];

		HelpPopupCon.help_PipeIndex  = tipIndex;
		helpPopupCon.setHelpPipe(HelpPopupCon.help_PipeIndex);
		//helpPopupCon.setHelpPipe(_index);
		
		if(tipIndex >= 170 && tipIndex <= 173)
		{
			GameObject.Find("text_explain").GetComponent<UILabel>().text = ts.HStr[170];
		}
		else
		{
			GameObject.Find("text_explain").GetComponent<UILabel>().text = ts.HStr[tipIndex];
		}
		
		//efm.Play(8);
		
		//go_Popup_Help.transform.localPosition = new Vector3(0, 800, 0);
//		TweenPosition twPosition = TweenPosition.Begin(go_Popup_Help, 0.3f, new Vector3(0, 0, 0));
//		twPosition.method = UITweener.Method.BounceIn;
	}
	
	float escapeTime = 0;
	bool bEscape = false;
	// Update is called once per frame
	void Update () 
	{
		if (bLoading) 
		{
			switch(loading_state)
			{
			case 0:

				setTip();

				setLoadingText("initialize object...");
				
				bInventoryMaxPlusText = false;
				bNewRecord = false;
				
				nowStarIndex = 0;
				
				bAniCon = true;	//this is controll all pipe animation
				
				StartPipeCount = 0;
				
				handObj = GameObject.Find ("HandObj");
				handObj.SetActive (false);
				
				go_LeftPanel = GameObject.Find ("Anchor_BottomLeft");
				go_RightPanel = GameObject.Find ("Anchor_BottomRight");
				go_BottomPanel = GameObject.Find ("Anchor_BottomInven");
				go_BottomPanel.transform.localPosition = new Vector3(0, -800, 0);
				go_RightTopPanel = GameObject.Find ("Anchor_TopRight");
				
				
				go_Popup_CalScore = GameObject.Find ("Popup_CalScore");
				
				bCalScoreFrame = false;
				
				
				
				bLock_All_Trigger = false;
				bGameStart = false;
				
				go_popup_fail = GameObject.Find ("Popup_Fail");
				go_popup_fail.SetActive (false);

				go_Char_fail = GameObject.Find ("Char_Fail");
				go_Char_fail.SetActive(false);

				go_GotoMenu_fail = GameObject.Find ("GOTOMENU");
				go_GotoMenu_fail.SetActive(false);

				bFail = false;
				
				go_bigText = GameObject.Find ("text_BigAlert");
				//go_bigText.SetActive (false);
				
				



//				baseTileNum = 1;//Random.Range (0, 7);
//				switch(baseTileNum)
//				{
//				case 0:
//					perfectTileNum = 4;
//					break;
//				case 1:
//					perfectTileNum = 5;
//					break;
//				case 2:
//					perfectTileNum = 3;
//					break;
//				case 3:
//					perfectTileNum = 2;
//					break;
//				case 4:
//					perfectTileNum = 1;
//					break;
//				case 5:
//					perfectTileNum = 6;
//					break;
//				case 6:
//					perfectTileNum = 2;
//					break;
//				}
				
				useWherePipe = 0;	//conveyorBelt mode
				
				conveyorSpeed = 1f;
				if(StageCon.useConveyorBoost)
				{
					if(pd.consumeItem[1] > 0)	//conveyor booster
					{
						pd.consumeItem[1]--;
						pd.saveData();
						conveyorSpeed = 0.5f;
					}
				}
				
				inventorySpeed = 1f;
				if(StageCon.useInventoryBoost)
				{
					if(pd.consumeItem[3] > 0)	//inven booster
					{
						pd.consumeItem[3]--;
						pd.saveData();
						inventorySpeed = 0.5f;
					}
				}
				
				bShowPerfectPlan = false;
				time_ShowPerfectPlan = 0;
				
				
				
				zoomPanObj = GameObject.Find ("ZoomObj");
				go_parent_Item = GameObject.Find ("layer_item");
				go_parent = GameObject.Find ("layer_controll");
				go_parent_perfectplan = GameObject.Find ("layer_perfectplan");
				go_parent_base = GameObject.Find ("layer_base");
				go_parent_top = GameObject.Find ("layer_top");
				//go_layer_calScore = GameObject.Find ("layer_calScore");
				
				
				
				go_Temp_FromInven = Instantiate(Resources.Load ("cwPrefabs/TempInvenPipe"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
				uis1_Temp_FromInven = go_Temp_FromInven.transform.FindChild("Layer2").GetComponent<UISprite>();
				uis2_Temp_FromInven = go_Temp_FromInven.transform.FindChild("Arrow").GetComponent<UISprite>();
				go_Temp_FromInven.name = "TempMoveInenPipe";
				go_Temp_FromInven.transform.parent = GameObject.Find ("Panel_tempInvenPipe").transform;
				go_Temp_FromInven.transform.localScale = new Vector3(1, 1, 1);
				uis1_Temp_FromInven.enabled = false;
				uis2_Temp_FromInven.enabled = false;
				
				go_TileEffect = Instantiate(Resources.Load ("cwPrefabs/TileEffect"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
				go_TileEffect.transform.parent = go_parent.transform;
				go_TileEffect.transform.localScale = new Vector3(1, 1, 1);
				go_TileEffect.GetComponent<UISprite> ().enabled = false;


				go_PlusTileEffect = new GameObject[8];
				for(int i=0; i<8; i++)
				{
					go_PlusTileEffect[i] = Instantiate(Resources.Load ("cwPrefabs/TileEffect"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
					go_PlusTileEffect[i].transform.parent = go_parent.transform;
					go_PlusTileEffect[i].transform.localScale = new Vector3(1, 1, 1);
					go_PlusTileEffect[i].GetComponent<UISprite> ().enabled = false;
				}

				loading_state++;
				break;
				
			case 1:
				setLoadingText("initialize conveyor...");
				
				go_parent_conveyorBelt = GameObject.Find ("ConveyorBelt");
				
				go_conveyorItem = new GameObject[10];
				for (int i=0; i<10; i++) 
				{
					go_conveyorItem[i] = GameObject.Find("ConveyorPipe"+i);
				}
				
				loading_state++;
				break;
				
			case 2:
				setLoadingText("initialize inventory...");
				
				go_invenItem = new GameObject[PlayerData.PIPE_ITEM_MAX];
				uis_InventoryItem = new UISprite[PlayerData.PIPE_ITEM_MAX];
				
				
				//setting inventory Item Count
				
				setAtlasSet (GlobalData.baseTileNum);
				setInvenPipeOpen ();

				active_ConveyorItem();
				setInvenCursorInitialize();

				loading_state++;
				break;
				
				
			case 3:				
				setLoadingText("initialize conveyor belt...");
				//setting conveyorBelt
				setConveyorBeltPipe ();
				
				loading_state++;
				break;
				
			case 4:
				setLoadingText("initialize load map...");
				
				//PlayerData.nowMapNumber1 = 4;
				loadMap (PlayerData.nowMapNumber1);
				
				loading_state++;
				break;
				
			case 5:
				setLoadingText("initialize user data...");
				
				//set perfectbuf for checking later
				for (int i=0; i<nRow; i++) 
				{
					for (int j=0; j<nCol; j++) 
					{
						perfectBuf[i][j] = 0;
						if(map[i][j].pipeID >= 1 && map[i][j].pipeID < 160)
						{
							if(map[i][j].pipeID == 3)
							{
								continue;
							}
							perfectBuf[i][j] = 1;
						}
					}
				}
				
				
				for (int i=0; i<pd.star[PlayerData.nowMapNumber1]; i++) 
				{
					setStar(i, 0);
				}
				
				total_Star = getTotalStar ();
				
				//				if (pd.record [PlayerData.nowMapNumber1] > 0) 
				//				{
				//					if (pd.record [PlayerData.nowMapNumber1] > pd.map_maxScore[PlayerData.nowMapNumber1]) {
				//						scoreMax = pd.record [PlayerData.nowMapNumber1];
				//						//GameObject.Find("text_recordValue").GetComponent<UILabel>().text = scoreMax.ToString();
				//					} else {
				//						scoreMax = pd.map_maxScore[PlayerData.nowMapNumber1];
				//						//GameObject.Find("text_recordValue").GetComponent<UILabel>().text = "0";
				//					}
				//				}
				//				else 
				//				{
				//					scoreMax = pd.map_maxScore[PlayerData.nowMapNumber1];
				//					//GameObject.Find("text_recordValue").GetComponent<UILabel>().text = "0";
				//				}
				//				GameObject.Find("text_recordValue").GetComponent<UILabel>().text = "0";
				
				//scoreMax -= 10000;
				
				refresh_UseScore();
				
				//limitNorm = pd.LimitNorm[PlayerData.nowMapNumber1];
				GameObject.Find("text_LimitNorm").GetComponent<UILabel>().text = limitNorm.ToString();	//
				
				loading_state++;
				break;
				
				
			case 6:
				setLoadingText("initialize create tile...");
				loading_state++;
				break;
				
			case 7:
				createPref_Pipes_Base ();
				
				loading_state++;
				break;
				
			case 8:
				setLoadingText("initialize Controll tile...");
				loading_state++;
				break;
				
			case 9:
				createPref_Pipes_Controll ();
				
				loading_state++;
				break;
				
			case 10:
				setLoadingText("initialize Perfect Plan tile...");
				loading_state++;
				break;
				
			case 11:
				createPref_Pipes_PerfectPlan ();
				
				loading_state++;
				break;
				
			case 12:
				setLoadingText("initialize Item...");
				loading_state++;
				break;
				
			case 13:
				createPref_Pipes_Item ();
				
				loading_state++;
				break;
				
			case 14:
				setLoadingText("initialize FillRect...");
				loading_state++;
				break;
				
			case 15:
				createPref_Pipes_FillRect ();
				
				loading_state++;
				break;
				
				
			case 16:
				setLoadingText("initialize water time...");
				
				//stageInit here......
				//refresh_InventoryBooster ();
				refresh_TurnOnItemCount ();
				refresh_WaterFlowTimeExpansion ();
				refresh_ShowPerfectPlan ();
				
				//set water timer....
				pd.flowTime = pd.flowDefaultTime;
				bWaterFlow = false;
				if(pd.flowTime > 0)
				{
					//Debug.Log("PD.flowTime"+PD.flowTime);
					GameObject.Find ("text_WaterTimeSlider").GetComponent<WaterTimeScript> ().setStartTimer();
					GameObject.Find ("text_WaterTimeSlider").GetComponent<WaterTimeScript> ().displayTimer();
					GameObject.Find ("text_WaterTimeSlider").GetComponent<WaterTimeScript> ().setPause();
				}
				
				//set timer
				bCheckLimitTimeCondition = false;
				if (DEBUG_CW) 
				{
					limitTime = 10;
					bCheckLimitTimeCondition = true;
				}
				
//				if(limitTime > 0)
//				{
//					showLimitTime(limitTime);
//				}
				
				go_parent_perfectplan.SetActive(false);
				//hidePerfectPlanPipe();
				
				
				
				
				bTurnOnWater = false;
				go_Panel_ScrollView = GameObject.Find ("Panel_ScrollView");
				go_ZoomPanObj = GameObject.Find ("ZoomPanObj");
				
				
				go_Popup_CalScore.SetActive (false);
				
				
				score_default = new int[nRow*nCol];
				score_cross = new int[nRow*nCol];
				score_oneway = new int[nRow*nCol];
				score_InOut = new int[nRow*nCol];
				score_1To2 = new int[nRow*nCol];
				score_1To3 = new int[nRow*nCol];
				score_WallInOut = new int[nRow*nCol];
				score_Miss = new int[nRow*nCol];
				
				
				go_popup_reward = GameObject.Find ("Popup_Reward");
				go_popup_score = GameObject.Find ("Popup_Score");
				go_default = GameObject.Find ("endScore_default");
				go_cross = GameObject.Find ("endScore_cross");
				go_special = GameObject.Find ("endScore_special");
				go_overuse = GameObject.Find ("endScore_overuse");
				go_timebonus = GameObject.Find ("endScore_timebonus");
				go_perfect = GameObject.Find ("endScore_perfect");
				go_missing = GameObject.Find ("endScore_missing");
				go_totalscore = GameObject.Find ("endScore_TotalScore");
				disableTextScore ();
				go_popup_score.SetActive (false);
				go_popup_reward.SetActive (false);
				
				
				//test
				//setUp_Reward ();
				
				bCheckCondition_Norm = false;
				
				GameObject.Find("text_record").GetComponent<UILabel>().text = "STAGE [FF8400]"+(PlayerData.nowMapNumber1+1)+"[-]";
				
				
				makeMinimumScreen ();
				
				toggleRightItemActive(false);
				
				
				loading_state++;
				bJustOneTimeCoroutine = false;
				break;
				
			case 17:
				setLoadingText("initialize ready...");
				
				//Debug.Log("case 8:");
				StartCoroutine(SleepLoading());
				
				//loading_state++;
				break;
				
			case 18:
				//Debug.Log("case 9:");
				GameObject.Find("Img_FillRect").GetComponent<FadeScript>().start();
				efm.Play(100);	//bgm
				loading_state++;
				break;
				
			case 19:
				break;
				
			case 20:
				//Debug.Log("case 10:");
				bLoading = false;
				setBigText (10);
				
				//go_Popup_Help.SetActive(false);
				HelpPopupCon.stop = true;
				break;
			}
			
			uiSlider_Loading.value = loading_state/19f;
		} 
		else 
		{
			
			//test count 
			//for escape while
			//if some problem, escape while 5 seconds later.
			if (aliveStream > 0) {
				escapeTime += Time.deltaTime;
				if (escapeTime > 5f) {
					bEscape = true;
				}
			}
			
			
			if (bAni_InventoryItem && bAniCon) {
				timer_InventoryItem += Time.deltaTime;
				
				uis_InventoryItem [invenCursor].fillAmount = timer_InventoryItem / inventorySpeed;
				if (uis_InventoryItem [invenCursor].fillAmount >= 1) {
					uis_InventoryItem [invenCursor].fillAmount = 0;
					timer_InventoryItem = 0;
					bAni_InventoryItem = false;
					
				}
			}
			
			if (bShowPerfectPlan) {
				time_ShowPerfectPlan += Time.deltaTime;
				//			Debug.Log(time_ShowPerfectPlan);
				if (time_ShowPerfectPlan > 30) {
					bShowPerfectPlan = false;
					time_ShowPerfectPlan = 0;
					
					go_parent_perfectplan.SetActive (false);
					//hidePerfectPlanPipe();
				}
			}
			
			if (bFailMark) {
				timer_FailMark += Time.deltaTime;
				if (timer_FailMark > 2f) {
					bFailMark = false;
					bLock_All_Trigger = true;
					setBigText (20);
				}
				
			}
			
			if (bCalScoreFrame) {
				scoreTimer += Time.deltaTime;
				if (scoreTimer > 0.3f) {
					if (stateScoreFrame == 1) {	//cross
						if (currentScoreFrameIndex < score_CrossCount) {
							scoreStrPath = "FillRectPipe" + score_cross [currentScoreFrameIndex];
							go_FillRectPipe = GameObject.Find (scoreStrPath);
							show_EndScore (go_FillRectPipe, score_cross [currentScoreFrameIndex], 1);
							
							efm.Play(1);
							Destroy (go_FillRectPipe);
							currentScoreFrameIndex++;
						} else {
							checkNextScoreFrame(1);
							//												stateScoreFrame = 2;
							//												currentScoreFrameIndex = 0;
						}
					} else if (stateScoreFrame == 2) {	//default
						if (currentScoreFrameIndex < score_defaultCount) {
							scoreStrPath = "FillRectPipe" + score_default [currentScoreFrameIndex];
							go_FillRectPipe = GameObject.Find (scoreStrPath);
							show_EndScore (go_FillRectPipe, score_default [currentScoreFrameIndex], 2);
							
							efm.Play(1);
							Destroy (go_FillRectPipe);
							currentScoreFrameIndex++;
						} else {
							checkNextScoreFrame(2);
							//												stateScoreFrame = 3;
							//												currentScoreFrameIndex = 0;
						}
					} else if (stateScoreFrame == 3) {	//Oneway
						if (currentScoreFrameIndex < score_OnewayCount) {
							scoreStrPath = "FillRectPipe" + score_oneway [currentScoreFrameIndex];
							go_FillRectPipe = GameObject.Find (scoreStrPath);
							
							show_EndScore (go_FillRectPipe, score_oneway [currentScoreFrameIndex], 3);
							
							efm.Play(1);
							Destroy (go_FillRectPipe);
							currentScoreFrameIndex++;
						} else {
							checkNextScoreFrame(3);
							//												stateScoreFrame = 4;
							//												currentScoreFrameIndex = 0;
						}
					} else if (stateScoreFrame == 4) {	//InOut
						if (currentScoreFrameIndex < score_InOutCount) {
							scoreStrPath = "FillRectPipe" + score_InOut [currentScoreFrameIndex];
							go_FillRectPipe = GameObject.Find (scoreStrPath);
							
							show_EndScore (go_FillRectPipe, score_InOut [currentScoreFrameIndex], 4);
							
							efm.Play(1);
							Destroy (go_FillRectPipe);
							currentScoreFrameIndex++;
						} else {
							checkNextScoreFrame(4);
							//												stateScoreFrame = 5;
							//												currentScoreFrameIndex = 0;
						}
					} else if (stateScoreFrame == 5) {	//1To2
						if (currentScoreFrameIndex < score_1To2Count) {
							scoreStrPath = "FillRectPipe" + score_1To2 [currentScoreFrameIndex];
							go_FillRectPipe = GameObject.Find (scoreStrPath);
							
							show_EndScore (go_FillRectPipe, score_1To2 [currentScoreFrameIndex], 5);
							
							efm.Play(1);
							Destroy (go_FillRectPipe);
							currentScoreFrameIndex++;
						} else {
							checkNextScoreFrame(5);
							//												stateScoreFrame = 6;
							//												currentScoreFrameIndex = 0;
						}
					} else if (stateScoreFrame == 6) {	//1To3
						if (currentScoreFrameIndex < score_1To3Count) {
							scoreStrPath = "FillRectPipe" + score_1To3 [currentScoreFrameIndex];
							go_FillRectPipe = GameObject.Find (scoreStrPath);
							
							show_EndScore (go_FillRectPipe, score_1To3 [currentScoreFrameIndex], 6);
							
							efm.Play(1);
							Destroy (go_FillRectPipe);
							currentScoreFrameIndex++;
						} else {
							checkNextScoreFrame(6);
							//												stateScoreFrame = 7;
							//												currentScoreFrameIndex = 0;
						}
					} else if (stateScoreFrame == 7) {	//WallInOut
						if (currentScoreFrameIndex < score_WallInOutCount) {
							scoreStrPath = "FillRectPipe" + score_WallInOut [currentScoreFrameIndex];
							go_FillRectPipe = GameObject.Find (scoreStrPath);
							
							show_EndScore (go_FillRectPipe, score_WallInOut [currentScoreFrameIndex], 7);
							
							efm.Play(1);
							Destroy (go_FillRectPipe);
							currentScoreFrameIndex++;
						} else {
							checkNextScoreFrame(7);
							//												stateScoreFrame = 8;
							//												currentScoreFrameIndex = 0;
						}
					} else if (stateScoreFrame == 8) {	//miss pipe
						if (currentScoreFrameIndex < score_MissCount) {
							scoreStrPath = "FillRectPipe" + score_Miss [currentScoreFrameIndex];
							go_FillRectPipe = GameObject.Find (scoreStrPath);
							
							show_EndScore (go_FillRectPipe, score_Miss [currentScoreFrameIndex], 8);
							
							efm.Play(1);
							Destroy (go_FillRectPipe);
							currentScoreFrameIndex++;
						} else {
							stateScoreFrame = 9;
							currentScoreFrameIndex = 0;
							
							//go_Popup_CalScore.SetActive(true);
							setUp_Success ();
							
						}
					}
					
					scoreTimer = 0;
				}
			}
		}
	}
	
	private void checkNextScoreFrame(int _nowFrame)
	{
		bool bSkip = false;
		if(_nowFrame == 0)
		{
			if(score_CrossCount > 0)
			{
				stateScoreFrame = 1;
				currentScoreFrameIndex = 0;
				scoreTimer = 0;
				bSkip = true;
			}
			_nowFrame++;
		}
		
		if(_nowFrame == 1 && !bSkip)
		{
			if(score_defaultCount > 0)
			{
				stateScoreFrame = 2;
				currentScoreFrameIndex = 0;
				scoreTimer = 0;
				bSkip = true;
			}
			_nowFrame++;
		}
		
		if(_nowFrame == 2 && !bSkip)
		{
			if(score_OnewayCount > 0)
			{
				stateScoreFrame = 3;
				currentScoreFrameIndex = 0;
				scoreTimer = 0;
				bSkip = true;
			}
			_nowFrame++;
		}
		
		if(_nowFrame == 3 && !bSkip)
		{
			if(score_InOutCount > 0)
			{
				stateScoreFrame = 4;
				currentScoreFrameIndex = 0;
				scoreTimer = 0;
				bSkip = true;
			}
			_nowFrame++;
		}
		
		if(_nowFrame == 4 && !bSkip)
		{
			if(score_1To2Count > 0)
			{
				stateScoreFrame = 5;
				currentScoreFrameIndex = 0;
				scoreTimer = 0;
				bSkip = true;
			}
			_nowFrame++;
		}
		
		if(_nowFrame == 5 && !bSkip)
		{
			if(score_1To3Count > 0)
			{
				stateScoreFrame = 6;
				currentScoreFrameIndex = 0;
				scoreTimer = 0;
				bSkip = true;
			}
			_nowFrame++;
		}
		
		if(_nowFrame == 6 && !bSkip)
		{
			if(score_WallInOutCount > 0)
			{
				stateScoreFrame = 7;
				currentScoreFrameIndex = 0;
				scoreTimer = 0;
				bSkip = true;
			}
			_nowFrame++;
		}
		
		if(_nowFrame == 7 && !bSkip)
		{
			if(score_MissCount > 0)
			{
				stateScoreFrame = 8;
				currentScoreFrameIndex = 0;
				scoreTimer = 0;
				bSkip = true;
			}
			_nowFrame++;
		}
		
		if(_nowFrame == 8 && !bSkip)
		{
			stateScoreFrame = 9;
			currentScoreFrameIndex = 0;
			scoreTimer = 0;
			bSkip = true;
			
			//go_Popup_CalScore.SetActive(true);
			setUp_Success ();
		}
		
		
	}
	
	private void setLoadingText(string _text)
	{
		GameObject.Find("text_loadingState").GetComponent<UILabel>().text = _text;
	}
	
	IEnumerator SleepLoading()
	{
		if (!bJustOneTimeCoroutine) 
		{
			bJustOneTimeCoroutine = true;
			yield return new WaitForSeconds (3f);
			//Debug.Log ("yield return new WaitForSeconds( 3f );");
			loading_state++;
			
		}
	}
	
	public void startGame()
	{
		loading_state++;
		//
	}
	
	
	
	
	
	public int getTotalStar ()
	{
		int returnValue = 0;
		for (int i=0; i<PlayerData.STAGE_SIZE; i++) 
		{
			returnValue += pd.star[i];
		}
		return returnValue;
	}
	
	public void setStar(int _index, int _type)
	{
		GameObject go = GameObject.Find ("STAR"+_index);
		if (_type == 0) 	//gray
		{
			go.GetComponent<UISprite>().spriteName = "ItemSprite014";
		}
		else if (_type == 1) 	//yellow
		{
			go.GetComponent<UISprite>().spriteName = "ItemSprite003";
		}
	}
	
	public void setBigText(int _type)
	{
		if (_type == 1) {
			go_bigText.SetActive (true);
			TextBigAlertScript script_textBigAlert = go_bigText.GetComponent<TextBigAlertScript> ();
			script_textBigAlert.setUpHurryUp ();
		} 
		else if (_type == 10) 
		{
			go_bigText.SetActive (true);
			TextBigAlertScript script_textBigAlert = go_bigText.GetComponent<TextBigAlertScript> ();
			script_textBigAlert.setUpReady ();
		}
		else if (_type == 20) 
		{
			//			go_bigText.SetActive (true);
			//			TextBigAlertScript script_textBigAlert = go_bigText.GetComponent<TextBigAlertScript> ();
			//			script_textBigAlert.setUpFail ();

			//moveOut_LeftRightPanel();

			setUp_Popup_Fail (0);
		}
	}
	
	
	public void setUp_Popup_Fail (int _failType)
	{
		
		go_popup_fail.SetActive (true);
		go_popup_fail.transform.localPosition = new Vector3(0, 800, 0);

		go_Char_fail.SetActive (true);
		go_Char_fail.transform.localPosition = new Vector3(400, 800, 0);

		go_GotoMenu_fail.SetActive(true);
		go_GotoMenu_fail.transform.localPosition = new Vector3(-800, -Screen.height/2, 0);

		if (_failType == 0)  	//norm limit fail...
		{
			efm.stop(6); //water sound
			GameObject.Find("text_fail_explain").GetComponent<UILabel>().text = "You failed to connect pipe!";
		}
		else if (_failType == 1) 	//norm limit fail...
		{
			GameObject.Find("text_fail_explain").GetComponent<UILabel>().text = "You have to connect pipe more than [FF0000]"+ limitNorm +"[-].";
		}
		
		
		efm.Play(8);
		
		TweenPosition twPosition = TweenPosition.Begin (go_popup_fail, 0.5f, new Vector3 (0, 0, 0));
		twPosition.method = UITweener.Method.BounceIn;
		EventDelegate.Add (twPosition.onFinished, callback_finished_move_popupFail, true);

		twPosition = TweenPosition.Begin (go_Char_fail, 0.5f, new Vector3 (400, -47, 0));
		twPosition.method = UITweener.Method.BounceIn;
	}
	
	void callback_finished_move_popupFail()
	{
		disable_all_trigger ();
		StartCoroutine(delay_Second(2f, 0));
	}

	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	IEnumerator delay_Second(float _delaySecond, int _call_func_index)
	{
		yield return new WaitForSeconds(_delaySecond); // wait 1 seconds

		switch(_call_func_index)
		{
		case 0:
			move_out_fail_popup();
			break;

		case 1:
			break;
		}
	}

	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	private void move_out_fail_popup()
	{
		efm.Play(8);

		go_FillRect_Screen.SetActive(false);

		TweenPosition twPosition = TweenPosition.Begin (go_popup_fail, 0.5f, new Vector3 (0, -800, 0));
		twPosition.method = UITweener.Method.BounceIn;
		EventDelegate.Add (twPosition.onFinished, callback_finished_move_popupFail1, true);

		twPosition = TweenPosition.Begin (go_Char_fail, 0.5f, new Vector3 (Screen.width/2-200, -Screen.height/2+40, 0));
		twPosition.method = UITweener.Method.BounceIn;

		twPosition = TweenPosition.Begin (go_GotoMenu_fail, 0.5f, new Vector3 (-Screen.width/2+110, -Screen.height/2+79, 0));
		twPosition.method = UITweener.Method.BounceIn;
	}

	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void callback_finished_move_popupFail1()
	{

	}

	public void disable_all_trigger ()
	{
		//GameObject.Find ("InventoryButton").GetComponent<BoxCollider> ().enabled = false;
		
		//GameObject.Find ("conveyorButton").GetComponent<BoxCollider> ().enabled = false;
		GameObject.Find ("stageItem0").GetComponent<BoxCollider> ().enabled = false;
		GameObject.Find ("stageItem1").GetComponent<BoxCollider> ().enabled = false;
		//GameObject.Find ("stageItem2").GetComponent<BoxCollider> ().enabled = false;
		GameObject.Find ("stageItem3").GetComponent<BoxCollider> ().enabled = false;
		
		//disable coveyoritems
		for (int i=0; i<10; i++) 
		{
			go_conveyorItem[i].GetComponent<BoxCollider>().enabled = false;
		}
		
		//disable invenitems
		for (int i=0; i<PlayerData.PIPE_ITEM_MAX; i++) 
		{
			go_invenItem[i].GetComponent<BoxCollider>().enabled = false;
			//uis_InventoryItem[i].parent.GetComponent<BoxCollider>().enabled = false;
		}
	}
	
	/// <summary>
	/// The width of the map.
	/// </summary>
	public float mapWidth;
	public float mapHeight;
	public void loadMap(int level) 
	{	
		//int shortValue = 0;
		//int byteValue = 0;
		
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
				limitNorm = br.ReadByte();
				limitTime = br.ReadInt16();

				pd.flowDefaultTime = limitTime;
				pd.flowTime = pd.flowDefaultTime;

				//map_maxScore = br.ReadInt32();
				//				Debug.Log("map_maxScore:  "+map_maxScore);
				
				mapdata = (width+2) * (height+2);
				perfectplan = (width+2) * (height+2);
				
				nCol = width+2;
				nRow = height+2;
				
				
				reSizeMapBuf (nRow, nCol);
				
				for (int i = 0; i < mapdata; i++)
				{
					map[i/nCol][i%nCol].pipeID = br.ReadByte();
				}

				for (int i = 0; i < mapdata; i++) 
				{
					mapBuf1[i/nCol][i%nCol] = br.ReadByte();
				}
				
				for (int i = 0; i < mapdata; i++) 
				{
					mapItem[i/nCol][i%nCol] = br.ReadByte();
				}
				
				userScore = 0;
				tmpUserScore = 0;
				tmpUserScore1 = 0;
				
				if(DEBUG_CW)
				{
					//map_maxScore = 0;
					limitNorm = 0;
					waterSpeed = 0.1f;
				}
				
				
			}
		} 
		catch(IOException ex)
		{
			Debug.Log(ex.Message);
		}
		
	}
	
	public void refresh_UseScore()
	{
		userScore = tmpUserScore1 + tmpUserScore;
		if (userScore < 0) 
		{
			userScore = 0;
		}
		GameObject.Find("text_UserScorer").GetComponent<UILabel>().text = userScore.ToString();
	}
	
	public string pathForDocumentsFile( string filename ) 
	{ 
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			string path = Application.dataPath.Substring( 0, Application.dataPath.Length - 5 );
			path = path.Substring( 0, path.LastIndexOf( '/' ) );
			return Path.Combine( Path.Combine( path, "Documents" ), filename );
		}
		
		else if(Application.platform == RuntimePlatform.Android)
		{
			string path = Application.persistentDataPath; 
			path = path.Substring(0, path.LastIndexOf( '/' ) ); 
			return Path.Combine (path, filename);
		} 
		
		else 
		{
			string path = Application.dataPath; 
			path = path.Substring(0, path.LastIndexOf( '/' ) );
			return Path.Combine (path, filename);
		}
	}
	
	public static void setBgAtlasSet(int _setNum)
	{
		switch(_setNum)
		{
		case 0:
			bgName = "PatternBg0";
			break;
			
		case 1:
			bgName = "PatternBg1";
			break;
			
		case 2:
			bgName = "PatternBg2";
			break;
			
		case 3:
			bgName = "PatternBg3";
			break;
			
		case 4:
			bgName = "PatternBg4";
			break;
			
		case 5:
			bgName = "PatternBg5";
			break;
			
		case 6:
			bgName = "PatternBg6";
			break;
		}
	}
	
	public static void setAtlasSet(int _setNum)
	{
		switch(_setNum)
		{
		case 0:
			basePipeAtlasPath = "Atlases/Atlas_Tile1";
			break;
			
		case 1:
			basePipeAtlasPath = "Atlases/Atlas_Tile2";
			break;
			
		case 2:
			basePipeAtlasPath = "Atlases/Atlas_Tile3";
			break;
			
		case 3:
			basePipeAtlasPath = "Atlases/Atlas_Tile4";
			break;
			
		case 4:
			basePipeAtlasPath = "Atlases/Atlas_Tile5";
			break;
			
		case 5:
			basePipeAtlasPath = "Atlases/Atlas_Tile6";
			break;
			
		case 6:
			basePipeAtlasPath = "Atlases/Atlas_Tile7";
			break;
		}
	}
	/// <summary>
	/// Cals the start position.
	/// </summary>
	private void calStartPos()
	{
		if (nCol % 2 == 0) 
		{
			//even
			SX = -nCol/2*90+45;
		} else {
			//odd
			SX = -nCol/2*90;
		}
		
		if (nRow % 2 == 0) 
		{
			//even
			SY = nRow/2*90-45;
		} else {
			//odd
			SY = nRow/2*90;
		}
	}
	
	
	
	
	public bool skipPipe = false;
	public void changePref_Tile_fromConveyorBelt(int _idx, int _pipeID)
	{
		Vector3 v3pos = gPipe[_idx].transform.localPosition;
		
		//if exist pipe, minus score
		if (!skipPipe) 
		{
			calScore_putPipe (sPipe[_idx].ID, _pipeID, v3pos);
		}
		
		sPipe[_idx].setID(_pipeID, 0);
		if ((invenPipeID >= 10 && invenPipeID <= 16)
		    || (invenPipeID >= 20 && invenPipeID <= 25)
		    || (invenPipeID >= 30 && invenPipeID <= 35)
		    || (invenPipeID >= 70 && invenPipeID <= 77)
		    || (invenPipeID >= 80 && invenPipeID <= 87)
		    ) {
			gPipe[_idx].transform.parent = go_parent.transform;
		}
		
		map[sPipe[_idx].ypos][sPipe[_idx].xpos].setInfo(_pipeID, _idx , 0, sPipe[_idx].xpos, sPipe[_idx].ypos);
		
		//_go.transform.localPosition = v3pos;
		
		getConnectCount();
		
	}
	
	private void calScore_putPipe(int _prevID, int _nowID, Vector3 _v3pos)
	{
		//Debug.Log ("_prevID: "+_prevID);
		if (_prevID == 1) 
		{
			if(_nowID == 16)
			{
				createNum (_v3pos.x, _v3pos.y, SCORE_NEW_CROSS);
				tmpUserScore1 += SCORE_NEW_CROSS;
			}
			else if(_nowID >= 10 && _nowID < 16)
			{
				createNum (_v3pos.x, _v3pos.y, SCORE_NEW_NORMAL);
				tmpUserScore1 += SCORE_NEW_NORMAL;
			}
			else
			{
				createNum (_v3pos.x, _v3pos.y, SCORE_NEW_SPECIAL);
				tmpUserScore1 += SCORE_NEW_SPECIAL;
			}
			
		}
		else
		{
			if(_prevID == 16)
			{
				createNum (_v3pos.x, _v3pos.y, SCORE_OVERWRITE_CROSS);
				tmpUserScore1 += SCORE_OVERWRITE_CROSS;
			}
			else if(_prevID >= 10 && _prevID < 16)
			{
				createNum (_v3pos.x, _v3pos.y, SCORE_OVERWRITE_NORMAL);
				tmpUserScore1 += SCORE_OVERWRITE_NORMAL;
			}
			else
			{
				createNum (_v3pos.x, _v3pos.y, SCORE_OVERWRITE_SPECIAL);
				tmpUserScore1 += SCORE_OVERWRITE_SPECIAL;
			}
		}
		if (tmpUserScore1 < 0) 
		{
			tmpUserScore1 = 0;
		}	
		refresh_UseScore();
	}
	
	private void calScore_skipPipe(int _nowID, Vector3 _v3pos)
	{
		if(_nowID == 16)
		{
			createSkipNum (_v3pos.x, _v3pos.y, SCORE_SKIP_CROSS);
			tmpUserScore1 += SCORE_SKIP_CROSS;
		}
		else if(_nowID >= 10 && _nowID < 16)
		{
			createSkipNum (_v3pos.x, _v3pos.y, SCORE_SKIP_NORMAL);
			tmpUserScore1 += SCORE_SKIP_NORMAL;
		}
		else
		{
			createSkipNum (_v3pos.x, _v3pos.y, SCORE_SKIP_SPECIAL);
			tmpUserScore1 += SCORE_SKIP_SPECIAL;
		}
		
		if (tmpUserScore1 < 0) 
		{
			tmpUserScore1 = 0;
		}
		refresh_UseScore();
	}
	
	
	public void setInvenCursorInitialize()
	{
		for(int i=0; i<pd.invenOpenLevel; i++)
		{
			go_invenItem [invenCursor].transform.FindChild ("Base").GetComponent<UISprite> ().spriteName = "Shop015";
		}
	}
	
	public void setInvenCursorPosition()
	{
		go_invenItem [invenCursor].transform.FindChild ("Base").GetComponent<UISprite> ().spriteName = "InvenCursor";
		//		GameObject go = GameObject.Find("invenCursorObj");
		//		Vector3 v3pos = go.transform.localPosition;
		//		v3pos.y = GameObject.Find ("InvenItem" + invenCursor).transform.localPosition.y+30;
		//		go.transform.localPosition = v3pos;
	}
	
	public bool checkReadyUseInventory()
	{
		bool returnBool = false;
		if (!bAni_InventoryItem && pd.pipes [invenCursor] > 0) 
		{
			returnBool = true;
		}
		return returnBool;
	}
	
	public bool changePref_Tile_fromInventory(int _idx)
	{
		Vector3 v3pos = gPipe[_idx].transform.localPosition;
		
		//if exist pipe, minus score
		calScore_putPipe(sPipe[_idx].ID, nowFireID, v3pos);
		
		//Destroy (nowGo);
		sPipe[_idx].setID(nowFireID, 0);
		
		gPipe[_idx].transform.parent = go_parent.transform;
		
		map[sPipe[_idx].ypos][sPipe[_idx].xpos].setInfo(nowFireID, _idx , 0, sPipe[_idx].xpos, sPipe[_idx].ypos);
		
		//cal inventory item data count
		pd.pipes [invenCursor]--;
		setInvenItemCount(invenCursor);
		pd.saveData();
		
		getConnectCount();
		
		
		return true;
	}

	public bool changePref_Tile_PlusPipe(int _xpos, int _ypos, int _effectIdx)
	{
		int tmpIndex = _ypos*nCol+_xpos;
		Vector3 v3pos = gPipe[tmpIndex].transform.localPosition;

		if(getID(_xpos, _ypos) == 1)
		{
			createPlusTileEffect(v3pos.x, v3pos.y, _effectIdx);

			//if exist pipe, minus score
			//calScore_putPipe(sPipe[_idx].ID, nowFireID, v3pos);
			
			//Destroy (nowGo);
			sPipe[tmpIndex].setID(getPerfectPlanID1(_xpos, _ypos), 0);
			
			gPipe[tmpIndex].transform.parent = go_parent.transform;
			
			map[_ypos][_xpos].setInfo(nowFireID, tmpIndex , 0, _xpos, _ypos);

			getConnectCount();
		}
		
		return true;
	}
	
	
	/// <summary>
	/// Creates the pref_ tile.
	/// </summary>
	private void createPref_Tile(int _pipeID, int _idx, int _xpos, int _ypos, int _layer)
	{
		
		setAtlasSet (GlobalData.baseTileNum);
		
		sPipe[_idx].setID(_pipeID, 0);
		map[_ypos][_xpos].setInfo(_pipeID, _idx , 0, _xpos, _ypos);
	}
	
	private void createPref_Tile_PerfectPlan(int _pipeID, int _idx, int _xpos, int _ypos)
	{
		string prefabsPath;
		
		prefabsPath = "cwPrefabs/Prefab_Pipe";
		
		if ((_pipeID >= 10 && _pipeID <= 16)
		    || (_pipeID >= 20 && _pipeID <= 25)
		    || (_pipeID >= 30 && _pipeID <= 35)
		    || (_pipeID >= 70 && _pipeID <= 77)
		    || (_pipeID >= 80 && _pipeID <= 87)
		    ) {
			setAtlasSet (GlobalData.perfectTileNum);
		}
		
		Vector3 v3pos = gPipe[_idx].transform.localPosition;
		
		GameObject _go = Instantiate(Resources.Load (prefabsPath), v3pos, Quaternion.identity) as GameObject;
		_go.GetComponent<PipeScript>().setID(_pipeID, 1);
		_go.GetComponent<PipeScript>().setPos(_xpos, _ypos);
		_go.GetComponent<PipeScript>().setIndex(_idx);

		if ((_pipeID >= 10 && _pipeID <= 16)
		    || (_pipeID >= 20 && _pipeID <= 25)
		    || (_pipeID >= 30 && _pipeID <= 35)
		    || (_pipeID >= 70 && _pipeID <= 77)
		    || (_pipeID >= 80 && _pipeID <= 87)
		    ) {
			_go.name = "Plan"+ _idx;
			setAtlasSet (GlobalData.perfectTileNum);
			_go.transform.parent = go_parent_perfectplan.transform;
			
		}
		_go.transform.localScale = new Vector3(1, 1, 1);
		_go.transform.localPosition = v3pos;
	}
	
	
	private void createPref_mapItem(int _itemID, Vector3 _pos, int _idx, int _xpos, int _ypos)
	{
		string prefabsPath = " ";
		
		switch(_itemID)
		{
		case 10:
			prefabsPath = "cwPrefabs/GameItem0";
			break;
			
		case 20:
			prefabsPath = "cwPrefabs/GameItem1";
			break;
			
		case 30:
			prefabsPath = "cwPrefabs/GameItem2";
			break;
			
		case 40:
			prefabsPath = "cwPrefabs/GameItem3";
			break;
			
		case 50:
			prefabsPath = "cwPrefabs/GameItem4";
			break;
			
		case 51:
			prefabsPath = "cwPrefabs/GameItem5";
			break;
			
		case 52:
			prefabsPath = "cwPrefabs/GameItem6";
			break;
			
		case 53:
			prefabsPath = "cwPrefabs/GameItem7";
			break;
			
		case 54:
			prefabsPath = "cwPrefabs/GameItem8";
			break;

		case 60: case 61: case 62: case 63: case 64: 
		case 65: case 66: case 67: case 68: case 69: 
		case 70: case 71: case 72: case 73: case 74: case 75: 
			prefabsPath = "cwPrefabs/GameItem" + (_itemID - 50);
			break;
			
		default:
			prefabsPath = " ";
			break;
		}
		
		
		//		if ((_pipeID >= 10 && _pipeID <= 16)
		//		    || (_pipeID >= 20 && _pipeID <= 25)
		//		    || (_pipeID >= 30 && _pipeID <= 35)
		//		    || (_pipeID >= 70 && _pipeID <= 77)
		//		    || (_pipeID >= 80 && _pipeID <= 87)
		//		    ) {
		//			setAtlasSet (perfectTileNum);
		//		}
		
		GameObject _go = Instantiate(Resources.Load (prefabsPath), _pos, Quaternion.identity) as GameObject;
		
		_go.name = "GItem"+ _idx;
		setAtlasSet (GlobalData.perfectTileNum);
		_go.transform.parent = go_parent_Item.transform;
		_go.transform.localScale = new Vector3(0.5f, 0.5f, 1);
		
		//		_go.GetComponent<PM>().pos.setPos(_xpos, _ypos);	//very important.....
		//map[_ypos][_xpos].setInfo(_pipeID, _idx , 0, _xpos, _ypos);
		//_go.GetComponent<PM>().posInfo.setInfo(_pipeID, _idx , 0, _xpos, _ypos);
		
		
		_go.transform.localPosition = _pos;
		
		
		switch(_itemID)
		{
		case 10:
			
			break;
			
		case 20:
			break;
			
		case 30:
			break;
			
		case 40:
			break;
			
		case 50:
			_go.transform.FindChild("ItemIcon").transform.FindChild("Time").GetComponent<BombScript>().play(30, _xpos, _ypos);
			break;
			
		case 51:
			_go.transform.FindChild("ItemIcon").transform.FindChild("Time").GetComponent<BombScript>().play(40, _xpos, _ypos);
			break;
			
		case 52:
			_go.transform.FindChild("ItemIcon").transform.FindChild("Time").GetComponent<BombScript>().play(50, _xpos, _ypos);
			break;
			
		case 53:
			_go.transform.FindChild("ItemIcon").transform.FindChild("Time").GetComponent<BombScript>().play(60, _xpos, _ypos);
			break;
			
		case 54:
			_go.transform.FindChild("ItemIcon").transform.FindChild("Time").GetComponent<BombScript>().play(90, _xpos, _ypos);
			break;
			
			
			
		}
	}
	
	//GameObject prefab_FillRectPipe;
	private void createPref_FillRectTile(int _pipeID, Vector3 _pos, int _idx, int _xpos, int _ypos)
	{
		string prefabsPath;
		
		prefabsPath = "cwPrefabs/FillRectPipe";
		
		GameObject _go = Instantiate(Resources.Load (prefabsPath), _pos, Quaternion.identity) as GameObject;
		//GameObject _go = Instantiate (prefab_FillRectPipe);//(Resources.Load (prefabsPath), _pos, Quaternion.identity) as GameObject;
		
		_go.name = "FillRectPipe"+ _idx;
		_go.transform.parent = go_Popup_CalScore.transform;
		_go.transform.localScale = new Vector3(1, 1, 1);
		_go.transform.localPosition = _pos;
	}
	
	
	/// <summary>
	/// Creates the pref_ tile.
	/// </summary>
	
	
	
	private void createPref_Pipes_Base()
	{
		string prefabsPath;
		
		calStartPos ();
		
		
		//PBase
		prefabsPath = "cwPrefabs/PBase";
		for (int i=0; i<nRow; i++) 
		{
			for (int j=0; j<nCol; j++) 
			{
				if(map [i] [j].pipeID == 0)
				{
					continue;
				}
				if (map [i] [j].pipeID < 160) 
				{
					GameObject _go = Instantiate (Resources.Load (prefabsPath), new Vector3 (SX + j * 90, SY - i * 90, 0), Quaternion.identity) as GameObject;
					
					_go.name = "Base" + (i * nCol + j);
					
					_go.transform.parent = go_parent_base.transform;
					
					_go.transform.localScale = new Vector3 (1, 1, 1);
					
					Vector3 pos = _go.transform.localPosition;
					pos.x = SX + j * 90;
					pos.y = SY - i * 90;
					_go.transform.localPosition = pos;
				}
			}
		}
	}
	
	private void createPref_Pipes_Controll()
	{
		//				string prefabsPath;
		//				//P Controll
		//				for (int i=0; i<nRow; i++) {
		//						for (int j=0; j<nCol; j++) {
		//								prefabsPath = "cwPrefabs/P" + (map [i] [j].pipeID);
		//
		//
		//
		//								//Debug.Log("prefabsPath"+prefabsPath);
		//								GameObject _go = Instantiate (Resources.Load (prefabsPath), new Vector2 (SX + j * 90, SY - i * 90), Quaternion.identity) as GameObject;
		//
		//								setAtlasSet (baseTileNum);
		//								_go.name = "Pipe" + (i * nCol + j);
		//
		////				Debug.Log("mapBuf[i][j]:   "+mapBuf[i][j]);
		//								if (map [i] [j].pipeID == 1) {
		//										_go.transform.parent = go_parent.transform;
		//								} else if (map [i] [j].pipeID == 3) {	//object
		//										_go.transform.parent = go_parent_top.transform;
		//								} else {
		//										_go.transform.parent = go_parent.transform;
		//								}
		//
		//								_go.transform.localScale = new Vector3 (1, 1, 1);
		//
		//								//setting button index....
		//								_go.GetComponent<PM> ().pos = new POS ();
		//								_go.GetComponent<PM> ().pos.setPos (j, i);	//very important.....
		//								map [i] [j].setInfo (map [i] [j].pipeID, (i * nCol + j), 0, j, i);
		//								//_go.GetComponent<PM>().posInfo.setInfo(map[i][j].pipeID, (i*nCol + j), 0, j, i);
		//
		//								//Debug.Log( (i*nCol+j) + "_go.GetComponent<PM>().pos:   "+_go.GetComponent<PM>().pos);
		//
		//				
		//								Vector3 pos = _go.transform.localPosition;
		//								pos.x = SX + j * 90;
		//								pos.y = SY - i * 90;
		//								_go.transform.localPosition = pos;
		//						}
		//				}
		
		for (int i=0; i<nRow*nCol; i++) 
		{
			gPipe[i] = Instantiate (Resources.Load ("cwPrefabs/Prefab_Pipe"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			sPipe[i] = gPipe[i].GetComponent<PipeScript>();
			//setAtlasSet (baseTileNum);
			gPipe[i].name = "Pipe" + (i);
			sPipe[i].setIndex(i);
		}
		
		string prefabsPath;
		//P Controll
		for (int i=0; i<nRow; i++) 
		{
			for (int j=0; j<nCol; j++) 
			{
				
				
				if (map [i] [j].pipeID == 1) {
					gPipe[i*nCol+j].transform.parent = go_parent.transform;
				} else if (map [i] [j].pipeID == 3) {	//object
					gPipe[i*nCol+j].transform.parent = go_parent_top.transform;
				} else {
					gPipe[i*nCol+j].transform.parent = go_parent.transform;
				}
				
				gPipe[i*nCol+j].transform.localScale = new Vector3 (1, 1, 1);
				
				sPipe[i*nCol+j].setPos (j, i);	//very important.....
				map [i] [j].setInfo (map [i] [j].pipeID, (i * nCol + j), 0, j, i);
				
				//				Vector3 pos = gPipe[i*nCol+j].transform.localPosition;
				//				pos.x = SX + j * 90;
				//				pos.y = SY - i * 90;
				gPipe[i*nCol+j].transform.localPosition = new Vector3(SX + j * 90, SY - i * 90, 0);
				
				sPipe[i*nCol+j].setID(map [i] [j].pipeID, 0);
			}
		}
	}
	
	private void createPref_Pipes_PerfectPlan()
	{
		string prefabsPath;
		//perfect plan
		for (int i=0; i<nRow; i++) {
			for (int j=0; j<nCol; j++) {
				if (mapBuf1 [i] [j] != 0) {
					if (mapBuf1 [i] [j] >= 110 && mapBuf1 [i] [j] <= 123) {
						//Debug.Log("mapBuf1[i][j]"+mapBuf1[i][j]);
						continue;
					}
					createPref_Tile_PerfectPlan (mapBuf1 [i] [j], (i * nCol + j), j, i); //perfect plan
				}
			}
		}
		
		
		//cheat
		if (DEBUG_CHEAT_SETUP_MAP) {
			for (int i=0; i<nRow; i++) {
				for (int j=0; j<nCol; j++) {
					if (mapBuf1 [i] [j] != 0) {
						//												GameObject _go = GameObject.Find ("Pipe" + (i * nCol + j));
						//												Vector3 pos = _go.transform.localPosition;
						//												//POS tmpPos = _go.GetComponent<PM> ().pos;
						//												Destroy (_go);
						
						createPref_Tile (mapBuf1 [i] [j], (i * nCol + j), j, i, 0); //perfect plan
					}
				}
			}
		}
		getConnectCount ();
		
	}
	
	private void createPref_Pipes_Item()
	{
		//perfect plan
		for (int i=0; i<nRow; i++) {
			for (int j=0; j<nCol; j++) {
				if (mapItem [i] [j] != 0) {
					GameObject _go = GameObject.Find ("Pipe" + (i * nCol + j));
					Vector3 v3pos = _go.transform.localPosition;
					
					createPref_mapItem (mapItem [i] [j], v3pos, (i * nCol + j), j, i); //perfect plan
				}
			}
		}
	}
	
	private void createPref_Pipes_FillRect()
	{
		
		//FillRect Tile... for Effect Score....
		//prefab_FillRectPipe = Instantiate(Resources.Load ("cwPrefabs/FillRectPipe"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		for (int i=0; i<nRow; i++) 
		{
			for (int j=0; j<nCol; j++) 
			{
				GameObject _go = GameObject.Find ("Pipe"+(i*nCol + j));
				Vector3 v3pos = _go.transform.localPosition;
				
				createPref_FillRectTile(mapBuf1[i][j], v3pos, (i*nCol + j), j, i); //perfect plan
			}
		}
		
		GameObject.Find ("FillRectTop").GetComponent<UISprite> ().SetRect (-1000, SY + 45, 2000, 1000);
		GameObject.Find ("FillRectBottom").GetComponent<UISprite> ().SetRect (-1000, SY -((nRow-1)*90)- 45-1000, 2000, 1000);
		GameObject.Find ("FillRectLeft").GetComponent<UISprite> ().SetRect (SX-45-1000, SY -((nRow-1)*90)- 45, 1000, ((nRow)*90));
		GameObject.Find ("FillRectRight").GetComponent<UISprite> ().SetRect (SX+45+((nCol-1)*90), SY -((nRow-1)*90)- 45, 1000, ((nRow)*90));
		
	}
	
	/// <summary>
	/// reSizeBuf
	/// </summary>
	public void reSizeMapBuf(int _Row, int _Col)
	{
		
		if (map != null) {
			map = null;
		}
		
		map = new POSINFO[_Row][];
		for (int i=0; i<_Row; i++) {
			map [i] = new POSINFO[_Col];
		}
		
		for (int i=0; i<_Row; i++) 
		{
			for (int j=0; j<_Col; j++) 
			{
				map [i][j] = new POSINFO();
			}
		}
		
		
		if (mapBuf1 != null) {
			mapBuf1 = null;
		}
		
		mapBuf1 = new byte[_Row][];
		for (int i=0; i<_Row; i++) {
			mapBuf1 [i] = new byte[_Col];
		}
		
		
		if (mapItem != null) {
			mapItem = null;
		}
		
		mapItem = new byte[_Row][];
		for (int i=0; i<_Row; i++) {
			mapItem [i] = new byte[_Col];
		}
		
		
		
		if (objIdxBuf != null) {
			objIdxBuf = null;
		}
		
		objIdxBuf = new byte[_Row][];
		for (int i=0; i<_Row; i++) {
			objIdxBuf [i] = new byte[_Col];
		}
		
		for (int i=0; i<_Row; i++) {
			for (int j=0; j<_Col; j++) {
				objIdxBuf[i][j] = (byte)(i*nCol+j);
			}
		}
		
		perfectBuf = new byte[_Row][];
		for (int i=0; i<_Row; i++) {
			perfectBuf [i] = new byte[_Col];
		}
		
		nowConnectBuf = new int[_Row][];
		beforeConnectBuf = new int[_Row][];
		for (int i=0; i<_Row; i++) {
			nowConnectBuf [i] = new int[_Col];
			beforeConnectBuf [i] = new int[_Col];
		}
		
		initConnectBuf ();
		
		gPipe = new GameObject[_Row*_Col];
		sPipe = new PipeScript[_Row*_Col];
		//		for (int i=0; i<_Row*_Col; i++) 
		//		{
		//			gPipe[i] = Instantiate (Resources.Load ("cwPrefabs/Prefab_Pipe"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		//			sPipe[i] = gPipe[i].GetComponent<PipeScript>();
		//			//setAtlasSet (baseTileNum);
		//			gPipe[i].name = "Pipe" + (i);
		//			sPipe[i].setIndex(i);
		//		}
		//		if (TEST_MAP_SETTING) 
		//		{
		//			for (int i=0; i<_Row; i++) {
		//				for (int j=0; j<_Col; j++) {
		//					map[i][j].pipeID = 1;
		//				}
		//			}
		//		}
	}
	
	
	
	
	
	
	
	
	
	
	IEnumerator delay(GameObject _go, bool _active){
		
		if (_go) {
			_go.SetActive(_active);
			yield return new WaitForSeconds(1f);
		}
	}
	
	//GameObject[] invenItem = new GameObject[PlayerData.PIPE_ITEM_MAX];
	private void setInvenPipeOpen()
	{
		for (int i=0; i<PlayerData.PIPE_ITEM_MAX; i++) 
		{
			//			invenItem[i] = GameObject.Find ("InvenItem"+i);
			go_invenItem[i] = GameObject.Find("InvenItem"+i);
			//			StartCoroutine( delay (go_invenItem[i], false));
			
			uis_InventoryItem[i] = go_invenItem[i].transform.FindChild("Layer11").GetComponent<UISprite>();
			
			//Debug.Log("pd.invenOpenLevel"+pd.invenOpenLevel);
			if(i < pd.invenOpenLevel)
			{
				setAtlas_InvenItem(i);
				setInvenItemCount(i);
			}
			else 
			{
				go_invenItem[i].SetActive(false);
			}
		}
		
		//		for (int i=0; i<pd.invenOpenLevel; i++) 
		//		{
		////			StartCoroutine( delay (go_invenItem[i], true));
		//
		//		}
	}
	
	private void setAtlas_InvenItem(int _index)
	{
		UIAtlas uia = Resources.Load<UIAtlas> (GameCon.basePipeAtlasPath);
		GameObject tmpInvenItem = GameObject.Find("InvenItem"+_index);
		//Debug.Log ("GameCon.basePipeAtlasPath   :   " +GameCon.basePipeAtlasPath);
		//Debug.Log ("tmpInvenItem   :   " +tmpInvenItem.name);
		tmpInvenItem.transform.FindChild ("Layer2").GetComponent<UISprite> ().atlas = uia;
		tmpInvenItem.transform.FindChild ("Layer11").GetComponent<UISprite> ().atlas = uia;
	}
	
	private void setInvenItemCount(int _index)
	{
		GameObject tmpInvenItem = GameObject.Find("InvenItem"+_index);
		tmpInvenItem.transform.FindChild("Count").GetComponent<UILabel>().text = ""+pd.pipes[_index];
	}
	
	
	
	
	
	
	
	void show_EndScore(GameObject _go, int _index, int _scoreType)
	{
		Vector3 v3pos = _go.transform.localPosition;
		
		if (_scoreType == 1)  //cross
		{
			createEndNum (v3pos.x, v3pos.y, SCORE_END_CROSS, 0);
		}
		else if (_scoreType == 2)  //default
		{
			createEndNum (v3pos.x, v3pos.y, SCORE_END_DEFAULT, 0);
		}
		else if (_scoreType == 3)  //oneway
		{
			createEndNum (v3pos.x, v3pos.y, SCORE_END_ONEWAY, 0);
		}
		else if (_scoreType == 4)  //inOut
		{
			createEndNum (v3pos.x, v3pos.y, SCORE_END_INOUT, 0);
		}
		else if (_scoreType == 5)  //1to2
		{
			createEndNum (v3pos.x, v3pos.y, SCORE_END_1TO2, 0);
		}
		else if (_scoreType == 6)  //1to3
		{
			createEndNum (v3pos.x, v3pos.y, SCORE_END_1TO3, 0);
		}
		else if (_scoreType == 7)  //WALLINOUT
		{
			createEndNum (v3pos.x, v3pos.y, SCORE_END_WALL_INOUT, 0);
		}
		else if (_scoreType == 8)  //MISS
		{
			createEndNum (v3pos.x, v3pos.y, SCORE_END_MISS, 0);
		}
		
		if (currentScoreFrameIndex +1 > limitNorm) 
		{
			createEndNum (v3pos.x, v3pos.y, SCORE_END_OVERUSE, 1);
		}
		
	}
	
	//	private void hidePerfectPlanPipe()
	//	{
	//		for (int i= 0; i<go_parent_perfectplan.transform.childCount; i++) 
	//		{
	//			go_parent_perfectplan.transform.GetChild(i).gameObject.SetActive(false);
	//		}
	//	}
	//
	//	private void showPerfectPlanPipe()
	//	{
	//		for (int i= 0; i<go_parent_perfectplan.transform.childCount; i++) 
	//		{
	//			go_parent_perfectplan.transform.GetChild(i).gameObject.SetActive(true);
	//		}
	//	}
	
	
	//	/// <summary>
	//	/// Raises the pipe click event.
	//	/// pipe change...
	//	/// </summary>
	//	/// <param name="_Idx">_ index.</param>
	//	public void OnPipeClick(int _Idx)
	//	{
	////		Debug.Log ("Fuck---:"+_Idx);
	//		GameObject _go = GameObject.Find ("Pipe"+_Idx);
	//		Vector3 pos = _go.transform.localPosition;
	//		POS tmpPos = _go.GetComponent<PM> ().pos;
	//		Destroy (_go);
	//
	//		createPref_Tile (10, pos, _Idx, tmpPos.x, tmpPos.y, 0);	//layer_controll panel
	//	}
	
	
	
	public void StartNextPipe_OUT(int _xpos, int _ypos)
	{
		//Debug.Log ("fuck.......");
		bool bEscape = false;
		//GameObject nowGo = GameObject.Find ("Pipe"+ getIndex(_pos) );
		GameObject nextGo = null;
		
		
		int next_idx = findOutPipe ( getID (_xpos, _ypos));
		if (next_idx != 0) 
		{
			setAniStartNextPipe (next_idx, 0);
		}
	}
	
	
	
	private int findOutPipe(int _pipeID)
	{
		bool bEscape = false;
		int returnValue = 0;
		
		if (_pipeID >= 130 && _pipeID <= 133) 
		{
			for (int i=0; i<nRow; i++) 
			{
				for (int j=0; j<nCol; j++) 
				{
					if(bEscape) continue;
					if(map[i][j].pipeID >= 134 && map[i][j].pipeID <= 137)
					{
						returnValue = i*nCol+j;
						bEscape = true;
					}		
				}
			}
		}
		else if (_pipeID >= 140 && _pipeID <= 143) 
		{
			for (int i=0; i<nRow; i++) 
			{
				for (int j=0; j<nCol; j++) 
				{
					if(bEscape) continue;
					if(map[i][j].pipeID >= 144 && map[i][j].pipeID <= 147)
					{
						returnValue = i*nCol+j;
						bEscape = true;
					}		
				}
			}
		}
		else if (_pipeID >= 150 && _pipeID <= 153) 
		{
			for (int i=0; i<nRow; i++) 
			{
				for (int j=0; j<nCol; j++) 
				{
					if(bEscape) continue;
					if(map[i][j].pipeID >= 154 && map[i][j].pipeID <= 157)
					{
						returnValue = i*nCol+j;
						bEscape = true;
					}		
				}
			}
		}
		
		
		return returnValue;
	}
	
	
	public void StartNextPipe_Wall(int _xpos, int _ypos)
	{
		//		Debug.Log ("Fuck come int");
		bool bEscape = false;
		//GameObject nowGo = GameObject.Find ("Pipe"+ getIndex(_pos)) ;
		//GameObject nextGo = null;
		int next_idx = 0;
		//POSINFO nextPosInfo = null;
		int enterDirection = ( get_direction(_xpos, _ypos) + 2) % 4;
		
		if (getID (_xpos, _ypos) == 171) 
		{
			for(int i=_ypos+1; i<nRow; i++)
			{
				if(bEscape) continue;
				if(map[i][_xpos].pipeID == 172)
				{
					next_idx = (i*nCol+_xpos);
					bEscape = true;
				}
			}
		}
		else if (getID (_xpos, _ypos) == 172) 
		{
			for(int i=_ypos-1; i>=0; i--)
			{
				if(bEscape) continue;
				if(map[i][_xpos].pipeID == 171)
				{
					next_idx = (i*nCol+_xpos);
					bEscape = true;
				}
			}
		}
		else if (getID (_xpos, _ypos) == 170) 
		{
			for(int j=_xpos+1; j<nCol; j++)
			{
				if(bEscape) continue;
				if(map[_ypos][j].pipeID == 173)
				{
					next_idx = (_ypos*nCol+j);
					bEscape = true;
				}
			}
		}
		else if (getID (_xpos, _ypos) == 173) 
		{
			for(int j=_xpos-1; j>=0; j--)
			{
				if(bEscape) continue;
				if(map[_ypos][j].pipeID == 170)
				{
					next_idx = (_ypos*nCol+j);
					bEscape = true;
				}
			}
		}
		
		if (next_idx != 0) 
		{
			setAniStartNextPipe (next_idx, enterDirection);
		}
	}
	
	public void calConnect_NextPipe_Wall(int _xpos, int _ypos)
	{
		//		Debug.Log ("Fuck come int");
		bool bEscape = false;
		//GameObject nowGo = GameObject.Find ("Pipe"+ getIndex(_pos)) ;
		//GameObject nextGo = null;
		//POSINFO nextPosInfo = null;
		
		int next_idx = 999;
		int enterDirection = ( get_direction_connect(_xpos, _ypos) + 2) % 4;
		
		if (getID (_xpos, _ypos) == 171) 
		{
			for(int i=_ypos+1; i<nRow; i++)
			{
				if(bEscape) continue;
				if(map[i][_xpos].pipeID == 172)
				{
					next_idx = (i*nCol+_xpos);
					bEscape = true;
				}
			}
		}
		else if (getID (_xpos, _ypos) == 172) 
		{
			for(int i=_ypos-1; i>=0; i--)
			{
				if(bEscape) continue;
				if(map[i][_xpos].pipeID == 171)
				{
					next_idx = (i*nCol+_xpos);
					bEscape = true;
				}
			}
		}
		else if (getID (_xpos, _ypos) == 170) 
		{
			for(int j=_xpos+1; j<nCol; j++)
			{
				if(bEscape) continue;
				if(map[_ypos][j].pipeID == 173)
				{
					next_idx = (_ypos*nCol+j);
					bEscape = true;
				}
			}
		}
		else if (getID (_xpos, _ypos) == 173) 
		{
			for(int j=_xpos-1; j>=0; j--)
			{
				if(bEscape) continue;
				if(map[_ypos][j].pipeID == 170)
				{
					next_idx = (_ypos*nCol+j);
					bEscape = true;
				}
			}
		}
		
		if (next_idx != 999) 
		{
			setConnectNextPipe (next_idx, enterDirection);
		}
		else {
			//Debug.Log("calConnect_NextPipe_Wall aliveStream -- ");
		}
	}
	
	
	
	public void calConnect_NextPipe_OUT(int _xpos, int _ypos)
	{
		//Debug.Log ("fuck.......");
		bool bEscape = false;
		//GameObject nowGo = GameObject.Find ("Pipe"+ getIndex(_pos) );
		GameObject nextGo = null;
		
		
		int next_idx = findOutPipe ( getID (_xpos, _ypos));
		if (next_idx != 0) 
		{
			setConnectNextPipe (next_idx, 0);
		} else {
			//Debug.Log("calConnect_NextPipe_OUT aliveStream -- ");
		}
	}
	
	
	
	private void initConnectBuf()
	{
		
		for (int i=0; i<nRow; i++) 
		{
			for (int j=0; j<nCol; j++) 
			{
				nowConnectBuf[i][j] = 0;
			}
		}
	}
	
	private void setConnectBuf(int _xpos, int _ypos)
	{
		nowConnectBuf[_ypos][_xpos] = 1;
	}
	
	private void makeBeforeConnectBuf()
	{
		for (int i=0; i<nRow; i++) 
		{
			for (int j=0; j<nCol; j++) 
			{
				beforeConnectBuf[i][j] = 0;
				if(nowConnectBuf[i][j] == 1)
				{
					beforeConnectBuf[i][j] = 2;
				}
			}
		}
	}
	
	
	private void getAddConnectCount_n_Score()
	{
		addConnectCount = 0;
		addScore = 0;
		for (int i=0; i<nRow; i++) 
		{
			for (int j=0; j<nCol; j++) 
			{
				if(beforeConnectBuf[i][j] == 0 && nowConnectBuf[i][j] == 1)
				{
					addConnectCount++;
					addScore += cal_tmpUserScore(map[i][j].pipeID);
				}
			}
		}
	}
	
	int lostScore = 0;
	int addScore = 0;
	private void getLostConnectCount_n_Score()
	{
		lostConnectCount = 0;
		lostScore = 0;
		for (int i=0; i<nRow; i++) 
		{
			for (int j=0; j<nCol; j++) 
			{
				if(beforeConnectBuf[i][j] == 2 && nowConnectBuf[i][j] == 0)
				{
					lostConnectCount++;
					lostScore += cal_tmpUserScore(map[i][j].pipeID);
				}
			}
		}
	}
	
	public void calConnect_NextPipe(int _xpos, int _ypos)
	{
		setConnectBuf (_xpos, _ypos);
		
		int netx_idx = getNextConnectObjIndex(_xpos, _ypos);
		
		int enterDirection = (get_direction_connect( _xpos, _ypos) + 2) % 4;
		
		if (!get_bFinish_connect( sPipe[netx_idx].xpos, sPipe[netx_idx].ypos, enterDirection)) {
			setConnectNextPipe (netx_idx, enterDirection);
		} else {
			GameCon.aliveStream--;
			//Debug.Log("calConnect_NextPipe aliveStream -- ");
		}
	}
	
	
	public int cal_tmpUserScore(int _pipeID)
	{
		int returnScore = 0;
		switch( _pipeID )
		{
		case 10:	case 40:
		case 11:	case 41:
		case 12:	case 42:
		case 13:	case 43:
		case 14:	case 44:
		case 15:	case 45:
		case 16:	case 46:
			returnScore += SCORE_END_DEFAULT;
			break;
			
		case 20:	case 30:	case 50:	case 60:
		case 21:	case 31:	case 51:	case 61:
		case 22:	case 32:	case 52:	case 62:
		case 23:	case 33:	case 53:	case 63:
		case 24:	case 34:	case 54:	case 64:
		case 25:	case 35:	case 55:	case 65:
			
			returnScore += SCORE_END_ONEWAY;
			break;
			
			
			
		case 70:	case 90:
		case 71:	case 91:
		case 72:	case 92:
		case 73:	case 93:
		case 74:	case 94:
		case 75:	case 95:
		case 76:	case 96:
		case 77: 	case 97:
			returnScore += SCORE_END_1TO2;
			break;
			
			
		case 80:	case 100:
		case 81:	case 101:
		case 82:	case 102:
		case 83:	case 103:
		case 84:	case 104:
		case 85:	case 105:
		case 86:	case 106:
		case 87:	case 107:
			returnScore += SCORE_END_1TO3;
			break;
			
		case 130:
		case 140:
		case 150:
		case 131:
		case 141:
		case 151:
		case 132:
		case 142:
		case 152:
		case 133:
		case 143:
		case 153:
		case 134:
		case 144:
		case 154:
		case 135:
		case 145:
		case 155:
		case 136:
		case 146:
		case 156:
		case 137:
		case 147:
		case 157:
			returnScore += SCORE_END_INOUT;
			break;
			
		case 170:
		case 171:
		case 172:
		case 173:
			returnScore += SCORE_END_WALL_INOUT;
			break;
		}
		return returnScore;
	}
	
	
	public void setConnectNextPipe(int _next_idx, int enterDirection)
	{
		
		int nextXpos = sPipe[_next_idx].xpos;
		int nextYpos = sPipe[_next_idx].ypos;
		
		int tmpPipeID = getID (nextXpos, nextYpos);
		switch( tmpPipeID )
		{
		case 10:	case 20:	case 30:	case 40:	case 50:	case 60:
			if(enterDirection == WEST)
			{
				//_nextGo.GetComponent<P10> ().calConnect (1);
				set_direction_connect(nextXpos, nextYpos, SOUTH);
				set_bFinish_connect(nextXpos, nextYpos, 3, true);
				set_bFinish_connect(nextXpos, nextYpos, 2, true);
				GameCon.connectCount++;
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				calConnect_NextPipe(nextXpos, nextYpos);
			}
			else if(enterDirection == SOUTH)
			{
				//_nextGo.GetComponent<P10> ().calConnect (2);
				
				
				set_direction_connect(nextXpos, nextYpos, WEST);
				set_bFinish_connect(nextXpos, nextYpos, 2, true);
				set_bFinish_connect(nextXpos, nextYpos, 3, true);
				GameCon.connectCount++;
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				calConnect_NextPipe(nextXpos, nextYpos);
			}
			break;
			
		case 11:	case 21:	case 31:	case 41:	case 51:	case 61:
			if(enterDirection == WEST)
			{
				//_nextGo.GetComponent<P11> ().calConnect (4);
				
				set_direction_connect(nextXpos, nextYpos, NORTH);
				set_bFinish_connect(nextXpos, nextYpos, 3, true);
				set_bFinish_connect(nextXpos, nextYpos, 0, true);
				GameCon.connectCount++;
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				calConnect_NextPipe(nextXpos, nextYpos);
			}
			else if(enterDirection == NORTH)
			{
				//_nextGo.GetComponent<P11> ().calConnect (3);
				
				set_direction_connect(nextXpos, nextYpos, WEST);
				set_bFinish_connect(nextXpos, nextYpos, 0, true);
				set_bFinish_connect(nextXpos, nextYpos, 3, true);
				GameCon.connectCount++;
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				calConnect_NextPipe(nextXpos, nextYpos);
			}
			break;
			
		case 12:	case 22:	case 32:	case 42:	case 52:	case 62:
			if(enterDirection == EAST)
			{
				//_nextGo.GetComponent<P12> ().calConnect (5);
				
				set_direction_connect(nextXpos, nextYpos, NORTH);
				set_bFinish_connect(nextXpos, nextYpos, 1, true);
				set_bFinish_connect(nextXpos, nextYpos, 0, true);
				GameCon.connectCount++;
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				calConnect_NextPipe(nextXpos, nextYpos);
			}
			else if(enterDirection == NORTH)
			{
				//_nextGo.GetComponent<P12> ().calConnect (6);
				
				set_direction_connect(nextXpos, nextYpos, EAST);
				set_bFinish_connect(nextXpos, nextYpos, 0, true);
				set_bFinish_connect(nextXpos, nextYpos, 1, true);
				GameCon.connectCount++;
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				calConnect_NextPipe(nextXpos, nextYpos);
			}
			break;
			
		case 13:	case 23:	case 33:	case 43:	case 53:	case 63:
			if(enterDirection == EAST)
			{
				//_nextGo.GetComponent<P13> ().calConnect (8);
				
				set_direction_connect(nextXpos, nextYpos, SOUTH);
				set_bFinish_connect(nextXpos, nextYpos, 1, true);
				set_bFinish_connect(nextXpos, nextYpos, 2, true);
				GameCon.connectCount++;
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				calConnect_NextPipe(nextXpos, nextYpos);
			}
			else if(enterDirection == SOUTH)
			{
				//_nextGo.GetComponent<P13> ().calConnect (7);
				
				set_direction_connect(nextXpos, nextYpos, EAST);
				set_bFinish_connect(nextXpos, nextYpos, 2, true);
				set_bFinish_connect(nextXpos, nextYpos, 1, true);
				GameCon.connectCount++;
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				calConnect_NextPipe(nextXpos, nextYpos);
			}
			break;
			
		case 14:	case 24:	case 34:	case 44:	case 54:	case 64:
			if(enterDirection == EAST)
			{
				set_direction_connect(nextXpos, nextYpos, WEST);
				set_bFinish_connect(nextXpos, nextYpos, 1, true);
				set_bFinish_connect(nextXpos, nextYpos, 3, true);
				GameCon.connectCount++;
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				calConnect_NextPipe(nextXpos, nextYpos);
				
			}
			else if(enterDirection == WEST)
			{
				
				set_direction_connect(nextXpos, nextYpos, EAST);
				set_bFinish_connect(nextXpos, nextYpos, 3, true);
				set_bFinish_connect(nextXpos, nextYpos, 1, true);
				GameCon.connectCount++;
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				calConnect_NextPipe(nextXpos, nextYpos);
			}
			break;
			
		case 15:	case 25:	case 35:	case 45:	case 55:	case 65:
			if(enterDirection == NORTH)
			{
				set_direction_connect(nextXpos, nextYpos, SOUTH);
				set_bFinish_connect(nextXpos, nextYpos, 0, true);
				set_bFinish_connect(nextXpos, nextYpos, 2, true);
				GameCon.connectCount++;
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				calConnect_NextPipe(nextXpos, nextYpos);
			}
			else if(enterDirection == SOUTH)
			{
				set_direction_connect(nextXpos, nextYpos, NORTH);
				set_bFinish_connect(nextXpos, nextYpos, 2, true);
				set_bFinish_connect(nextXpos, nextYpos, 0, true);
				GameCon.connectCount++;
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				calConnect_NextPipe(nextXpos, nextYpos);
				
			}
			break;
			
		case 16: case 46:
			if(enterDirection == NORTH)
			{
				//_nextGo.GetComponent<P16> ().calConnect (16);
				
				set_direction_connect(nextXpos, nextYpos, SOUTH);
				set_bFinish_connect(nextXpos, nextYpos, 0, true);
				set_bFinish_connect(nextXpos, nextYpos, 2, true);
				GameCon.connectCount++;
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				calConnect_NextPipe(nextXpos, nextYpos);
			}
			else if(enterDirection == EAST)
			{
				//_nextGo.GetComponent<P16> ().calConnect (14);
				set_direction_connect(nextXpos, nextYpos, WEST);
				set_bFinish_connect(nextXpos, nextYpos, 1, true);
				set_bFinish_connect(nextXpos, nextYpos, 3, true);
				GameCon.connectCount++;
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				calConnect_NextPipe(nextXpos, nextYpos);
			}
			else if(enterDirection == SOUTH)
			{
				//_nextGo.GetComponent<P16> ().calConnect (15);
				
				set_direction_connect(nextXpos, nextYpos, NORTH);
				set_bFinish_connect(nextXpos, nextYpos, 2, true);
				set_bFinish_connect(nextXpos, nextYpos, 0, true);
				GameCon.connectCount++;
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				calConnect_NextPipe(nextXpos, nextYpos);
			}
			else if(enterDirection == WEST)
			{
				//				_nextGo.GetComponent<P16> ().calConnect (13);
				
				set_direction_connect(nextXpos, nextYpos, EAST);
				set_bFinish_connect(nextXpos, nextYpos, 3, true);
				set_bFinish_connect(nextXpos, nextYpos, 1, true);
				GameCon.connectCount++;
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				calConnect_NextPipe(nextXpos, nextYpos);
			}
			break;
			
		case 70:	case 90:
			if(enterDirection == NORTH)
			{
				//_nextGo.GetComponent<P70> ().calConnect (41);
				
				set_bFinish_connect(nextXpos, nextYpos, 0, true);
				aliveStream++;
				connectCount++;
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				set_bFinish_connect(nextXpos, nextYpos, 1, true);
				set_direction_connect(nextXpos, nextYpos, 1);
				//set_direction_connect(nextXpos, nextYpos, outDirection[1]);
				calConnect_NextPipe(nextXpos, nextYpos);
				
				set_bFinish_connect(nextXpos, nextYpos, 3, true);
				set_direction_connect(nextXpos, nextYpos, 3);
				//set_direction_connect(nextXpos, nextYpos, outDirection[3]);
				calConnect_NextPipe(nextXpos, nextYpos);
			}
			break;
		case 71:	case 91:
			if(enterDirection == EAST)
			{
				//_nextGo.GetComponent<P71> ().calConnect (42);
				
				set_bFinish_connect(nextXpos, nextYpos, 1, true);
				aliveStream++;
				connectCount++;
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				set_bFinish_connect(nextXpos, nextYpos, 0, true);
				set_direction_connect(nextXpos, nextYpos, 0);
				
				//set_direction_connect(nextXpos, nextYpos, outDirection[3]);
				calConnect_NextPipe(nextXpos, nextYpos);
				
				set_bFinish_connect(nextXpos, nextYpos, 2, true);
				set_direction_connect(nextXpos, nextYpos, 2);
				//set_direction_connect(nextXpos, nextYpos, outDirection[3]);
				calConnect_NextPipe(nextXpos, nextYpos);
			}
			break;
		case 72:	case 92:
			if(enterDirection == SOUTH)
			{
				//_nextGo.GetComponent<P72> ().calConnect (43);
				
				set_bFinish_connect(nextXpos, nextYpos, 2, true);
				aliveStream++;
				connectCount++;
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				set_bFinish_connect(nextXpos, nextYpos, 1, true);
				set_direction_connect(nextXpos, nextYpos, 1);
				//set_direction_connect(nextXpos, nextYpos, outDirection[3]);
				calConnect_NextPipe(nextXpos, nextYpos);
				
				set_bFinish_connect(nextXpos, nextYpos, 3, true);
				set_direction_connect(nextXpos, nextYpos, 3);
				//set_direction_connect(nextXpos, nextYpos, outDirection[3]);
				calConnect_NextPipe(nextXpos, nextYpos);
			}
			break;
		case 73:	case 93:
			if(enterDirection == WEST)
			{
				//_nextGo.GetComponent<P73> ().calConnect (44);
				
				set_bFinish_connect(nextXpos, nextYpos, 3, true);
				aliveStream++;
				connectCount++;
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				set_bFinish_connect(nextXpos, nextYpos, 0, true);
				set_direction_connect(nextXpos, nextYpos, 0);
				//set_direction_connect(nextXpos, nextYpos, outDirection[3]);
				calConnect_NextPipe(nextXpos, nextYpos);
				
				set_bFinish_connect(nextXpos, nextYpos, 2, true);
				set_direction_connect(nextXpos, nextYpos, 2);
				//set_direction_connect(nextXpos, nextYpos, outDirection[3]);
				calConnect_NextPipe(nextXpos, nextYpos);
			}
			break;
			
		case 74:	case 94:
			if(enterDirection == EAST)
			{
				//_nextGo.GetComponent<P74> ().calConnect (45);
				set_bFinish_In_connect(nextXpos, nextYpos, 1, true);
				set_bFinish_connect(nextXpos, nextYpos, 1, true);
				aliveStream--;
				if(get_bFinish_In_connect(nextXpos, nextYpos, 1) && get_bFinish_In_connect(nextXpos, nextYpos, 3))
				{
					aliveStream++;
					connectCount++;
					tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
					set_direction_connect(nextXpos, nextYpos, 0);
					set_bFinish_connect(nextXpos, nextYpos, 0, true);
					calConnect_NextPipe(nextXpos, nextYpos);
				}
			}
			else if(enterDirection == WEST)
			{
				//_nextGo.GetComponent<P74> ().calConnect (46);
				set_bFinish_In_connect(nextXpos, nextYpos, 3, true);
				set_bFinish_connect(nextXpos, nextYpos, 3, true);
				aliveStream--;
				if(get_bFinish_In_connect(nextXpos, nextYpos, 1) && get_bFinish_In_connect(nextXpos, nextYpos, 3))
				{
					aliveStream++;
					connectCount++;
					tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
					set_direction_connect(nextXpos, nextYpos, 0);
					set_bFinish_connect(nextXpos, nextYpos, 0, true);
					calConnect_NextPipe(nextXpos, nextYpos);
				}
			}
			break;
			
		case 75:	case 95:
			if(enterDirection == NORTH)
			{
				//_nextGo.GetComponent<P75> ().calConnect (47);
				set_bFinish_In_connect(nextXpos, nextYpos, 0, true);
				set_bFinish_connect(nextXpos, nextYpos, 0, true);
				aliveStream--;
				if(get_bFinish_In_connect(nextXpos, nextYpos, 0) && get_bFinish_In_connect(nextXpos, nextYpos, 2))
				{
					aliveStream++;
					connectCount++;
					tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
					set_direction_connect(nextXpos, nextYpos, 1);
					set_bFinish_connect(nextXpos, nextYpos, 1, true);
					calConnect_NextPipe(nextXpos, nextYpos);
				}
			}
			else if(enterDirection == SOUTH)
			{
				//_nextGo.GetComponent<P75> ().calConnect (48);
				set_bFinish_In_connect(nextXpos, nextYpos, 2, true);
				set_bFinish_connect(nextXpos, nextYpos, 2, true);
				aliveStream--;
				if(get_bFinish_In_connect(nextXpos, nextYpos, 0) && get_bFinish_In_connect(nextXpos, nextYpos, 2))
				{
					aliveStream++;
					connectCount++;
					tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
					set_direction_connect(nextXpos, nextYpos, 1);
					set_bFinish_connect(nextXpos, nextYpos, 1, true);
					calConnect_NextPipe(nextXpos, nextYpos);
				}
			}
			break;
			
		case 76:	case 96:
			if(enterDirection == EAST)
			{
				//_nextGo.GetComponent<P76> ().calConnect (49);
				set_bFinish_In_connect(nextXpos, nextYpos, 1, true);
				set_bFinish_connect(nextXpos, nextYpos, 1, true);
				aliveStream--;
				if(get_bFinish_In_connect(nextXpos, nextYpos, 1) && get_bFinish_In_connect(nextXpos, nextYpos, 3))
				{
					aliveStream++;
					connectCount++;
					tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
					set_direction_connect(nextXpos, nextYpos, 2);
					set_bFinish_connect(nextXpos, nextYpos, 2, true);
					calConnect_NextPipe(nextXpos, nextYpos);
				}
			}
			else if(enterDirection == WEST)
			{
				//_nextGo.GetComponent<P76> ().calConnect (50);
				set_bFinish_In_connect(nextXpos, nextYpos, 3, true);
				set_bFinish_connect(nextXpos, nextYpos, 3, true);
				aliveStream--;
				if(get_bFinish_In_connect(nextXpos, nextYpos, 1) && get_bFinish_In_connect(nextXpos, nextYpos, 3))
				{
					aliveStream++;
					connectCount++;
					tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
					set_direction_connect(nextXpos, nextYpos, 2);
					set_bFinish_connect(nextXpos, nextYpos, 2, true);
					calConnect_NextPipe(nextXpos, nextYpos);
				}
			}
			break;
			
		case 77: case 97:
			if(enterDirection == NORTH)
			{
				//_nextGo.GetComponent<P77> ().calConnect (51);
				set_bFinish_In_connect(nextXpos, nextYpos, 0, true);
				set_bFinish_connect(nextXpos, nextYpos, 0, true);
				aliveStream--;
				if(get_bFinish_In_connect(nextXpos, nextYpos, 0) && get_bFinish_In_connect(nextXpos, nextYpos, 2))
				{
					aliveStream++;
					connectCount++;
					tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
					set_direction_connect(nextXpos, nextYpos, 3);
					set_bFinish_connect(nextXpos, nextYpos, 3, true);
					calConnect_NextPipe(nextXpos, nextYpos);
				}
			}
			else if(enterDirection == SOUTH)
			{
				//_nextGo.GetComponent<P77> ().calConnect (52);
				set_bFinish_In_connect(nextXpos, nextYpos, 2, true);
				set_bFinish_connect(nextXpos, nextYpos, 2, true);
				aliveStream--;
				if(get_bFinish_In_connect(nextXpos, nextYpos, 0) && get_bFinish_In_connect(nextXpos, nextYpos, 2))
				{
					aliveStream++;
					connectCount++;
					tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
					set_direction_connect(nextXpos, nextYpos, 3);
					set_bFinish_connect(nextXpos, nextYpos, 3, true);
					calConnect_NextPipe(nextXpos, nextYpos);
				}
			}
			break;
			
			
		case 80:	case 100:
			if(enterDirection == NORTH)
			{
				//_nextGo.GetComponent<P80> ().calConnect (25);
				set_bFinish_connect(nextXpos, nextYpos, 0, true);
				aliveStream+=2;
				connectCount++;
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				set_bFinish_connect(nextXpos, nextYpos, 1, true);
				set_direction_connect(nextXpos, nextYpos, 1);
				//set_direction_connect(nextXpos, nextYpos, outDirection[1]);
				calConnect_NextPipe(nextXpos, nextYpos);
				
				set_bFinish_connect(nextXpos, nextYpos, 2, true);
				set_direction_connect(nextXpos, nextYpos, 2);
				//set_direction_connect(nextXpos, nextYpos, outDirection[1]);
				calConnect_NextPipe(nextXpos, nextYpos);
				
				set_bFinish_connect(nextXpos, nextYpos, 3, true);
				set_direction_connect(nextXpos, nextYpos, 3);
				//set_direction_connect(nextXpos, nextYpos, outDirection[3]);
				calConnect_NextPipe(nextXpos, nextYpos);
			}
			break;
		case 81:	case 101:
			if(enterDirection == EAST)
			{
				//_nextGo.GetComponent<P81> ().calConnect (26);
				set_bFinish_connect(nextXpos, nextYpos, 1, true);
				aliveStream+=2;
				connectCount++;
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				set_bFinish_connect(nextXpos, nextYpos, 0, true);
				set_direction_connect(nextXpos, nextYpos, 0);
				//set_direction_connect(nextXpos, nextYpos, outDirection[1]);
				calConnect_NextPipe(nextXpos, nextYpos);
				
				set_bFinish_connect(nextXpos, nextYpos, 2, true);
				set_direction_connect(nextXpos, nextYpos, 2);
				//set_direction_connect(nextXpos, nextYpos, outDirection[1]);
				calConnect_NextPipe(nextXpos, nextYpos);
				
				set_bFinish_connect(nextXpos, nextYpos, 3, true);
				set_direction_connect(nextXpos, nextYpos, 3);
				//set_direction_connect(nextXpos, nextYpos, outDirection[3]);
				calConnect_NextPipe(nextXpos, nextYpos);
			}
			break;
		case 82:	case 102:
			if(enterDirection == SOUTH)
			{
				//_nextGo.GetComponent<P82> ().calConnect (27);
				set_bFinish_connect(nextXpos, nextYpos, 2, true);
				aliveStream+=2;
				connectCount++;
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				set_bFinish_connect(nextXpos, nextYpos, 0, true);
				set_direction_connect(nextXpos, nextYpos, 0);
				//set_direction_connect(nextXpos, nextYpos, outDirection[1]);
				calConnect_NextPipe(nextXpos, nextYpos);
				
				set_bFinish_connect(nextXpos, nextYpos, 1, true);
				set_direction_connect(nextXpos, nextYpos, 1);
				//set_direction_connect(nextXpos, nextYpos, outDirection[1]);
				calConnect_NextPipe(nextXpos, nextYpos);
				
				set_bFinish_connect(nextXpos, nextYpos, 3, true);
				set_direction_connect(nextXpos, nextYpos, 3);
				//set_direction_connect(nextXpos, nextYpos, outDirection[3]);
				calConnect_NextPipe(nextXpos, nextYpos);
			}
			break;
		case 83:	case 103:
			if(enterDirection == WEST)
			{
				//_nextGo.GetComponent<P83> ().calConnect (28);
				set_bFinish_connect(nextXpos, nextYpos, 3, true);
				aliveStream+=2;
				connectCount++;
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				set_bFinish_connect(nextXpos, nextYpos, 0, true);
				set_direction_connect(nextXpos, nextYpos, 0);
				//set_direction_connect(nextXpos, nextYpos, outDirection[1]);
				calConnect_NextPipe(nextXpos, nextYpos);
				
				set_bFinish_connect(nextXpos, nextYpos, 1, true);
				set_direction_connect(nextXpos, nextYpos, 1);
				//set_direction_connect(nextXpos, nextYpos, outDirection[1]);
				calConnect_NextPipe(nextXpos, nextYpos);
				
				set_bFinish_connect(nextXpos, nextYpos, 2, true);
				set_direction_connect(nextXpos, nextYpos, 2);
				//set_direction_connect(nextXpos, nextYpos, outDirection[3]);
				calConnect_NextPipe(nextXpos, nextYpos);
			}
			break;
			
		case 84:	case 104:
			if(enterDirection == EAST)
			{
				//_nextGo.GetComponent<P84> ().calConnect (29);
				set_bFinish_In_connect(nextXpos, nextYpos, 1, true);
				set_bFinish_connect(nextXpos, nextYpos, 1, true);
				aliveStream--;
				if(get_bFinish_In_connect(nextXpos, nextYpos, 1) && get_bFinish_In_connect(nextXpos, nextYpos, 2) && get_bFinish_In_connect(nextXpos, nextYpos, 3))
				{
					aliveStream++;
					connectCount++;
					tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
					set_direction_connect(nextXpos, nextYpos, 0);
					set_bFinish_connect(nextXpos, nextYpos, 0, true);
					calConnect_NextPipe(nextXpos, nextYpos);
				}
			}
			else if(enterDirection == SOUTH)
			{
				//_nextGo.GetComponent<P84> ().calConnect (30);
				set_bFinish_In_connect(nextXpos, nextYpos, 2, true);
				set_bFinish_connect(nextXpos, nextYpos, 2, true);
				aliveStream--;
				if(get_bFinish_In_connect(nextXpos, nextYpos, 1) && get_bFinish_In_connect(nextXpos, nextYpos, 2) && get_bFinish_In_connect(nextXpos, nextYpos, 3))
				{
					aliveStream++;
					connectCount++;
					tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
					set_direction_connect(nextXpos, nextYpos, 0);
					set_bFinish_connect(nextXpos, nextYpos, 0, true);
					calConnect_NextPipe(nextXpos, nextYpos);
				}
			}
			else if(enterDirection == WEST)
			{
				//_nextGo.GetComponent<P84> ().calConnect (31);
				set_bFinish_In_connect(nextXpos, nextYpos, 3, true);
				set_bFinish_connect(nextXpos, nextYpos, 3, true);
				aliveStream--;
				if(get_bFinish_In_connect(nextXpos, nextYpos, 1) && get_bFinish_In_connect(nextXpos, nextYpos, 2) && get_bFinish_In_connect(nextXpos, nextYpos, 3))
				{
					aliveStream++;
					connectCount++;
					tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
					set_direction_connect(nextXpos, nextYpos, 0);
					set_bFinish_connect(nextXpos, nextYpos, 0, true);
					calConnect_NextPipe(nextXpos, nextYpos);
				}
			}
			break;
			
		case 85:	case 105:
			if(enterDirection == NORTH)
			{
				//_nextGo.GetComponent<P85> ().calConnect (32);
				set_bFinish_In_connect(nextXpos, nextYpos, 0, true);
				set_bFinish_connect(nextXpos, nextYpos, 0, true);
				aliveStream--;
				if(get_bFinish_In_connect(nextXpos, nextYpos, 0) && get_bFinish_In_connect(nextXpos, nextYpos, 2) && get_bFinish_In_connect(nextXpos, nextYpos, 3))
				{
					aliveStream++;
					connectCount++;
					tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
					set_direction_connect(nextXpos, nextYpos, 1);
					set_bFinish_connect(nextXpos, nextYpos, 1, true);
					calConnect_NextPipe(nextXpos, nextYpos);
				}
			}
			else if(enterDirection == SOUTH)
			{
				//_nextGo.GetComponent<P85> ().calConnect (33);
				set_bFinish_In_connect(nextXpos, nextYpos, 2, true);
				set_bFinish_connect(nextXpos, nextYpos, 2, true);
				aliveStream--;
				if(get_bFinish_In_connect(nextXpos, nextYpos, 0) && get_bFinish_In_connect(nextXpos, nextYpos, 2) && get_bFinish_In_connect(nextXpos, nextYpos, 3))
				{
					aliveStream++;
					connectCount++;
					tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
					set_direction_connect(nextXpos, nextYpos, 1);
					set_bFinish_connect(nextXpos, nextYpos, 1, true);
					calConnect_NextPipe(nextXpos, nextYpos);
				}
			}
			else if(enterDirection == WEST)
			{
				//_nextGo.GetComponent<P85> ().calConnect (34);
				set_bFinish_In_connect(nextXpos, nextYpos, 3, true);
				set_bFinish_connect(nextXpos, nextYpos, 3, true);
				aliveStream--;
				if(get_bFinish_In_connect(nextXpos, nextYpos, 0) && get_bFinish_In_connect(nextXpos, nextYpos, 2) && get_bFinish_In_connect(nextXpos, nextYpos, 3))
				{
					aliveStream++;
					connectCount++;
					tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
					set_direction_connect(nextXpos, nextYpos, 1);
					set_bFinish_connect(nextXpos, nextYpos, 1, true);
					calConnect_NextPipe(nextXpos, nextYpos);
				}
			}
			break;
			
		case 86:	case 106:
			if(enterDirection == NORTH)
			{
				//_nextGo.GetComponent<P86> ().calConnect (35);
				set_bFinish_In_connect(nextXpos, nextYpos, 0, true);
				set_bFinish_connect(nextXpos, nextYpos, 0, true);
				aliveStream--;
				if(get_bFinish_In_connect(nextXpos, nextYpos, 0) && get_bFinish_In_connect(nextXpos, nextYpos, 1) && get_bFinish_In_connect(nextXpos, nextYpos, 3))
				{
					aliveStream++;
					connectCount++;
					tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
					set_direction_connect(nextXpos, nextYpos, 2);
					set_bFinish_connect(nextXpos, nextYpos, 2, true);
					calConnect_NextPipe(nextXpos, nextYpos);
				}
			}
			else if(enterDirection == EAST)
			{
				//_nextGo.GetComponent<P86> ().calConnect (36);
				set_bFinish_In_connect(nextXpos, nextYpos, 1, true);
				set_bFinish_connect(nextXpos, nextYpos, 1, true);
				aliveStream--;
				if(get_bFinish_In_connect(nextXpos, nextYpos, 0) && get_bFinish_In_connect(nextXpos, nextYpos, 1) && get_bFinish_In_connect(nextXpos, nextYpos, 3))
				{
					aliveStream++;
					connectCount++;
					tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
					set_direction_connect(nextXpos, nextYpos, 2);
					set_bFinish_connect(nextXpos, nextYpos, 2, true);
					calConnect_NextPipe(nextXpos, nextYpos);
				}
			}
			else if(enterDirection == WEST)
			{
				//_nextGo.GetComponent<P86> ().calConnect (37);
				set_bFinish_In_connect(nextXpos, nextYpos, 3, true);
				set_bFinish_connect(nextXpos, nextYpos, 3, true);
				aliveStream--;
				if(get_bFinish_In_connect(nextXpos, nextYpos, 0) && get_bFinish_In_connect(nextXpos, nextYpos, 1) && get_bFinish_In_connect(nextXpos, nextYpos, 3))
				{
					aliveStream++;
					connectCount++;
					tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
					set_direction_connect(nextXpos, nextYpos, 2);
					set_bFinish_connect(nextXpos, nextYpos, 2, true);
					calConnect_NextPipe(nextXpos, nextYpos);
				}
			}
			break;
			
		case 87:	case 107:
			if(enterDirection == NORTH)
			{
				//_nextGo.GetComponent<P87> ().calConnect (38);
				set_bFinish_In_connect(nextXpos, nextYpos, 0, true);
				set_bFinish_connect(nextXpos, nextYpos, 0, true);
				aliveStream--;
				if(get_bFinish_In_connect(nextXpos, nextYpos, 0) && get_bFinish_In_connect(nextXpos, nextYpos, 1) && get_bFinish_In_connect(nextXpos, nextYpos, 2))
				{
					aliveStream++;
					connectCount++;
					tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
					set_direction_connect(nextXpos, nextYpos, 3);
					set_bFinish_connect(nextXpos, nextYpos, 3, true);
					calConnect_NextPipe(nextXpos, nextYpos);
				}
			}
			else if(enterDirection == EAST)
			{
				//_nextGo.GetComponent<P87> ().calConnect (39);
				set_bFinish_In_connect(nextXpos, nextYpos, 1, true);
				set_bFinish_connect(nextXpos, nextYpos, 1, true);
				aliveStream--;
				if(get_bFinish_In_connect(nextXpos, nextYpos, 0) && get_bFinish_In_connect(nextXpos, nextYpos, 1) && get_bFinish_In_connect(nextXpos, nextYpos, 2))
				{
					aliveStream++;
					connectCount++;
					tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
					set_direction_connect(nextXpos, nextYpos, 3);
					set_bFinish_connect(nextXpos, nextYpos, 3, true);
					calConnect_NextPipe(nextXpos, nextYpos);
				}
			}
			else if(enterDirection == SOUTH)
			{
				//_nextGo.GetComponent<P87> ().calConnect (40);
				set_bFinish_In_connect(nextXpos, nextYpos, 2, true);
				set_bFinish_connect(nextXpos, nextYpos, 2, true);
				aliveStream--;
				if(get_bFinish_In_connect(nextXpos, nextYpos, 0) && get_bFinish_In_connect(nextXpos, nextYpos, 1) && get_bFinish_In_connect(nextXpos, nextYpos, 2))
				{
					aliveStream++;
					connectCount++;
					tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
					set_direction_connect(nextXpos, nextYpos, 3);
					set_bFinish_connect(nextXpos, nextYpos, 3, true);
					calConnect_NextPipe(nextXpos, nextYpos);
				}
			}
			break;
			
			
		case 120:	//end
			//_nextGo.GetComponent<PEnd> ().calConnect (21);
			set_bFinish_connect(nextXpos, nextYpos, 2, true);
			aliveStream--;
			break;
			
		case 121:	//end
			//_nextGo.GetComponent<PEnd> ().calConnect (22);
			set_bFinish_connect(nextXpos, nextYpos, 0, true);
			aliveStream--;
			break;
			
		case 122:	//end
			//_nextGo.GetComponent<PEnd> ().calConnect (23);
			set_bFinish_connect(nextXpos, nextYpos, 3, true);
			aliveStream--;
			break;
			
		case 123:	//end
			//_nextGo.GetComponent<PEnd> ().calConnect (24);
			set_bFinish_connect(nextXpos, nextYpos, 1, true);
			aliveStream--;
			break;
			
		case 130:
		case 140:
		case 150:
			//_nextGo.GetComponent<PIN> ().calConnect (21);
			set_bFinish_connect(nextXpos, nextYpos, 2, true);
			tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
			calConnect_NextPipe_OUT(nextXpos, nextYpos);
			break;
			
		case 131:
		case 141:
		case 151:
			//_nextGo.GetComponent<PIN> ().calConnect (22);
			set_bFinish_connect(nextXpos, nextYpos, 0, true);
			tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
			calConnect_NextPipe_OUT(nextXpos, nextYpos);
			break;
			
		case 132:
		case 142:
		case 152:
			//_nextGo.GetComponent<PIN> ().calConnect (23);
			set_bFinish_connect(nextXpos, nextYpos, 3, true);
			tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
			calConnect_NextPipe_OUT(nextXpos, nextYpos);
			break;
			
		case 133:
		case 143:
		case 153:
			//_nextGo.GetComponent<PIN> ().calConnect (24);
			set_bFinish_connect(nextXpos, nextYpos, 1, true);
			tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
			calConnect_NextPipe_OUT(nextXpos, nextYpos);
			break;
			
		case 134:
		case 144:
		case 154:
			//_nextGo.GetComponent<PStart> ().calConnect (17);
			
			set_direction_connect(nextXpos, nextYpos, SOUTH);
			set_bFinish_connect(nextXpos, nextYpos, 0, true);
			set_bFinish_connect(nextXpos, nextYpos, 2, true);
			tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
			calConnect_NextPipe(nextXpos, nextYpos);
			break;
		case 135:
		case 145:
		case 155:
			//_nextGo.GetComponent<PStart> ().calConnect (18);
			set_direction_connect(nextXpos, nextYpos, NORTH);
			set_bFinish_connect(nextXpos, nextYpos, 0, true);
			set_bFinish_connect(nextXpos, nextYpos, 2, true);
			tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
			calConnect_NextPipe(nextXpos, nextYpos);
			break;
		case 136:
		case 146:
		case 156:
			//_nextGo.GetComponent<PStart> ().calConnect (19);
			set_direction_connect(nextXpos, nextYpos, WEST);
			set_bFinish_connect(nextXpos, nextYpos, 1, true);
			set_bFinish_connect(nextXpos, nextYpos, 3, true);
			tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
			calConnect_NextPipe(nextXpos, nextYpos);
			break;
		case 137:
		case 147:
		case 157:
			//_nextGo.GetComponent<PStart> ().calConnect (20);
			set_direction_connect(nextXpos, nextYpos, EAST);
			set_bFinish_connect(nextXpos, nextYpos, 1, true);
			set_bFinish_connect(nextXpos, nextYpos, 3, true);
			tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
			calConnect_NextPipe(nextXpos, nextYpos);
			break;
			
		case 170:
			if(enterDirection == WEST)
			{
				//_nextGo.GetComponent<P170> ().calConnect (53);
				
				set_direction_connect(nextXpos, nextYpos, EAST);
				set_bFinish_connect(nextXpos, nextYpos, 1, true);
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				calConnect_NextPipe(nextXpos, nextYpos);
				
			}
			else if(enterDirection == EAST)
			{
				//_nextGo.GetComponent<P170> ().calConnect (54);
				set_direction_connect(nextXpos, nextYpos, WEST);
				set_bFinish_connect(nextXpos, nextYpos, 1, true);
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				calConnect_NextPipe_Wall(nextXpos, nextYpos);
			}
			break;
			
		case 171:
			if(enterDirection == SOUTH)
			{
				//_nextGo.GetComponent<P171> ().calConnect (57);
				set_direction_connect(nextXpos, nextYpos, NORTH);
				set_bFinish_connect(nextXpos, nextYpos, 2, true);
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				calConnect_NextPipe_Wall(nextXpos, nextYpos);
			}
			else if(enterDirection == NORTH)
			{
				//_nextGo.GetComponent<P171> ().calConnect (58);
				set_direction_connect(nextXpos, nextYpos, SOUTH);
				set_bFinish_connect(nextXpos, nextYpos, 0, true);
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				calConnect_NextPipe(nextXpos, nextYpos);
			}
			break;
			
		case 172:
			if(enterDirection == SOUTH)
			{
				//_nextGo.GetComponent<P172> ().calConnect (59);
				
				set_direction_connect(nextXpos, nextYpos, NORTH);
				set_bFinish_connect(nextXpos, nextYpos, 2, true);
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				calConnect_NextPipe(nextXpos, nextYpos);
				
			}
			else if(enterDirection == NORTH)
			{
				//_nextGo.GetComponent<P172> ().calConnect (60);
				set_direction_connect(nextXpos, nextYpos, SOUTH);
				set_bFinish_connect(nextXpos, nextYpos, 0, true);
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				calConnect_NextPipe_Wall(nextXpos, nextYpos);
			}
			break;
			
		case 173:
			if(enterDirection == WEST)
			{
				//_nextGo.GetComponent<P173> ().calConnect (55);
				set_direction_connect(nextXpos, nextYpos, EAST);
				set_bFinish_connect(nextXpos, nextYpos, 3, true);
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				calConnect_NextPipe_Wall(nextXpos, nextYpos);
			}
			else if(enterDirection == EAST)
			{
				//_nextGo.GetComponent<P173> ().calConnect (56);
				set_direction_connect(nextXpos, nextYpos, WEST);
				set_bFinish_connect(nextXpos, nextYpos, 1, true);
				tmpUserScore +=  cal_tmpUserScore(tmpPipeID);
				calConnect_NextPipe(nextXpos, nextYpos);
			}
			break;
		}
	}
	
	
	
	
	
	
	
	public void StartNextPipe(int _xpos, int _ypos)
	{
		int next_idx = getNextObjIndex(_xpos, _ypos);
		//GameObject nextGo = GameObject.Find ("Pipe"+getNextObjIndex(_pos));
		
		int nextXpos = sPipe[next_idx].xpos;
		int nextYpos = sPipe[next_idx].ypos;
		//POS nextPos = nextGo.GetComponent<PM> ().pos;
		int enterDirection = ( get_direction( _xpos, _ypos) + 2) % 4;
		
		if (! get_bFinish( nextXpos, nextYpos, enterDirection) ) {
			//Debug.Log("Fail.....2");
			setAniStartNextPipe (next_idx, enterDirection);
		} 
		else
		{
			createFailMark(gPipe[next_idx].transform.localPosition, enterDirection);
			
			TurnOnWaterSetup();
			bFail = true;
		}
	}
	
	public void setAniStartNextPipe(int _next_idx, int enterDirection)
	{
		
		//		POS nextPos = _nextGo.GetComponent<PM> ().pos;
		
		switch( sPipe[_next_idx].ID )
		{
		case 10: case 20: case 30:
		case 40: case 50: case 60:
			if(enterDirection == WEST)
			{
				sPipe[_next_idx].setType (1);//_nextGo.GetComponent<P10> ().setType (1);
			}
			else if(enterDirection == SOUTH)
			{
				sPipe[_next_idx].setType (2);
			}
			break;
			
		case 11: case 21: case 31:
		case 41: case 51: case 61:
			if(enterDirection == WEST)
			{
				sPipe[_next_idx].setType (4);
			}
			else if(enterDirection == NORTH)
			{
				sPipe[_next_idx].setType (3);
			}
			break;
			
		case 12: case 22: case 32:
		case 42: case 52: case 62:
			if(enterDirection == EAST)
			{
				sPipe[_next_idx].setType (5);
			}
			else if(enterDirection == NORTH)
			{
				sPipe[_next_idx].setType (6);
			}
			break;
			
		case 13: case 23: case 33:
		case 43: case 53: case 63:
			if(enterDirection == EAST)
			{
				sPipe[_next_idx].setType (8);
			}
			else if(enterDirection == SOUTH)
			{
				sPipe[_next_idx].setType (7);
			}
			break;
			
		case 14: case 24: case 34: 
		case 44: case 54: case 64:
			if(enterDirection == EAST)
			{
				sPipe[_next_idx].setType (10);
			}
			else if(enterDirection == WEST)
			{
				sPipe[_next_idx].setType (9);
			}
			break;
			
		case 15: case 25: case 35: 
		case 45: case 55: case 65: 
			if(enterDirection == NORTH)
			{
				sPipe[_next_idx].setType (12);
			}
			else if(enterDirection == SOUTH)
			{
				sPipe[_next_idx].setType (11);
			}
			break;
			
		case 16:
		case 46:
			//					Debug.Log ("_________nextGo---:"+_nextGo.GetComponent<PM>().posInfo.pipeID);
			//					Debug.Log ("------------------\n");
			if(enterDirection == NORTH)
			{
				sPipe[_next_idx].setType (16);
			}
			else if(enterDirection == EAST)
			{
				sPipe[_next_idx].setType (14);
			}
			else if(enterDirection == SOUTH)
			{
				sPipe[_next_idx].setType (15);
			}
			else if(enterDirection == WEST)
			{
				sPipe[_next_idx].setType (13);
			}
			break;
			
		case 70:
		case 90:
			if(enterDirection == NORTH)
			{
				sPipe[_next_idx].setType (41);
			}
			break;
		case 71:
		case 91:
			if(enterDirection == EAST)
			{
				sPipe[_next_idx].setType (42);
			}
			break;
		case 72:
		case 92:
			if(enterDirection == SOUTH)
			{
				sPipe[_next_idx].setType (43);
			}
			break;
		case 73:
		case 93:
			if(enterDirection == WEST)
			{
				sPipe[_next_idx].setType (44);
			}
			break;
			
		case 74:
		case 94:
			if(enterDirection == EAST)
			{
				sPipe[_next_idx].setType (45);
			}
			else if(enterDirection == WEST)
			{
				sPipe[_next_idx].setType (46);
			}
			break;
			
		case 75:
		case 95:
			if(enterDirection == NORTH)
			{
				sPipe[_next_idx].setType (47);
			}
			else if(enterDirection == SOUTH)
			{
				sPipe[_next_idx].setType (48);
			}
			break;
			
		case 76:
		case 96:
			if(enterDirection == EAST)
			{
				sPipe[_next_idx].setType (49);
			}
			else if(enterDirection == WEST)
			{
				sPipe[_next_idx].setType (50);
			}
			break;
			
		case 77:
		case 97:
			if(enterDirection == NORTH)
			{
				sPipe[_next_idx].setType (51);
			}
			else if(enterDirection == SOUTH)
			{
				sPipe[_next_idx].setType (52);
			}
			break;
			
			
		case 80:
		case 100:
			if(enterDirection == NORTH)
			{
				sPipe[_next_idx].setType (25);
			}
			break;
		case 81:
		case 101:
			if(enterDirection == EAST)
			{
				sPipe[_next_idx].setType (26);
			}
			break;
		case 82:
		case 102:
			if(enterDirection == SOUTH)
			{
				sPipe[_next_idx].setType (27);
			}
			break;
		case 83:
		case 103:
			if(enterDirection == WEST)
			{
				sPipe[_next_idx].setType (28);
			}
			break;
			
		case 84:
		case 104:
			if(enterDirection == EAST)
			{
				sPipe[_next_idx].setType (29);
			}
			else if(enterDirection == SOUTH)
			{
				sPipe[_next_idx].setType (30);
			}
			else if(enterDirection == WEST)
			{
				sPipe[_next_idx].setType (31);
			}
			break;
			
		case 85:
		case 105:
			if(enterDirection == NORTH)
			{
				sPipe[_next_idx].setType (32);
			}
			else if(enterDirection == SOUTH)
			{
				sPipe[_next_idx].setType (33);
			}
			else if(enterDirection == WEST)
			{
				sPipe[_next_idx].setType (34);
			}
			break;
			
		case 86:
		case 106:
			if(enterDirection == NORTH)
			{
				sPipe[_next_idx].setType (35);
			}
			else if(enterDirection == EAST)
			{
				sPipe[_next_idx].setType (36);
			}
			else if(enterDirection == WEST)
			{
				sPipe[_next_idx].setType (37);
			}
			break;
			
		case 87:
		case 107:
			if(enterDirection == NORTH)
			{
				sPipe[_next_idx].setType (38);
			}
			else if(enterDirection == EAST)
			{
				sPipe[_next_idx].setType (39);
			}
			else if(enterDirection == SOUTH)
			{
				sPipe[_next_idx].setType (40);
			}
			break;
			
			
		case 120:	//end
			sPipe[_next_idx].setType (21);
			break;
			
		case 121:	//end
			sPipe[_next_idx].setType (22);
			break;
			
		case 122:	//end
			sPipe[_next_idx].setType (23);
			break;
			
		case 123:	//end
			sPipe[_next_idx].setType (24);
			break;
			
		case 130:
		case 140:
		case 150:
			sPipe[_next_idx].setType (21);
			break;
			
		case 131:
		case 141:
		case 151:
			sPipe[_next_idx].setType (22);
			break;
			
		case 132:
		case 142:
		case 152:
			sPipe[_next_idx].setType (23);
			break;
			
		case 133:
		case 143:
		case 153:
			sPipe[_next_idx].setType (24);
			break;
			
		case 134:
		case 144:
		case 154:
			sPipe[_next_idx].setType (17);
			break;
		case 135:
		case 145:
		case 155:
			sPipe[_next_idx].setType (18);
			break;
		case 136:
		case 146:
		case 156:
			sPipe[_next_idx].setType (19);
			break;
		case 137:
		case 147:
		case 157:
			sPipe[_next_idx].setType (20);
			break;
			
		case 170:
			if(enterDirection == WEST)
			{
				sPipe[_next_idx].setType (53);
			}
			else if(enterDirection == EAST)
			{
				sPipe[_next_idx].setType (54);
			}
			break;
			
		case 171:
			if(enterDirection == SOUTH)
			{
				sPipe[_next_idx].setType (57);
			}
			else if(enterDirection == NORTH)
			{
				sPipe[_next_idx].setType (58);
			}
			break;
			
		case 172:
			if(enterDirection == SOUTH)
			{
				sPipe[_next_idx].setType (59);
			}
			else if(enterDirection == NORTH)
			{
				sPipe[_next_idx].setType (60);
			}
			break;
			
		case 173:
			if(enterDirection == WEST)
			{
				sPipe[_next_idx].setType (55);
			}
			else if(enterDirection == EAST)
			{
				sPipe[_next_idx].setType (56);
			}
			break;
		}
	}
	
	private int getNextObjIndex(int _xpos, int _ypos)
	{
		int returnValue = 0;
		switch (get_direction(_xpos, _ypos)) 
		{
		case 0:
			if(_ypos - 1 >= 0)
			{
				returnValue = objIdxBuf[_ypos-1][_xpos];
			}
			
			break;
			
		case 1:
			if(_xpos + 1 <= nCol -1)
			{
				returnValue = objIdxBuf[_ypos][_xpos+1];
			}
			break;
			
		case 2:
			if(_ypos + 1 <= nRow -1)
			{
				returnValue = objIdxBuf[_ypos+1][_xpos];
			}
			break;
			
		case 3:
			if(_xpos - 1 >= 0)
			{
				returnValue = objIdxBuf[_ypos][_xpos-1];
			}
			break;
		}
		return returnValue;
	}
	
	private int getNextConnectObjIndex(int _xpos, int _ypos)
	{
		int returnValue = 0;
		switch (get_direction_connect(_xpos, _ypos)) 
		{
		case 0:
			if(_ypos - 1 >= 0)
			{
				returnValue = objIdxBuf[_ypos-1][_xpos];
			}
			
			break;
			
		case 1:
			if(_xpos + 1 <= nCol -1)
			{
				returnValue = objIdxBuf[_ypos][_xpos+1];
			}
			break;
			
		case 2:
			if(_ypos + 1 <= nRow -1)
			{
				returnValue = objIdxBuf[_ypos+1][_xpos];
			}
			break;
			
		case 3:
			if(_xpos - 1 >= 0)
			{
				returnValue = objIdxBuf[_ypos][_xpos-1];
			}
			break;
		}
		return returnValue;
	}
	
	//	private int getNextIdx(POSINFO _posInfo)
	//	{
	//		bool returnValue = false;
	//		int nextIdx = mapBuf[_posInfo.ypos-1][_posInfo.xpos];
	//		switch(nextIdx)
	//		{
	//		case 10:		case 13:		case 15:		case 16:
	//		case 23:		case 25:
	//		case 30:
	//		case 40:		case 43:		case 45:		case 46:
	//		case 53:		case 55:
	//		case 60:
	//		case 120:
	//		case 130:
	//		case 140:
	//		case 150:
	//		case 72:		case 75:		case 77:
	//		case 82:		case 84:		case 85:		case 87:
	//		case 92:		case 95:		case 97:
	//		case 102:		case 104:		case 105:		case 107:
	//		case 171:
	//			returnValue = true;
	//			break;
	//
	//		}
	//		return returnValue;
	//	}
	//
	//	private void isPossibleGotoEast(POSINFO _posInfo)
	//	{
	//		bool returnValue = false;
	//		int nextIdx = mapBuf[_posInfo.ypos][_posInfo.xpos+1];
	//		switch(nextIdx)
	//		{
	//		case 10:		case 11:		case 14:		case 16:
	//		case 20:
	//		case 31:		case 34:
	//		case 40:		case 41:		case 44:		case 46:
	//		case 50:		
	//		case 61:		case 664:
	//		case 122:
	//		case 132:
	//		case 142:
	//		case 152:
	//		case 73:		case 74:		case 76:
	//		case 83:		case 84:		case 85:		case 86:
	//		case 93:		case 94:		case 96:
	//		case 103:		case 104:		case 105:		case 106:
	//		case 173:
	//			returnValue = true;
	//			break;
	//			
	//		}
	//		return returnValue;
	//	}
	//
	//	private void isPossibleGotoSouth(POSINFO _posInfo)
	//	{
	//		bool returnValue = false;
	//		int nextIdx = mapBuf[_posInfo.ypos+1][_posInfo.xpos];
	//		switch(nextIdx)
	//		{
	//		case 11:		case 12:		case 15:		case 16:
	//		case 21:
	//		case 32:		case 35:
	//		case 41:		case 42:		case 45:		case 46:
	//		case 51:
	//		case 62:		case 65:
	//		case 121:
	//		case 131:
	//		case 141:
	//		case 151:
	//		case 70:		case 75:		case 77:
	//		case 80:		case 85:		case 86:		case 87:
	//		case 90:		case 95:		case 97:
	//		case 100:		case 105:		case 106:		case 107:
	//		case 172:
	//		
	//			returnValue = true;
	//			break;
	//			
	//		}
	//		return returnValue;
	//	}
	//
	//	private void isPossibleGotoWest(POSINFO _posInfo)
	//	{
	//		bool returnValue = false;
	//		int nextIdx = mapBuf[_posInfo.ypos][_posInfo.xpos-1];
	//		switch(nextIdx)
	//		{
	//		case 12:		case 13:		case 14:		case 16:
	//		case 22:		case 24:
	//		case 33:
	//		case 42:		case 43:		case 44:		case 46:
	//		case 52:		case 54:
	//		case 63:
	//		case 123:
	//		case 133:
	//		case 143:
	//		case 153:
	//		case 71:		case 74:		case 76:
	//		case 81:		case 84:		case 86:		case 87:
	//		case 91:		case 94:		case 96:
	//		case 101:		case 104:		case 106:		case 107:
	//		case 170:
	//			returnValue = true;
	//			break;
	//			
	//		}
	//		return returnValue;
	//	}
	
	//	private void isPossibleGotoNorth(POSINFO _posInfo)
	//	{
	//		bool returnValue = false;
	//		int nextIdx = mapBuf[_posInfo.ypos-1][_posInfo.xpos];
	//		switch(nextIdx)
	//		{
	//		case 10:		case 13:		case 15:		case 16:
	//		case 23:		case 25:
	//		case 30:
	//		case 40:		case 43:		case 45:		case 46:
	//		case 53:		case 55:
	//		case 60:
	//		case 120:
	//		case 130:
	//		case 140:
	//		case 150:
	//		case 72:		case 75:		case 77:
	//		case 82:		case 84:		case 85:		case 87:
	//		case 92:		case 95:		case 97:
	//		case 102:		case 104:		case 105:		case 107:
	//		case 171:
	//			returnValue = true;
	//			break;
	//			
	//		}
	//		return returnValue;
	//	}
	//	
	//	private void isPossibleGotoEast(POSINFO _posInfo)
	//	{
	//		bool returnValue = false;
	//		int nextIdx = mapBuf[_posInfo.ypos][_posInfo.xpos+1];
	//		switch(nextIdx)
	//		{
	//		case 10:		case 11:		case 14:		case 16:
	//		case 20:
	//		case 31:		case 34:
	//		case 40:		case 41:		case 44:		case 46:
	//		case 50:		
	//		case 61:		case 664:
	//		case 122:
	//		case 132:
	//		case 142:
	//		case 152:
	//		case 73:		case 74:		case 76:
	//		case 83:		case 84:		case 85:		case 86:
	//		case 93:		case 94:		case 96:
	//		case 103:		case 104:		case 105:		case 106:
	//		case 173:
	//			returnValue = true;
	//			break;
	//			
	//		}
	//		return returnValue;
	//	}
	//	
	//	private void isPossibleGotoSouth(POSINFO _posInfo)
	//	{
	//		bool returnValue = false;
	//		int nextIdx = mapBuf[_posInfo.ypos+1][_posInfo.xpos];
	//		switch(nextIdx)
	//		{
	//		case 11:		case 12:		case 15:		case 16:
	//		case 21:
	//		case 32:		case 35:
	//		case 41:		case 42:		case 45:		case 46:
	//		case 51:
	//		case 62:		case 65:
	//		case 121:
	//		case 131:
	//		case 141:
	//		case 151:
	//		case 70:		case 75:		case 77:
	//		case 80:		case 85:		case 86:		case 87:
	//		case 90:		case 95:		case 97:
	//		case 100:		case 105:		case 106:		case 107:
	//		case 172:
	//			
	//			returnValue = true;
	//			break;
	//			
	//		}
	//		return returnValue;
	//	}
	//	
	//	private void isPossibleGotoWest(POSINFO _posInfo)
	//	{
	//		bool returnValue = false;
	//		int nextIdx = mapBuf[_posInfo.ypos][_posInfo.xpos-1];
	//		switch(nextIdx)
	//		{
	//		case 12:		case 13:		case 14:		case 16:
	//		case 22:		case 24:
	//		case 33:
	//		case 42:		case 43:		case 44:		case 46:
	//		case 52:		case 54:
	//		case 63:
	//		case 123:
	//		case 133:
	//		case 143:
	//		case 153:
	//		case 71:		case 74:		case 76:
	//		case 81:		case 84:		case 86:		case 87:
	//		case 91:		case 94:		case 96:
	//		case 101:		case 104:		case 106:		case 107:
	//		case 170:
	//			returnValue = true;
	//			break;
	//			
	//		}
	//		return returnValue;
	//	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	public void OnClick_InventoryButton()
	{
		efm.Play(8);
		GameObject conveyorBase = GameObject.Find("ConveyorObj");
		if (conveyorBase)
		{
			Vector3 pos = new Vector3(conveyorBase.transform.localPosition.x, 800, 0);
			TweenPosition twPosition = TweenPosition.Begin( conveyorBase, 0.5f, pos );
			twPosition.method = UITweener.Method.Linear;
		}
		
		GameObject InventoryBase = GameObject.Find("InventoryObj");
		if (InventoryBase)
		{
			Vector3 pos = new Vector3(InventoryBase.transform.localPosition.x, 800, 0);
			TweenPosition twPosition = TweenPosition.Begin( InventoryBase, 0.5f, pos );
			twPosition.method = UITweener.Method.Linear;
		}
		useWherePipe = 1;
	}
	
	public void OnClick_ConveyorButton()
	{
		efm.Play(8);
		GameObject conveyorBase = GameObject.Find("ConveyorObj");
		if (conveyorBase)
		{
			Vector3 pos = new Vector3(conveyorBase.transform.localPosition.x, 0, 0);
			TweenPosition twPosition = TweenPosition.Begin( conveyorBase, 0.5f, pos );
			twPosition.method = UITweener.Method.Linear;
		}
		
		GameObject InventoryBase = GameObject.Find("InventoryObj");
		if (InventoryBase)
		{
			Vector3 pos = new Vector3(InventoryBase.transform.localPosition.x, 0, 0);
			TweenPosition twPosition = TweenPosition.Begin( InventoryBase, 0.5f, pos );
			twPosition.method = UITweener.Method.Linear;
		}
		useWherePipe = 0;
	}

	public void active_ConveyorItem()
	{
		InventoryItemButtonObj.SetActive(false);
		ConveyorItemButtonObj.SetActive(true);

		setInvenCursorInitialize();
		GameObject.Find("Base_FireConveyorBelt").GetComponent<UISprite>().spriteName = "InvenCursor";
		useWherePipe = 0;
	}


	public void active_InventoryItem()
	{
		ConveyorItemButtonObj.SetActive(false);
		InventoryItemButtonObj.SetActive(true);
		GameObject.Find("Base_FireConveyorBelt").GetComponent<UISprite>().spriteName = "Shop015";
		useWherePipe = 1;
	}


	
	public void OnClick_InvenItem0()
	{
		active_InventoryItem();
		//p10
		if (!bAni_InventoryItem) {
			setInvenCursorInitialize();
			invenCursor = 0;
			invenPipeID = 10;
			setInvenCursorPosition ();
			
			//createNmoveInventoryPipe();
		}
	}
	public void OnClick_InvenItem1()
	{
		active_InventoryItem();
		//p11
		if (!bAni_InventoryItem) {
			setInvenCursorInitialize();
			invenCursor = 1;
			invenPipeID = 11;
			setInvenCursorPosition ();
			
			//createNmoveInventoryPipe();
		}
	}
	public void OnClick_InvenItem2()
	{
		active_InventoryItem();
		//p12
		if (!bAni_InventoryItem) {
			setInvenCursorInitialize();
			invenCursor = 2;
			invenPipeID = 12;
			setInvenCursorPosition ();
			
			//createNmoveInventoryPipe();
		}
	}
	public void OnClick_InvenItem3()
	{
		active_InventoryItem();
		//p13
		if (!bAni_InventoryItem) {
			setInvenCursorInitialize();
			invenCursor = 3;
			invenPipeID = 13;
			setInvenCursorPosition ();
			
			//createNmoveInventoryPipe();
		}
	}
	public void OnClick_InvenItem4()
	{
		active_InventoryItem();
		//p14
		if (!bAni_InventoryItem) {
			setInvenCursorInitialize();
			invenCursor = 4;
			invenPipeID = 14;
			setInvenCursorPosition ();
		}
	}
	public void OnClick_InvenItem5()
	{
		active_InventoryItem();
		//p15
		if (!bAni_InventoryItem) {
			setInvenCursorInitialize();
			invenCursor = 5;
			invenPipeID = 15;
			setInvenCursorPosition ();
		}
	}
	public void OnClick_InvenItem6()
	{
		active_InventoryItem();
		//p16
		if (!bAni_InventoryItem) {
			setInvenCursorInitialize();
			invenCursor = 6;
			invenPipeID = 16;
			setInvenCursorPosition ();
		}
	}
	public void OnClick_InvenItem7()
	{
		active_InventoryItem();
		//p70
		if (!bAni_InventoryItem) {
			setInvenCursorInitialize();
			invenCursor = 7;
			invenPipeID = 70;
			setInvenCursorPosition ();
		}
	}
	public void OnClick_InvenItem8()
	{
		active_InventoryItem();
		//p71
		if (!bAni_InventoryItem) {
			setInvenCursorInitialize();
			invenCursor = 8;
			invenPipeID = 71;
			setInvenCursorPosition ();
		}
	}
	public void OnClick_InvenItem9()
	{
		active_InventoryItem();
		//p72
		if (!bAni_InventoryItem) {
			setInvenCursorInitialize();
			invenCursor = 9;
			invenPipeID = 72;
			setInvenCursorPosition ();
		}
	}
	public void OnClick_InvenItem10()
	{
		active_InventoryItem();
		//p73
		if (!bAni_InventoryItem) {
			setInvenCursorInitialize();
			invenCursor = 10;
			invenPipeID = 73;
			setInvenCursorPosition ();
		}
	}
	public void OnClick_InvenItem11()
	{
		active_InventoryItem();
		//p74
		if (!bAni_InventoryItem) {
			setInvenCursorInitialize();
			invenCursor = 11;
			invenPipeID = 74;
			setInvenCursorPosition ();
		}
	}
	public void OnClick_InvenItem12()
	{
		active_InventoryItem();
		//p75
		if (!bAni_InventoryItem) {
			invenCursor = 12;
			invenPipeID = 75;
			setInvenCursorPosition ();
		}
	}
	public void OnClick_InvenItem13()
	{
		active_InventoryItem();
		//p76
		if (!bAni_InventoryItem) {
			setInvenCursorInitialize();
			invenCursor = 13;
			invenPipeID = 76;
			setInvenCursorPosition ();
		}
	}
	public void OnClick_InvenItem14()
	{
		active_InventoryItem();
		//p77
		if (!bAni_InventoryItem) {
			setInvenCursorInitialize();
			invenCursor = 14;
			invenPipeID = 77;
			setInvenCursorPosition ();
		}
	}
	public void OnClick_InvenItem15()
	{
		active_InventoryItem();
		//p80
		if (!bAni_InventoryItem) {
			setInvenCursorInitialize();
			invenCursor = 15;
			invenPipeID = 80;
			setInvenCursorPosition ();
		}
	}
	
	public void OnClick_InvenItem16()
	{
		active_InventoryItem();
		//p81
		if (!bAni_InventoryItem) {
			setInvenCursorInitialize();
			invenCursor = 16;
			invenPipeID = 81;
			setInvenCursorPosition ();
		}
	}
	public void OnClick_InvenItem17()
	{
		active_InventoryItem();
		//p82
		if (!bAni_InventoryItem) {
			setInvenCursorInitialize();
			invenCursor = 17;
			invenPipeID = 82;
			setInvenCursorPosition ();
		}
	}
	public void OnClick_InvenItem18()
	{
		active_InventoryItem();
		//p83
		if (!bAni_InventoryItem) {
			setInvenCursorInitialize();
			invenCursor = 18;
			invenPipeID = 83;
			setInvenCursorPosition ();
		}
	}
	public void OnClick_InvenItem19()
	{
		active_InventoryItem();
		//p84
		if (!bAni_InventoryItem) {
			setInvenCursorInitialize();
			invenCursor = 19;
			invenPipeID = 84;
			setInvenCursorPosition ();
		}
	}
	public void OnClick_InvenItem20()
	{
		active_InventoryItem();
		//p85
		if (!bAni_InventoryItem) {
			setInvenCursorInitialize();
			invenCursor = 20;
			invenPipeID = 85;
			setInvenCursorPosition ();
		}
	}
	public void OnClick_InvenItem21()
	{
		active_InventoryItem();
		//p86
		if (!bAni_InventoryItem) {
			setInvenCursorInitialize();
			invenCursor = 21;
			invenPipeID = 86;
			setInvenCursorPosition ();
		}
	}
	public void OnClick_InvenItem22()
	{
		active_InventoryItem();
		//p87
		if (!bAni_InventoryItem) {
			setInvenCursorInitialize();
			invenCursor = 22;
			invenPipeID = 87;
			setInvenCursorPosition ();
		}
	}
	
	
	
	
	
	
	
	
	
	
	
	
	public int conveyorCreateLevel = 10; //default
	//	public int conveyorCreateLevel = 20; //default + oneway
	//	public int conveyorCreateLevel = 30; //default + oneway + 1to2
	//	public int conveyorCreateLevel = 40; //default + oneway + 1to2 + 1to3
	
	
	public int getCreatePipeID()
	{
		int createPipeID = 10;
		int[] onewayPipe = {20, 21, 22, 23, 24, 25, 30, 31, 32, 33, 34, 35};
		int[] oneTotwoPipe = {70, 71, 72, 73, 74, 75, 76, 77};
		int[] oneToThreePipe = {80, 81, 82, 83, 84, 85, 86, 87};
		GameObject go = null;
		
		if (conveyorCreateLevel == 10) 
		{
			createPipeID = Random.Range(10, 17);
		}
		else if (conveyorCreateLevel == 20) 
		{
			if(RandRatio(10))
			{
				createPipeID = onewayPipe[Random.Range(0, 10)];
			}
			else
			{
				createPipeID = Random.Range(10, 17);
			}
		}
		else if (conveyorCreateLevel == 30) 
		{
			if(RandRatio(20))
			{
				createPipeID = oneTotwoPipe[Random.Range(0, 8)];
			}
			else
			{
				if(RandRatio(10))
				{
					createPipeID = onewayPipe[Random.Range(0, 10)];
				}
				else
				{
					createPipeID = Random.Range(10, 17);
				}
			}
		}
		else if (conveyorCreateLevel == 40) 
		{
			if(RandRatio(20))
			{
				if(RandRatio(50))
				{
					createPipeID = oneToThreePipe[Random.Range(0, 8)];
				}
				else
				{
					createPipeID = oneTotwoPipe[Random.Range(0, 8)];
				}
			}
			else
			{
				if(RandRatio(10))
				{
					createPipeID = onewayPipe[Random.Range(0, 10)];
				}
				else
				{
					createPipeID = Random.Range(10, 17);
				}
			}
		}
		
		return createPipeID;
	}
	
	void setConveyorBeltPipe()
	{
		//GameObject go = null;
		for (int i=0; i<10; i++) 
		{
			//go = GameObject.Find ("ConveyorPipe"+i);
			go_conveyorItem[i].GetComponent<ConveyorPipeScript>().setPipe( getCreatePipeID() );
			
		}
	}
	
	bool RandRatio(int percent)
	{
		bool returnValue = false;
		int value = Random.Range (0, 100);
		if (value < percent) 
		{
			returnValue = true;
		}
		return returnValue;
	}
	
	
	
	/// <summary>
	/// ConveyorBelt Click
	/// </summary>
	public void OnClick_Conveyor0()
	{
		//		Debug.Log ("OnClick_Conveyor0");
		skipThisConveyorPipe (0);
	}
	
	public void OnClick_Conveyor1()
	{
		//		Debug.Log ("OnClick_Conveyor1");
		skipThisConveyorPipe (1);
	}
	
	public void OnClick_Conveyor2()
	{
		//		Debug.Log ("OnClick_Conveyor2");
		skipThisConveyorPipe (2);
	}
	
	public void OnClick_Conveyor3()
	{
		//		Debug.Log ("OnClick_Conveyor3");
		skipThisConveyorPipe (3);
	}
	
	public void OnClick_Conveyor4()
	{
		//		Debug.Log ("OnClick_Conveyor4");
		skipThisConveyorPipe (4);
	}
	
	public void OnClick_Conveyor5()
	{
		//		Debug.Log ("OnClick_Conveyor5");
		skipThisConveyorPipe (5);
	}
	
	public void OnClick_Conveyor6()
	{
		//		Debug.Log ("OnClick_Conveyor6");
		skipThisConveyorPipe (6);
	}
	
	public void OnClick_Conveyor7()
	{
		//		Debug.Log ("OnClick_Conveyor7");
		skipThisConveyorPipe (7);
	}
	
	public void OnClick_Conveyor8()
	{
		//		Debug.Log ("OnClick_Conveyor8");
		skipThisConveyorPipe (8);
	}
	
	public void OnClick_Conveyor9()
	{
		//		Debug.Log ("OnClick_Conveyor9");
		skipThisConveyorPipe (9);
	}
	
	private void skipThisConveyorPipe(int _idx)
	{
		if(useWherePipe == 1)
		{
			active_ConveyorItem();
			return;
		}


		//go_conveyorItem
		//GameObject go = GameObject.Find ("ConveyorPipe"+_idx);
		//Vector3 pos = go.transform.localPosition;
		Vector3 v3pos = go_conveyorItem[_idx].transform.localPosition;
		ConveyorPipeScript conveyPipeScript = go_conveyorItem[_idx].GetComponent<ConveyorPipeScript> ();
		if (v3pos.y > 735 && checkReadyMove()) 
		{
			//calScore_skipPipe(conveyPipeScript.pipeID, v3pos);
			moveConveyor(true);
		}
	}
	
	
	public Vector3 v3pos_nowPipe;
	public int nowPipeID;
	public int nowPipeIndex;
	public int nowFireID;
	public POS pos_eatItem;
	public void moveConveyor(bool _skipThisPipe)
	{
		GameObject go = null;
		GameObject go_belt = null;
		for (int i=0; i<10; i++) 
		{
			go = GameObject.Find ("ConveyorPipe" + i);
			go.GetComponent<ConveyorPipeScript> ().move (_skipThisPipe);
			
		}
		
		efm.Play (5);
		for (int i=0; i<8; i++) 
		{
			//just belt ani
			go_belt = GameObject.Find ("Belt"+i);
			go_belt.GetComponent<BeltScript>().move();
		}
	}
	
	public void createNmoveInventoryPipe()
	{
		
		uis1_Temp_FromInven.enabled = true;
		uis2_Temp_FromInven.enabled = true;
		
		setAtlasSet (GlobalData.baseTileNum);
		
		//GameObject go_Temp = Instantiate(Resources.Load ("cwPrefabs/TempInvenPipe"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		
		//UISprite uis = go_Temp_FromInven.transform.FindChild("Layer2").GetComponent<UISprite>();
		uis1_Temp_FromInven.atlas = Resources.Load<UIAtlas> (GameCon.basePipeAtlasPath);
		//UISprite uisArrow = go_Temp_FromInven.transform.FindChild("Arrow").GetComponent<UISprite>();
		
		//set base
		switch(invenPipeID)
		{
		case 10:	case 20:	case 30:
			uis1_Temp_FromInven.spriteName = "10b";
			break;
			
		case 11:	case 21:	case 31:
			uis1_Temp_FromInven.spriteName = "11b";
			break;
			
		case 12:	case 22:	case 32:
			uis1_Temp_FromInven.spriteName = "12b";
			break;
			
		case 13:	case 23:	case 33:
			uis1_Temp_FromInven.spriteName = "13b";
			break;
			
		case 14:	case 24:	case 34:
			uis1_Temp_FromInven.spriteName = "14b";
			break;
			
		case 15:	case 25:	case 35:
			uis1_Temp_FromInven.spriteName = "15b";
			break;
			
		case 16:
			uis1_Temp_FromInven.spriteName = "16b";
			break;
			
		case 70:	case 74:
			uis1_Temp_FromInven.spriteName = "70b";
			break;
			
		case 71:	case 75:
			uis1_Temp_FromInven.spriteName = "71b";
			break;
			
		case 72:	case 76:
			uis1_Temp_FromInven.spriteName = "72b";
			break;
			
		case 73:	case 77:
			uis1_Temp_FromInven.spriteName = "73b";
			break;
			
		case 80:	case 84:
		case 81:	case 85:
		case 82:	case 86:
		case 83:	case 87:
			uis1_Temp_FromInven.spriteName = "80b";
			break;
		}
		
		
		//		Debug.Log("pipeID:   "+pipeID);
		//set arrow
		if (invenPipeID >= 10 && invenPipeID <= 16) 
		{
			
			uis2_Temp_FromInven.alpha = 0;
			//			Debug.Log("uis2_Temp_FromInven.alpha:   "+uis2_Temp_FromInven.alpha);
		} else {
			
			uis2_Temp_FromInven.alpha = 1;
			if(invenPipeID >= 20 && invenPipeID <= 25)
			{
				uis2_Temp_FromInven.atlas = Resources.Load<UIAtlas> ("Atlases/TopClockTiles");
				uis2_Temp_FromInven.spriteName = (invenPipeID-10)+"t";
			}
			else if(invenPipeID >= 30 && invenPipeID <= 35)
			{
				uis2_Temp_FromInven.atlas = Resources.Load<UIAtlas> ("Atlases/TopCounterTiles");
				uis2_Temp_FromInven.spriteName = (invenPipeID-20)+"t";
			}
			else if((invenPipeID >= 70 && invenPipeID <= 73) || (invenPipeID >= 80 && invenPipeID <= 83))
			{
				uis2_Temp_FromInven.atlas = Resources.Load<UIAtlas> ("Atlases/TopClockTiles");
				uis2_Temp_FromInven.spriteName = (invenPipeID)+"t";
			}
			else if((invenPipeID >= 74 && invenPipeID <= 77) || (invenPipeID >= 84 && invenPipeID <= 87))
			{
				uis2_Temp_FromInven.atlas = Resources.Load<UIAtlas> ("Atlases/TopCounterTiles");
				uis2_Temp_FromInven.spriteName = (invenPipeID)+"t";
			}
		}
		
		//		go_Temp_FromInven.name = "TempMoveInenPipe";
		//		//go_Temp.transform.parent = GameObject.Find ("InventoryObj").transform;//go_invenItem[invenCursor].transform;
		//		go_Temp_FromInven.transform.parent = GameObject.Find ("Panel_tempInvenPipe").transform;//go_invenItem[invenCursor].transform;
		
		go_Temp_FromInven.transform.localScale = new Vector3(1, 1, 1);
		
		
		efm.Play (4);
		
		Vector3 v3pos = GameObject.Find ("Panel_inventoryScrollView").transform.localPosition;
		Vector3 v3pos1 = go_invenItem[invenCursor].transform.localPosition;
		Vector3 v3pos2 = go_invenItem [invenCursor].transform.FindChild ("Layer2").transform.localPosition;
		//Debug.Log ("v3pos"+v3pos);
		v3pos.x += v3pos1.x+v3pos2.x;
		v3pos.y += v3pos1.y+v3pos2.y;
		go_Temp_FromInven.transform.localPosition = v3pos;
		
		
		Vector3 _v3pos_LeftTop = GameObject.Find ("Anchor_BottomInven").transform.localPosition;
		Vector3 _v3pos_Panel_ScrollView = go_Panel_ScrollView.transform.localPosition;
		Vector3 _v3scale_zoomObj = zoomPanObj.transform.localScale;
		
		Vector3 v3pos_target = v3pos;
		
		
		v3pos_target.x = -_v3pos_LeftTop.x+_v3pos_Panel_ScrollView.x+(v3pos_nowPipe.x*_v3scale_zoomObj.x);
		v3pos_target.y = -_v3pos_LeftTop.y+_v3pos_Panel_ScrollView.y+(v3pos_nowPipe.y*_v3scale_zoomObj.y);
		//Debug.Log ("v3pos_target"+v3pos_target);
		
		float tmpDuring = 0.3f;
		
		TweenScale twScale = TweenScale.Begin(go_Temp_FromInven, tmpDuring, _v3scale_zoomObj);
		TweenPosition twPosition = TweenPosition.Begin( go_Temp_FromInven, tmpDuring, v3pos_target); //VenderCon.VENDER_SPEED
		twPosition.method = UITweener.Method.EaseIn;
		EventDelegate.Add( twPosition.onFinished, callback_createNmoveInventoryPipe, true);
		
	}
	
	void callback_createNmoveInventoryPipe()
	{
		//Debug.Log ("6");
		changePref_Tile_fromInventory (nowPipeIndex); 
		
		createTileEffect(v3pos_nowPipe.x, v3pos_nowPipe.y);
		
		int id_eatItem = getID_EatItem (pos_eatItem);
		if (id_eatItem >= 50 && id_eatItem <= 54) //bomb
		{
			if(nowFireID == getPerfectPlanID(pos_eatItem))
			{
				checkEatItem(pos_eatItem.x, pos_eatItem.y);
			}
		}
		else if (id_eatItem >= 60 && id_eatItem <= 75)	//plus pipe item 
		{
			if(nowFireID == getPerfectPlanID(pos_eatItem))
			{
				checkPlusItem(pos_eatItem.x, pos_eatItem.y);
			}
		}
		
		//Destroy (GameObject.Find ("TempMoveInenPipe"));
		uis1_Temp_FromInven.enabled = false;
		uis2_Temp_FromInven.enabled = false;
	}
	
	
	
	public bool checkReadyMove()
	{
		bool returnValue = true;
		GameObject go = null;
		GameObject go_belt = null;
		for (int i=0; i<10; i++) 
		{
			go = GameObject.Find ("ConveyorPipe" + i);
			if (go.GetComponent<ConveyorPipeScript> ().bMove) 
			{
				returnValue = false;
			}
		}
		
		for (int i=0; i<8; i++) 
		{
			//just belt ani
			go_belt = GameObject.Find ("Belt"+i);
			if(go_belt.GetComponent<BeltScript>().bMove)
			{
				returnValue = false;
			}
		}
		return returnValue;
	}
	
	
	public int getFireReadyPipe()
	{
		int returnValue = 10;
		GameObject go = null;
		Vector3 pos;
		for (int i=0; i<10; i++) 
		{
			go = GameObject.Find ("ConveyorPipe" + i);
			pos = go.transform.localPosition;
			if(pos.y > 735)
			{
				returnValue = go.GetComponent<ConveyorPipeScript>().pipeID;
			}
		}
		return returnValue;
	}
	
	
	
	
	
	
	public void createFailMark(Vector3 _v3pos, int _enterDirection)
	{
		
		GameObject _go = Instantiate(Resources.Load ("cwPrefabs/FailMark"), new Vector3(300, 3000, 0), Quaternion.identity) as GameObject;
		//Debug.Log ("_enterDirection"+_enterDirection);
		switch (_enterDirection) 
		{
		case 0:
			_v3pos.y += 45;
			break;
			
		case 1:
			_v3pos.x += 45;
			break;
			
		case 2:
			_v3pos.y -= 45;
			break;
			
		case 3:
			_v3pos.x -= 45;
			break;
		}
		
		_go.transform.parent = go_parent_top.transform;
		
		_go.transform.localScale = new Vector3(1, 1, 1);
		
		_go.transform.localPosition = _v3pos;
		
		bFailMark = true;
		timer_FailMark = 0;

		go_FillRect_Screen.SetActive(true);
		moveOut_LeftRightPanel();
	}
	
	
	public void createTileEffect(float _xpos, float _ypos)
	{
		
		Vector3 pos = new Vector3(_xpos, _ypos, 0);
		//		pos.x = _xpos;
		//		pos.y = _ypos;
		//
		//		_go.transform.parent = go_parent.transform;
		//		_go.transform.localScale = new Vector3(1, 1, 1);
		
		go_TileEffect.transform.localPosition = pos;
		go_TileEffect.GetComponent<TileEffectScript> ().play ();
		
		efm.Play (1);
	}

	public void createPlusTileEffect(float _xpos, float _ypos, int _idx)
	{
		
		Vector3 pos = new Vector3(_xpos, _ypos, 0);
		go_PlusTileEffect[_idx].transform.localPosition = pos;
		go_PlusTileEffect[_idx].GetComponent<TileEffectScript> ().play ();
		
		efm.Play (1);
	}
	
	public void createEndNum(float _xpos, float _ypos, int _scoreValue, int _colorType)
	{
		GameObject _go = null;
		
		if (_scoreValue < 0) 
		{
			_go = Instantiate(Resources.Load ("cwPrefabs/RedN"), new Vector3(300, 300, 0), Quaternion.identity) as GameObject;
		}
		else if (_scoreValue >= 0) 
		{
			if(_colorType == 0)
			{
				_go = Instantiate(Resources.Load ("cwPrefabs/BlueN"), new Vector3(300, 300, 0), Quaternion.identity) as GameObject;
			}
			else if(_colorType == 1)
			{
				_go = Instantiate(Resources.Load ("cwPrefabs/GreenN"), new Vector3(300, 300, 0), Quaternion.identity) as GameObject;
				_ypos += 50;
			}
		}
		
		Vector3 pos = _go.transform.localPosition;
		pos.x = _xpos;
		pos.y = _ypos;
		
		_go.transform.parent = go_parent.transform;
		
		_go.transform.localScale = new Vector3(1, 1, 1);
		
		_go.transform.localPosition = pos;
		
		_go.GetComponent<NumScript> ().play (_scoreValue);
	}
	
	public void createNum(float _xpos, float _ypos, int _scoreValue)
	{
		GameObject _go = null;
		
		if (_scoreValue < 0) 
		{
			_go = Instantiate(Resources.Load ("cwPrefabs/RedN"), new Vector3(300, 300, 0), Quaternion.identity) as GameObject;
		}
		else if (_scoreValue >= 0) 
		{
			_go = Instantiate(Resources.Load ("cwPrefabs/BlueN"), new Vector3(300, 300, 0), Quaternion.identity) as GameObject;
		}
		
		Vector3 pos = _go.transform.localPosition;
		pos.x = _xpos;
		pos.y = _ypos;
		
		_go.transform.parent = go_parent.transform;
		
		_go.transform.localScale = new Vector3(1, 1, 1);
		
		_go.transform.localPosition = pos;
		
		_go.GetComponent<NumScript> ().play (_scoreValue);
	}
	
	public void createTextNum(float _xpos, float _ypos, string _text, int _type)
	{
		GameObject _go = null;
		
		if (_type == 0) //Gold
		{
			_go = Instantiate(Resources.Load ("cwPrefabs/YellowN"), new Vector3(300, 300, 0), Quaternion.identity) as GameObject;
			
		}
		else if (_type == 1) //Gem
		{
			_go = Instantiate(Resources.Load ("cwPrefabs/PinkN"), new Vector3(300, 300, 0), Quaternion.identity) as GameObject;
			
		}
		else if (_type == 2) //lost
		{
			_go = Instantiate(Resources.Load ("cwPrefabs/RedN"), new Vector3(300, 300, 0), Quaternion.identity) as GameObject;
			
		}
		else if (_type == 3) //add
		{
			_go = Instantiate(Resources.Load ("cwPrefabs/BlueN"), new Vector3(300, 300, 0), Quaternion.identity) as GameObject;
			
		}
		
		
		Vector3 pos = _go.transform.localPosition;
		pos.x = _xpos;
		pos.y = _ypos;
		
		_go.transform.parent = go_parent_top.transform;
		
		_go.transform.localScale = new Vector3(2, 2, 1);
		
		_go.transform.localPosition = pos;
		
		_go.GetComponent<NumScript> ().play (_text);
	}
	
	public void createSkipNum(float _xpos, float _ypos, int _scoreValue)
	{
		GameObject _go = null;
		
		if (_scoreValue < 0) 
		{
			_go = Instantiate(Resources.Load ("cwPrefabs/RedN"), new Vector3(300, 300, 0), Quaternion.identity) as GameObject;
		}
		else if (_scoreValue >= 0) 
		{
			_go = Instantiate(Resources.Load ("cwPrefabs/BlueN"), new Vector3(300, 300, 0), Quaternion.identity) as GameObject;
		}
		
		Vector3 pos = _go.transform.localPosition;
		pos.x = _xpos;
		pos.y = _ypos;
		
		_go.transform.parent = go_parent_conveyorBelt.transform;
		
		//_go.transform.parent = go_parent_perfectplan.transform;
		
		_go.transform.localScale = new Vector3(1, 1, 1);
		
		_go.transform.localPosition = pos;
		
		_go.GetComponent<NumScript> ().play (_scoreValue);
	}
	
	
	
	
	
	
	
	
	void FixedUpdate()
	{
		setConnectCountText ();
		//		setAliveStreamText ();
	}
	
	int beforeConnectCount = 0;
	int lostConnectCount = 0;
	int addConnectCount = 0;
	public int getConnectCount()
	{
		int returnConnectCount = 0;
		
		tmpUserScore = 0;
		initConnectBuf ();
		
		aliveStream = 0;
		connectCount = 0;
		
		escapeTime = 0;
		bEscape = false;
		
		setPipeFinish_connectAll ();
		startConnect ();
		
		
		
		
		
		getLostConnectCount_n_Score ();
		if (lostConnectCount > 0) 
		{
			createTextNum (0, +270, "Lost " + lostConnectCount + " Connect "+ lostScore, 2);
		}
		
		//getAddConnectCount_n_Score();//connectCount - (beforeConnectCount - lostConnectCount);
		if (connectCount > 0) 
		{
			createTextNum (0, +200, connectCount + " Connect "+tmpUserScore, 3);
		}
		
		
		
		//		Debug.Log ("tmpUserScore1:   "+tmpUserScore1);
		//		Debug.Log ("lostScore:   "+lostScore);
		//		Debug.Log ("tmpUserScore:--------------------------   "+tmpUserScore);
		refresh_UseScore ();
		
		makeBeforeConnectBuf ();
		beforeConnectCount = connectCount;
		
		return returnConnectCount;
		
	}
	
	
	public void setPipeFinish_connectAll ()
	{
		//GameObject _go;
		for (int i=0; i<nRow; i++) 
		{
			for (int j=0; j<nCol; j++) 
			{
				//				_go = GameObject.Find("Pipe"+(i*nCol+j));
				//				_go.GetComponent<PM>().posInfo.setFinish_connect();
				
				map[i][j].setFinish_connect();
			}
		}
	}
	
	public void startConnect()
	{
		GameObject _go;
		StartPipeCount = 0;
		for (int i=0; i<nRow; i++) 
		{
			for (int j=0; j<nCol; j++) 
			{
				switch(map[i][j].pipeID)
				{
				case 110:
					sPipe[i*nCol+j].calConnect(17);
					StartPipeCount++;
					break;
					
				case 111:
					sPipe[i*nCol+j].calConnect(18);
					StartPipeCount++;
					break;
					
				case 112:
					sPipe[i*nCol+j].calConnect(19);
					StartPipeCount++;
					break;
					
				case 113:
					sPipe[i*nCol+j].calConnect(20);
					StartPipeCount++;
					break;
				}
			}
		}
	}
	
	public void setConnectCountText()
	{
		GameObject go1 = GameObject.Find("ConnectCount");
		go1.GetComponent<UILabel>().text = ""+connectCount;
	}
	
	//	public static void setAliveStreamText()
	//	{
	//		GameObject go = GameObject.Find("AliveStream");
	//		go.GetComponent<UILabel>().text = ""+aliveStream;
	//	}
	
	
	
	
	
	
	public int getIndex(int _xpos, int _ypos)
	{
		return map [_ypos] [_xpos].Idx;
	}
	
	public int getID(int _xpos, int _ypos)
	{
		return map [_ypos] [_xpos].pipeID;
	}
	
	public int getPerfectPlanID(POS _pos)
	{
		return mapBuf1 [_pos.y] [_pos.x];
	}

	public int getPerfectPlanID1(int _xpos, int _ypos)
	{
		return mapBuf1 [_ypos] [_xpos];
	}
	
	public void set_bEndFinish(int _xpos, int _ypos, bool _value)
	{
		map [_ypos] [_xpos].bEndFinish = _value;
	}
	
	
	public bool bCheckCondition_Norm = false;
	public void check_limit_Condition_Norm()
	{
		//bCheckCondition_Norm = false;
		
		//Debug.Log("pass 1");
		if (limitNorm > 0) 
		{
			//	Debug.Log("pass 2");
			if(check_NormLimit())
			{
				//		Debug.Log("pass 3");
				//norm limit condition success...
				bCheckCondition_Norm = true;
			}
			else
			{
				//norm limit condition fail...
				setHandObj(1);	//norm
				setUp_Popup_Fail(1);
				return;
			}
		}
		else
		{
			bCheckCondition_Norm = true;
		}
		
		if (bCheckCondition_Norm) 
		{
			if(checkPerfectClear())
			{
				//Debug.Log("2");
				//here is perfect clear
				ScoreValue_PerfectBonus = PERFECT_BONUS;
				createStarEffect();
			}
			else
			{
				//Debug.Log("3");
				//here is not perfect clear
				ScoreValue_PerfectBonus = 0;
				goto_calScore();
			}
			
		}
	}
	
	
	//	public void goto_calStar()
	//	{
	//	}
	
	//	public void check_limit_Condition_Score()
	//	{
	//		bool bCheckCondition_Score = false;
	//
	//		if (limitScore > 0) 
	//		{
	//			if(check_ScoreLimit())
	//			{
	//				//norm limit condition success...
	//				bCheckCondition_Score = true;
	//			}
	//			else
	//			{
	//				//score limit condition fail...
	//				go_popup_score.SetActive(false);
	//				go_Popup_CalScore.SetActive(false);
	//				moveIn_LeftRightPanel();
	//				setHandObj(0);
	//				setUp_Popup_Fail(2);
	//				return;
	//			}
	//		}
	//		else
	//		{
	//			bCheckCondition_Score = true;
	//		}
	//
	//		if (bCheckCondition_Score) 
	//		{
	//			goto_calGold();
	//		}
	//	}
	
	public void setHandObj(int _type)
	{
		handObj.SetActive (true);
		Vector3 v3pos = new Vector3 (0, 0, 0);
		
		if (_type == 0) //limit Score
		{
			v3pos = GameObject.Find ("text_scorelimit").transform.localPosition;
			v3pos.x -= 150;
		}
		else if (_type == 1) //limit norm
		{
			v3pos = GameObject.Find ("PipeLimit").transform.localPosition;
			v3pos.x -= 240;
		}
		else if (_type == 2) //time limit
		{
			v3pos = GameObject.Find ("text_timelimit").transform.localPosition;
			v3pos.x -= 150;
		}
		v3pos.y -= 70;
		handObj.transform.localPosition = v3pos;
		handObj.GetComponent<TweenPosition> ().from = v3pos;
		v3pos.x += 20;
		handObj.GetComponent<TweenPosition> ().to = v3pos;
		
		
	}
	
	void goto_calGold()
	{
		//Debug.Log ("goto_Gold");
	}

	public void moveIn_InventoryItem()
	{
		efm.Play(8);

		Vector3 v3pos = new Vector3(0, -400, 0);

		TweenPosition twPosition = TweenPosition.Begin( go_BottomPanel, 0.5f, v3pos ); //VenderCon.VENDER_SPEED
		twPosition.method = UITweener.Method.BounceOut;

	}

	void moveIn_LeftRightPanel()
	{
		Vector3 v3pos = go_LeftPanel.transform.localPosition;
		v3pos.x += 1000;
		
		TweenPosition twPosition = TweenPosition.Begin( go_LeftPanel, 0.5f, v3pos ); //VenderCon.VENDER_SPEED
		twPosition.method = UITweener.Method.BounceOut;
		
		v3pos = go_RightPanel.transform.localPosition;
		v3pos.x -= 1000;
		
		twPosition = TweenPosition.Begin( go_RightPanel, 0.5f, v3pos ); //VenderCon.VENDER_SPEED
		twPosition.method = UITweener.Method.BounceOut;
		
		twPosition = TweenPosition.Begin( go_RightTopPanel, 0.5f, v3pos ); //VenderCon.VENDER_SPEED
		twPosition.method = UITweener.Method.BounceOut;
		
		v3pos = go_BottomPanel.transform.localPosition;
		v3pos.y -= 1000;
		
		twPosition = TweenPosition.Begin( go_BottomPanel, 0.5f, v3pos ); //VenderCon.VENDER_SPEED
		twPosition.method = UITweener.Method.BounceOut;
		//lock scrollview
		//GameObject.Find ("Panel_ScrollView").GetComponent<UIScrollView> ().enabled = false;
	}
	
	void moveOut_LeftRightPanel()
	{
		Vector3 v3pos = go_LeftPanel.transform.localPosition;
		v3pos.x -= 1000;
		
		TweenPosition twPosition = TweenPosition.Begin( go_LeftPanel, 0.5f, v3pos ); //VenderCon.VENDER_SPEED
		twPosition.method = UITweener.Method.BounceOut;
		
		v3pos = go_RightPanel.transform.localPosition;
		v3pos.x += 1000;
		
		twPosition = TweenPosition.Begin( go_RightPanel, 0.5f, v3pos ); //VenderCon.VENDER_SPEED
		twPosition.method = UITweener.Method.BounceOut;

		twPosition = TweenPosition.Begin( go_RightTopPanel, 0.5f, v3pos ); //VenderCon.VENDER_SPEED
		twPosition.method = UITweener.Method.BounceOut;

		v3pos = go_BottomPanel.transform.localPosition;
		v3pos.y -= 1000;
		
		twPosition = TweenPosition.Begin( go_BottomPanel, 0.5f, v3pos ); //VenderCon.VENDER_SPEED
		twPosition.method = UITweener.Method.BounceOut;
		

		
	}
	
	public void Lock_PanelScrollView()
	{
		//lock scrollview
		GameObject.Find ("Panel_ScrollView").GetComponent<UIScrollView> ().enabled = false;
	}
	
	public void goto_calScore()
	{
		//Debug.Log("pass 4");
		
		moveOut_LeftRightPanel ();
		Lock_PanelScrollView ();
		
		makeMinimumScreen ();
		
		go_Popup_CalScore.SetActive (true);
		
		bCalScoreFrame = true;
		
		
		disable_all_trigger ();
		
		init_all_Buf ();
		makeCrossBuf ();
		
		//makeDefaultBuf ();
		//makeOnewayBuf ();
		makeInOutBuf ();
		make1To2Buf ();
		make1To3Buf ();
		make1To3Buf ();
		//makeWallInOutBuf ();
		makeMissBuf ();
		
		ScoreValue_TotalScore = 
			ScoreValue_Default +
				ScoreValue_Cross +
				ScoreValue_Special +
				ScoreValue_OverUse +
				ScoreValue_TimeBonus +
				ScoreValue_PerfectBonus +
				ScoreValue_Missing +
				userScore;
		
		if (ScoreValue_TotalScore > pd.record [PlayerData.nowMapNumber1]) 
		{
			bNewRecord = true;
			
		}
		
		checkNextScoreFrame(0);
		//		stateScoreFrame = 1;	//cross
		//		currentScoreFrameIndex = 0;
		//		scoreTimer = 0;
		
	}
	
	
	void init_all_Buf()
	{
		for (int i=0; i<nRow; i++) 
		{
			for (int j=0; j<nCol; j++) 
			{
				//initialize 0, because 0 index is not use....
				score_default[i*nCol+j] = 0;
				score_cross[i*nCol+j] = 0;
				score_oneway[i*nCol+j] = 0;
				score_InOut[i*nCol+j] = 0;
				score_1To2[i*nCol+j] = 0;
				score_1To3[i*nCol+j] = 0;
				score_WallInOut[i*nCol+j] = 0;
				score_Miss[i*nCol+j] = 0;
			}
		}
		
		ScoreValue_Default = userScore;
		ScoreValue_Cross = 0;
		ScoreValue_Special = 0;
		ScoreValue_OverUse = (connectCount - limitNorm)*OVERUSE_BONUS;
		ScoreValue_TimeBonus = getTimeBonus();
		ScoreValue_PerfectBonus = 0;
		ScoreValue_Missing = 0;
		ScoreValue_TotalScore = 0;
	}
	
	int getTimeBonus()
	{
		int returnValue = 0;
		if (pd.flowTime >= 0) 
		{
			returnValue = (int)(pd.flowTime * TIME_BONUS);
		}
		return returnValue;
	}
	
	int score_CrossCount;
	void makeCrossBuf()
	{
		score_CrossCount = 0;
		for (int i=0; i<nRow; i++) 
		{
			for (int j=0; j<nCol; j++) 
			{
				if(map[i][j].pipeID == 16
				   || map[i][j].pipeID == 46)
				{
					if(getEndCount(j, i) == 2)
					{
						score_cross[score_CrossCount] = i*nCol+j;
						score_CrossCount++;
						ScoreValue_Cross += SCORE_END_CROSS;
					}
				}
			}
		}
		
		//		Debug.Log ("score_CrossCount: "+score_CrossCount);
	}
	
	int score_defaultCount;
	void makeDefaultBuf()
	{
		score_defaultCount = 0;
		for (int i=0; i<nRow; i++) 
		{
			for (int j=0; j<nCol; j++) 
			{
				if( (map[i][j].pipeID >= 10 && map[i][j].pipeID <= 15)  
				   || (map[i][j].pipeID >= 40 && map[i][j].pipeID <= 45)  
				   || map[i][j].pipeID == 16
				   ||  map[i][j].pipeID == 46
				   )
				{
					if(getEndCount(j, i) == 1)
					{
						score_default[score_defaultCount] = i*nCol+j;
						score_defaultCount++;
						ScoreValue_Default += SCORE_END_DEFAULT;
					}
				}
			}
		}
		//		Debug.Log ("score_defaultCount: "+score_defaultCount);
	}
	
	int score_OnewayCount;
	void makeOnewayBuf()
	{
		score_OnewayCount = 0;
		for (int i=0; i<nRow; i++) 
		{
			for (int j=0; j<nCol; j++) 
			{
				if( 
				   (map[i][j].pipeID >= 20 && map[i][j].pipeID <= 25)
				   || (map[i][j].pipeID >= 50 && map[i][j].pipeID <= 55)
				   || (map[i][j].pipeID >= 30 && map[i][j].pipeID <= 35)
				   || (map[i][j].pipeID >= 60 && map[i][j].pipeID <= 65)
				   )
				{
					if(getEndCount(j, i) == 1)
					{
						score_oneway[score_OnewayCount] = i*nCol+j;
						score_OnewayCount++;
						ScoreValue_Special += SCORE_END_ONEWAY;
					}
				}
			}
		}
		//		Debug.Log ("score_OnewayCount: "+score_OnewayCount);
	}
	
	int score_InOutCount;
	void makeInOutBuf()
	{
		score_InOutCount = 0;
		for (int i=0; i<nRow; i++) 
		{
			for (int j=0; j<nCol; j++) 
			{
				if( 
				   (map[i][j].pipeID >= 130 && map[i][j].pipeID <= 157)
				   )
				{
					if(getEndCount(j, i) == 1)
					{
						score_InOut[score_InOutCount] = i*nCol+j;
						score_InOutCount++;
						ScoreValue_Special += SCORE_END_INOUT;
					}
				}
			}
		}
		//		Debug.Log ("score_InOutCount: "+score_InOutCount);
	}
	
	int score_1To2Count;
	void make1To2Buf()
	{
		score_1To2Count = 0;
		for (int i=0; i<nRow; i++) 
		{
			for (int j=0; j<nCol; j++) 
			{
				if( (map[i][j].pipeID >= 70 && map[i][j].pipeID <= 73)
				   || (map[i][j].pipeID >= 90 && map[i][j].pipeID <= 93))
				{
					if(getEndCount(j, i) == 2)
					{
						score_1To2[score_1To2Count] = i*nCol+j;
						score_1To2Count++;
						ScoreValue_Special += SCORE_END_1TO2;
					}
				}
				else if( (map[i][j].pipeID >= 74 && map[i][j].pipeID <= 77)
				        || (map[i][j].pipeID >= 94 && map[i][j].pipeID <= 97))
				{
					if(getEndCount(j, i) == 1)
					{
						score_1To2[score_1To2Count] = i*nCol+j;
						score_1To2Count++;
						ScoreValue_Special += SCORE_END_1TO2;
					}
				}
			}
		}
		//		Debug.Log ("score_1To2Count: "+score_1To2Count);
	}
	
	int score_1To3Count;
	void make1To3Buf()
	{
		score_1To3Count = 0;
		for (int i=0; i<nRow; i++) 
		{
			for (int j=0; j<nCol; j++) 
			{
				if( (map[i][j].pipeID >= 80 && map[i][j].pipeID <= 83)
				   || (map[i][j].pipeID >= 100 && map[i][j].pipeID <= 103))
				{
					if(getEndCount(j, i) == 3)
					{
						score_1To3[score_1To3Count] = i*nCol+j;
						score_1To3Count++;
						ScoreValue_Special += SCORE_END_1TO3;
					}
				}
				else if( (map[i][j].pipeID >= 84 && map[i][j].pipeID <= 87)
				        || (map[i][j].pipeID >= 104 && map[i][j].pipeID <= 107))
				{
					if(getEndCount(j, i) == 1)
					{
						score_1To3[score_1To3Count] = i*nCol+j;
						score_1To3Count++;
						ScoreValue_Special += SCORE_END_1TO3;
					}
				}
			}
		}
		//		Debug.Log ("score_1To3Count: "+score_1To3Count);
	}
	
	int score_WallInOutCount;
	void makeWallInOutBuf()
	{
		score_WallInOutCount = 0;
		for (int i=0; i<nRow; i++) 
		{
			for (int j=0; j<nCol; j++) 
			{
				if( (map[i][j].pipeID >= 170 && map[i][j].pipeID <= 173) )
				{
					if(getEndCount(j, i) == 1)
					{
						score_WallInOut[score_WallInOutCount] = i*nCol+j;
						score_WallInOutCount++;
						ScoreValue_Special += SCORE_END_WALL_INOUT;
					}
				}
			}
		}
		//		Debug.Log ("score_WallInOutCount: "+score_WallInOutCount);
	}
	
	int score_MissCount;
	void makeMissBuf()
	{
		score_MissCount = 0;
		for (int i=0; i<nRow; i++) 
		{
			for (int j=0; j<nCol; j++) 
			{
				if( (map[i][j].pipeID >= 10 && map[i][j].pipeID < 160) )
				{
					if(getEndCount(j, i) == 0)
					{
						score_Miss[score_MissCount] = i*nCol+j;
						score_MissCount++;
						ScoreValue_Missing += SCORE_END_MISS;
					}
				}
			}
		}
		//		Debug.Log ("score_MissCount: "+score_MissCount);
	}
	
	//	public bool check_ScoreLimit()
	//	{
	//		bool returnValue = false;
	//
	//		//ScoreValue_TotalScore is last value
	//		if(limitScore <= ScoreValue_TotalScore)
	//		{
	//			returnValue = true;
	//		}
	//		return returnValue;
	//		
	//	}
	
	public bool check_NormLimit()
	{
		bool returnValue = false;
		
		if(limitNorm <= connectCount)
		{
			returnValue = true;
		}
		return returnValue;
		
	}
	
	public bool check_End_all()
	{
		bool returnValue = false;
		int endCount = 0;
		EndPipeCount = 0;
		for(int i=0; i<nRow; i++)
		{
			for(int j=0; j<nCol; j++)
			{
				//Debug.Log("map [i] [j].bEndFinish:"+map [i] [j].bEndFinish);
				
				if(map[i][j].pipeID >= 120 && map[i][j].pipeID <= 123)
				{
					endCount++;
					if(map [i] [j].bEndFinish)
					{
						EndPipeCount++;
					}
				}
			}
		}
		
		Debug.Log("StartPipeCount: "+StartPipeCount);
		Debug.Log("StartPipeCount: "+StartPipeCount);
		if (endCount == EndPipeCount) 
		{
			Debug.Log("true");
			returnValue = true;
		}
		return returnValue;
	}
	
	
	
	public int getEndCount(int _x, int _y)
	{
		return map [_y] [_x].endCount;
	}
	
	public void addEndCount(int _xpos, int _ypos)
	{
		map [_ypos] [_xpos].endCount++;
	}
	
	public void set_bFinish(int _xpos, int _ypos, int _direction, bool _value)
	{
		map [_ypos] [_xpos].bFinish [_direction] = _value;
	}
	
	public bool get_bFinish(int _xpos, int _ypos, int _direction)
	{
		return map [_ypos] [_xpos].bFinish [_direction];
	}
	
	public void set_bFinish_connect(int _xpos, int _ypos, int _direction, bool _value)
	{
		map [_ypos] [_xpos].bFinish_connect [_direction] = _value;
	}
	
	public void set_bFinish_In_connect(int _xpos, int _ypos, int _direction, bool _value)
	{
		map [_ypos] [_xpos].bFinish_In_connect [_direction] = _value;
	}
	
	public bool get_bFinish_connect(int _xpos, int _ypos, int _direction)
	{
		return map[_ypos][_xpos].bFinish_connect [_direction];
	}
	
	public bool get_bFinish_In_connect(int _xpos, int _ypos, int _direction)
	{
		return map [_ypos][_xpos].bFinish_In_connect [_direction];
	}
	
	public void set_direction(int _xpos, int _ypos, int _direction)
	{
		map [_ypos] [_xpos].direction = _direction;
	}
	
	public void set_direction_connect(int _xpos, int _ypos, int _direction)
	{
		map [_ypos] [_xpos].direction_connect = _direction;
	}
	
	public int get_direction(int _xpos, int _ypos)
	{
		return map [_ypos] [_xpos].direction;
	}
	
	public int get_direction_connect(int _xpos, int _ypos)
	{
		return map [_ypos] [_xpos].direction_connect;
	}
	
	
	public void TurnOnWater()
	{
		GameObject _go;
		for (int i=0; i<nRow; i++) 
		{
			for (int j=0; j<nCol; j++) 
			{
				switch(map[i][j].pipeID)
				{
				case 110:
					sPipe[i*nCol+j].setType(17);
					break;
					
				case 111:
					sPipe[i*nCol+j].setType (18);
					break;
					
				case 112:
					sPipe[i*nCol+j].setType (19);
					break;
					
				case 113:
					sPipe[i*nCol+j].setType (20);
					break;
				}
			}
		}
	}
	
	
	public void OnClick_TurnOnWater()
	{
		//		if(pd.consumeItem[5] > 0 && !bTurnOnWater && !bFail)
		//		{
		//
		//			pd.consumeItem[5]--;
		//			pd.saveData();
		
		refresh_TurnOnItemCount ();
		WaterTimeScript.bStartTimer = false;
		
		TurnOnWater ();
		TurnOnWaterSetup();
		
		createTextNum(0, 0, "WATER START!", 2);
		//		}
	}
	
	public void TurnOnWaterSetup()
	{
		bTurnOnWater = true;
		efm.Play(6);
		makeMinimumScreen ();
	}
	
	void makeMinimumScreen()
	{
		go_ZoomPanObj.GetComponent<ZoomButtom> ().setMinimumScreen ();
		Vector3 v3pos =  go_Panel_ScrollView.transform.localPosition;
		v3pos.x = 0;
		v3pos.y = 0;
		go_Panel_ScrollView.transform.localPosition = v3pos;
	}
	
	//	public void OnClick_InventoryBoost()
	//	{
	//		if(pd.consumeItem[3] > 0 && inventorySpeed > 0.5f && !bFail)
	//		{
	//			
	//			pd.consumeItem[3]--;
	//			pd.saveData();
	//
	//			inventorySpeed = 0.5f;
	////			refresh_InventoryBooster();
	//			WaterTimeScript.bStartTimer = false;
	//			
	////			TurnOnWater ();
	//		}
	//	}
	
	public void OnClick_WaterTimeExtention()
	{
		//		Debug.Log("pd.consumeItem[5]:"+pd.consumeItem[5]);
		//		Debug.Log("bWaterFlow"+bWaterFlow);
		//		Debug.Log("pd.flowTime:"+pd.flowTime);
		//		Debug.Log("pd.flowDefaultTime:"+pd.flowDefaultTime);
		
		if(pd.consumeItem[6] > 0 && bWaterFlow == false && pd.flowTime > 0 
		   && pd.flowTime < pd.flowDefaultTime && !bFail)
		{
			pd.consumeItem[6]--;
			pd.saveData();
			
			pd.flowTime += 30;
			if(pd.flowTime > pd.flowDefaultTime)
			{
				pd.flowTime = pd.flowDefaultTime;
			}
			
			refresh_WaterFlowTimeExpansion();
			
			
		}
	}
	
	public void OnClick_PerfectPlan()
	{
		if(pd.consumeItem[4] > 0 && bShowPerfectPlan == false && !bFail)
		{
			pd.consumeItem[4]--;
			pd.saveData();
			
			bShowPerfectPlan = true;
			time_ShowPerfectPlan = 0;
			go_parent_perfectplan.SetActive(true);
			//showPerfectPlanPipe();
			
			refresh_ShowPerfectPlan();
		}
	}
	
	void refresh_TurnOnItemCount()
	{
		GameObject.Find ("stageItem3").transform.FindChild ("Count").GetComponent<UILabel> ().text = pd.consumeItem [5].ToString ();
	}
	
	//	void refresh_InventoryBooster()
	//	{
	//		GameObject.Find ("stageItem2").transform.FindChild ("Count").GetComponent<UILabel> ().text = pd.consumeItem [3].ToString ();
	//	}
	
	void refresh_WaterFlowTimeExpansion()
	{
		GameObject.Find ("stageItem0").transform.FindChild ("Count").GetComponent<UILabel> ().text = pd.consumeItem [6].ToString ();
	}
	
	void refresh_ShowPerfectPlan()
	{
		GameObject.Find ("stageItem1").transform.FindChild ("Count").GetComponent<UILabel> ().text = pd.consumeItem [4].ToString ();
	}
	
	
	
	
	public void disableTextScore()
	{
		
		go_default.SetActive (false);
		go_cross.SetActive (false);
		go_special.SetActive (false);
		go_overuse.SetActive (false);
		go_timebonus.SetActive (false);
		go_perfect.SetActive (false);
		go_missing.SetActive (false);
		go_totalscore.SetActive (false);
	}
	
	public void setTextScore(int _id)
	{
		if (_id == 0) //default
		{
			go_default.SetActive (true);
			go_default.transform.FindChild("text_DefaultPipeValue").GetComponent<UILabel>().text = ScoreValue_Default.ToString();
		}
		else if (_id == 1) 	//cross
		{
			go_cross.SetActive (true);
			go_cross.transform.FindChild("text_CrossPipeValue").GetComponent<UILabel>().text = ScoreValue_Cross.ToString();
		}
		else if (_id == 2) //special	
		{
			go_special.SetActive (true);
			go_special.transform.FindChild("text_SpecialPipeValue").GetComponent<UILabel>().text = ScoreValue_Special.ToString();
		}
		else if (_id == 3) //over use
		{
			go_overuse.SetActive (true);
			go_overuse.transform.FindChild("text_OverUsePipeValue").GetComponent<UILabel>().text = ScoreValue_OverUse.ToString();
		}
		else if (_id == 4) //time bonus
		{
			go_timebonus.SetActive (true);
			go_timebonus.transform.FindChild("text_TimeBonusValue").GetComponent<UILabel>().text = ScoreValue_TimeBonus.ToString();
		}
		else if (_id == 5) //perfect bonus
		{
			go_perfect.SetActive (true);
			//ScoreValue_PerfectBonus = PERFECT_BONUS;
			go_perfect.transform.FindChild("text_PerfectClearBonusValue").GetComponent<UILabel>().text = ScoreValue_PerfectBonus.ToString();
		}
		else if (_id == 6) //missing
		{
			go_missing.SetActive (true);
			go_missing.transform.FindChild("text_MissingPipeValue").GetComponent<UILabel>().text = ScoreValue_Missing.ToString();
		}
		else if (_id == 7) //total
		{
			go_totalscore.SetActive (true);
			go_totalscore.transform.FindChild("text_TotalScoreValue").GetComponent<UILabel>().text = ScoreValue_TotalScore.ToString();
		}
		else if (_id == 100) //reward gold
		{
			go_reward_Gold.SetActive (true);
			
		}
	}
	
	
	
	
	public void Click_Fail_Retry()
	{
		Application.LoadLevel ("GameStage");
	}
	
	public void Click_Fail_MovetoMap()
	{
		Application.LoadLevel ("StageScene");
	}
	
	
	
	public void setUp_Success()
	{
		go_popup_score.SetActive (true);
		go_popup_score.transform.localPosition = new Vector3(0, 800, 0);
		
		Vector3 v3pos = go_popup_score.transform.localPosition;
		v3pos.y = 0;
		
		efm.Play(8);
		
		TweenPosition twPosition = TweenPosition.Begin (go_popup_score, 0.5f, v3pos);
		twPosition.method = UITweener.Method.BounceIn;
		EventDelegate.Add (twPosition.onFinished, setUp_Success_Sub1, true);
		
		//Debug.Log (go_popup_score.transform.localPosition);
	}
	
	private void setUp_Success_Sub1()
	{
		go_popup_score.transform.localPosition = new Vector3 (0, 0, 0);
		go_popup_score.GetComponent<EndScoreScript>().play();
	}
	
	
	
	
	int[] rewardIndex = new int[5];
	int[] rewardCount = new int[5];
	
	int rewardGoldValue = 30000;
	GameObject go_reward_Gold;
	
	public void setUp_Reward()
	{
		TweenPosition twPotion = TweenPosition.Begin (go_popup_score, 0.3f, new Vector3 (0, -800, 0));
		twPotion.method = UITweener.Method.BounceOut;
		EventDelegate.Add (twPotion.onFinished, callback_finish_moveout_popup_score, true);
		
		//go_popup_score.SetActive (false);
		go_popup_reward.SetActive (true);
		
		//		go_reward_Gold = GameObject.Find ("reward_Gold");
		//		go_reward_Gold.SetActive (false);
		
		if (bInventoryMaxPlusText) 
		{
			go_popup_reward.transform.FindChild("reward_Gold").transform.localPosition = new Vector3(110, 0, 0);
		} 
		else 
		{
			go_popup_reward.transform.FindChild("reward_Gold").transform.localPosition = new Vector3(110, -50, 0);
			go_popup_reward.transform.FindChild("reward_Star").gameObject.SetActive(false);
		}
		
		
		//reward gold setting
		rewardGoldValue = 1000 + (PlayerData.nowMapNumber1 * 5) +  (userScore/ 20);
		cwSetText ("text_rewardGoldValue", rewardGoldValue.ToString());
		
		//reward giftbox setting
		for (int i=0; i<5; i++) 
		{
			rewardIndex[i] = 0;
			rewardCount[i] = 0;
		}
		
		int howManyMake = 1 + PlayerData.nowMapNumber1/40;
		if (howManyMake > 5) 
		{
			howManyMake = 5;
		}
		
		for (int i=0; i<howManyMake; i++) 
		{
			makeRewardList(i);
		}
		
		GameObject go_item = null;
		for (int i=0; i<5; i++) 
		{
			go_item = GameObject.Find("RewardItem"+i);
			go_item.GetComponent<RewardItemScript>().setItem(rewardIndex[i], rewardCount[i]);
		}
		
		efm.Play(8);
		
		TweenPosition twPotion1 = TweenPosition.Begin (go_popup_reward, 0.3f, new Vector3 (0, 0, 0));
		twPotion1.delay = 0.3f;
		twPotion1.method = UITweener.Method.BounceIn;
		EventDelegate.Add (twPotion1.onFinished, callback_finish_moveout_popup_reward, true);
		
	}
	
	private void callback_finish_moveout_popup_reward()
	{
		//record
		
		
		pd.life++;
		pd.saveLifeTime = System.DateTime.UtcNow.ToFileTimeUtc();
		
		pd.gold += rewardGoldValue;
		
		saveReward ();
		
		if (bInventoryMaxPlusText) 
		{
			pd.invenMax++;
		}
		
		//next stage open
		pd.isOpen [PlayerData.nowMapNumber1 + 1] = 1;
		pd.lastStage = PlayerData.nowMapNumber1 + 1;
		
		if(pd.isOpen [240] == 1)
		{
			if(pd.invenOpenLevel < 23)
			{
				pd.invenOpenLevel = 23;
			}
		}
		else if(pd.isOpen [200] == 1)
		{
			if(pd.invenOpenLevel < 15)
			{
				pd.invenOpenLevel = 15;
			}
		}
		else if(pd.isOpen [20] == 1)
		{
			if(pd.invenOpenLevel < 7)
			{
				pd.invenOpenLevel = 7;
			}
		}
		
		
		if (pd.record [PlayerData.nowMapNumber1] < ScoreValue_TotalScore) 
		{
			pd.record [PlayerData.nowMapNumber1] = ScoreValue_TotalScore;
		}
		
		if (nowStarIndex > pd.star [PlayerData.nowMapNumber1]) 
		{
			pd.star [PlayerData.nowMapNumber1] = (byte)(nowStarIndex);
		}
		
		pd.saveData ();
	}
	
	
	private void saveReward()
	{
		int tempIndex = 0;
		for (int i=0; i<5; i++) 
		{
			switch(rewardIndex[i])
			{
			case 0:
				break;
				
			case 10: 
			case 11:
			case 12:
			case 13:
			case 14:
			case 15:
			case 16:
				tempIndex = rewardIndex[i]-10;
				pd.pipes[tempIndex] += rewardCount[i];
				if(pd.pipes[tempIndex] > pd.invenMax)
				{
					pd.pipes[tempIndex] = pd.invenMax;
				}
				break;
				
			case 70: 
			case 71:
			case 72:
			case 73:
			case 74:
			case 75:
			case 76:
			case 77:
				tempIndex = rewardIndex[i]-70+7;
				pd.pipes[tempIndex] += rewardCount[i];
				if(pd.pipes[tempIndex] > pd.invenMax)
				{
					pd.pipes[tempIndex] = pd.invenMax;
				}
				break;
				
			case 80: 
			case 81:
			case 82:
			case 83:
			case 84:
			case 85:
			case 86:
			case 87:
				tempIndex = rewardIndex[i]-80+15;
				pd.pipes[tempIndex] += rewardCount[i];
				if(pd.pipes[tempIndex] > pd.invenMax)
				{
					pd.pipes[tempIndex] = pd.invenMax;
				}
				break;
				
			case 300:
				pd.gold += rewardCount[i];
				break;
				
			case 310:
				pd.gem += rewardCount[i];
				break;
				
			case 400:	//time 30 extention
				pd.consumeItem[6] += rewardCount[i];
				if(pd.consumeItem[6] > 99)
				{
					pd.consumeItem[6] = 99;
				}
				break;
				
			case 401:	//perfect plan
				pd.consumeItem[4] += rewardCount[i];
				if(pd.consumeItem[4] > 99)
				{
					pd.consumeItem[4] = 99;
				}
				break;
				
			case 402:	//turn on water
				pd.consumeItem[5] += rewardCount[i];
				if(pd.consumeItem[5] > 99)
				{
					pd.consumeItem[5] = 99;
				}
				break;
			}
			
		}
	}
	//checkNextScoreFrame(0);
	//		stateScoreFrame = 1;	//cross
	//		currentScoreFrameIndex = 0;
	//		scoreTimer = 0;
	private void callback_finish_moveout_popup_score()
	{
		go_popup_score.SetActive (false);
	}
	
	private void makeRewardList(int _index)
	{
		if (RandRatio (1)) 
		{
			//gem
			rewardIndex[_index] = 310;
			rewardCount[_index] = 1;
		} else {
			if (RandRatio (3)) 
			{
				//item and gold
				if (RandRatio (50)) 
				{
					rewardIndex[_index] = 300;
					rewardCount[_index] = 10*Random.Range(0, 101);
				}
				else
				{
					switch(Random.Range(0, 3))
					{
					case 0:
						rewardIndex[_index] = 400;
						rewardCount[_index] = 1;
						break;
						
					case 1:
						rewardIndex[_index] = 401;
						rewardCount[_index] = 1;
						break;
						
					case 2:
						rewardIndex[_index] = 402;
						rewardCount[_index] = 1;
						break;
					}
				}
			}
			else
			{
				//pipe
				if(pd.invenOpenLevel == 6)
				{
					rewardIndex[_index] = Random.Range(10, 16);
					rewardCount[_index] = Random.Range(1, 6);
				}
				else if(pd.invenOpenLevel == 7)
				{
					rewardIndex[_index] = Random.Range(10, 17);
					rewardCount[_index] = Random.Range(1, 6);
				}
				else if(pd.invenOpenLevel == 15)
				{
					if(RandRatio(70))
					{
						rewardIndex[_index] = Random.Range(10, 17);
						rewardCount[_index] = Random.Range(1, 6);
					}
					else
					{
						rewardIndex[_index] = Random.Range(70, 78);
						rewardCount[_index] = Random.Range(1, 6);
					}
				}
				else if(pd.invenOpenLevel == 23)
				{
					if(RandRatio(70))
					{
						rewardIndex[_index] = Random.Range(10, 17);
						rewardCount[_index] = Random.Range(1, 6);
					}
					else
					{
						if(RandRatio(70))
						{
							rewardIndex[_index] = Random.Range(70, 78);
							rewardCount[_index] = Random.Range(1, 6);
						}
						else
						{
							rewardIndex[_index] = Random.Range(80, 88);
							rewardCount[_index] = Random.Range(1, 6);
						}
						
					}
				}
			}
		}
	}
	
//	public void showLimitTime(float _time)
//	{
//		GameObject go = GameObject.Find ("text_LimitTime");
//		if (go == null) 
//		{
//			Debug.Log("go is null");
//		}
//		
//		int minute = (int)(_time/60);
//		int second = (int)(_time%60);
//		go.GetComponent<UILabel>().text = string.Format("{0:D2}", minute) + ":"+string.Format("{0:D2}", second);
//	}
	
	public void Close_Reward()
	{
		Application.LoadLevel ("StageScene");
	}
	
	
	
	public int getID_EatItem(POS _pos)
	{
		return mapItem [_pos.y] [_pos.x];
	}
	
	public void checkEatItem(int _xpos, int _ypos)
	{
		GameObject _go = null;
		
		int tmpItemID = mapItem [_ypos] [_xpos];
		if (tmpItemID > 0) 
		{
			_go = GameObject.Find ("GItem" + (_ypos * nCol + _xpos));
			Vector3 v3pos = _go.transform.localPosition;
			createGItemEffect(_xpos, _ypos, _go);
			
			InitEatItem(_xpos, _ypos);
			Destroy(_go);
			
			
			switch(tmpItemID)
			{
			case 10:	//star
				createStarEffect();
				break;
				
			case 20:	//GiftBox
				break;
				
			case 30:	//Coin
				createTextNum(v3pos.x, v3pos.y, "GOLD +1000", 0);
				pd.gold += 1000;
				pd.saveData();
				break;
				
			case 40:	//Gem
				createTextNum(v3pos.x, v3pos.y, "GEM +1", 1);
				pd.gem++;
				pd.saveData();
				break;
				
			case 50:	//Bomb 30s
				break;
				
			case 51:	//Bomb 40s
				break;
				
			case 52:	//Bomb 50s
				break;
				
			case 53:	//Bomb 60s
				break;
				
			case 54:	//Bomb 90s
				break;

			}
		}
	}

	int[] randomPos = {0, 1, 2, 3};
	private void mixRandomPos()
	{
		int swap1 = 0;
		int swap2 = 0;
		int swap3 = 0;

		for(int i=0; i<20; i++)
		{
			swap1 = Random.Range(0, 4);
			swap2 = Random.Range(0, 4);

			swap3 = randomPos[swap1];
			randomPos[swap1] = randomPos[swap2];
			randomPos[swap2] = swap3;
		}
	}

	public void checkPlusItem(int _xpos, int _ypos)
	{


		GameObject _go = null;
		bool bEscape = false;
		
		int tmpItemID = mapItem [_ypos] [_xpos];
		if (tmpItemID > 0) 
		{
			_go = GameObject.Find ("GItem" + (_ypos * nCol + _xpos));
			Vector3 v3pos = _go.transform.localPosition;
			createGItemEffect(_xpos, _ypos, _go);
			
			InitEatItem(_xpos, _ypos);
			Destroy(_go);
			
			
			switch(tmpItemID)
			{
				//plus item
			case 60:	//all direction
				if(checkLeft(_xpos, _ypos))
				{
					changePref_Tile_PlusPipe(_xpos-1, _ypos, 3);
				}
				if(checkRight(_xpos, _ypos))
				{
					changePref_Tile_PlusPipe(_xpos+1, _ypos, 1);
				}
				if(checkDown(_xpos, _ypos))
				{
					changePref_Tile_PlusPipe(_xpos, _ypos+1, 2);
				}
				if(checkUp(_xpos, _ypos))
				{
					changePref_Tile_PlusPipe(_xpos, _ypos-1, 0);
				}
				break;
			case 61:	//left, right
				if(checkLeft(_xpos, _ypos))
				{
					changePref_Tile_PlusPipe(_xpos-1, _ypos, 3);
				}
				if(checkRight(_xpos, _ypos))
				{
					changePref_Tile_PlusPipe(_xpos+1, _ypos, 1);
				}
				break;
			case 62:	//up, down
				if(checkDown(_xpos, _ypos))
				{
					changePref_Tile_PlusPipe(_xpos, _ypos+1, 2);
				}
				if(checkUp(_xpos, _ypos))
				{
					changePref_Tile_PlusPipe(_xpos, _ypos-1, 0);
				}
				break;
			case 63:	//up
				if(checkUp(_xpos, _ypos))
				{
					changePref_Tile_PlusPipe(_xpos, _ypos-1, 0);
				}
				break;
			case 64:	//right
				if(checkRight(_xpos, _ypos))
				{
					changePref_Tile_PlusPipe(_xpos+1, _ypos, 1);
				}
				break;
			case 65:	//down
				if(checkDown(_xpos, _ypos))
				{
					changePref_Tile_PlusPipe(_xpos, _ypos+1, 2);
				}
				break;
			case 66:	//left
				if(checkLeft(_xpos, _ypos))
				{
					changePref_Tile_PlusPipe(_xpos-1, _ypos, 3);
				}
				break;
			case 67:	//random

				mixRandomPos();

				for(int i = 0; i<4; i++)
				{
					if(bEscape) continue;
					switch(randomPos[i])
					{
					case 0:	//up
						if(checkUp(_xpos, _ypos))
						{
							if(changePref_Tile_PlusPipe(_xpos, _ypos-1, 0)){bEscape = true;}
						}
						break;
					case 1:	//right
						if(checkRight(_xpos, _ypos))
						{
							if(changePref_Tile_PlusPipe(_xpos+1, _ypos, 1)){bEscape = true;}
						}
						break;
					case 2:	//down
						if(checkDown(_xpos, _ypos))
						{
							if(changePref_Tile_PlusPipe(_xpos, _ypos+1, 2)){bEscape = true;}
						}
						break;
					case 3:	//left
						if(checkLeft(_xpos, _ypos))
						{
							if(changePref_Tile_PlusPipe(_xpos-1, _ypos, 3)){bEscape = true;}
						}
						break;
					}
				}
				break;

			case 68:	//all 
				if(checkLeft(_xpos, _ypos))
				{
					changePref_Tile_PlusPipe(_xpos-1, _ypos, 3);
					if(checkLeft(_xpos-1, _ypos))
					{
						changePref_Tile_PlusPipe(_xpos-2, _ypos, 7);
					}
				}
				if(checkRight(_xpos, _ypos))
				{
					changePref_Tile_PlusPipe(_xpos+1, _ypos, 1);
					if(checkRight(_xpos+1, _ypos))
					{
						changePref_Tile_PlusPipe(_xpos+2, _ypos, 5);
					}
				}
				if(checkDown(_xpos, _ypos))
				{
					changePref_Tile_PlusPipe(_xpos, _ypos+1, 2);
					if(checkDown(_xpos, _ypos+1))
					{
						changePref_Tile_PlusPipe(_xpos, _ypos+2, 6);
					}
				}
				if(checkUp(_xpos, _ypos))
				{
					changePref_Tile_PlusPipe(_xpos, _ypos-1, 0);
					if(checkUp(_xpos, _ypos-1))
					{
						changePref_Tile_PlusPipe(_xpos, _ypos-2, 4);
					}
				}
				break;
			case 69:	//left, right
				if(checkLeft(_xpos, _ypos))
				{
					changePref_Tile_PlusPipe(_xpos-1, _ypos, 3);
					if(checkLeft(_xpos-1, _ypos))
					{
						changePref_Tile_PlusPipe(_xpos-2, _ypos, 7);
					}
				}
				if(checkRight(_xpos, _ypos))
				{
					changePref_Tile_PlusPipe(_xpos+1, _ypos, 1);
					if(checkRight(_xpos+1, _ypos))
					{
						changePref_Tile_PlusPipe(_xpos+2, _ypos, 5);
					}
				}
				break;
			case 70:	//up, down
				if(checkDown(_xpos, _ypos))
				{
					changePref_Tile_PlusPipe(_xpos, _ypos+1, 2);
					if(checkDown(_xpos, _ypos+1))
					{
						changePref_Tile_PlusPipe(_xpos, _ypos+2, 6);
					}
				}
				if(checkUp(_xpos, _ypos))
				{
					changePref_Tile_PlusPipe(_xpos, _ypos-1, 0);
					if(checkUp(_xpos, _ypos-1))
					{
						changePref_Tile_PlusPipe(_xpos, _ypos-2, 4);
					}
				}
				break;
			case 71:	//up
				if(checkUp(_xpos, _ypos))
				{
					changePref_Tile_PlusPipe(_xpos, _ypos-1, 0);
					if(checkUp(_xpos, _ypos-1))
					{
						changePref_Tile_PlusPipe(_xpos, _ypos-2, 4);
					}
				}
				break;
			case 72:	//right
				if(checkRight(_xpos, _ypos))
				{
					changePref_Tile_PlusPipe(_xpos+1, _ypos, 1);
					if(checkRight(_xpos+1, _ypos))
					{
						changePref_Tile_PlusPipe(_xpos+2, _ypos, 5);
					}
				}
				break;
			case 73:	//down
				if(checkDown(_xpos, _ypos))
				{
					changePref_Tile_PlusPipe(_xpos, _ypos+1, 2);
					if(checkDown(_xpos, _ypos+1))
					{
						changePref_Tile_PlusPipe(_xpos, _ypos+2, 6);
					}
				}
				break;
			case 74:	//left
				if(checkLeft(_xpos, _ypos))
				{
					changePref_Tile_PlusPipe(_xpos-1, _ypos, 3);
					if(checkLeft(_xpos-1, _ypos))
					{
						changePref_Tile_PlusPipe(_xpos-2, _ypos, 7);
					}
				}
				break;
			case 75:	//random
				mixRandomPos();
				
				for(int i = 0; i<4; i++)
				{
					if(bEscape) continue;
					switch(randomPos[i])
					{
					case 0:	//up
						if(checkUp(_xpos, _ypos))
						{
							if(changePref_Tile_PlusPipe(_xpos, _ypos-1, 0)){bEscape = true;}
							if(checkUp(_xpos, _ypos-1))
							{
								if(changePref_Tile_PlusPipe(_xpos, _ypos-2, 4)){bEscape = true;}
							}
						}
						break;
					case 1:	//right
						if(checkRight(_xpos, _ypos))
						{
							if(changePref_Tile_PlusPipe(_xpos+1, _ypos, 1)){bEscape = true;}
							if(checkRight(_xpos+1, _ypos))
							{
								if(changePref_Tile_PlusPipe(_xpos+2, _ypos, 5)){bEscape = true;}
							}
						}
						break;
					case 2:	//down
						if(checkDown(_xpos, _ypos))
						{
							if(changePref_Tile_PlusPipe(_xpos, _ypos+1, 2)){bEscape = true;}
							if(checkDown(_xpos, _ypos+1))
							{
								if(changePref_Tile_PlusPipe(_xpos, _ypos+2, 6)){bEscape = true;}
							}
						}
						break;
					case 3:	//left
						if(checkLeft(_xpos, _ypos))
						{
							if(changePref_Tile_PlusPipe(_xpos-1, _ypos, 3)){bEscape = true;}
							if(checkLeft(_xpos-1, _ypos))
							{
								if(changePref_Tile_PlusPipe(_xpos-2, _ypos, 7)){bEscape = true;}
							}
						}
						break;
					}
				}
				break;
				
			}
		}
	}

	public bool checkRight(int _xpos, int _ypos)
	{
		bool returnValue = false;
		if(_xpos+1 < nCol)
		{
			returnValue = true;
		}
		return returnValue;
	}

	public bool checkLeft(int _xpos, int _ypos)
	{
		bool returnValue = false;
		if(_xpos-1 >= 0)
		{
			returnValue = true;
		}
		return returnValue;
	}

	public bool checkDown(int _xpos, int _ypos)
	{
		bool returnValue = false;
		if(_ypos+1 < nRow)
		{
			returnValue = true;
		}
		return returnValue;
	}
	
	public bool checkUp(int _xpos, int _ypos)
	{
		bool returnValue = false;
		if(_ypos-1 >= 0)
		{
			returnValue = true;
		}
		return returnValue;
	}
	
	public void createNewRecordEffect()
	{
		Vector3 v3pos = new Vector3(-234, -152, 0);
		
		GameObject _go = Instantiate(Resources.Load ("cwPrefabs/NewRecordEffect"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		
		_go.transform.parent = GameObject.Find("Popup_Score").transform;
		
		_go.transform.localScale = new Vector3(10, 10, 1);
		_go.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 20));
		
		_go.transform.localPosition = v3pos;
		
		//TweenRotation twRotation = TweenRotation.Begin(_go, 0.5f, Quaternion.Euler(Time.deltaTime, Time.deltaTime, 0f));
		_go.GetComponent<NewRecordScript> ().move ();
	}
	
	public void createStarEffect()
	{
		efm.Play (2);
		
		Vector3 v3pos = new Vector3(0, 0, 0);
		GameObject _go = Instantiate(Resources.Load ("cwPrefabs/StarEffect"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		
		_go.transform.parent = GameObject.Find("Panel_Star").transform;
		
		_go.transform.localScale = new Vector3(2, 2, 1);
		Vector3 v3pos2 = GameObject.Find ("Anchor_TopRight").transform.localPosition;
		Vector3 v3pos1 = GameObject.Find ("STAR"+nowStarIndex).transform.localPosition;
		
		
		v3pos.x -= v3pos2.x;
		v3pos.y -= (v3pos2.y-100);
		_go.transform.localPosition = v3pos;
		
		if (nowStarIndex >= 3) 
		{
			_go.GetComponent<StarEffectScript> ().move_perfect ();
		}
		else 
		{
			_go.GetComponent<StarEffectScript> ().move ();
		}
	}
	
	
	public void createGItemEffect(int _xpos, int _ypos, GameObject _go_Item)
	{
		
		efm.Play (2);
		Vector3 v3pos = _go_Item.transform.localPosition;
		Vector3 v3pos1 = _go_Item.transform.FindChild("ItemIcon").transform.localPosition;
		
		v3pos.y += (v3pos1.y/2);
		
		GameObject _go = Instantiate(Resources.Load ("cwPrefabs/GItemEffect"), new Vector3(300, 300, 0), Quaternion.identity) as GameObject;
		
		_go.transform.parent = go_parent_Item.transform;
		
		_go.transform.localScale = new Vector3(1, 1, 1);
		
		_go.transform.localPosition = v3pos;
		
		//_go.GetComponent<TileEffectScript> ().play ();
	}
	
	public void createGItemEffectOnly(float _x, float _y)
	{
		
		//efm.Play (2);
		Vector3 v3pos = new Vector3(_x, _y, 0);
		
		GameObject _go = Instantiate(Resources.Load ("cwPrefabs/GItemEffect"), new Vector3(300, 300, 0), Quaternion.identity) as GameObject;
		
		_go.transform.parent = go_parent_Item.transform;
		
		_go.transform.localScale = new Vector3(1, 1, 1);
		
		_go.transform.localPosition = v3pos;
		
		//_go.GetComponent<TileEffectScript> ().play ();
	}
	
	
	public void createBombEffect(Vector3 _pos, GameObject _go_Item)
	{
		
		efm.Play (3);
		//Vector3 v3pos = _go_Item.transform.parent.transform.parent.transform.localPosition;
		//		Vector3 v3pos1 = _go_Item.transform.FindChild("ItemIcon").transform.localPosition;
		//		
		//		v3pos.y += (v3pos1.y/2);
		
		GameObject _go = Instantiate(Resources.Load ("cwPrefabs/BombEffect"), new Vector3(256, 256, 0), Quaternion.identity) as GameObject;
		
		_go.transform.parent = go_parent_Item.transform;
		
		_go.transform.localScale = new Vector3(1, 1, 1);
		
		_go.transform.localPosition = _pos;
		
		//_go.GetComponent<TileEffectScript> ().play ();
	}
	
	
	//	public void createPref_FixedTile(int _pipeID, Vector3 _pos, int _idx, int _xpos, int _ypos, int _layer)
	public void createPref_FixedTile(int _pipeID, Vector3 _v3pos, int _xpos, int _ypos)
	{
		//string prefabsPath;
		
		//prefabsPath = "cwPrefabs/Prefab_Pipe";
		
		//setAtlasSet (baseTileNum);
		
		//GameObject _go = Instantiate(Resources.Load (prefabsPath), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		
		
		sPipe[getID(_ypos, _xpos)].setID(_pipeID, 0);
		//		_go.name = "Pipe"+ (_ypos*nCol+_xpos);
		//		_go.transform.parent = go_parent.transform;
		//		_go.transform.localScale = new Vector3(1, 1, 1);
		
		//setting button index....
		//_go.GetComponent<PM>().pos.setPos(_pos.x, _pos.y);	//very important.....
		map[_ypos][_xpos].setInfo(_pipeID, (_ypos*nCol+_xpos) , 0, _xpos, _ypos);
		//_go.GetComponent<PM>().posInfo.setInfo(_pipeID, _idx , 0, _xpos, _ypos);
		
		
		//		_go.transform.localPosition = _v3pos;
	}
	
	
	
	
	public void changeFireID()
	{
		
		if (nowFireID >= 10 && nowFireID <= 35) 
		{
			nowFireID += 30;
			//Debug.Log ("nowFireID:  "+nowFireID);
		} else if (nowFireID >= 70 && nowFireID <= 87) 
		{
			nowFireID += 20;
		}
	}
	
	public void InitEatItem(int _xpos, int _ypos)
	{
		mapItem[_ypos][_xpos] = 0;
	}
	
	
	
	public bool checkPerfectClear()
	{
		//Debug.Log ("1");
		bool perfect = true;
		for(int i=0; i<nRow; i++)
		{
			for(int j=0; j<nCol; j++)
			{
				if(perfectBuf[i][j] == 1 )
				{
					if(map[i][j].endCount == 0)
					{
						perfect = false;
					}
				}
			}
		}
		return perfect;
	}
	
	
	
	public void cwSetText(string _objectName, string _text)
	{
		GameObject.Find(_objectName).GetComponent<UILabel>().text = _text;
	}
	
	public void toggleRightItemActive(bool _toggle)
	{
		if(_toggle)
		{
			GameObject.Find("stageItem0").GetComponent<BoxCollider>().enabled = true;
			GameObject.Find("stageItem1").GetComponent<BoxCollider>().enabled = true;
			GameObject.Find("stageItem3").GetComponent<BoxCollider>().enabled = true;
			GameObject.Find("stageItem4").GetComponent<BoxCollider>().enabled = true;
		}
		else
		{
			GameObject.Find("stageItem0").GetComponent<BoxCollider>().enabled = false;
			GameObject.Find("stageItem1").GetComponent<BoxCollider>().enabled = false;
			GameObject.Find("stageItem3").GetComponent<BoxCollider>().enabled = false;
			GameObject.Find("stageItem4").GetComponent<BoxCollider>().enabled = false;
		}
	}
	
	
	
	/////////////////////////////////////////////////////////////////////////////
	void debug(int _number)
	{
		Debug.Log ("DebugCodeNum: "+_number);
	}
}
