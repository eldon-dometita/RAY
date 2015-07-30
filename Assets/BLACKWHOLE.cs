//using UnityEngine;
//using System.Collections;
//
//public class BLACKWHOLE : MonoBehaviour {
//
//	public int id;
//	public int state;
//	public int time;	//second
//	public bool[] help;
//
//	public BLACKWHOLE()
//	{
//		help = new bool[3];
//	}
//
//	// Use this for initialization
//	void Start () {
////		help = new bool[3];
////		for (int i=0; i<3; i++) {
////			help[i] = new bool();
////				}
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
//
//	public void setID(int _id)
//	{
//		id = _id;
//	}
//
//	//public void drawLock(Vector2 _pos, GameObject _parent)
//	public void drawLock(GameObject _parent)
//	{
//		GameObject _go = Instantiate(Resources.Load ("Prefabs/Lock"), new Vector2(0, 0), Quaternion.identity) as GameObject;
//		_go.transform.parent = _parent.transform;
//
//		_go.transform.localScale = new Vector3(1, 1, 1);
//		
//		
//		Vector3 pos = _go.transform.localPosition;
//		pos.x = -10;
//		pos.y = -5;
//		_go.transform.localPosition = pos;
//	}
//}
