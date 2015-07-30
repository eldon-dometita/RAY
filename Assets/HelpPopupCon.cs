using UnityEngine;
using System.Collections;

public class HelpPopupCon : MonoBehaviour {

	public byte[,] helpArr = new byte[,]{
		{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 	0, 0, 0, 0, 0},
		
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 14, 10, 0, 0, 	0, 0, 15, 0, 0,		0, 0, 0, 0, 0},	//10
		{0, 0, 0, 0, 0, 		0, 0, 15, 0, 0,		0, 14, 11, 0, 0, 	0, 0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 15, 0, 0, 	0, 0, 12, 14, 0, 	0, 0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 13, 14, 0, 	0, 0, 15, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 14, 14, 14, 0, 	0, 0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 15, 0, 0, 	0, 0, 15, 0, 0, 	0, 0, 15, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 13, 10, 0, 	0, 14, 16, 11, 0, 	0, 0, 15, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 		0, 0, 0, 0, 0},
		
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 14, 20, 0, 0, 	0, 0, 15, 0, 0,		0, 0, 0, 0, 0},	//20
		{0, 0, 0, 0, 0, 		0, 0, 15, 0, 0,		0, 14, 21, 0, 0, 	0, 0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 15, 0, 0, 	0, 0, 22, 14, 0, 	0, 0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 23, 14, 0, 	0, 0, 15, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 14, 24, 14, 0, 	0, 0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 15, 0, 0, 	0, 0, 25, 0, 0, 	0, 0, 15, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 		0, 0, 0, 0, 0},
		
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 14, 30, 0, 0, 	0, 0, 15, 0, 0,		0, 0, 0, 0, 0},	//30
		{0, 0, 0, 0, 0, 		0, 0, 15, 0, 0,		0, 14, 31, 0, 0, 	0, 0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 15, 0, 0, 	0, 0, 32, 14, 0, 	0, 0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 33, 14, 0, 	0, 0, 15, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 14, 34, 14, 0, 	0, 0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 15, 0, 0, 	0, 0, 35, 0, 0, 	0, 0, 15, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 		0, 0, 0, 0, 0},
		
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 14, 40, 0, 0, 	0, 0, 15, 0, 0,		0, 0, 0, 0, 0},	//40
		{0, 0, 0, 0, 0, 		0, 0, 15, 0, 0,		0, 14, 41, 0, 0, 	0, 0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 15, 0, 0, 	0, 0, 42, 14, 0, 	0, 0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 43, 14, 0, 	0, 0, 15, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 14, 44, 14, 0, 	0, 0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 15, 0, 0, 	0, 0, 45, 0, 0, 	0, 0, 15, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 13, 10, 0, 	0, 14, 16, 11, 0, 	0, 0, 15, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 		0, 0, 0, 0, 0},
		
		
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 14, 50, 0, 0, 	0, 0, 15, 0, 0,		0, 0, 0, 0, 0},	//50
		{0, 0, 0, 0, 0, 		0, 0, 15, 0, 0,		0, 14, 51, 0, 0, 	0, 0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 15, 0, 0, 	0, 0, 52, 14, 0, 	0, 0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 53, 14, 0, 	0, 0, 15, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 14, 54, 14, 0, 	0, 0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 15, 0, 0, 	0, 0, 55, 0, 0, 	0, 0, 15, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 		0, 0, 0, 0, 0},
		
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 14, 60, 0, 0, 	0, 0, 15, 0, 0,		0, 0, 0, 0, 0},	//60
		{0, 0, 0, 0, 0, 		0, 0, 15, 0, 0,		0, 14, 61, 0, 0, 	0, 0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 15, 0, 0, 	0, 0, 62, 14, 0, 	0, 0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 63, 14, 0, 	0, 0, 15, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 14, 64, 14, 0, 	0, 0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 15, 0, 0, 	0, 0, 65, 0, 0, 	0, 0, 15, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		
		{0, 0, 0, 0, 0,			0, 0, 15, 0, 0, 	0, 14, 70, 14, 0, 	0, 0, 0, 0, 0,		0, 0, 0, 0, 0},	//70
		{0, 0, 0, 0, 0, 		0, 0, 15, 0, 0, 	0, 0, 71, 14, 0, 	0, 0, 15, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 14, 72, 14, 0, 	0, 0, 15, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 15, 0, 0, 	0, 14, 73, 0, 0, 	0, 0, 15, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0,			0, 0, 15, 0, 0, 	0, 14, 74, 14, 0, 	0, 0, 0, 0, 0,		0, 0, 0, 0, 0},	//74
		{0, 0, 0, 0, 0, 		0, 0, 15, 0, 0, 	0, 0, 75, 14, 0, 	0, 0, 15, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 14, 76, 14, 0, 	0, 0, 15, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 15, 0, 0, 	0, 14, 77, 0, 0, 	0, 0, 15, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		
		{0, 0, 0, 0, 0,			0, 0, 15, 0, 0,		0, 14, 80, 14, 0,	0, 0, 15, 0, 0,		0, 0, 0, 0, 0},	//80
		{0, 0, 0, 0, 0,			0, 0, 15, 0, 0,		0, 14, 81, 14, 0,	0, 0, 15, 0, 0,		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0,			0, 0, 15, 0, 0,		0, 14, 82, 14, 0,	0, 0, 15, 0, 0,		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0,			0, 0, 15, 0, 0,		0, 14, 83, 14, 0,	0, 0, 15, 0, 0,		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0,			0, 0, 15, 0, 0,		0, 14, 84, 14, 0,	0, 0, 15, 0, 0,		0, 0, 0, 0, 0},	//84
		{0, 0, 0, 0, 0,			0, 0, 15, 0, 0,		0, 14, 85, 14, 0,	0, 0, 15, 0, 0,		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0,			0, 0, 15, 0, 0,		0, 14, 86, 14, 0,	0, 0, 15, 0, 0,		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0,			0, 0, 15, 0, 0,		0, 14, 87, 14, 0,	0, 0, 15, 0, 0,		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		
		{0, 0, 0, 0, 0,			0, 0, 15, 0, 0, 	0, 14, 90, 14, 0, 	0, 0, 0, 0, 0,		0, 0, 0, 0, 0},	//90
		{0, 0, 0, 0, 0, 		0, 0, 15, 0, 0, 	0, 0, 91, 14, 0, 	0, 0, 15, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 14, 92, 14, 0, 	0, 0, 15, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 15, 0, 0, 	0, 14, 93, 0, 0, 	0, 0, 15, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0,			0, 0, 15, 0, 0, 	0, 14, 94, 14, 0, 	0, 0, 0, 0, 0,		0, 0, 0, 0, 0},	//94
		{0, 0, 0, 0, 0, 		0, 0, 15, 0, 0, 	0, 0, 95, 14, 0, 	0, 0, 15, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 14, 96, 14, 0, 	0, 0, 15, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 15, 0, 0, 	0, 14, 97, 0, 0, 	0, 0, 15, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		
		{0, 0, 0, 0, 0,			0, 0, 15, 0, 0,		0, 14, 100, 14, 0,	0, 0, 15, 0, 0,		0, 0, 0, 0, 0},	//100
		{0, 0, 0, 0, 0,			0, 0, 15, 0, 0,		0, 14, 101, 14, 0,	0, 0, 15, 0, 0,		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0,			0, 0, 15, 0, 0,		0, 14, 102, 14, 0,	0, 0, 15, 0, 0,		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0,			0, 0, 15, 0, 0,		0, 14, 103, 14, 0,	0, 0, 15, 0, 0,		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0,			0, 0, 15, 0, 0,		0, 14, 104, 14, 0,	0, 0, 15, 0, 0,		0, 0, 0, 0, 0},	//104
		{0, 0, 0, 0, 0,			0, 0, 15, 0, 0,		0, 14, 105, 14, 0,	0, 0, 15, 0, 0,		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0,			0, 0, 15, 0, 0,		0, 14, 106, 14, 0,	0, 0, 15, 0, 0,		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0,			0, 0, 15, 0, 0,		0, 14, 107, 14, 0,	0, 0, 15, 0, 0,		0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},	//110
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},	
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},	//120
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},	
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		
		{0, 0, 0, 15, 0, 		14, 14, 132, 15, 0, 		0, 134, 157, 11, 0, 		0, 	141, 153, 146, 0, 	0, 0, 0, 0, 0},	//130
		{0, 0, 0, 0, 0, 		0, 140, 147, 152, 0, 		0, 15, 133, 14, 14, 		0, 	135, 13, 156, 0, 	0, 0, 15, 0, 0},	
		{0, 15, 0, 0, 0, 		0, 131, 147, 10, 0, 		0, 143, 136, 15, 0, 		14, 14, 156, 151, 0, 	0, 0, 0, 0, 0},
		{0, 15, 0, 0, 0, 		0, 155, 144, 134, 0, 		0, 133, 16, 16, 14, 		0, 	153, 11, 141, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},	//140
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},	
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},	//150
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},	
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},	//160
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},	
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		
		{160, 15, 0, 0, 163, 		170, 11, 13, 14, 173, 		170, 14, 11, 13, 173, 		170, 14, 10, 12, 173, 	160, 0, 15, 0, 163},	//170
		{161, 171, 171, 171, 161, 		14, 11, 12, 11, 0, 		14, 14, 10, 0, 0, 		0, 	13, 16, 10, 0, 	162, 172, 172, 172, 162},	
		{160, 0, 0, 0, 163, 		170, 14, 10, 13, 173, 		170, 10, 12, 16, 173, 		170, 11, 13, 16, 173, 	160, 0, 15, 15, 163},	//170
		{161, 171, 171, 171, 161, 		0, 15, 15, 12, 14, 		14, 11, 12, 10, 0, 		0, 	13, 10, 15, 0, 	162, 172, 172, 172, 162},	
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
		{0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 0, 0, 0, 0, 		0, 	0, 0, 0, 0, 	0, 0, 0, 0, 0},
	};

	GameObject go_helpObj;
	public GameObject[] gHPipe;
	public HelpPipeScript[] sHPipe;
	
	const int NORTH = 0;
	const int EAST = 1;
	const int SOUTH = 2;
	const int WEST = 3;

	public static int help_PipeIndex;
	static int help_InOutIndex = 130;

	public static bool stop = false;

	//public static HelpPopupCon _instance;

	public static HelpPopupCon Instance;
