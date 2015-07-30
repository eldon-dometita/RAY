using UnityEngine;
using System.Collections;

public class GItemEffectScript : MonoBehaviour {


	public float frame;
	public float timer;
	UITexture uit;
	// Use this for initialization
	void Awake () {
		frame = 0;
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

		if (frame == 0) 
		{
			uit.mainTexture = Resources.Load("Images/Effect/GItemEffect/hit_4x2001") as Texture;
		}
		else if (frame == 1) 
		{
			uit.mainTexture = Resources.Load("Images/Effect/GItemEffect/hit_4x2002") as Texture;
		}
		else if (frame == 2) 
		{
			uit.mainTexture = Resources.Load("Images/Effect/GItemEffect/hit_4x2003") as Texture;
		}
		else if (frame == 3) 
		{
			uit.mainTexture = Resources.Load("Images/Effect/GItemEffect/hit_4x2004") as Texture;
		}
		else if (frame == 4) 
		{
			uit.mainTexture = Resources.Load("Images/Effect/GItemEffect/hit_4x2005") as Texture;
		}
		else if (frame == 5) 
		{
			uit.mainTexture = Resources.Load("Images/Effect/GItemEffect/hit_4x2006") as Texture;
		}
		else if (frame == 6) 
		{
			uit.mainTexture = Resources.Load("Images/Effect/GItemEffect/hit_4x2007") as Texture;
		}
		else if (frame == 7) 
		{
			//uit.mainTexture = Resources.Load("Images/Effect/GItemEffect/hit_4x2000") as Texture;
			frame = 0;
			Destroy(this.gameObject);
		}

	}
}
