using UnityEngine;
using System.Collections;

public class WaterTimeScript : MonoBehaviour {

	GameCon gameCon;
	PlayerData pd;

	//float timer;
	public static bool bStartTimer;
	UILabel uil_time;

	GameObject go_Base;

	UISlider uiSlider;
	public float limitTime;		//connect value form stagedata
	bool bHurryUpText;
	// Use this for initialization

	void Awake () {

		bHurryUpText = true;

		gameCon = GameObject.Find("GameCon").GetComponent<GameCon>();
		pd = PlayerData.Instance;

		//timer = 0;
		bStartTimer = false;


		go_Base = GameObject.Find("waterTimeBase");
		uil_time = GameObject.Find("text_WaterTimeSlider").GetComponent<UILabel>();
		if (uil_time == null) {
			Debug.Log("this is null");
				}

		//uiSlider = this.GetComponent<UISlider> ();
	}
	
	// Update is called once per frame
	void Update () {

//		Debug.Log("bStartTimer"+ bStartTimer);
		if (bStartTimer) 
		{
			pd.flowTime -= Time.deltaTime;

			displayTimer();

			//uiSlider.value = (pd.flowTime / pd.flowDefaultTime);


			if(pd.flowTime <= 30)
			{
				if(bHurryUpText)
				{
					bHurryUpText = false;
					gameCon.setBigText(1);
				}
			}
			else
			{
				bHurryUpText = true;
			}

			if(pd.flowTime <= 0)
			{
				bStartTimer = false;

				this.gameObject.SetActive(false);
				gameCon.bWaterFlow = true;			//this value use for check, if water flow, no more use extention item...

				//water flow start.....
				gameCon.TurnOnWater();
				gameCon.bTurnOnWater = true;

				gameCon.efm.Play(6);

				gameCon.createTextNum(0, 0, "WATER START!", 2);
				//gameCon.TurnOnWaterSetup();

			}
		}
	}

	public void setPause()
	{
		bStartTimer = false;
	}

	public void setResume()
	{
		bStartTimer = true;
	}

	public void setStartTimer()
	{
		bStartTimer = true;
		limitTime = pd.flowTime;

	}

	public void displayTimer()
	{
		int minute = 0;
		int second = 0;



		minute = (int)(pd.flowTime/60);
		second = (int)(pd.flowTime%60);

		if(pd.flowTime < 10)
		{
			go_Base.GetComponent<UISprite>().height = 130;
			go_Base.transform.localPosition =  new Vector3(0, -70, 0);
			uil_time.transform.localPosition = new Vector3(0, -70, 0);
			uil_time.fontSize = 200;
			uil_time.text = "[FF0000]" + second + "[-]";
		}
		else if(pd.flowTime < 30)
		{
			go_Base.GetComponent<UISprite>().height = 60;
			go_Base.transform.localPosition =  new Vector3(0, -40, 0);
			uil_time.transform.localPosition = new Vector3(0, -40, 0);
			uil_time.fontSize = 80;
			//uil_time.text = "[FF0000]"+string.Format("{0:D2}", minute) + " : "+string.Format("{0:D2}[-]", second);
			uil_time.text = "[FF0000]"+ minute + " : "+string.Format("{0:D2}[-]", second);
		}
		else
		{
			go_Base.GetComponent<UISprite>().height = 60;
			go_Base.transform.localPosition =  new Vector3(0, -40, 0);
			uil_time.transform.localPosition = new Vector3(0, -40, 0);
			uil_time.fontSize = 80;
			//uil_time.text = string.Format("{0:D2}", minute) + " : "+string.Format("{0:D2}", second);
			uil_time.text = minute + " : "+string.Format("{0:D2}", second);
		}

		
	}
}
