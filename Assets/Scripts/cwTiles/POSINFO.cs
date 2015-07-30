using UnityEngine;
using System.Collections;

public class POSINFO{

	public int pipeID;
	public int Idx;
	public int direction;
//	public int xpos;
//	public int ypos;
	public POS pos;


	public bool bEndFinish;
	public bool bEndFinish_connect;

	public bool[] bFinish;


	//cal connect
	public int direction_connect;
	public bool[] bFinish_connect;
	public bool[] bFinish_In_connect;

	public int endCount;

	public POSINFO()
	{
		pipeID = 0;
		Idx = 0;
		direction = 0;
		pos = new POS ();
		bFinish = new bool[4];
		bFinish_connect = new bool[4];
		bFinish_In_connect = new bool[4];

		for (int i=0; i<4; i++) 
		{
			bFinish[i] = false;
			bFinish_connect[i] = false;
			bFinish_In_connect[i] = false;
		}

		bEndFinish = false;
		endCount = 0;
	}

	public void setInfo(int _pipeID, int _idx, int _dir, int _x, int _y)
	{
		pipeID = _pipeID;
		Idx = _idx;
		direction = _dir;
		pos.setPos (_x, _y);
		setFinish(_pipeID);
	}

	private void setFinish(int _pipeID)
	{
		for (int i=0; i<4; i++) 
		{
			bFinish [i] = false;
		}

		switch(_pipeID)
		{
			//0, 1
		case 10: case 40:
			bFinish[0] = true;
			bFinish[1] = true;
			break;

			//1, 2
		case 11: case 41:
			bFinish[1] = true;
			bFinish[2] = true;
			break;

			//2, 3
		case 12: case 42:
			bFinish[2] = true;
			bFinish[3] = true;
			break;

			//0, 3
		case 13: case 43:
			bFinish[0] = true;
			bFinish[3] = true;
			break;

			//0, 2
		case 14: case 44: case 74: case 94: case 76: case 96:
			bFinish[0] = true;
			bFinish[2] = true;
			break;

			//1, 3
		case 15: case 45: case 75: case 95: case 77: case 97:
			bFinish[1] = true;
			bFinish[3] = true;
			break;

			//0, 1, 2
		case 20: case 50:
		case 31: case 61:
		case 34: case 64:
		case 112: case 122: case 132: case 142: case 152:
		case 136: case 146: case 156: case 73: case 93:
		case 83: case 103: 
			bFinish[0] = true;
			bFinish[1] = true;
			bFinish[2] = true;
			break;

			//1, 2, 3
		case 21: case 51:
		case 32: case 62:
		case 35: case 65:
		case 111: case 121: case 131: case 141: case 151:
		case 135: case 145: case 155: case 70: case 90:
		case 80: case 100: 
			bFinish[1] = true;
			bFinish[2] = true;
			bFinish[3] = true;
			break;

			//0, 2, 3
		case 22: case 52:
		case 24: case 54:
		case 33: case 63:
		case 113: case 123: case 133: case 143: case 153:
		case 137: case 147: case 157: case 71: case 91:
		case 81: case 101: 
			bFinish[2] = true;
			bFinish[3] = true;
			bFinish[0] = true;
			break;

			//0, 1, 3
		case 23: case 53: 
		case 25: case 55: 
		case 30: case 60:
		case 110: case 120: case 130: case 140: case 150:
		case 134: case 144: case 154: case 72: case 92:
		case 82: case 102: 
			bFinish[1] = true;
			bFinish[3] = true;
			bFinish[0] = true;
			break;

		case 160: case 161: case 162: case 163:
		case 164: case 165: case 166: case 167:
		case 174: case 175: case 176: case 177:
		case 3: case 1: case 2:
		case 180:
		case 181:
		case 182:
		case 183:
		case 184:
		case 185:
		case 186:
		case 187:
		case 188:
		case 189:
		case 190:
		case 191:
			bFinish[0] = true;
			bFinish[1] = true;
			bFinish[2] = true;
			bFinish[3] = true;
			break;


		}
	}


	public void setFinish_connect()
	{
		for (int i=0; i<4; i++) 
		{
			bFinish_connect[i] = false;
			bFinish_In_connect[i] = false;
		}
		bEndFinish_connect = false;

		switch(pipeID)
		{
		case 0:
		case 1:
			for (int i=0; i<4; i++) 
			{
				bFinish_connect[i] = true;
			}
			break;
			//0, 1
		case 10: case 40:
			bFinish_connect[0] = true;
			bFinish_connect[1] = true;
			break;
			
			//1, 2
		case 11: case 41:
			bFinish_connect[1] = true;
			bFinish_connect[2] = true;
			break;
			
			//2, 3
		case 12: case 42:
			bFinish_connect[2] = true;
			bFinish_connect[3] = true;
			break;
			
			//0, 3
		case 13: case 43:
			bFinish_connect[0] = true;
			bFinish_connect[3] = true;
			break;
			
			//0, 2
		case 14: case 44: case 74: case 94: case 76: case 96:
			bFinish_connect[0] = true;
			bFinish_connect[2] = true;
			break;
			
			//1, 3
		case 15: case 45: case 75: case 95: case 77: case 97:
			bFinish_connect[1] = true;
			bFinish_connect[3] = true;
			break;
			
			//0, 1, 2
		case 20: case 50:
		case 31: case 61:
		case 34: case 64:
		case 112: case 122: case 132: case 142: case 152:
		case 136: case 146: case 156: case 73: case 93:
		case 83: case 103: 
			bFinish_connect[0] = true;
			bFinish_connect[1] = true;
			bFinish_connect[2] = true;
			break;
			
			//1, 2, 3
		case 21: case 51:
		case 32: case 62:
		case 35: case 65:
		case 111: case 121: case 131: case 141: case 151:
		case 135: case 145: case 155: case 70: case 90:
		case 80: case 100: 
			bFinish_connect[1] = true;
			bFinish_connect[2] = true;
			bFinish_connect[3] = true;
			break;
			
			//0, 2, 3
		case 22: case 52:
		case 24: case 54:
		case 33: case 63:
		case 113: case 123: case 133: case 143: case 153:
		case 137: case 147: case 157: case 71: case 91:
		case 81: case 101: 
			bFinish_connect[2] = true;
			bFinish_connect[3] = true;
			bFinish_connect[0] = true;
			break;
			
			//0, 1, 3
		case 23: case 53: 
		case 25: case 55: 
		case 30: case 60:
		case 110: case 120: case 130: case 140: case 150:
		case 134: case 144: case 154: case 72: case 92:
		case 82: case 102: 
			bFinish_connect[1] = true;
			bFinish_connect[3] = true;
			bFinish_connect[0] = true;
			break;
			
		case 160: case 161: case 162: case 163:
		case 164: case 165: case 166: case 167:
		case 174: case 175: case 176: case 177:
		case 3:

		case 2:
		case 180:
		case 181:
		case 182:
		case 183:
		case 184:
		case 185:
		case 186:
		case 187:
		case 188:
		case 189:
		case 190:
		case 191:
			bFinish_connect[0] = true;
			bFinish_connect[1] = true;
			bFinish_connect[2] = true;
			bFinish_connect[3] = true;
			break;
			
			
		}
	}


//	public bool checkAllFinish()
//	{
//		bool returnValue = true;
//
//		for (int i=0; i<4; i++) 
//		{
//			if(!bFinish[i])
//			{
//				returnValue = false;
//			}
//		}
//		return returnValue;
//	}

}
