using UnityEngine;
using System.Collections;
using System;

public class GameThread : MonoBehaviour {

	PlayerData pd;
	long currentTime;
	public static int runTime;
	public static int second_6hour = 21600;
	public static int second_24hour = 86400;

	public static int lifeTime;
	public static int seconde_Life_Minute = 480;//8minute
//	public static int second_6hour = 5;
//	public static int second_24hour = 5;
	// Use this for initialization
	void Start () {
		pd = PlayerData.Instance;
	}
	
	// Update is called once per frame
	void Update () {

		if (pd.bUnlimitTime == 1 || pd.bUnlimitTime == 2) 
		{
			currentTime = DateTime.UtcNow.ToFileTimeUtc();

			runTime = (int)((currentTime - pd.saveUnlimitTime)/10000000);

			if ( (pd.bUnlimitTime == 1 && runTime > second_6hour) 
			    || (pd.bUnlimitTime == 2 && runTime > second_24hour) ) 
			{
				pd.bUnlimitTime = 3;
				pd.saveData();
			}
		}

		if (pd.life < 5) {
				currentTime = DateTime.UtcNow.ToFileTimeUtc ();
				lifeTime = (int)((currentTime - pd.saveLifeTime) / 10000000);
	
				if (lifeTime > seconde_Life_Minute) {
						pd.life++;
						pd.saveLifeTime = currentTime;

						getLifeTime ();
						pd.saveData ();
				}
		}
//		
//		elapsedTicks = currentDate.Ticks - centuryBegin.Ticks;
//		elapsedSpan = new TimeSpan(elapsedTicks);

		//Debug.Log ("Time:   "+ DateTime.UtcNow);
	}

	public static string getLifeTime()
	{
		string returnString = "00:00";
		long tmpTime = (seconde_Life_Minute - GameThread.lifeTime);
		if (tmpTime < 0) 
		{
			return returnString;
		}
		int minute = (int)(tmpTime /60);
		int second = (int)(tmpTime % 60);
		returnString = string.Format("{0:D2}:", minute)+string.Format("{0:D2}", second);
		
		return returnString;
	}
}
