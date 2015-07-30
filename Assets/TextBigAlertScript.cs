using UnityEngine;
using System.Collections;

public class TextBigAlertScript : MonoBehaviour {

	GameCon gameCon;

	float timer;
	int alertType;
	bool bStartText;
	int START_POSITION_Y;
	PlayerData pd;

	void Awake()
	{
		gameCon = GameObject.Find ("GameCon").GetComponent<GameCon>();
		pd = PlayerData.Instance;

		START_POSITION_Y = 600;
		alertType = 0;
		timer = 0;
		bStartText = false;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (bStartText) 
		{

			timer += Time.deltaTime;
			//Debug.Log("timer:"+timer);
			switch(alertType)
			{
			case 1:
				break;

			case 2:
				if(timer > 1f)
				{
					setUpHurryUp1();
				}
				break;

			case 3:
				break;


			case 10:
				if(timer > 1f)
				{
					callback_Ready_Step0();
				}
				break;

			case 11:
				if(timer > 1f)
				{
					callback_Ready_Step1();
				}
				break;

			case 12:
				if(timer > 1f)
				{
					callback_Ready_Step2();
				}
				break;

			case 13:
				if(timer > 1f)
				{
					callback_Ready_Step3();
				}
				break;

			case 14:
				if(timer > 1f)
				{
					callback_Ready_Step4();
				}
				break;

			case 15:
				if(timer > 1f)
				{
					callback_Ready_Step5();
				}
				break;

			case 16:
				break;



			case 20:
				break;

			case 21:
				if(timer > 1f)
				{
					setUpFail1();
				}
				break;

			case 22:
				break;

			case 23:
				break;
			}
		}
	}


	/// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// /////

	/// /// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// /////
	public void setUpFail()
	{
		//Debug.Log("1");
		bStartText = true;
		timer = 0;
		alertType = 20;

		this.GetComponent<UILabel>().text = "FAIL!";
		this.GetComponent<UILabel> ().color = new Color32 (237, 28, 36, 255);
		
		Vector3 v3pos = this.transform.localPosition;
		v3pos.x = 0;
		v3pos.y = -START_POSITION_Y;
		
		this.transform.localPosition = v3pos;
		
		v3pos.y = 0;
		
		TweenPosition twPosition = TweenPosition.Begin( this.gameObject, 0.5f, v3pos ); //VenderCon.VENDER_SPEED
		twPosition.method = UITweener.Method.BounceIn;
		EventDelegate.Add( twPosition.onFinished, callback_Fail_Step1, true);
	}

	void callback_Fail_Step1()
	{
		//Debug.Log("2");
		timer = 0;
		alertType = 21;
		
	}
	
	public void setUpFail1()
	{
		//Debug.Log("3");
		alertType = 22;
		
		Vector3 v3pos = this.transform.localPosition;
		
		v3pos.y = START_POSITION_Y;
		
		TweenPosition twPosition = TweenPosition.Begin( this.gameObject, 0.5f, v3pos ); //VenderCon.VENDER_SPEED
		twPosition.method = UITweener.Method.BounceOut;
		EventDelegate.Add( twPosition.onFinished, callback_Fail_Step2, true);
		
		
	}
	
	void callback_Fail_Step2()
	{
		timer = 0;
		alertType = 0;
		bStartText = false;

		gameCon.setUp_Popup_Fail (0);
	}




	/// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// /////
	public void setUpReady()
	{
		bStartText = true;
		timer = 0;
		alertType = 10;
	}

	void callback_Ready_Step0()
	{
		gameCon.efm.Play(7);
		timer = 0;
		alertType = 11;

		this.GetComponent<UILabel>().text = "READY!";
		this.GetComponent<UILabel> ().color = new Color32 (0, 255, 255, 255);

		Vector3 v3pos = this.transform.localPosition;
		v3pos.x = 0;
		v3pos.y = -START_POSITION_Y;
		
		this.transform.localPosition = v3pos;
		
		v3pos.y = 0;

		TweenPosition twPosition = TweenPosition.Begin( this.gameObject, 0.5f, v3pos ); //VenderCon.VENDER_SPEED
		twPosition.method = UITweener.Method.BounceIn;
		//EventDelegate.Add( twPosition.onFinished, callback_Ready_Step1, true);
	}

