using UnityEngine;
using System.Collections;

public class mapBackground : MonoBehaviour {
	public UIAtlas atlasSRC;
	
	GameObject[] backgrounds;
	string[] imgname;
	
	int backNumber = 1;
	int xDistance;
	int yDistance;
	
	void Start () {
		imgname = new string[7];
		imgname[0] = "conveyor&Inventory-2007";
		imgname[1] = "conveyor&Inventory-2068";
		imgname[2] = "conveyor&Inventory-2069";
		imgname[3] = "conveyor&Inventory-2070";
		imgname[4] = "conveyor&Inventory-2071";
		imgname[5] = "conveyor&Inventory-2072";
		imgname[6] = "conveyor&Inventory-2073";
	
		backgrounds = new GameObject[backNumber];
		for (backNumber = 0; backNumber < backgrounds.Length; backNumber++)
		{
			backgrounds[backNumber] = createGObject("background", backNumber);
			createBG(40, new Vector2(180, 180), 5, backgrounds[backNumber], imgname[backNumber], atlasSRC);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// create background
	void createBG(int tileCount, Vector2 tileDistance, int tileCut, GameObject obj, string spriteName, UIAtlas atlas)
	{
		for (int i = 1; i <= tileCount; i ++)
		{	
			createSprite(obj, atlas, spriteName, tileDistance, new Vector2(xDistance, yDistance));
			yDistance += int.Parse(tileDistance.x.ToString());
			
			if (i%tileCut == 0)
			{
				xDistance += int.Parse(tileDistance.x.ToString());
				yDistance = 0;
			}
		}
	}
	
	// create gameobject
	GameObject createGObject(string objName, int objIndex)
	{
		GameObject obj = new GameObject();
		obj.transform.parent = gameObject.transform;
		obj.name = objName + objIndex;
		obj.transform.localScale = new Vector3(1, 1, 1);
		obj.transform.localPosition = Vector2.zero;
		return obj;
	}
	
	// create sprite
	GameObject createSprite(GameObject parent, UIAtlas atlas, string sprite, Vector2 size, Vector2 position)
	{
		GameObject quantity = new GameObject();
		UISprite UISquantity = quantity.AddComponent<UISprite>();
		UISquantity.atlas = atlas;
		
		UISquantity.width = int.Parse(size.x.ToString());
		UISquantity.height = int.Parse(size.y.ToString());
		
		UISquantity.spriteName = sprite;
		UISquantity.depth = 1;
		
		quantity.transform.parent = parent.transform;
		quantity.transform.localScale = new Vector3(1, 1, 1);
		quantity.transform.localPosition = position;
		
		//.AddComponent<UIDragScrollView>();
		//quantity.AddComponent<UICenterOnClick>();
		//BoxCollider box = quantity.AddComponent<BoxCollider>();
		//box.size = new Vector2 (spacing, size.y);
		
		return quantity;
	}
}
