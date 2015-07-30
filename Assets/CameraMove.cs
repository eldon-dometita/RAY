//using UnityEngine;
//using System.Collections;
//using System.IO;
//
//
//public class CameraMove : MonoBehaviour {
//
//	int[] SBX = {
//		115,234,407,550,443,271,297,416,496,681,772,647,650,777,934,976,1121,1225,1134,1122,
//		1220,1566,1560,1606,1756,1864,1839,1791,1798,1913,2059,2065,2077,2235,2358,2375,2524,2612,2536,2400,
//		2304,2443,2609,2727,
//
//		2990,2950,3087,3222,3291,3238,3140,3088,3238,3389,3535,3656,3799,3835,3805,3842,3972,4119,4240,4213,
//		4123,4121,4259,4495,4551,4681,4831,4877,4813,4681,4777,4638,4934,5035,5056,5129,5267,5425,5580,5654,
//		5636,5706,6020,6148,
//
//		6121,6220,6366,6462,6478,6485,6572,6711,6859,6994,7090,7001,6854,6753,6756,6861,7001,7141,7394,7418,
//		7320,7406,7561,7710,7835,7840,7744,7601,7577,7685,7835,7914,7969,8114,8123,8257,8426,8568,8571,8436,
//		8297,8338,8486,8584
//	};
//
//	int[] SBY = {
//		-235,-323,-314,-214,-119,-109,28,149,292,325,205,98,-35,-92,-5,158,280,158,33,-113,
//		-240,4,131,264,319,227,98,-39,-177,-272,-274,-133,5,12,126,274,325,195,50,-61,
//		-195,-300,-217,-100,
//
//		109,227,264,209,88,-47,-166,-298,-334,-268,-242,-308,-256,-125,5,135,209,218,144,15,
//		-106,-237,-264,35,162,246,245,120,-9,-105,-289,-242,-263,-152,-9,122,197,226,185,72,
//		-72,-184,-36,-93,
//
//		-229,-299,-286,-185,-55,72,185,234,235,205,120,26,-19,-108,-231,-298,-299,-266,-51,63,
//		156,268,286,269,197,74,-37,-111,-239,-313,-315,-202,-65,-121,-262,-323,-311,-214,-82,-16,
//		36,172,202,95
//	};
//
//	int[] DX = {
//		148,162,183,288,311,338,468,490,512,530,514,491,381,352,324,242,242,252,331,352,
//		373,445,455,464,556,583,610,727,745,753,730,718,698,617,610,615,670,688,710,838,
//		858,883,951,955,960,1014,1028,1050,1174,1189,1195,1198,1186,1171,1106,1098,1095,1122,1136,1156,
//		1285,1311,1332,1358,1457,1475,1499,1517,1535,1554,1553,1554,1545,1553,1564,1651,1671,1696,1800,1824,
//		1842,1859,1855,1849,1814,1806,1798,1776,1773,1772,1810,1830,1853,1957,1976,1997,2101,2105,2097,2020,
//		2012,2017,2127,2148,2173,2292,2316,2335,2351,2347,2352,2415,2438,2467,2572,2588,2600,2599,2586,2571,
//		2487,2465,2444,2349,2328,2311,2316,2336,2363,2500,2530,2560,2644,2653,2672,2789,2810,2832,2850,2923,
//		2939,2955,2972,
//
//		2962,2952,2946,2983,3007,3032,3135,3156,3175,3253,3271,3283,3293,3286,3274,3193,3175,3160,3099,3090,
//		3086,3125,3151,3180,3290,3313,3333,3435,3463,3489,3566,3582,3599,3707,3731,3751,3826,3836,3839,3812,
//		3808,3806,3805,3809,3814,3878,3896,3915,4025,4045,4065,4165,4184,4203,4239,4234,4228,4171,4156,4143,
//		4100,4096,4097,4153,4176,4500,4199,4318,4336,4353,4370,4448,4459,4469,4477,4502,4511,4520,4584,4604,
//		4623,4737,4758,4780,4864,4878,4884,4863,4858,4846,4756,4737,4718,4636,4629,4627,4675,4696,4719,4830,
//		4854,4877,4982,4998,5012,5035,5035,5038,5073,5080,5090,5171,5189,5212,5319,5342,5366,5478,5502,5525,
//		5618,5633,5646,5641,5636,5634,5636,5646,5661,5755,5780,5806,5831,5931,5945,5962,5974,6077,6102,6122,
//		6143,6138,6130,
//
//		6128,6148,6170,6268,6290,6311,6412,6426,6439,6473,6473,6473,6470,6468,6472,6501,6512,6525,6616,6636,
//		6658,6763,6785,6807,6906,6926,6946,7032,7048,7062,7077,7065,7050,6946,6927,6908,6804,6787,6774,6734,
//		6732,6737,6780,6796,6816,6907,6927,6951,7049,7068,7087,7186,7204,7224,7242,7320,7337,7353,7369,7434,
//		7443,7441,7383,7369,7347,7320,7331,7350,7459,7483,7505,7615,7637,7659,7757,7777,7801,7851,7855,7850,
//		7811,7799,7783,7691,7671,7647,7566,7560,7563,7593,7610,7633,7896,7912,7916,7911,7916,7930,8022,8050,
//		8075,8123,8123,8117,8147,8169,8198,8315,8343,8369,8483,8507,8528,8586,8594,8594,8536,8512,8486,8381,
//		8356,8331,8287,8289,8299,8385,8408,8430,8526,8538,8545,8620,8643,8670,8692,8715,8734,8811,8833,8849,
//		8862,8871,8883
//	};
//
//	int[] DY = {
//		-276,-293,-308,-331,-334,-332,-293,-281,-265,-168,-147,-138,-132,-135,-133,-67,-44,-21,73,90,
//		101,195,215,239,314,325,326,301,281,259,158,144,131,48,27,6,-75,-91,-96,-83,
//		-74,-56,43,67,96,207,226,237,252,231,209,107,89,75,-19,-39,-61,-165,-183,-200,
//		-220,-207,-193,-174,-106,-92,-76,-58,-41,48,68,89,180,203,223,299,308,316,307,293,
//		275,179,159,139,45,25,5,-88,-111,-132,-220,-238,-253,-286,-290,-291,-239,-215,-185,-100,
//		-74,-39,7,6,6,32,47,69,171,200,225,314,323,327,300,279,250,143,115,92,
//		3,-13,-25,-102,-121,-144,-244,-263,-279,-301,-290,-267,-170,-145,-119,-72,-63,-50,-37,23,
//		37,51,67,
//
//		150,167,186,264,272,272,252,243,232,176,157,135,41,17,-4,-91,-108,-125,-209,-229,
//		-252,-327,-337,-340,-327,-312,-298,-234,-227,-227,-272,-286,-300,-310,-305,-293,-215,-194,-170,-81,
//		-60,-38,49,71,91,171,181,189,217,219,219,200,190,179,99,77,57,-29,-46,-64,
//		-151,-171,-193,-268,-274,0,-275,-242,-230,-215,-196,-80,-58,-34,-10,80,99,118,203,216,
//		226,264,265,260,210,190,168,72,49,28,-41,-52,-65,-149,-171,-196,-271,-279,-286,-292,
//		-292,-287,-231,-215,-197,-104,-80,-54,37,59,79,157,169,179,222,226,227,216,211,203,
//		155,138,117,25,2,-23,-119,-139,-157,-196,-197,-196,-193,-135,-119,-98,-77,-31,-36,-52,
//		-142,-164,-186,
//
//		-271,-283,-290,-310,-312,-310,-258,-245,-229,-142,-123,-103,-12,8,28,112,128,145,214,220,
//		224,234,234,234,231,226,218,185,173,159,79,64,51,8,2,-2,-47,-57,-71,-150,
//		-169,-189,-261,-271,-279,-301,-302,-302,-298,-295,-289,-235,-223,-209,-193,-134,-122,-108,-91,-14,
//		4,28,98,115,122,200,221,236,277,279,279,283,281,277,258,248,232,156,134,115,
//		32,13,-3,-66,-74,-82,-157,-176,-198,-277,-291,-302,-296,-274,-248,-152,-126,-108,-62,-69,
//		-85,-167,-191,-215,-299,-314,-322,-324,-324,-322,-287,-277,-260,-170,-147,-122,-45,-32,-25,-11,
//		-6,5,83,105,127,204,209,209,173,154,130,65,52,50,59,76,93,153,166,184,
//		204,226,247
//	};
//
//	int[] BlackWholeX = {
//		1411, 2903, 4414, 5892, 7290, 8782
//	};
//
//	int[] BlackWholeY = {
//		-132, -3, -132, -165, -162, 127
//	};
//
//
//	int[] PlanetX = {
//		0, 662, 1922
//	};
//	
//	int[] PlanetY = {
//		400, -64, 419
//	};
//
//
//
//	string[] strStageButtonImage = {
//		"StageButton1", 
//		"StageButton2",
//		"StageButton3",
//		"StageButton4",
//		"StageButton5",
//		"StageButton6"
//	};
//
//	public int nowStageIndex = 0;			//this is userdata!!!!
//	BLACKWHOLE nowBlackWhole;		//this is userdata!!!!
//	public bool bAutoFocus = false;
//	float axisValue = 0;
//	//tempStarValue.... later change userData star...
//	int[] tmpStar = {
////		1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
////		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
////		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
////		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
////		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
////		
////		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
////		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
////		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
////		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
////		0, 0, 0, 0, 0, 0, 0, 0, 0, 0
//
//		1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
//		1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
//		1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
//		1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
//		1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
//		
//		1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
//		1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
//		1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
//		1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
//		1, 1, 1, 1, 1, 1, 1, 1, 1, 1 
//	};
//
//
//	Camera cam;
//	int Alive_LX;
//	int Alive_RX;
//
//	GameObject bgPattern;
//	GameObject StageButtonObj;
//	GameObject DotObj;
//	GameObject BlackWholeObj;
//	GameObject PlanetObj;
//	GameObject BlackSquare;
//
//	GameObject pan;
//
//	int ScreenHX;
//
//	Vector3 prePos;
//	Vector3 nowPos;
//
//
//	// Use this for initialization
//	void Awake () {
//
//
//		Alive_LX = -200;
//		Vector3 v3pos = GameObject.Find ("Anchor_Right").transform.localPosition;
//		Alive_RX = (int)(v3pos.x * 2 + 100);
//
////		camObj = GameObject.Find( "Camera" );
////		if ( camObj != null )
////		{
////			// Should output the real dimensions of scene viewport
////			Debug.Log( camObj.GetComponent<Camera>().pixelRect);
////		}
//
//
//		//setting temporary value -------------------
//		nowBlackWhole = new BLACKWHOLE ();
//		nowBlackWhole.id = 2;
//		nowBlackWhole.state = 0;
//		nowBlackWhole.time = 84400;	//24 hour to second
//		for (int i= 0; i<3; i++) 
//		{
//			nowBlackWhole.help[i] = false;
//		}
//		nowStageIndex = 0;
//		bAutoFocus = true;
//		//finish setting temporary value -------------------
//
//		//draw4dit = new DRAW4DIT ();
//
//		cam = GameObject.Find("Camera").GetComponent<Camera>();
//		//Debug.Log( cam.pixelRect);
//
//		pan = GameObject.Find ("Panel_Base");
//
////		Vector3 pos = cam.transform.localPosition;
////		pos.x += Screen.width / 2;
////		cam.transform.localPosition = pos;
////
//		ScreenHX = Screen.width / 2;
////		Debug.Log (Screen.width);
////		Debug.Log (Screen.height);
//
//		//BlackSquare = GameObject.Find ("FillRectSquare");
//		//BlackSquare.transform.localPosition = pos;
//
//		//makePosTextFile ();
//
//		nowPos = pan.transform.localPosition;
//		prePos = nowPos;
//
//		initStageButton ();
//	}
//
//	//DRAW4DIT draw4dit;
//
//	const int STAGE_MAX = 100;		//if value is change, don't forget...
//	const int BUTTON_MAX = 30;
//	const int DOT_MAX = 100;
//	const int BLACKWHOLE_MAX = 6;	//if value is change, don't forget...
//	const int PLANET_MAX = 3;		//if value is change, don't forget...
//
//
//
//	void initStageButton()
//	{
//		Vector3 tmpPos;
//		//UISprite tmpUISprite;
//		int nowID = 0;
//		int dotID = 0;
//		int blackWholeID = 0;
//		int PlanetID = 0;
//
//		//stage button ----------------------------------------------------------------------------------------------------------------
//		for (int i=0; i<BUTTON_MAX; i++) 
//		{
//			nowID = i;
//			StageButtonObj = GameObject.Find("SB"+i);
//
//			//set pos
//			tmpPos = StageButtonObj.transform.localPosition;
//			tmpPos.x = SBX[nowID];
//			tmpPos.y = SBY[nowID];
//			StageButtonObj.transform.localPosition = tmpPos;
//
//			//set button Image
//			StageButtonObj.GetComponent<UISprite>().spriteName = strStageButtonImage[tmpStar[nowID]];
//
//			//set ID
//			StageButtonObj.GetComponent<STAGE>().setID(nowID);
//
//			if(tmpStar[nowID] >= 1)
//			{
//				StageButtonObj.transform.FindChild("text_id").GetComponent<UILabel>().text = nowID.ToString();
//				//draw4dit.drawID(nowID, new Vector2(-6, -3), StageButtonObj);
//			}
//			//Debug.Log("nowID:"+i+"---"+StageButtonObj.GetComponent<STAGE>().id);
//		}
//
//		//dot ----------------------------------------------------------------------------------------------------------------
//		for (int i=0; i<DOT_MAX; i++) 
//		{
//			dotID = i;
//			DotObj = GameObject.Find("dot"+i);
//			
//			//set pos
//			tmpPos = DotObj.transform.localPosition;
//			tmpPos.x = DX[dotID];
//			tmpPos.y = DY[dotID];
//			DotObj.transform.localPosition = tmpPos;
//
//			DotObj.GetComponent<DOT>().setID(dotID);
//		}
//
//		//blackWhole ----------------------------------------------------------------------------------------------------------------
//		for (int i=0; i<2; i++) 
//		{
//			blackWholeID = i;
//			BlackWholeObj = GameObject.Find("BW"+i);
//			
//			//set pos
//			tmpPos = BlackWholeObj.transform.localPosition;
//			tmpPos.x = BlackWholeX[blackWholeID];
//			tmpPos.y = BlackWholeY[blackWholeID];
//			BlackWholeObj.transform.localPosition = tmpPos;
//			
//			BlackWholeObj.GetComponent<BLACKWHOLE>().setID(blackWholeID);
//
//			if(nowBlackWhole.id <= blackWholeID)
//			{
//				//draw lock image
//				if(BlackWholeObj.transform.childCount > 0)
//				{
//					Destroy(BlackWholeObj.transform.GetChild(0).gameObject);
//				}
//				BlackWholeObj.GetComponent<BLACKWHOLE>().drawLock(BlackWholeObj);
//			}
//		}
//
//		//Planet ----------------------------------------------------------------------------------------------------------------
//		for (int i=0; i<3; i++) {
//			PlanetID = i;
//			PlanetObj = GameObject.Find ("Planet" + i);
//	
//				//set pos
//			tmpPos = PlanetObj.transform.localPosition;
//			tmpPos.x = PlanetX [PlanetID];
//			tmpPos.y = PlanetY [PlanetID];
//			PlanetObj.transform.localPosition = tmpPos;
//	
//			PlanetObj.GetComponent<PLANET> ().setID (PlanetID);
//		}
//	}
//	
//	// Update is called once per frame
//	int Direction;
//	Vector3 tmpPanPos;
//	int SCROLL_MAX = -10000;
//
//	void Update () 
//	{
//		nowPos = pan.transform.localPosition;
//
//		if (nowPos.x - prePos.x > 0) {
//						Direction = 1;
//				} else if (nowPos.x - prePos.x < 0) {
//						Direction = -1;
//				} else {
//						Direction = 0;
//				}
//		prePos = nowPos;
//
//		//		if (bAutoFocus) 
////		{
////			axisValue = +0.1f;
////			
////			cam.transform.Translate (axisValue, 0, 0);
////			
////			Vector3 camPos = cam.transform.localPosition;
////
////
////			if(camPos.x > SBX[nowStageIndex])
////			{
////				camPos.x = SBX[nowStageIndex];
////				bAutoFocus = false;
////			}
////
////			if (camPos.x < ScreenHX) {
////				camPos.x = ScreenHX;
////			}
////			if (camPos.x > 10080 - ScreenHX) {
////				camPos.x = 10080 - ScreenHX;
////			}
////
////			cam.transform.localPosition = camPos;
////			
////			BlackSquare.transform.localPosition = camPos;
////		} 
////		else 
////		{
////			
////			if (Input.touchCount > 0) {
////				
////				axisValue = -Input.GetTouch(0).deltaPosition.x * 0.1f;
////				cam.transform.Translate (axisValue, 0, 0);
////				
////			}
////			else if (Input.GetMouseButton (0)) {
////						axisValue = -Input.GetAxis ("Mouse X") * 0.1f;
////						
////						
////				cam.transform.Translate (axisValue, 0, 0);	
////						
////			}
////				
////			
////				
////			Vector3 camPos = cam.transform.localPosition;
////			if (camPos.x < ScreenHX) {
////				camPos.x = ScreenHX;
////			}
////			if (camPos.x > 10080 - ScreenHX) {
////				camPos.x = 10080 - ScreenHX;
////			}
////			cam.transform.localPosition = camPos;
////			
////			BlackSquare.transform.localPosition = camPos;
////			//Debug.Log(camPos.x);
////		}
//
//		tmpPanPos = pan.transform.localPosition;
//		if (tmpPanPos.x > 0) 
//		{
//			tmpPanPos.x = 0;
//			pan.transform.localPosition = tmpPanPos;
//		}
//		if (tmpPanPos.x < SCROLL_MAX - Screen.width) 
//		{
//			tmpPanPos.x = SCROLL_MAX - Screen.width;
//			pan.transform.localPosition = tmpPanPos;
//		}
//
//		calUpdate ();
//	}
//
//	void calUpdate()
//	{
//
//
////		Debug.Log ((int)cam.transform.localPosition.x);
////		Debug.Log (ScreenHX);
////		Debug.Log (Alive_LX);
////		Debug.Log (Alive_RX);
////
//
//		Vector3 tmpObjPos;
//		Vector3 tmpPos = new Vector3 (0, 0, 0);
//		int tmpID = 0;
//
//		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
//		//bg pattern check
//		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
//		for (int i=0; i<13; i++) 
//		{
//			bgPattern = GameObject.Find("BG"+i);
//
//			tmpObjPos = bgPattern.transform.localPosition;
//			tmpPos.x = tmpPanPos.x + tmpObjPos.x;
//			tmpPos.y = 0;
//			tmpPos.z = 0;
//
//
//			if((tmpPos.x < Alive_LX) && Direction < 0)
//			{
////				Debug.Log("b-------:"+tmpObjPos.x);
//				tmpObjPos.x += 2340;
////				Debug.Log("n-------:"+tmpObjPos.x);
//				bgPattern.transform.localPosition = tmpObjPos;
//			}
//			else if((tmpPos.x > Alive_RX) && Direction > 0)
//			{
//				tmpObjPos.x -= 2340;
//				bgPattern.transform.localPosition = tmpObjPos;
//
////				Debug.Log("tmpObjPos.x -= 1800;");
//			}
//
//			if(Direction != 0)
//			{
//				if(tmpObjPos.x < 1440)
//				{
//					bgPattern.GetComponent<UISprite>().spriteName = "PatternBg0";
//				}
//				else if(tmpObjPos.x >= 1440 && tmpObjPos.x < 2880)
//				{
//					bgPattern.GetComponent<UISprite>().spriteName = "PatternBg1";
//				}
//				else if(tmpObjPos.x >= 2880 && tmpObjPos.x < 4320)
//				{
//					bgPattern.GetComponent<UISprite>().spriteName = "PatternBg2";
//				}
//				else if(tmpObjPos.x >= 4320 && tmpObjPos.x < 5760)
//				{
//					bgPattern.GetComponent<UISprite>().spriteName = "PatternBg3";
//				}
//				else if(tmpObjPos.x >= 5760 && tmpObjPos.x < 7200)
//				{
//					bgPattern.GetComponent<UISprite>().spriteName = "PatternBg4";
//				}
//				else if(tmpObjPos.x >= 7200 && tmpObjPos.x < 8640)
//				{
//					bgPattern.GetComponent<UISprite>().spriteName = "PatternBg5";
//				}
//				else if(tmpObjPos.x >= 8640 && tmpObjPos.x < 10080)
//				{
//					bgPattern.GetComponent<UISprite>().spriteName = "PatternBg6";
//				}
//			}
//		
//		}
//
//
////		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
////		//stagebutton check
////		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
//		for (int i=0; i<BUTTON_MAX; i++) 
//		{
//
//			StageButtonObj = GameObject.Find("SB"+i);
//
//
//
//			tmpID = StageButtonObj.GetComponent<STAGE>().id;
//			tmpObjPos = StageButtonObj.transform.localPosition;
//			tmpPos.x = tmpPanPos.x + tmpObjPos.x;
//			tmpPos.y = 0;
//			tmpPos.z = 0;
//
////			if(i == 0)
////			{
////
////				Vector3 v3pos = StageButtonObj.transform.localPosition;
////				GameObject.Find ("text_1").GetComponent<UILabel> ().text = v3pos.x.ToString ();
////				GameObject.Find ("text_2").GetComponent<UILabel> ().text = v3pos.y.ToString ();
////				GameObject.Find ("text_3").GetComponent<UILabel> ().text = "";
////				GameObject.Find ("text_4").GetComponent<UILabel> ().text = "tmpPanPos.x: "+tmpPanPos.x.ToString ();
////			}
//
//			if((tmpPos.x < Alive_LX) && Direction < 0)
//			{
//				if(tmpID < STAGE_MAX - BUTTON_MAX)
//				{
//					tmpID += BUTTON_MAX;
//					tmpObjPos.x = SBX[tmpID];
//					tmpObjPos.y = SBY[tmpID];
//					StageButtonObj.transform.localPosition = tmpObjPos;
//
//					//set button Image
//					StageButtonObj.GetComponent<UISprite>().spriteName = strStageButtonImage[tmpStar[tmpID]];
//					
//					//set ID
//					StageButtonObj.GetComponent<STAGE>().id = tmpID;
//
//					if(tmpStar[tmpID] >= 1)
//					{
////						if(StageButtonObj.transform.childCount > 0)
////						{
////							Destroy(StageButtonObj.transform.GetChild(0).gameObject);
////						}
//						StageButtonObj.transform.FindChild("text_id").GetComponent<UILabel>().text = tmpID.ToString();
//						
//					}
//				}
//			}
//			else if((tmpPos.x > Alive_RX) && Direction > 0)
//			{
//				if(tmpID >= BUTTON_MAX)
//				{
//					tmpID -= BUTTON_MAX;
//
//					if(tmpID < 0)
//					{
//						tmpObjPos.x = -300;
//						//tmpObjPos.y = SBY[tmpID];
//						StageButtonObj.transform.localPosition = tmpObjPos;
//					}
//					else
//					{
//						tmpObjPos.x = SBX[tmpID];
//						tmpObjPos.y = SBY[tmpID];
//						StageButtonObj.transform.localPosition = tmpObjPos;
//						
//						//set button Image
//						StageButtonObj.GetComponent<UISprite>().spriteName = strStageButtonImage[tmpStar[tmpID]];
//						
//						//set ID
//						StageButtonObj.GetComponent<STAGE>().id = tmpID;
//						
//						if(tmpStar[tmpID] >= 1)
//						{
//							//						if(StageButtonObj.transform.childCount > 0)
//							//						{
//							//							Destroy(StageButtonObj.transform.GetChild(0).gameObject);
//							//						}
//							StageButtonObj.transform.FindChild("text_id").GetComponent<UILabel>().text = tmpID.ToString();
//						}
//					}
//
//				}
//			}
//		}
//
//
//
//		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//		////dot check
//		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//		for (int i=0; i<DOT_MAX; i++) 
//		{
//			DotObj = GameObject.Find("dot"+i);
//			
//			tmpID = DotObj.GetComponent<DOT>().id;
//			tmpObjPos = DotObj.transform.localPosition;
//			tmpPos.x = tmpPanPos.x + tmpObjPos.x;
//			tmpPos.y = 0;
//			tmpPos.z = 0;
//
//			if((tmpPos.x < Alive_LX) && Direction < 0)
//			{
//				if(tmpID < 328)
//				{
//					tmpID += 100;
//					tmpObjPos.x = DX[tmpID];
//					tmpObjPos.y = DY[tmpID];
//					DotObj.transform.localPosition = tmpObjPos;
//
//					DotObj.GetComponent<DOT>().id = tmpID;
//				}
//			}
//			
//			if((tmpPos.x > Alive_RX) && Direction > 0)
//			{
//				if(tmpID >= 100)
//				{
//					tmpID -= 100;
//					tmpObjPos.x = DX[tmpID];
//					tmpObjPos.y = DY[tmpID];
//					DotObj.transform.localPosition = tmpObjPos;
//
//					DotObj.GetComponent<DOT>().id = tmpID;
//				}
//			}
//		}
//
//		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
//		//blackWhole check
//		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
//		for (int i=0; i<2; i++) 
//		{
//			BlackWholeObj = GameObject.Find("BW"+i);
//
//			tmpID = BlackWholeObj.GetComponent<BLACKWHOLE>().id;
//			tmpObjPos = BlackWholeObj.transform.localPosition;
//			tmpPos.x = tmpPanPos.x + tmpObjPos.x;
//			tmpPos.y = 0;
//			tmpPos.z = 0;
//
//			if((tmpPos.x < Alive_LX) && Direction < 0)
//			{
//				if(tmpID < BLACKWHOLE_MAX-2)
//				{
//					tmpID += 2;
//					tmpObjPos.x = BlackWholeX[tmpID];
//					tmpObjPos.y = BlackWholeY[tmpID];
//					BlackWholeObj.transform.localPosition = tmpObjPos;
//					
//					BlackWholeObj.GetComponent<BLACKWHOLE>().id = tmpID;
//
//					if(nowBlackWhole.id <= tmpID)
//					{
//						//draw lock image
//						if(BlackWholeObj.transform.childCount > 0)
//						{
//							Destroy(BlackWholeObj.transform.GetChild(0).gameObject);
//						}
//						BlackWholeObj.GetComponent<BLACKWHOLE>().drawLock(BlackWholeObj);
//					}
//				}
//			}
//			
//			if((tmpPos.x > Alive_RX) && Direction > 0)
//			{
//				if(tmpID > 1 )
//				{
//					tmpID -= 2;
//					tmpObjPos.x = BlackWholeX[tmpID];
//					tmpObjPos.y = BlackWholeY[tmpID];
//					BlackWholeObj.transform.localPosition = tmpObjPos;
//					
//					BlackWholeObj.GetComponent<BLACKWHOLE>().id = tmpID;
//
//					if(nowBlackWhole.id <= tmpID)
//					{
//						//draw lock image
//						if(BlackWholeObj.transform.childCount > 0)
//						{
//							Destroy(BlackWholeObj.transform.GetChild(0).gameObject);
//						}
//						BlackWholeObj.GetComponent<BLACKWHOLE>().drawLock(BlackWholeObj);
//					}
//				}
//			}
//		}
//
//		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
//		//Planet check
//		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
//		for (int i=0; i<3; i++) {
//
//			PlanetObj = GameObject.Find ("Planet" + i);
//
//			tmpID = PlanetObj.GetComponent<PLANET>().id;
//			tmpObjPos = PlanetObj.transform.localPosition;
//			tmpPos.x = tmpPanPos.x + tmpObjPos.x;
//			tmpPos.y = 0;
//			tmpPos.z = 0;
//
//			if((tmpPos.x < Alive_LX) && Direction < 0)
//			{
//				if(tmpID < PLANET_MAX-3)
//				{
//					tmpID += 3;
//					tmpObjPos.x = PlanetX[tmpID];
//					tmpObjPos.y = PlanetY[tmpID];
//					PlanetObj.transform.localPosition = tmpObjPos;
//					
//					PlanetObj.GetComponent<PLANET>().id = tmpID;
//				}
//			}
//			
//			if((tmpPos.x > Alive_RX) && Direction > 0)
//			{
//				if(tmpID > 2 )
//				{
//					tmpID -= 3;
//					tmpObjPos.x = PlanetX[tmpID];
//					tmpObjPos.y = PlanetY[tmpID];
//					PlanetObj.transform.localPosition = tmpObjPos;
//					
//					PlanetObj.GetComponent<PLANET>().id = tmpID;
//				}
//			}
//		}
//
//
//		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
//		//
//		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	}
//
//
//
//
//	void makePosTextFile()
//	{
//		GameObject stagePos;
//		FileStream fs1 = new FileStream("PosData2.txt", FileMode.OpenOrCreate, FileAccess.Write);
//		StreamWriter sw = new StreamWriter(fs1);
//		
//		sw.Write("int[] SBX = {");
//		for (int i=0; i<44; i++) 
//		{
//			stagePos = GameObject.Find ("SB"+i);
//			Vector3 pos = stagePos.transform.localPosition;
//			
//			if(i%20 == 0)
//			{
//				sw.Write("\n");
//			}
//			if(i == 43)
//			{
//				sw.Write((int)(pos.x)+"\n");
//			}
//			else{
//				sw.Write((int)(pos.x)+",");
//			}
//		}
//		sw.Write("};\n");
//		
//		sw.Write("int[] SBY = {");
//		for (int i=0; i<44; i++) 
//		{
//			stagePos = GameObject.Find ("SB"+i);
//			Vector3 pos = stagePos.transform.localPosition;
//			
//			if(i%20 == 0)
//			{
//				sw.Write("\n");
//			}
//			if(i == 43)
//			{
//				sw.Write((int)(pos.y)+"\n");
//			}
//			else{
//				sw.Write((int)(pos.y)+",");
//			}
//		}
//		sw.Write("};\n");
//		
//		sw.Write("int[] DX = {");
//		for (int i=0; i<143; i++) 
//		{
//			stagePos = GameObject.Find ("dot"+i);
//			Vector3 pos = stagePos.transform.localPosition;
//			
//			if(i%20 == 0)
//			{
//				sw.Write("\n");
//			}
//			if(i == 142)
//			{
//				sw.Write((int)(pos.x)+"\n");
//			}
//			else{
//				sw.Write((int)(pos.x)+",");
//			}
//		}
//		sw.Write("};\n");
//		
//		sw.Write("int[] DY = {");
//		for (int i=0; i<143; i++) 
//		{
//			stagePos = GameObject.Find ("dot"+i);
//			Vector3 pos = stagePos.transform.localPosition;
//			
//			if(i%20 == 0)
//			{
//				sw.Write("\n");
//			}
//			if(i == 142)
//			{
//				sw.Write((int)(pos.y)+"\n");
//			}
//			else{
//				sw.Write((int)(pos.y)+",");
//			}
//		}
//		sw.Write("};\n");
//		
//		sw.Close();
//	}
//}
