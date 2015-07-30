//using UnityEngine;
//using System.Collections;
//
//public class PipeObject : MonoBehaviour {
//
//	public class aPipeObject {
//		// pipe no
//		public int pipeNo;
//		
//		// pipe name
//		//getpipename(pipeNo);
//		
//		// animateNo 1= left, 2= right, 3= top, 4= bottom, 5= mid
//		public void PipeAnimate(int animateNo)
//		{
//		
//		}
//	}
//	
//	string pipeName = "outbottomthreein";
//	public UIAtlas atlas;
//	
//	void Start()
//	{
//		SetUISprite();
//	}
//	
//	void PipeInit(int pipeNo)
//	{
//		//getpipename(pipeNo);
//		pipeName = "outbottomthreein";	
//		SetUISprite();	
//	}
//	
//	void SetUISprite() 
//	{
//		// check atlas
//		if (!atlas) Debug.Log ("NO ATLAS FOUND!");
//		
//		// create new gameobject for the sprite
//		GameObject uispriteobj = new GameObject();
//		
//		// add uisprite component
//		UISprite uisprite = uispriteobj.AddComponent<UISprite>();
//		
//		// set atlas (shoppipes)
//		uisprite.atlas = atlas;
//		
//		// set uisprite source (pipename)
//		uisprite.spriteName = pipeName;
//		
//		// set uisprite xy dimension
//		uisprite.width = 90;
//		uisprite.height = 90;
//		
//		// set name of this gameobject
//		uispriteobj.name = pipeName;
//		
//		// put this gameobject parent
//		uispriteobj.transform.parent = this.gameObject.transform;
//		
//		// transform scale to 1, 1
//		uispriteobj.transform.localScale = new Vector2 (1, 1);
//	}
//}
