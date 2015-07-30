using UnityEngine;
using System.Collections;

public class mapInfo : MonoBehaviour {

	public int stageNo;
	public int levelNo;
	public bool state;
	public int stars;
	
	string[] stageButtons;
	UISprite sprite;
	
	void Start () {
		sprite = GetComponent<UISprite>();
		stageButtons = new string[7];
		for (int i = 1; i < stageButtons.Length; i ++)
			stageButtons[i] = "StageButton" + i;
	}
	
	void Update () {
		if (state && stars >= 0)
		{
			sprite.spriteName = stageButtons[stars+2];
			GetComponent<BoxCollider>().enabled = true;
		}
		else if (!state)
		{
			sprite.spriteName = stageButtons[1];
			GetComponent<BoxCollider>().enabled = false;
		}
	}
}
