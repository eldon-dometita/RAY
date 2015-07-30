using UnityEngine;
using System.Collections;

public class NewRecordScript : MonoBehaviour {

	GameCon gameCon;
	PlayerData pd;
	bool bTimerStart;
	float timer;

	EffectSoundManagerScript efm;

	// Use this for initialization
	void Awake () {

		bTimerStart = false;
		timer = 0;
		gameCon = GameObject.Find ("GameCon").GetComponent<GameCon> ();
		pd = PlayerData.Instance;

		efm = EffectSoundManagerScript.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		if (bTimerStart) 
		{
			timer += Time.deltaTime;
			if(timer > 1f)
			{
				callback_finish1();
				bTimerStart = false;
			}
		}
	}
	
	public void move()
	{
		efm.Play(4);
		
		TweenScale twScale = TweenScale.Begin (this.gameObject, 0.3f, new Vector3 (1f, 1f, 1f));
		EventDelegate.Add( twScale.onFinished, callback_finish, true);
		//twScale.delay = 0.3f;
	}
	
	private void callback_finish()
	{
		this.transform.Rotate(new Vector3 (0, 0, 20));

		efm.Play(3);

		gameCon.createGItemEffectOnly(this.gameObject.transform.localPosition.x, this.gameObject.transform.localPosition.y);
		//gameCon.setUp_Reward();
		TweenScale twScale1 = TweenScale.Begin (this.gameObject, 2f, new Vector3 (1.2f, 1.2f, 1f));
		//EventDelegate.Add( twScale1.onFinished, callback_finish1, true);
		twScale1.style = UITweener.Style.Loop;
		bTimerStart = true;
		timer = 0;
	}

	private void callback_finish1()
	{
		gameCon.setUp_Reward();
	}
}
