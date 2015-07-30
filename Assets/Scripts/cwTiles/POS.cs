using UnityEngine;
using System.Collections;

public class POS{

	public int x;
	public int y;

	public POS()
	{
		x = 0;
		y = 0;
	}

	public POS(int _xpos, int _ypos)
	{
		x = _xpos;
		y = _ypos;
	}

	public void setPos(int _xpos, int _ypos)
	{
		x = _xpos;
		y = _ypos;
	}
}
