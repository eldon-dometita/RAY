using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {

	float timer;
	int sec;
	bool bStartAni;
	GameCon gameCon;

	int xpos;
	int ypos;

	// Use this for initialization
	void Awake () {

		gameCon = GameObject.Find ("GameCon").GetComponent<GameCon> ();
		bStartAni = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (bStartAni && WaterTimeScript.bStartTimer) 
		{
			timer -= Time.deltaTime;
			sec = (int)(timer);
			this.GetComponent<UILabel> ().text = sec.ToString ();
			if (timer <= 0) 
			{
//				_go = GameObject.Find ("GItem" + (_pos.y * nCol + _pos.x));
				GameObject _go_parent = this.transform.parent.transform.parent.gameObject;

				gameCon.createBombEffect(_go_parent.transform.localPosition, _go_parent);
				gameCon.InitEatItem(xpos, ypos);
				Destroy(_go_parent);

				//change fixed tile

				GameObject _go_nowBaseTile = GameObject.Find("Base"+(ypos*GameCon.nCol+xpos)) as GameObject; 
				Destroy(_go_nowBaseTile);
				GameObject _go_nowTile = GameObject.Find("Pipe"+(ypos*GameCon.nCol+xpos)) as GameObject; 
				Vector3 v3pos = _go_nowTile.transform.localPosition;
				Destroy(_go_nowTile);
				gameCon.createPref_FixedTile(184, v3pos, xpos, ypos);
			}
		}
	}

	public void play(int _sec, int _x, int _y)
	{
		timer = _sec;
		bStartAni = true;
		xpos = _x;
		ypos = _y;
	}
}