	void callback_Ready_Step1()
	{
		gameCon.efm.Play(7);
		timer = 0;
		alertType = 12;

		this.GetComponent<UILabel>().text = "3";

		Vector3 v3pos = new Vector3 (1.2f, 1.2f, 1f);
		TweenScale twScale = TweenScale.Begin( this.gameObject, 0.2f, v3pos ); //VenderCon.VENDER_SPEED
		EventDelegate.Add( twScale.onFinished, callback_orgin_scale, true);

	}

	void callback_orgin_scale()
	{
		Vector3 v3pos = new Vector3 (1f, 1f, 1f);
		this.transform.localScale = v3pos;
	}

	void callback_Ready_Step2()
	{
		gameCon.efm.Play(7);
		timer = 0;
		alertType = 13;
		
		this.GetComponent<UILabel>().text = "2";

		Vector3 v3pos = new Vector3 (1.2f, 1.2f, 1f);
		TweenScale twScale = TweenScale.Begin( this.gameObject, 0.2f, v3pos ); //VenderCon.VENDER_SPEED
		EventDelegate.Add( twScale.onFinished, callback_orgin_scale, true);
	}

	void callback_Ready_Step3()
	{
		gameCon.efm.Play(7);
		timer = 0;
		alertType = 14;
		
		this.GetComponent<UILabel>().text = "1";

		Vector3 v3pos = new Vector3 (1.2f, 1.2f, 1f);
		TweenScale twScale = TweenScale.Begin( this.gameObject, 0.2f, v3pos ); //VenderCon.VENDER_SPEED
		EventDelegate.Add( twScale.onFinished, callback_orgin_scale, true);
	}

	void callback_Ready_Step4()
	{
		gameCon.efm.Play(7);
		timer = 0;
		alertType = 15;
		
		this.GetComponent<UILabel>().text = "START!";

		Vector3 v3pos = new Vector3 (1.2f, 1.2f, 1f);
		TweenScale twScale = TweenScale.Begin( this.gameObject, 0.2f, v3pos ); //VenderCon.VENDER_SPEED
		EventDelegate.Add( twScale.onFinished, callback_orgin_scale, true);
	}

	public void callback_Ready_Step5()
	{
		alertType = 16;
		
		Vector3 v3pos = this.transform.localPosition;
		
		v3pos.y = START_POSITION_Y;
		
		TweenPosition twPosition = TweenPosition.Begin( this.gameObject, 0.5f, v3pos ); //VenderCon.VENDER_SPEED
		twPosition.method = UITweener.Method.BounceOut;
		EventDelegate.Add( twPosition.onFinished, callback_Ready_Step6, true);
	}
	
	void callback_Ready_Step6()
	{
		timer = 0;
		alertType = 0;
		bStartText = false;

		WaterTimeScript.bStartTimer = true;

		gameCon.toggleRightItemActive(true);
		gameCon.bGameStart = true;

		gameCon.moveIn_InventoryItem();

//		if(gameCon.limitTime > 0)
//		{
//			gameCon.bCheckLimitTimeCondition = true;
//			GameObject.Find ("TimeLimitObj").GetComponent<LimitTimeScript> ().setStartTimer(gameCon.limitTime);
//		}
	}

	/// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// /////



	/// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// /////
	public void setUpHurryUp()
	{
		bStartText = true;
		alertType = 1;

		this.GetComponent<UILabel>().text = "Hurry Up!";
		this.GetComponent<UILabel> ().color = new Color32 (237, 28, 36, 255);

		Vector3 v3pos = this.transform.localPosition;
		v3pos.x = 0;
		v3pos.y = -START_POSITION_Y;

		this.transform.localPosition = v3pos;

		v3pos.y = 0;

		TweenPosition twPosition = TweenPosition.Begin( this.gameObject, 0.5f, v3pos ); //VenderCon.VENDER_SPEED
		twPosition.method = UITweener.Method.BounceIn;
		EventDelegate.Add( twPosition.onFinished, callback_HurryUp_Step1, true);


	}

	void callback_HurryUp_Step1()
	{
		timer = 0;
		alertType = 2;

	}

	public void setUpHurryUp1()
	{
		alertType = 3;
		
		Vector3 v3pos = this.transform.localPosition;

		v3pos.y = START_POSITION_Y;
		
		TweenPosition twPosition = TweenPosition.Begin( this.gameObject, 0.5f, v3pos ); //VenderCon.VENDER_SPEED
		twPosition.method = UITweener.Method.BounceOut;
		EventDelegate.Add( twPosition.onFinished, callback_HurryUp_Step2, true);
		
		
	}

	void callback_HurryUp_Step2()
	{
		timer = 0;
		alertType = 0;
		bStartText = false;
	}

	/// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// //////// /////
}
