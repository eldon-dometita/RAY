using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZoomButtom : MonoBehaviour 
{
	float defaultScaleX, defaultScaleY;
	public float currentScaleX, currentScaleY;
	public float addValToScroll = 0.03f;
	public GameObject zoomPanObj;

	float MIN_SCALE = 0.62f;
	float MAX_SCALE = 1.5f;

	bool bCenter;
	bool bTwoTouch;

	void Awake()
	{
		if (!zoomPanObj)
			zoomPanObj = GameObject.Find ("ZoomObj");
		//zoomPanObj = GameObject.Find ("ZoomObj");

		zoomPanObj.transform.localScale = new Vector2(currentScaleX,currentScaleY); 
		defaultScaleX = currentScaleX;
		defaultScaleY = currentScaleX;

		bCenter = false;
		bTwoTouch = false;
	}
	
	void Update()
	{
		//print (" gridObj.transform.localScale " + gridObj.transform.localScale);
	
		#if UNITY_EDITOR
		//zoom
		if((Input.GetAxis("Mouse ScrollWheel") > 0)) // forward
		{

			if (defaultScaleX <= currentScaleX)
			{
				if(defaultScaleX >= MAX_SCALE) return;
				if (defaultScaleX <= currentScaleX)
				{
					defaultScaleX = defaultScaleX + addValToScroll;
					if(defaultScaleX >= MAX_SCALE)
					{
						defaultScaleX = MAX_SCALE;
					}
					defaultScaleY = defaultScaleX;
					
					zoomPanObj.transform.localScale = new Vector2(defaultScaleX,defaultScaleY); 
					currentScaleX = defaultScaleX;
					currentScaleY = defaultScaleY;
				}
			}
//			ChangeParentScale(zoomPanObj.transform, zoomPanObj.transform.localScale);
		}
		if ((Input.GetAxis("Mouse ScrollWheel") < 0)) // back          
		{

			if (defaultScaleX >= currentScaleX) 
			{
				if(defaultScaleX <= MIN_SCALE) return;
				if (defaultScaleX >= currentScaleX) 
				{
					defaultScaleX = defaultScaleX - addValToScroll;
					if(defaultScaleX <= MIN_SCALE)
					{
						defaultScaleX = MIN_SCALE;
					}
					defaultScaleY = defaultScaleX;
					
					zoomPanObj.transform.localScale = new Vector2(defaultScaleX,defaultScaleY);
					currentScaleX = defaultScaleX;
					currentScaleY = defaultScaleY;
				}
			}
//			ChangeParentScale(zoomPanObj.transform, zoomPanObj.transform.localScale);
		}

		// One Finger Pan
//		if (Input.GetMouseButton(0))
//		{
//			float x = Input.GetAxis("Mouse X") * 0.1f;
//			float y = Input.GetAxis("Mouse Y") * 0.1f;
//			
//			//print (" XXX " + x);
//			//print (" YYY " + y);
////			zoomPanObj.transform.Translate(x,y,0);
//		}
		#endif
		
		// limit panning
//		Vector2 gridPos = gridObj.transform.position;
//		if (gridObj.transform.localScale.x <= 1.4f && gridObj.transform.localScale.y <= 1.4f)
//		{
//			gridPos.x = Mathf.Clamp(gridObj.transform.position.x, -0.25f, 0.25f);
//			gridPos.y = Mathf.Clamp(gridObj.transform.position.y, -0.20f, 0.20f);
//		} 
//		else
//		{
//			gridPos.x = Mathf.Clamp(gridObj.transform.position.x, -0.85f, 0.85f);
//			gridPos.y = Mathf.Clamp(gridObj.transform.position.y, -0.85f, 0.85f);
//		}
//		
//		gridObj.transform.position = gridPos;
		
		//if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
		//{
		//	Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
		//}
		
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved){
//			float x = Input.GetAxis("Mouse X") * 0.01f;
//			float y = Input.GetAxis("Mouse Y") * 0.01f;
//			zoomPanObj.transform.Translate(x,y,0);
		}
		if(Input.touchCount == 2)
		{ 
			bTwoTouch = true;
			Touch touch0 = Input.GetTouch(0);
			Touch touch1 = Input.GetTouch(1);
			
			if (touch0.phase == TouchPhase.Moved && touch1.phase == TouchPhase.Moved)
			{
				Vector2 prevDist = (touch0.position - touch0.deltaPosition) - (touch1.position - touch1.deltaPosition);
				Vector2 curDist = touch0.position - touch1.position;
				float delta = curDist.magnitude - prevDist.magnitude;
				
				if (delta > 0)
				{
					if(defaultScaleX >= MAX_SCALE) return;
					if (defaultScaleX <= currentScaleX)
					{
						defaultScaleX = defaultScaleX + addValToScroll;
						if(defaultScaleX >= MAX_SCALE)
						{
							defaultScaleX = MAX_SCALE;
						}
						defaultScaleY = defaultScaleX;

						zoomPanObj.transform.localScale = new Vector2(defaultScaleX,defaultScaleY); 
						currentScaleX = defaultScaleX;
						currentScaleY = defaultScaleY;
					}
				} 
				else if (delta < 0)
				{
					if(defaultScaleX <= MIN_SCALE) return;
					if (defaultScaleX >= currentScaleX) 
					{
						defaultScaleX = defaultScaleX - addValToScroll;
						if(defaultScaleX <= MIN_SCALE)
						{
							defaultScaleX = MIN_SCALE;
						}
						defaultScaleY = defaultScaleX;

						zoomPanObj.transform.localScale = new Vector2(defaultScaleX,defaultScaleY);
						currentScaleX = defaultScaleX;
						currentScaleY = defaultScaleY;


					}
				} 
			}
		}
		else if(Input.touchCount == 1)
		{
			if(bTwoTouch)
			{
				bCenter = true;
				bTwoTouch = false;
			}
		}
		else if(Input.touchCount <= 0)
		{
			bTwoTouch = false;
		}


		if(currentScaleX == MIN_SCALE)
		{
			bCenter = true;
		}
		else
		{
			bCenter = false;
		}

		if(bCenter)
		{
			makeCenter();
		}
	}

	void makeCenter ()
	{
		UICenterOnChild center = NGUITools.FindInParents<UICenterOnChild>(gameObject);
		UIPanel panel = NGUITools.FindInParents<UIPanel>(gameObject);
		

		if (center != null)
		{
			if (center.enabled)
				center.CenterOn(transform);
		}
		else if (panel != null && panel.clipping != UIDrawCall.Clipping.None)
		{
			UIScrollView sv = panel.GetComponent<UIScrollView>();
			Vector3 offset = -panel.cachedTransform.InverseTransformPoint(transform.position);
			if (!sv.canMoveHorizontally) offset.x = panel.cachedTransform.localPosition.x;
			if (!sv.canMoveVertically) offset.y = panel.cachedTransform.localPosition.y;
			SpringPanel.Begin(panel.cachedGameObject, offset, 6f);

		}
	}

	public void setZoomPanScale(float _scaleX, float _scaleY)
	{
		zoomPanObj.transform.localScale = new Vector2(_scaleX,_scaleY);
		currentScaleX = _scaleX;
		currentScaleY = _scaleY;

		makeCenter ();
	}


	public static float mapHeight;

	public void setMinimumScreen()
	{
		bool bEscape = true;
		float tmpWidthScale = 1.0f;
		float tmpHeightScale = 1.0f;
		float minimumWidthScale = 1.0f;
		float minimumHeightScale = 1.0f;



		float gameHeight;

		float mapWidth;
		float gameWidth;

		gameHeight = Screen.height;
		float ratio = gameHeight/800;

		mapHeight = (GameCon.nRow * 90)*ratio;


		gameWidth = Screen.width - (372*ratio);
		mapWidth = (GameCon.nCol * 90)*ratio;



//		Debug.Log("gameWidth:"+gameWidth);
//		Debug.Log("Screen.height:"+Screen.height);
//
//
//		Debug.Log("ratio:"+ratio);

		bEscape = true;
		while (bEscape) 
		{
			if(mapWidth < gameWidth)
			{
				bEscape = false;
				minimumWidthScale = tmpWidthScale; 
				//Debug.Log("mapWidth:"+mapWidth);
			}
			else{
				mapWidth = ((GameCon.nCol-1) * 90)*ratio;
				tmpWidthScale -= 0.01f;
				mapWidth*= tmpWidthScale;
			}
		}

		bEscape = true;
		while (bEscape) 
		{
			if(mapHeight < gameHeight)
			{
				bEscape = false;
				minimumHeightScale = tmpHeightScale; 
			}
			else{
				mapHeight = ((GameCon.nRow - 1) * 90)*ratio;
				tmpHeightScale -= 0.01f;
				mapHeight*= tmpHeightScale;
			}
		}
	
		if(minimumWidthScale > minimumHeightScale)
		{
			zoomPanObj.transform.localScale = new Vector2(minimumHeightScale,minimumHeightScale);
			currentScaleX = minimumHeightScale;
			currentScaleY = minimumHeightScale;
		}
		else
		{
			zoomPanObj.transform.localScale = new Vector2(minimumWidthScale,minimumWidthScale);
			currentScaleX = minimumWidthScale;
			currentScaleY = minimumWidthScale;
		}


		
		makeCenter ();

	}

}