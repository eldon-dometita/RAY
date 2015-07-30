using UnityEngine;
using System.Collections;

public class SoundManagerScript : MonoBehaviour {


	private static SoundManagerScript _instance;
	// Static singleton property
	public static SoundManagerScript Instance
	{
		// Here we use the ?? operator, to return 'instance' if 'instance' does not equal null
		// otherwise we assign instance to a new component and return that
		get 
		{ 
			if(_instance == null)
			{
				
				//				_instance = new GameObject("PlayerData").AddComponent<PlayerData>(); 
				//				_instance.Init();
				GameObject obj = new GameObject ();
				obj.hideFlags = HideFlags.HideAndDontSave;
				_instance = obj.AddComponent<SoundManagerScript> ();
				DontDestroyOnLoad(obj);
			}
			
			return _instance;
		}
	}


	// any other methods you need
}

