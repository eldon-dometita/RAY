//using UnityEngine;
//using System.Collections;
//
//public class mapmap1 : MonoBehaviour {
//
//	float speed = 0.1f;
//	void Update () {
//		if (Input.touchCount > 0 && 
//		    Input.GetTouch(0).phase == TouchPhase.Moved) {
//			
//			// Get movement of the finger since last frame
//			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
//			
//			// Move object across XY plane
//			GameObject.Find("Camera").transform.Translate (-touchDeltaPosition.x * speed, 
//			                    						   -touchDeltaPosition.y * speed, 0);
//		}
//	}
//}
//	
