using UnityEngine;
using System.Collections;

public class EndScoreScript : MonoBehaviour {

	float timer;
	GameCon gameCon;
	int show_step;
	bool bStartAni;
	void Awake()
	{
		bStartAni = false;
		show_step = 0;
		timer = 0;
		gameCon = GameObject.Find ("GameCon").GetComponent<GameCon> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (bStartAni) 
		{
			timer += Time.deltaTime;

			if (timer > 0.2f) {
				switch (show_step) 
				{
				case 0:	//default
						createScoreEffect (0);
						break;

				case 1:	//cross
						createScoreEffect (1);
						break;

				case 2:	//special
						createScoreEffect (2);
						break;

				case 3:	//over use
						createScoreEffect (3);
						break;

				case 4:	//time bonus
						createScoreEffect (4);
						break;

				case 5:	//perfect bonus
						createScoreEffect (5);
						break;

				case 6:	//missing
						createScoreEffect (6);
						break;

				case 7:	//total
						createScoreEffect (7);
						if(gameCon.bNewRecord)
						{
							gameCon.createNewRecordEffect();
							bStartAni = false;
							show_step = 11;
							timer = 0;
						}
						break;

				case 8:	//where to go?
						
						break;

				case 10:
					bStartAni = false;
					if(!gameCon.bNewRecord)
					{
						//checkLimitScore();
						gameCon.setUp_Reward();
						bStartAni = false;
					}
					break;
				}


				show_step++;
				timer = 0;
			}
		}
	}

	public void play()
	{
		timer = 0;
		bStartAni = true;
	}

	public void createScoreEffect(int _id)
	{
		int[] posY = {101, 66, 31, -4, -39, -74, -109, -165};
		GameObject _go = Instantiate(Resources.Load ("cwPrefabs/ScoreEffect"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		_go.transform.parent = GameObject.Find("Popup_Score").transform;
		
		_go.transform.localScale = new Vector3(1, 1, 1);

		Vector3 v3pos = _go.transform.localPosition;
		v3pos.x = 0;
		v3pos.y = posY[_id];

		_go.transform.localPosition = v3pos;

		_go.GetComponent<ScoreEffectScript> ().play (_id);
	}

	public void setTextScore(int _id)
	{
		gameCon.setTextScore(_id);
	}

//	public void checkLimitScore()
//	{
//		gameCon.check_limit_Condition_Score();
//	}
}
