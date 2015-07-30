using UnityEngine;
using System.Collections;

public class PinchZoom : MonoBehaviour {
	private float orthoZoomSpeed = 0.05f;        // The rate of change of the orthographic size in orthographic mode.
	private float orthoCamSize;
	private Vector3 camPos;
	private float maxZoom;
	private float minZoom = 3;
	private float panSpeed = -0.05f;
	public float ScreenWidth = 802;
	public float SideMenuWidth = 80.2f;
	public float topRightX;// = 721.8f;
	public static bool isPanning;        // Is the camera being panned?
	public static bool isItPanning;
	
	Vector3 bottomLeft;
	Vector3 topRight;
	
	float cameraMaxY;
	float cameraMinY;
	float cameraMaxX;
	float cameraMinX;

	//private GameObject parentObject;
	
	GameObject backgroundCam;
	GameObject background;
	
	void Start()
	{

		//parentObject = new GameObject("ParentObject");
		//parentObject.transform.parent = transform.parent;
		//parentObject.transform.position = new Vector3(transform.position.x*-1, transform.position.y*-1, transform.position.z);
		//transform.parent = parentObject.transform;

		backgroundCam = GameObject.Find("Main Camera (Third)");
		background = GameObject.Find("bg3");
	
		Vector3 backgroundPosition = background.transform.position;
		backgroundPosition.z = Camera.main.orthographicSize;
		background.transform.position = backgroundPosition;
	
		ScreenWidth = Screen.width	;
		SideMenuWidth = Screen.width * 0.25f; //0.1953f;
		topRightX = ScreenWidth - SideMenuWidth;
		isPanning = false;
		maxZoom = Camera.main.orthographicSize;
		orthoCamSize = maxZoom;
		camPos = Camera.main.transform.position;
		
		//set max camera bounds (assumes camera is max zoom and centerwed on Start)
		topRight = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(topRightX, GetComponent<Camera>().pixelHeight, -transform.position.z));
		bottomLeft = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0, 0, -transform.position.z));
		
		cameraMaxX = topRight.x;
		cameraMaxY = topRight.y;
		cameraMinX = bottomLeft.x;
		cameraMinY = bottomLeft.y;
		
		// adjustment
		//cameraMaxX-=1.1f;
	}
	
	void Update ()
	{
		#if UNITY_EDITOR
		//click and drag
		if(Input.GetMouseButton(0)){
			isPanning = true;
			//Debug.Log (PinchZoom.isPanning);
			float x = Input.GetAxis("Mouse X") * panSpeed;
			float y = Input.GetAxis("Mouse Y") * panSpeed;
			transform.Translate(x,y,0);
			
			//print ("background.transform.position" + background.transform.position);
			
			//if (background.transform.position.y > 22 && background.transform.position.y < 24)
				backgroundCam.transform.Translate(-x/5,-y/5,0);
				
			isPanning = false;
		}
		#endif
		
		
		// One Finger Pan
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved){
			isPanning = true;
			//            Touch touchZero = Input.GetTouch(0);
			//            float x = touchZero.position.x * panSpeed;
			//            float y = touchZero.position.y * panSpeed;
			float x = Input.GetAxis("Mouse X") * panSpeed;
			float y = Input.GetAxis("Mouse Y") * panSpeed;
			transform.Translate(x,y,0);
			
			backgroundCam.transform.Translate(-x/5,-y/5,0);
			
			isPanning = false;
			//isItPanning = true;
		}
		
		Vector3 backgroundPosition = background.transform.position;
		
		#if UNITY_EDITOR
		//zoom
		if((Input.GetAxis("Mouse ScrollWheel") > 0) && Camera.main.orthographicSize > minZoom ) // forward
		{
			Camera.main.orthographicSize = Camera.main.orthographicSize - orthoZoomSpeed;
			backgroundPosition.z = Camera.main.orthographicSize - orthoZoomSpeed*2;
			background.transform.position = backgroundPosition;
		}
		
		if ((Input.GetAxis("Mouse ScrollWheel") < 0) && Camera.main.orthographicSize < maxZoom) // back          
		{
			Camera.main.orthographicSize = Camera.main.orthographicSize + orthoZoomSpeed;
			backgroundPosition.z = Camera.main.orthographicSize + orthoZoomSpeed*2;
			background.transform.position = backgroundPosition;
		}
		#endif
		
		// 2 finger Zoom
		if (Input.touchCount == 2){
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);
			
			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
			
			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
			
			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
			
			// If the camera is orthographic...
			if (GetComponent<Camera>().orthographic)
			{
				// ... change the orthographic size based on the change in distance between the touches.
				GetComponent<Camera>().orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
				
				backgroundPosition.z += deltaMagnitudeDiff * orthoZoomSpeed/2;
				background.transform.position = backgroundPosition;
				
				// Make sure the orthographic size never drops below zero.
				GetComponent<Camera>().orthographicSize = Mathf.Max(GetComponent<Camera>().orthographicSize, minZoom);
				backgroundCam.GetComponent<Camera>().orthographicSize = Mathf.Max(backgroundCam.GetComponent<Camera>().orthographicSize, 2);
				
				// Make sure the orthographic size never goes above original size.
				GetComponent<Camera>().orthographicSize = Mathf.Min(GetComponent<Camera>().orthographicSize, maxZoom);
				backgroundCam.GetComponent<Camera>().orthographicSize = Mathf.Min(backgroundCam.GetComponent<Camera>().orthographicSize, 2);
			}
		}
		
		
		// On double tap image will be set at original position and scale
		else if(Input.touchCount==1 && Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(0).tapCount==2)
		{
			//camera.orthographicSize = orthoCamSize;
			//Camera.main.transform.position = camPos;
		}
		
		
		//check if camera is out-of-bounds, if so, move back in-bounds
		topRight = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(topRightX, GetComponent<Camera>().pixelHeight, -transform.position.z));
		bottomLeft = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(20,0,-transform.position.z));
		
		// right
		if(topRight.x > cameraMaxX)
		{
			transform.position = new Vector3(transform.position.x - (topRight.x - cameraMaxX), transform.position.y, transform.position.z);
		}
		
		// top
		if(topRight.y > cameraMaxY)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y - (topRight.y - cameraMaxY), transform.position.z);
		}
		
		if(bottomLeft.x < cameraMinX)
		{
			transform.position = new Vector3(transform.position.x + (cameraMinX - bottomLeft.x), transform.position.y, transform.position.z);
		}
		
		if(bottomLeft.y < cameraMinY)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + (cameraMinY - bottomLeft.y), transform.position.z);
		}
		
		
		// If back button press andriod
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
		
	}
}