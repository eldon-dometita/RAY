using UnityEngine;
using System.Collections;

public class StarEffectScript : MonoBehaviour {

	GameCon gameCon;
	PlayerData pd;
	// Use this for initialization
	void Awake () {
		gameCon = GameObject.Find ("GameCon").GetComponent<GameCon> ();
		pd = PlayerData.Instance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void move()
	{
		Vector3 v3pos1 = GameObject.Find ("STAR"+gameCon.nowStarIndex).transform.localPosition;

		TweenPosition twPosition = TweenPosition.Begin( this.gameObject, 0.5f, v3pos1 ); //VenderCon.VENDER_SPEED
		twPosition.delay = 0.3f;
		twPosition.method = UITweener.Method.Linear;
		EventDelegate.Add( twPosition.onFinished, callback_finish, true);
		
		TweenScale twScale = TweenScale.Begin (this.gameObject, 0.5f, new Vector3 (0.3f, 0.3f, 1f));
		twScale.delay = 0.3f;
	}

	private void callback_finish()
	{
//		if (pd.star [PlayerData.nowMapNumber1] <= gameCon.nowStarIndex + 1) 
//		{
			gameCon.setStar (gameCon.nowStarIndex, 1);
//		} else {
//			gameCon.setStar (gameCon.nowStarIndex, 0);
//		}
		gameCon.nowStarIndex++;
		if ( (gameCon.total_Star + gameCon.nowStarIndex)%20 == 0) 
		{
			gameCon.bInventoryMaxPlusText = true;
		}
		Destroy (this.gameObject);

		if(gameCon.bCheckCondition_Norm)
		{
			gameCon.goto_calScore ();
		}
	}

	public void move_perfect()
	{
		this.transform.FindChild("text_awesome").GetComponent<UILabel>().text = "AWESOME! \n [00FFFF]PERFECT CLEAR![-]";

		Vector3 v3pos1 = GameObject.Find ("STAR"+gameCon.nowStarIndex).transform.localPosition;
		
		TweenPosition twPosition = TweenPosition.Begin( this.gameObject, 0.5f, v3pos1 ); //VenderCon.VENDER_SPEED
		twPosition.delay = 1f;
		twPosition.method = UITweener.Method.Linear;
		EventDelegate.Add( twPosition.onFinished, callback_finish_perfect, true);
		
		TweenScale twScale = TweenScale.Begin (this.gameObject, 0.5f, new Vector3 (0.3f, 0.3f, 1f));
		twScale.delay = 1f;
	}
	
	private void callback_finish_perfect()
	{
		//		if (pd.star [PlayerData.nowMapNumber1] <= gameCon.nowStarIndex + 1) 
		//		{
		gameCon.setStar (gameCon.nowStarIndex, 1);
		//		} else {
		//			gameCon.setStar (gameCon.nowStarIndex, 0);
		//		}
		gameCon.nowStarIndex++;
		if ( (gameCon.total_Star + gameCon.nowStarIndex)%20 == 0) 
		{
			gameCon.bInventoryMaxPlusText = true;
		}
		Destroy (this.gameObject);


		gameCon.goto_calScore ();
	}
}

