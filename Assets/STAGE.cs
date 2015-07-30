using UnityEngine;
using System.Collections;

public class STAGE : MonoBehaviour {

	public int id;
	public int imgId; //0:lock 1:Active 2:1star 3:2star 4:3star 5:4star

	PlayerData pd;

	// Use this for initialization
	void Awake () {
		pd = PlayerData.Instance;
//		id = 0;
//		imgId = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setID(int _id)
	{
		if (_id >= 0 && _id < 1000) 
		{
			id = _id;
			//set id image with drawNum function
		}
	}

	public void setImg(int _imgId)
	{
		if (_imgId >= 0 && _imgId <= 5) 
		{
			imgId = _imgId;
			//set imgId
		}
	}

	void OnClick()
	{
		PlayerData.nowMapNumber1 = id;

		Application.LoadLevel ("GameStage");
		
		//mf = gameObject.AddComponent<MapFileController>();
		//mf.ReadNCreateFileMap(id+1);
	
//		//setting to connect gameplay...
//		// player data
//		pd = PlayerData.Instance;
//		// perfect plan
//		toggleguide = true;
//		// map class
//		mapFileController = gameObject.AddComponent<MapFileController>();
//		// load level
//		level = LevelController.Level;
//		// draw map
//		mapFileController.ReadNCreateFileMap(1);
//		// start check
//		CheckNext(gameObject)

	}
}
