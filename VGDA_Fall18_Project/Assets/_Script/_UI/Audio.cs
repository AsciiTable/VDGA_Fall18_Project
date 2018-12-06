using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{

	public static AudioClip WalkSFX;
	public static AudioClip ThudSFX;
	private static AudioSrouce MusicSource;
	

	// Use this for initialization
	void Start ()
	{
		MusicSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	pubic static void PlaySound(string clip)
	{
		switch (clip)
		{
			default:
				break;
		}
	}
}
