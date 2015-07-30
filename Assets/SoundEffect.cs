using UnityEngine;
using System.Collections;

public class SoundEffect : MonoBehaviour {

	AudioSource audioSrc;
//	public AudioClip audioClip;
//	public bool bLoop;
	[Range(0f, 1f)] public float volume = 1f;
	[Range(0f, 2f)] public float pitch = 1f;

	// Use this for initialization
	void Awake () {
		audioSrc = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
//		if(bLoop)
//		{
//
//		}
	}

	public void PlayLoop(AudioClip _audioClip)
	{
//		bLoop = true;
//		audioClip = _audioClip;
		//NGUITools.PlaySound(audioClip, volume, pitch);

		audioSrc.loop = true;
		audioSrc.clip = _audioClip;
		audioSrc.Play();
	}


	public void Play(AudioClip _audioClip)
	{
//		bLoop = false;
//		audioClip = _audioClip;
//		NGUITools.PlaySound(audioClip, volume, pitch);
		audioSrc.loop = false;
		audioSrc.clip = _audioClip;
		audioSrc.Play();
	}
}