//	{
//		get 
//		{ 
//			if(_instance == null)
//			{
//				GameObject obj = new GameObject ();
//				obj.hideFlags = HideFlags.HideAndDontSave;
//				_instance = obj.AddComponent<HelpPopupCon> ();
//
//			}
//			return _instance;
//		}
//	}

	// Use this for initialization
	void Awake () 
	{
		Instance = this;
		InitPopup();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void InitPopup()
	{
		stop = false;
		go_helpObj = GameObject.Find("HelpPipesObj");
		
		gHPipe = new GameObject[25];
		sHPipe = new HelpPipeScript[25];
		
		for(int i=0; i<25; i++)
		{
			gHPipe[i] = GameObject.Find("HPipe"+i);  
//			gHPipe[i] = Instantiate(Resources.Load("cwPrefabs/Prefab_HPipe"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
//			gHPipe[i].name = "HPipe"+i;
			
			gHPipe[i].transform.parent = go_helpObj.transform;
			gHPipe[i].transform.localScale = new Vector3(1, 1, 1);
			gHPipe[i].transform.localPosition = new Vector3(-180+90*(i%5), 180-90*(i/5), 0);
			sHPipe[i] = gHPipe[i].GetComponent<HelpPipeScript>();
			sHPipe[i].setIndex(i);
			sHPipe[i].setPos(i%5, i/5);
		}
	}



	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void setHelpPipe(int _index)
	{
		//int _index = _src_index%1000;
		switch(_index)
		{
		case 10: case 20: case 30: case 40: case 50: case 60:
		case 1010:
		case 1020:
		case 1030:
		case 1040:
		case 1050:
		case 1060:
			initHPipe(_index%1000);
			if(_index <= 1000){setAniStartNextPipe(11, WEST);}
			else {setAniStartNextPipe(17, SOUTH);}
			break;
			
		case 11: case 21: case 31: case 41: case 51: case 61:
		case 1011:
		case 1021:
		case 1031:
		case 1041:
		case 1051:
		case 1061:
			initHPipe(_index%1000);
			if(_index <= 1000){setAniStartNextPipe(11, WEST);}
			else {setAniStartNextPipe(7, NORTH);}
			break;
			
		case 12: case 22: case 32: case 42: case 52: case 62:
		case 1012:
		case 1022:
		case 1032:
		case 1042:
		case 1052:
		case 1062:
			initHPipe(_index%1000);
			if(_index <= 1000){setAniStartNextPipe(13, EAST);}
			else {setAniStartNextPipe(7, NORTH);}
			break;
			
		case 13: case 23: case 33: case 43: case 53: case 63:
		case 1013:
		case 1023:
		case 1033:
		case 1043:
		case 1053:
		case 1063:
			initHPipe(_index%1000);
			if(_index <= 1000){setAniStartNextPipe(13, EAST);}
			else {setAniStartNextPipe(17, SOUTH);}
			break;
			
		case 14: case 24: case 34: case 44: case 54: case 64:
		case 1014:
		case 1024:
		case 1034:
		case 1044:
		case 1054:
		case 1064:
			initHPipe(_index%1000);
			if(_index <= 1000){setAniStartNextPipe(11, WEST);}
			else {setAniStartNextPipe(13, EAST);}
			break;
			
		case 15: case 25: case 35: case 45: case 55: case 65:
		case 1015:
		case 1025:
		case 1035:
		case 1045:
		case 1055:
		case 1065:
			initHPipe(_index%1000);
			if(_index <= 1000){setAniStartNextPipe(7, NORTH);}
			else {setAniStartNextPipe(17, SOUTH);}
			break;
			
		case 16:
		case 1016:
		case 46:
		case 1046:
			initHPipe(_index%1000);
			if(_index <= 1000){setAniStartNextPipe(11, WEST);}
			else {setAniStartNextPipe(17, SOUTH);}
			break;
			
		case 70: case 90:
		case 1070: case 1090:
			initHPipe(_index%1000);
			if(_index <= 1000){setAniStartNextPipe(7, NORTH);}
			else {setAniStartNextPipe(7, NORTH);}
			break;
			
		case 71: case 91:
		case 1071: case 1091:
			initHPipe(_index%1000);
			if(_index <= 1000){setAniStartNextPipe(13, EAST);}
			else {setAniStartNextPipe(13, EAST);}
			break;
			
		case 72: case 92:
		case 1072: case 1092:
			initHPipe(_index%1000);
			if(_index <= 1000){setAniStartNextPipe(17, SOUTH);}
			else {setAniStartNextPipe(17, SOUTH);}
			break;
			
		case 73: case 93:
		case 1073: case 1093:
			initHPipe(_index%1000);
			if(_index <= 1000){setAniStartNextPipe(11, WEST);}
			else {setAniStartNextPipe(11, WEST);}
			break;
			
		case 74: case 94:
		case 1074: case 1094:
			initHPipe(_index%1000);
			if(_index <= 1000){
				setAniStartNextPipe(11, WEST);
				StartCoroutine(delay_OneSecond(13, EAST));
			}
			else {
				setAniStartNextPipe(13, EAST);
				StartCoroutine(delay_OneSecond(11, WEST));
			}
			break;
			
		case 75: case 95:
		case 1075: case 1095:
			initHPipe(_index%1000);
			if(_index <= 1000){
				setAniStartNextPipe(7, NORTH);
				StartCoroutine(delay_OneSecond(17, SOUTH));
			}
			else {
				setAniStartNextPipe(17, SOUTH);
				StartCoroutine(delay_OneSecond(7, NORTH));
			}
			break;
			
		case 76: case 96:
		case 1076: case 1096:
			initHPipe(_index%1000);
			if(_index <= 1000){
				setAniStartNextPipe(11, WEST);
				StartCoroutine(delay_OneSecond(13, EAST));
			}
			else {
				setAniStartNextPipe(13, EAST);
				StartCoroutine(delay_OneSecond(11, WEST));
			}
			break;
			
		case 77: case 97:
		case 1077: case 1097:
			initHPipe(_index%1000);
			if(_index <= 1000){
				setAniStartNextPipe(7, NORTH);
				StartCoroutine(delay_OneSecond(17, SOUTH));
			}
			else {
				setAniStartNextPipe(17, SOUTH);
				StartCoroutine(delay_OneSecond(7, NORTH));
			}
			break;
			
		case 80: case 100:
		case 1080: case 1100:
			initHPipe(_index%1000);
			if(_index <= 1000){setAniStartNextPipe(7, NORTH);}
			else {setAniStartNextPipe(7, NORTH);}
			break;
			
		case 81: case 101:
		case 1081: case 1101:
			initHPipe(_index%1000);
			if(_index <= 1000){setAniStartNextPipe(13, EAST);}
			else {setAniStartNextPipe(13, EAST);}
			break;
			
		case 82: case 102:
		case 1082: case 1102:
			initHPipe(_index%1000);
			if(_index <= 1000){setAniStartNextPipe(17, SOUTH);}
			else {setAniStartNextPipe(17, SOUTH);}
			break;
			
		case 83: case 103:
		case 1083: case 1103:
			initHPipe(_index%1000);
			if(_index <= 1000){setAniStartNextPipe(11, WEST);}
			else {setAniStartNextPipe(11, WEST);}
			break;
			
		case 84: case 104:
		case 1084: case 1104:
			initHPipe(_index%1000);
			if(_index <= 1000){
				if(Time.deltaTime%2 == 0)
				{
					setAniStartNextPipe(11, WEST);
					StartCoroutine(delay_Two(17, SOUTH, 13, EAST));
				}else{
					setAniStartNextPipe(17, SOUTH);
					StartCoroutine(delay_Two(11, WEST, 13, EAST));
				}
			}
			else {
				if(Time.deltaTime%2 == 0)
				{
					setAniStartNextPipe(13, EAST);
					StartCoroutine(delay_Two(17, SOUTH, 11, WEST));
				}else {
					setAniStartNextPipe(17, SOUTH);
					StartCoroutine(delay_Two(13, EAST, 11, WEST));
				}
			}
			break;
			
		case 85: case 105:
		case 1085: case 1105:
			initHPipe(_index%1000);
			if(_index <= 1000){
				if(Time.deltaTime%2 == 0)
				{
					setAniStartNextPipe(7, NORTH);
					StartCoroutine(delay_Two(11, WEST, 17, SOUTH));
				}else{
					setAniStartNextPipe(11, WEST);
					StartCoroutine(delay_Two(7, NORTH, 17, SOUTH));
				}
			}
			else {
				if(Time.deltaTime%2 == 0)
				{
					setAniStartNextPipe(17, SOUTH);
					StartCoroutine(delay_Two(11, WEST, 7, NORTH));
				}else {
					setAniStartNextPipe(11, WEST);
					StartCoroutine(delay_Two(17, SOUTH, 7, NORTH));
				}
			}
			break;
			
		case 86: case 106:
		case 1086: case 1106:
			initHPipe(_index%1000);
			if(_index <= 1000){
				if(Time.deltaTime%2 == 0)
				{
					setAniStartNextPipe(11, WEST);
					StartCoroutine(delay_Two(7, NORTH, 13, EAST));
				}else{
					setAniStartNextPipe(7, NORTH);
					StartCoroutine(delay_Two(11, WEST, 13, EAST));
				}
			}
			else {
				if(Time.deltaTime%2 == 0)
				{
					setAniStartNextPipe(13, EAST);
					StartCoroutine(delay_Two(7, NORTH, 11, WEST));
				}else {
					setAniStartNextPipe(7, NORTH);
					StartCoroutine(delay_Two(13, EAST, 11, WEST));
				}
			}
			break;
			
		case 87: case 107:
		case 1087: case 1107:
			initHPipe(_index%1000);
			if(_index <= 1000){
				if(Time.deltaTime%2 == 0)
				{
					setAniStartNextPipe(7, NORTH);
					StartCoroutine(delay_Two(13, EAST, 17, SOUTH));
				}else{
					setAniStartNextPipe(13, EAST);
					StartCoroutine(delay_Two(7, NORTH, 17, SOUTH));
				}
			}
			else {
				if(Time.deltaTime%2 == 0)
				{
					setAniStartNextPipe(17, SOUTH);
					StartCoroutine(delay_Two(13, EAST, 7, NORTH));
				}else {
					setAniStartNextPipe(13, EAST);
					StartCoroutine(delay_Two(17, SOUTH, 7, NORTH));
				}
			}
			break;
			
		case 130: case 131: case 132: case 133: case 134: case 135: case 136: case 137:
		case 140: case 141: case 142: case 143: case 144: case 145: case 146: case 147:
		case 150: case 151: case 152: case 153: case 154: case 155: case 156: case 157:
		case 1130: case 1131: case 1132: case 1133: case 1134: case 1135: case 1136: case 1137:
		case 1140: case 1141: case 1142: case 1143: case 1144: case 1145: case 1146: case 1147:
		case 1150: case 1151: case 1152: case 1153: case 1154: case 1155: case 1156: case 1157:
			help_InOutIndex++;
			if(help_InOutIndex > 133)
			{
				help_InOutIndex = 130;
			}
			initHPipe(help_InOutIndex);
			if(help_InOutIndex == 130)
			{
				setAniStartNextPipe(5, WEST);
			}
			else if(help_InOutIndex == 131)
			{
				setAniStartNextPipe(14, EAST);
			}
			else if(help_InOutIndex == 132)
			{
				setAniStartNextPipe(1, NORTH);
			}
			else if(help_InOutIndex == 133)
			{
				setAniStartNextPipe(14, EAST);
			}
			break;
			
		case 170: 
		case 1170:
			initHPipe(_index%1000);
			setAniStartNextPipe(1, NORTH);
			break;
			
		case 171: 
		case 1171: 
			initHPipe(_index%1000);
			setAniStartNextPipe(10, WEST);
			break;
		case 172: 
		case 1172: 
			initHPipe(_index%1000);
			setAniStartNextPipe(23, SOUTH);
			break;
		case 173:
		case 1173:
			initHPipe(_index%1000);
			setAniStartNextPipe(9, EAST);
			break;
		}
	}
	
	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	IEnumerator delay_OneSecond(int _idx, int _enterDirection){
		yield return new WaitForSeconds(0.5f); // wait 1 seconds
		//yield return new WaitForSeconds(efm.audioClip.length); //Waits till Audio is done playing
		//Application.LoadLevel(levelName);
		setAniStartNextPipe(_idx, _enterDirection);
		
	}
	
	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	IEnumerator delay_Two(int _idx, int _enterDirection, int _idx1, int _enterDirection1){
		yield return new WaitForSeconds(0.5f); // wait 1 seconds
		setAniStartNextPipe(_idx, _enterDirection);
		StartCoroutine(delay_OneSecond(_idx1, _enterDirection1));
	}
	
	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	private void initHPipe(int _index)
	{
		//		if(go_helpObj == null)
		//		{
		//			go_helpObj = GameObject.Find("HelpPipesObj");
		//		}
		
		if(_index < 130)
		{
			if(_index == 16 || _index == 46)
			{
				go_helpObj.transform.localScale = new Vector3(1.2f, 1.2f, 1);
			}
			else
			{
				go_helpObj.transform.localScale = new Vector3(2f, 2f, 1);
			}
			
			for(int i=0; i<25; i++)
			{
				if(i == 12)
				{
					sHPipe[i].setID(helpArr[_index%1000,i], 1);
				}
				else{
					sHPipe[i].setID(helpArr[_index%1000,i], 0);
				}
			}
		}
		else if(_index >= 130 && _index <160)
		{
			go_helpObj.transform.localScale = new Vector3(0.9f, 0.9f, 1);
			
			for(int i=0; i<25; i++)
			{
				sHPipe[i].setID(helpArr[_index%1000,i], 0);
			}
		}
		else if(_index >= 170 && _index <=173)
		{
			go_helpObj.transform.localScale = new Vector3(0.8f, 0.8f, 1);
			
			for(int i=0; i<25; i++)
			{
				sHPipe[i].setID(helpArr[_index%1000,i], 0);
			}
		}
		
		GameCon.bHelpAniCon = true;
	}
	
	/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void setAniStartNextPipe(int _next_idx, int enterDirection)
	{
		
		//		POS nextPos = _nextGo.GetComponent<PM> ().pos;
		
		switch( sHPipe[_next_idx].ID )
		{
		case 10: case 20: case 30:
		case 40: case 50: case 60:
			//			Debug.Log("enterDirection: "+enterDirection);
			//			Debug.Log("_next_idx: "+_next_idx);
			if(enterDirection == WEST)
			{
				sHPipe[_next_idx].setType (1);//_nextGo.GetComponent<P10> ().setType (1);
			}
			else if(enterDirection == SOUTH)
			{
				sHPipe[_next_idx].setType (2);
			}
			break;
			
		case 11: case 21: case 31:
		case 41: case 51: case 61:
			if(enterDirection == WEST)
			{
				sHPipe[_next_idx].setType (4);
			}
			else if(enterDirection == NORTH)
			{
				sHPipe[_next_idx].setType (3);
			}
			break;
			
		case 12: case 22: case 32:
		case 42: case 52: case 62:
			if(enterDirection == EAST)
			{
				sHPipe[_next_idx].setType (5);
			}
			else if(enterDirection == NORTH)
			{
				sHPipe[_next_idx].setType (6);
			}
			break;
			
		case 13: case 23: case 33:
		case 43: case 53: case 63:
			if(enterDirection == EAST)
			{
				sHPipe[_next_idx].setType (8);
			}
			else if(enterDirection == SOUTH)
			{
				sHPipe[_next_idx].setType (7);
			}
			break;
			
		case 14: case 24: case 34: 
		case 44: case 54: case 64:
			if(enterDirection == EAST)
			{
				sHPipe[_next_idx].setType (10);
			}
			else if(enterDirection == WEST)
			{
				sHPipe[_next_idx].setType (9);
			}
			break;
			
		case 15: case 25: case 35: 
		case 45: case 55: case 65: 
			if(enterDirection == NORTH)
			{
				sHPipe[_next_idx].setType (12);
			}
			else if(enterDirection == SOUTH)
			{
				sHPipe[_next_idx].setType (11);
			}
			break;
			
		case 16:
		case 46:
			//					Debug.Log ("_________nextGo---:"+_nextGo.GetComponent<PM>().posInfo.pipeID);
			//					Debug.Log ("------------------\n");
			if(enterDirection == NORTH)
			{
				sHPipe[_next_idx].setType (16);
			}
			else if(enterDirection == EAST)
			{
				sHPipe[_next_idx].setType (14);
			}
			else if(enterDirection == SOUTH)
			{
				sHPipe[_next_idx].setType (15);
			}
			else if(enterDirection == WEST)
			{
				sHPipe[_next_idx].setType (13);
			}
			break;
			
		case 70:
		case 90:
			if(enterDirection == NORTH)
			{
				sHPipe[_next_idx].setType (41);
			}
			break;
		case 71:
		case 91:
			if(enterDirection == EAST)
			{
				sHPipe[_next_idx].setType (42);
			}
			break;
		case 72:
		case 92:
			if(enterDirection == SOUTH)
			{
				sHPipe[_next_idx].setType (43);
			}
			break;
		case 73:
		case 93:
			if(enterDirection == WEST)
			{
				sHPipe[_next_idx].setType (44);
			}
			break;
			
		case 74:
		case 94:
			if(enterDirection == EAST)
			{
				sHPipe[_next_idx].setType (45);
			}
			else if(enterDirection == WEST)
			{
				sHPipe[_next_idx].setType (46);
			}
			break;
			
		case 75:
		case 95:
			if(enterDirection == NORTH)
			{
				sHPipe[_next_idx].setType (47);
			}
			else if(enterDirection == SOUTH)
			{
				sHPipe[_next_idx].setType (48);
			}
			break;
			
		case 76:
		case 96:
			if(enterDirection == EAST)
			{
				sHPipe[_next_idx].setType (49);
			}
			else if(enterDirection == WEST)
			{
				sHPipe[_next_idx].setType (50);
			}
			break;
			
		case 77:
		case 97:
			if(enterDirection == NORTH)
			{
				sHPipe[_next_idx].setType (51);
			}
			else if(enterDirection == SOUTH)
			{
				sHPipe[_next_idx].setType (52);
			}
			break;
			
			
		case 80:
		case 100:
			if(enterDirection == NORTH)
			{
				sHPipe[_next_idx].setType (25);
			}
			break;
		case 81:
		case 101:
			if(enterDirection == EAST)
			{
				sHPipe[_next_idx].setType (26);
			}
			break;
		case 82:
		case 102:
			if(enterDirection == SOUTH)
			{
				sHPipe[_next_idx].setType (27);
			}
			break;
		case 83:
		case 103:
			if(enterDirection == WEST)
			{
				sHPipe[_next_idx].setType (28);
			}
			break;
			
		case 84:
		case 104:
			if(enterDirection == EAST)
			{
				sHPipe[_next_idx].setType (29);
			}
			else if(enterDirection == SOUTH)
			{
				sHPipe[_next_idx].setType (30);
			}
			else if(enterDirection == WEST)
			{
				sHPipe[_next_idx].setType (31);
			}
			break;
			
		case 85:
		case 105:
			if(enterDirection == NORTH)
			{
				sHPipe[_next_idx].setType (32);
			}
			else if(enterDirection == SOUTH)
			{
				sHPipe[_next_idx].setType (33);
			}
			else if(enterDirection == WEST)
			{
				sHPipe[_next_idx].setType (34);
			}
			break;
			
		case 86:
		case 106:
			if(enterDirection == NORTH)
			{
				sHPipe[_next_idx].setType (35);
			}
			else if(enterDirection == EAST)
			{
				sHPipe[_next_idx].setType (36);
			}
			else if(enterDirection == WEST)
			{
				sHPipe[_next_idx].setType (37);
			}
			break;
			
		case 87:
		case 107:
			if(enterDirection == NORTH)
			{
				sHPipe[_next_idx].setType (38);
			}
			else if(enterDirection == EAST)
			{
				sHPipe[_next_idx].setType (39);
			}
			else if(enterDirection == SOUTH)
			{
				sHPipe[_next_idx].setType (40);
			}
			break;
			
			
		case 120:	//end
			sHPipe[_next_idx].setType (21);
			break;
			
		case 121:	//end
			sHPipe[_next_idx].setType (22);
			break;
			
		case 122:	//end
			sHPipe[_next_idx].setType (23);
			break;
			
		case 123:	//end
			sHPipe[_next_idx].setType (24);
			break;
			
		case 130:
		case 140:
		case 150:
			sHPipe[_next_idx].setType (21);
			break;
			
		case 131:
		case 141:
		case 151:
			sHPipe[_next_idx].setType (22);
			break;
			
		case 132:
		case 142:
		case 152:
			sHPipe[_next_idx].setType (23);
			break;
			
		case 133:
		case 143:
		case 153:
			sHPipe[_next_idx].setType (24);
			break;
			
		case 134:
		case 144:
		case 154:
			sHPipe[_next_idx].setType (17);
			break;
		case 135:
		case 145:
		case 155:
			sHPipe[_next_idx].setType (18);
			break;
		case 136:
		case 146:
		case 156:
			sHPipe[_next_idx].setType (19);
			break;
		case 137:
		case 147:
		case 157:
			sHPipe[_next_idx].setType (20);
			break;
			
		case 170:
			if(enterDirection == WEST)
			{
				sHPipe[_next_idx].setType (53);
			}
			else if(enterDirection == EAST)
			{
				sHPipe[_next_idx].setType (54);
			}
			break;
			
		case 171:
			if(enterDirection == SOUTH)
			{
				sHPipe[_next_idx].setType (57);
			}
			else if(enterDirection == NORTH)
			{
				sHPipe[_next_idx].setType (58);
			}
			break;
			
		case 172:
			if(enterDirection == SOUTH)
			{
				sHPipe[_next_idx].setType (59);
			}
			else if(enterDirection == NORTH)
			{
				sHPipe[_next_idx].setType (60);
			}
			break;
			
		case 173:
			if(enterDirection == WEST)
			{
				sHPipe[_next_idx].setType (55);
			}
			else if(enterDirection == EAST)
			{
				sHPipe[_next_idx].setType (56);
			}
			break;
		}
	}
}
