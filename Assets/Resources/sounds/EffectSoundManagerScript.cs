using UnityEngine;
using System.Collections;

public class EffectSoundManagerScript : MonoBehaviour {


	public AudioClip audioClip;

	AudioClip audioClip_menu;
	AudioClip audioClip_putPipe;
	AudioClip audioClip_itemEat;
	AudioClip audioClip_bomb;
	AudioClip audioClip_fly;

	AudioClip audioClip_conveyorBelt;
	AudioClip audioClip_water;
	AudioClip audioClip_countDown;
	AudioClip audioClip_popup;
	AudioClip audioClip_scoreEffect;
	//AudioClip audioClip_popup;

	[Range(0f, 1f)] public float volume = 1f;
	[Range(0f, 2f)] public float pitch = 1f;


	private static EffectSoundManagerScript _instance;
	// Static singleton property
	public static EffectSoundManagerScript Instance
	{
		// Here we use the ?? operator, to return 'instance' if 'instance' does not equal null
		// otherwise we assign instance to a new component and return that
		get 
		{ 
			if(_instance == null)
			{
				GameObject obj = new GameObject ();
				obj.hideFlags = HideFlags.HideAndDontSave;
				_instance = obj.AddComponent<EffectSoundManagerScript> ();
				//DontDestroyOnLoad(obj);
				_instance.init();
			}
			
			return _instance;
		}
	}

	void init()
	{
		audioClip_menu = Resources.Load("sounds/menu") as AudioClip;
		audioClip_putPipe= Resources.Load("sounds/putPipe") as AudioClip;
		audioClip_itemEat = Resources.Load("sounds/Pulsar Shot") as AudioClip;
		audioClip_bomb = Resources.Load("sounds/bomb") as AudioClip;
		audioClip_fly = Resources.Load("sounds/Sword Whoosh 01") as AudioClip;
		
		audioClip_conveyorBelt = Resources.Load("sounds/Printer") as AudioClip;
		audioClip_water = Resources.Load("sounds/Water Boil") as AudioClip;
		audioClip_countDown = Resources.Load("sounds/CountDown") as AudioClip;
		audioClip_popup = Resources.Load("sounds/Popup") as AudioClip;
		audioClip_scoreEffect = Resources.Load("sounds/ScoreEffect") as AudioClip;
	}

	public void Play (int _index_effect)
	{

		switch(_index_effect)
		{
		case 0:	//default menu
			audioClip = audioClip_menu;//Resources.Load("sounds/menu") as AudioClip;
			NGUITools.PlaySound(audioClip, volume, pitch);
			break;

		case 1:	//put pipe
			audioClip = audioClip_putPipe;//Resources.Load("sounds/putPipe") as AudioClip;
			NGUITools.PlaySound(audioClip, volume, pitch);
			break;

		case 2:	//item eat
			audioClip = audioClip_itemEat;//Resources.Load("sounds/Pulsar Shot") as AudioClip;
			NGUITools.PlaySound(audioClip, volume, pitch);
			break;

		case 3:	//bomb
			audioClip = audioClip_bomb;//Resources.Load("sounds/bomb") as AudioClip;
			NGUITools.PlaySound(audioClip, volume, pitch);
			break;

		case 4:	//fly pipe
			audioClip = audioClip_fly;//Resources.Load("sounds/Sword Whoosh 01") as AudioClip;
			NGUITools.PlaySound(audioClip, volume, pitch);
			break;

		

		case 5:	//convery belt
			if(GameObject.Find("SoundEffect_Printer") == null)
			{
				audioClip = audioClip_conveyorBelt;//Resources.Load("sounds/Printer") as AudioClip;
				GameObject go_Snd = Instantiate(Resources.Load("cwPrefabs/SoundEffect")) as GameObject;
				go_Snd.name = "SoundEffect_Printer";
				go_Snd.GetComponent<SoundEffect>().PlayLoop(audioClip);
			}
			break;

		case 6:	//water
			if(GameObject.Find("SoundEffect_Water") == null)
			{
				audioClip = audioClip_water;//Resources.Load("sounds/Water Boil") as AudioClip;
				GameObject go_Snd = Instantiate(Resources.Load("cwPrefabs/SoundEffect")) as GameObject;
				go_Snd.name = "SoundEffect_Water";
				go_Snd.GetComponent<SoundEffect>().PlayLoop(audioClip);
			}
			break;

		case 7:	//CoundDonw
			audioClip = audioClip_countDown;//Resources.Load("sounds/CountDown") as AudioClip;
			NGUITools.PlaySound(audioClip, volume, pitch);
			break;

		case 8:	//Popup
			audioClip = audioClip_popup;//Resources.Load("sounds/Popup") as AudioClip;
			NGUITools.PlaySound(audioClip, volume, pitch);
			break;

		case 9:	//Popup
			audioClip = audioClip_scoreEffect;//Resources.Load("sounds/ScoreEffect") as AudioClip;
			NGUITools.PlaySound(audioClip, volume, pitch);
			break;

		case 100:	//Bgm1
			if(GameObject.Find("SoundEffect_BGM") == null)
			{
				audioClip = Resources.Load("sounds/BGM1") as AudioClip;
				GameObject go_Snd = Instantiate(Resources.Load("cwPrefabs/SoundEffect")) as GameObject;
				go_Snd.name = "SoundEffect_BGM";
				go_Snd.GetComponent<SoundEffect>().PlayLoop(audioClip);
			}
			break;
		}

	}

	public void stop(int _index_effect)
	{
		GameObject go_Snd = null;
		switch(_index_effect)
		{
		case 0:	//default menu
			break;
			
		case 1:	//put pipe
			break;
			
		case 2:	//item eat
			break;
			
		case 3:	//bomb
			break;
			
		case 4:	//fly pipe
			break;
			
		case 5:	//convery belt
			go_Snd = GameObject.Find("SoundEffect_Printer");
			if(go_Snd != null)
			{
				Destroy(go_Snd);
			}
			break;
			
		case 6:	//water
			go_Snd = GameObject.Find("SoundEffect_Water");
			if(go_Snd != null)
			{
				Destroy(go_Snd);
			}
			break;

		case 100:	//BGM
			go_Snd = GameObject.Find("SoundEffect_BGM");
			if(go_Snd != null)
			{
				Destroy(go_Snd);
			}
			break;
		}
	}

}
