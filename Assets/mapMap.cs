//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//
//public class mapMap : MonoBehaviour {
//
//	public UIAtlas atlasSRC;
//	public UIAtlas atlasSRC1;
//	public UIAtlas backgroundAtlas;
//	
//	GameObject parent;
//	GameObject contain;
//	Camera cameraMain;
//	
//	GameObject[] backgrounds;
//	string[] imgname;
//	
//	int backNumber = 2;
//	
//	public GameObject[] stageBackgrounds = new GameObject[6];
//	public GameObject[] stageBackgroundsPlanets = new GameObject[6];
//	public float startLocation = 0;
//	float lastLocation;
//	
//	// Use this for initialization
//	void Start () {
//		
//		// background
//		contain = GameObject.Find("background"); //new GameObject();
//		
//		// camera
//		cameraMain = GameObject.Find("Camera").GetComponent<Camera>();
//		
//		imgname = new string[7];
//		imgname[0] = "conveyor&Inventory-2007";
//		imgname[1] = "conveyor&Inventory-2068";
//		imgname[2] = "conveyor&Inventory-2069";
//		imgname[3] = "conveyor&Inventory-2070";
//		imgname[4] = "conveyor&Inventory-2071";
//		imgname[5] = "conveyor&Inventory-2072";
//		imgname[6] = "conveyor&Inventory-2073";
//		
//		
//		GameObject main = new GameObject();
//		main.transform.parent = GameObject.Find("Background").transform;
//		main.transform.localScale = new Vector3 (1, 1, 1);
//		GameObject hey = Instantiate(stageBackgrounds[0]) as GameObject;
//		hey.transform.parent = main.transform;
//		hey.transform.localScale = new Vector3 (1, 1, 1);
//		
//		GameObject backPlanet = Instantiate(stageBackgroundsPlanets[0]) as GameObject;
//		backPlanet.transform.parent = main.transform;
//		backPlanet.transform.localScale = new Vector3 (1, 1, 1);
//		
//		// stage 1 planets
//		generatePlanet(main);
//		// stage 1 dots
//		generateDot(main);
//		
//		
//		//hey.transform.localPosition = new Vector2 (1300, 0);
//		
//		lastLocation = startLocation;
//		
////		backgrounds = new GameObject[backNumber];
////		for (backNumber = 0; backNumber < backgrounds.Length; backNumber++)
////		{
////			backgrounds[backNumber] = createGObject("background", backNumber);
////			createBG(40, new Vector2(180, 180), 5, backgrounds[backNumber], imgname[backNumber], atlasSRC);
////		}
////		
////		backgrounds[1].transform.localPosition = new Vector2(1440, 0);
////		
////		
////		generatePlanet();
////		generateDot();
//	}
//	
//	bool respawnState;
//	int stageNumber;
//	
//	void Update () {
//		if (Input.GetMouseButton(0))
//		{	
//		
//			float x = -Input.GetAxis("Mouse X") * 0.1f;
//			cameraMain.transform.Translate(x,0,0);
//			
//			// going right
//			if (cameraMain.transform.localPosition.x > lastLocation && !respawnState)
//			{
//				respawnState = true;
//				lastLocation += 1260;
//				
//				stageNumber++;
//				
//				if (stageNumber < stageBackgrounds.Length)
//				{
//					// instantiate background
//					GameObject main = new GameObject();
//					main.transform.parent = GameObject.Find("Background").transform;
//					main.transform.localScale = new Vector3 (1, 1, 1);
//					GameObject hey = Instantiate(stageBackgrounds[stageNumber]) as GameObject;
//					hey.transform.parent = main.transform;
//					hey.transform.localScale = new Vector3 (1, 1, 1);
//					hey.transform.localPosition = new Vector2 (lastLocation, 0);
//				}
//					
//				if (stageNumber > 1)
//					GameObject.Find("Background").transform.GetChild(stageNumber-2).gameObject.SetActive(false);
//				
//				respawnState = false;
//			}
//			
//			// going left
//			if (cameraMain.transform.localPosition.x < (lastLocation-1260) && stageNumber > 1)
//			{
//				// disabled prev
// 				GameObject.Find("Background").transform.GetChild(stageNumber-2).gameObject.SetActive(true);
//				// delete next
//				if (stageNumber < stageBackgrounds.Length)
//					Destroy(GameObject.Find("Background").transform.GetChild(stageNumber).gameObject);
//				
//				lastLocation-=1260;
//				stageNumber--;
//			}
//			
//			// lock the map from the very initial map
//			if (cameraMain.transform.localPosition.x <= 0 || cameraMain.transform.localPosition.x == 0)
//				cameraMain.transform.localPosition = new Vector2(0,0);
//				
//			// lock the map from the very last map
//			if (cameraMain.transform.localPosition.x >= 5040 || cameraMain.transform.localPosition.x == 5040)
//				cameraMain.transform.localPosition = new Vector2(5040,0);	
//		}
//	}
//	
//	Dictionary<Vector2, int> planets;
//	List<Vector2> dots;
//	
//	void generateDot(GameObject GObj)
//	{
//		dots = new List<Vector2>();
//		
//		dots.Add(new Vector2(-467, -253));
//		dots.Add(new Vector2(-453, -269));
//		dots.Add(new Vector2(-433, -280));
//		dots.Add(new Vector2(-327, -295));
//		dots.Add(new Vector2(-306, -294));
//		dots.Add(new Vector2(-283, -287));
//		dots.Add(new Vector2(-180, -234));
//		dots.Add(new Vector2(-164, -222));
//		dots.Add(new Vector2(-151, -206));
//		dots.Add(new Vector2(-139, -119));
//		dots.Add(new Vector2(-152, -101));
//		dots.Add(new Vector2(-169, -90));
//		dots.Add(new Vector2(-261, -54));
//		dots.Add(new Vector2(-282, -53));
//		dots.Add(new Vector2(-300, -56));
//		dots.Add(new Vector2(-407, -35));
//		dots.Add(new Vector2(-419, -16));
//		dots.Add(new Vector2(-412, 4));
//		dots.Add(new Vector2(-342, 101));
//		dots.Add(new Vector2(-322, 110));
//		dots.Add(new Vector2(-306, 123));
//		
//		GameObject GO = new GameObject();
//		
//		foreach(Vector2 dot in dots)
//		{
//			var objsprite = createSprite(GO, atlasSRC1, "StageBase001", new Vector2(18, 22), dot, 1);
//			objsprite.transform.parent = GO.transform;
//		}
//		
//		GO.transform.parent = GObj.transform;
//		GO.transform.localScale = new Vector3 (1, 1, 1);
//	}
//	
//	void generatePlanet(GameObject GObj)
//	{
//		planets = new Dictionary<Vector2, int>();
//		
//		planets.Add(new Vector2(-495, -218), 0);
//		planets.Add(new Vector2(-381, -288), 1);
//		planets.Add(new Vector2(-232, -258), 2);
//		planets.Add(new Vector2(-116, -163), 3);
//		planets.Add(new Vector2(-218, -77), 4);
//		planets.Add(new Vector2(-357, -68), 5);
//		planets.Add(new Vector2(-374, 64), 6);
//		planets.Add(new Vector2(-254, 147), 7);
//		planets.Add(new Vector2(-168, 276), 8);
//		planets.Add(new Vector2(-17, 319), 9);
//		planets.Add(new Vector2(86, 224), 10);
//		planets.Add(new Vector2(-26, 116), 11);
//		planets.Add(new Vector2(34, -7), 12);
//		planets.Add(new Vector2(174, -65), 13);
//		planets.Add(new Vector2(264, 64), 14);
//		planets.Add(new Vector2(298, 195), 15);
//		planets.Add(new Vector2(413, 309), 16);
//		planets.Add(new Vector2(500, 188), 17);
//		planets.Add(new Vector2(449, 54), 18);
//		planets.Add(new Vector2(712, -51), 19);
//	
//		GameObject GO = new GameObject();
//		
//		foreach(KeyValuePair<Vector2, int> planet in planets)
//		{
//			var objsprite = createSprite(GO, backgroundAtlas, "StageBase000", new Vector2(96, 78), planet.Key, 2, true);
//			mapInfo mapinfo = objsprite.AddComponent<mapInfo>();
//				
//			mapinfo.stars = Random.Range (0,4);
//			bool state = false;
//			if (Random.Range(0, 2) == 1)
//				state = true;
//			mapinfo.state = state;
//			
//			mapinfo.levelNo = planet.Value;
//			
//			objsprite.transform.parent = GO.transform;
//			objsprite.name = "Stage" + planet.Value;
//		}
//		GO.name = "Planets";
//		GO.transform.parent = GObj.transform;
//		GO.transform.localScale = new Vector3 (1, 1, 1);
//	}
//	
//	// delete child of any parent gameobject
//	void deleteChildren (GameObject obj)
//	{
//		Transform[] objChild = obj.GetComponentsInChildren<Transform>();
//		for (int i = 1; i < objChild.Length; i ++)
//			Destroy(objChild[i].gameObject);
//	}
//	
//	// create background
//	void createBG(int tileCount, Vector2 tileDistance, int tileCut, GameObject obj, string spriteName, UIAtlas atlas)
//	{
//		int xDistance = 0;
//		int yDistance = 0;
//		for (int i = 1; i <= tileCount; i ++)
//		{	
//			createSprite(obj, atlas, spriteName, tileDistance, new Vector2(xDistance, yDistance), 0);
//			yDistance += int.Parse(tileDistance.x.ToString());
//			
//			if (i%tileCut == 0)
//			{
//				xDistance += int.Parse(tileDistance.x.ToString());
//				yDistance = 0;
//			}
//		}
//	}
//	
//	// create gameobject
//	GameObject createGObject(GameObject GObj, string objName = null)
//	{
//		GameObject obj = new GameObject();
//		if (objName != null)
//			obj.name = objName;
//		obj.transform.localScale = new Vector3(1, 1, 1);
//		obj.transform.localPosition = Vector2.zero;
//		obj.transform.parent = GObj.transform;
//		return obj;
//	}
//	
//	// create sprite
//	GameObject createSprite(GameObject parent, UIAtlas atlas, string sprite, Vector2 size, Vector2 position, int depth, bool clickable = false)
//	{
//		GameObject quantity = new GameObject();
//		UISprite UISquantity = quantity.AddComponent<UISprite>();
//		UISquantity.atlas = atlas;
//		
//		UISquantity.width = int.Parse(size.x.ToString());
//		UISquantity.height = int.Parse(size.y.ToString());
//		
//		UISquantity.spriteName = sprite;
//		UISquantity.depth = depth;
//		
//		quantity.transform.parent = parent.transform;
//		quantity.transform.localScale = new Vector3(1, 1, 1);
//		quantity.transform.localPosition = position;
//		
//		if (clickable) 
//		{
//			UIButtonScale butScale = quantity.AddComponent<UIButtonScale>();
//			butScale.hover = new Vector3 (1.5f, 1.5f, 1.5f);
//		
//			BoxCollider box = quantity.AddComponent<BoxCollider>();
//			box.size = new Vector2 (UISquantity.width, UISquantity.height);
//		}
//		
//		//.AddComponent<UIDragScrollView>();
//		//quantity.AddComponent<UICenterOnClick>();
//		//BoxCollider box = quantity.AddComponent<BoxCollider>();
//		//box.size = new Vector2 (spacing, size.y);
//		
//		return quantity;
//	}
//}
