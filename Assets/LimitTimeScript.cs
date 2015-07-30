using UnityEngine;
using System.Collections;

public class LimitTimeScript : MonoBehaviour {

	GameCon gameCon;
	float timer;
	bool bStartTime;
	// Use this for initialization
	void Awake () {
		gameCon = GameObject.Find ("GameCon").GetComponent<GameCon> ();

		timer = 0;
		bStartTime = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (bStartTime) 
		{

			timer -= Time.deltaTime;

			//gameCon.showLimitTime(timer);

			if(timer <=0)
			{
				timer = 0;
				bStartTime = false;

				//go to result
				// if come here, stage fail....

				//water time lock.
				WaterTimeScript.bStartTimer = false;
				//all pipe animation lock
				GameCon.bAniCon = false;
				//all trigger lock
				gameCon.disable_all_trigger();
				//lock panel_scrollview
				gameCon.Lock_PanelScrollView ();
				//show hand indicator
				gameCon.setHandObj(2);	//time fail
				//show fail popup window
				gameCon.setUp_Popup_Fail(3);

			}
		}
	}

	void setTime(int _sec)
	{
		timer = _sec;
	}


	public void setPause()
	{
		bStartTime = false;
	}

	public void setResume()
	{
		bStartTime = true;
	}

	public void setStartTimer(int _sec)
	{
		bStartTime = true;
		timer = _sec;

	}


}
