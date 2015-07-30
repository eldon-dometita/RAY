using UnityEngine;
using System.Collections;

public class BombEffedtScript : MonoBehaviour {

	public float frame;
	public float timer;
	UITexture uit;
	string str;
	int[] index = 
	{
		0, 3, 7, 10, 13, 16, 19, 22
	};
	// Use this for initialization
	void Awake () {
		frame = 0;
		str = " ";
		uit = this.transform.FindChild("Effect").GetComponent<UITexture> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		timer += Time.deltaTime;
		if (timer > 0.05f) {
			frame++;
			timer = 0;
		}
		
		
		//Debug.Log (uit.name);
		//Debug.Log (uit.mainTexture);
		str = "Images/Effect/BombEffect/explosion";

		if (frame == 8) 
		{
				frame = 0;
				Destroy (this.gameObject);
		}
		else
		{
			uit.mainTexture = Resources.Load(str+index[(int)(frame)]) as Texture;
		}

	}
}
