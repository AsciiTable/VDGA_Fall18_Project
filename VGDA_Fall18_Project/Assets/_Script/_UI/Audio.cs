using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{

	public static AudioClip ThudSFX;
	public static AudioClip BatSwingSFX;
	public static AudioClip JumpSFX;
	public static AudioClip FootstepsSFX;
	public static AudioClip SoundTellingSFX;
    public static AudioClip BloodSplatSFX;
    public static AudioClip ChipEatingSFX;
    public static AudioClip LeavesStepSFX;
    public static AudioClip TireScreechSFX;
    public static AudioClip AmbulanceSFX;
    static AudioSource MusicSource;
	

	// Use this for initialization
	void Start ()
	{
		FootstepsSFX = Resources.Load<AudioClip>("bob_poofs");
		ThudSFX = Resources.Load<AudioClip>("8BIT_RETRO_Hit_Bump_Distorted_Thud_mono");
		BatSwingSFX = Resources.Load<AudioClip>("WHOOSH_Short_03_mono");
		JumpSFX = Resources.Load<AudioClip>("8BIT_RETRO_Jump_Glide_Up_Muffled_mono");
		SoundTellingSFX = Resources.Load<AudioClip>("bgm_cutscene_soundtelling");

        BloodSplatSFX = Resources.Load<AudioClip>("blood_splat");
        ChipEatingSFX = Resources.Load<AudioClip>("chip_eating");
        LeavesStepSFX = Resources.Load<AudioClip>("leaves_rustle_step");
        TireScreechSFX = Resources.Load<AudioClip>("tire_squeal");
        AmbulanceSFX = Resources.Load<AudioClip>("ambulance");
        MusicSource = GetComponent<AudioSource>();
	}
	
	public static void PlaySound(string clip)
	{
		switch (clip)
		{
			case "8BIT_RETRO_Hit_Bump_Distorted_Thud_mono":
				MusicSource.PlayOneShot(ThudSFX);
				break;
			
			case "WHOOSH_Short_03_mono":
				MusicSource.PlayOneShot(BatSwingSFX);
				break;
			
			case "bob_poofs":
				MusicSource.PlayOneShot(FootstepsSFX);
				break;
			
			case "8BIT_RETRO_Jump_Glide_Up_Muffled_mono":
				MusicSource.PlayOneShot(JumpSFX);
				break;
			
			case "bgm_cutscene_soundtelling":
				MusicSource.PlayOneShot(SoundTellingSFX);
				break;

            case "blood_splat":
                MusicSource.PlayOneShot(BloodSplatSFX);
                break;
            case "chip_eating":
                MusicSource.PlayOneShot(ChipEatingSFX);
                break;
            case "leaves_rustle_step":
                MusicSource.PlayOneShot(LeavesStepSFX);
                break;
            case "tire_squeal":
                MusicSource.PlayOneShot(TireScreechSFX);
                break;
            case "ambulance":
                MusicSource.PlayOneShot(AmbulanceSFX);
                break;

            default:
				break;
		}
	}
}
