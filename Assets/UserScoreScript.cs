using UnityEngine;
using System.Collections;

public class UserScoreScript : MonoBehaviour {

	GameCon gameCon;
	//UISlider uiSlider;
	float tempValue1 = 0;
	float tempValue2 = 0;
	// Use this for initialization
	void Start () {
	
		gameCon = GameObject.Find("GameCon").GetComponent<GameCon>();
//		uiSlider = this.GetComponent<UISlider> ();

		tempValue2 = 0;

	}
	
	// Update is called once per frame
	void Update () {

		if(tempValue2 == 0)
		{
			tempValue2 = GameCon.scoreMax;
		}

		if (tempValue2 > 0) 
		{
			tempValue1 = GameCon.userScore;
//			uiSlider.value = (tempValue1 / tempValue2);
		}
	}
}
