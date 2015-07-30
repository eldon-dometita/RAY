//using UnityEngine;
//using System.Collections;
//
//public class Help_Pipe : MonoBehaviour {
//
//	public int help_PipeIndex;
//
//	GameObject[] gHPipe;
//	PipeScript[] sHPipe;
//
//
//	int[,] helpArr = new int[,]{
//		{0, 0, 0, 0, 0, 0, 0, 0, 0},
//		{0, 0, 0, 0, 0, 0, 0, 0, 0},
//		{0, 0, 0, 0, 0, 0, 0, 0, 0},
//		{0, 0, 0, 0, 0, 0, 0, 0, 0},
//		{0, 0, 0, 0, 0, 0, 0, 0, 0},
//		{0, 0, 0, 0, 0, 0, 0, 0, 0},
//		{0, 0, 0, 0, 0, 0, 0, 0, 0},
//		{0, 0, 0, 0, 0, 0, 0, 0, 0},
//		{0, 0, 0, 0, 0, 0, 0, 0, 0},
//		{0, 0, 0, 0, 0, 0, 0, 0, 0},
//
//		{0, 0, 0, 113, 10, 0, 0, 121, 0},
//		{0, 0, 0, 113, 10, 0, 0, 121, 0}
//	};
//
//	// Use this for initialization
//	void Awake () {
//
//		GlobalData.baseTileNum = 0;
//
//		gHPipe = new GameObject[9];
//		sHPipe = new PipeScript[9];
//
//		for(int i=0; i<9; i++)
//		{
//			gHPipe[i] = GameObject.Find("HPipe"+i);
//			sHPipe[i] = gHPipe[i].GetComponent<PipeScript>();
//		}
//
//		setHelpPipe(10);
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
//
//	void setHelpPipe(int _index)
//	{
//		switch(_index)
//		{
//		case 10:
//			for(int i=0; i<9; i++)
//			{
//				sHPipe[i].setID(helpArr[_index,i], 0);
//			}
//			break;
//		}
//	}
//}
