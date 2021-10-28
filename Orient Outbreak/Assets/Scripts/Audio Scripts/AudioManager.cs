using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public AudioMixerGroup masterGroup;
	[Range(0.0001f, 1f)] public float masterSlider; // for audio fadin/out transitions
	
	public AudioMixerGroup bgmGroup;
	public AudioMixerGroup sfxGroup;

	public Sound[] sounds;
	[SerializeField] private bool isInTransition;

    private void Update()
    {
		// During transitions, change in master volume is done by animation. If not in transition, change is done by slider UI
		if(isInTransition)
			masterGroup.audioMixer.SetFloat("masterVol", Mathf.Log10(masterSlider) * 20);
	}

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = s.mixerGroup;
		}

		isInTransition = true;
		
	}

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		
		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

	//// Slider UI sets bgm volume
	//public void SetBGMVolume(float volumeValue)
 //   {
	//	bgmGroup.audioMixer.SetFloat("bgmVol", Mathf.Log10(volumeValue) * 20);
	//}

	//public void SetSFXVolume(float volumeValue)
	//{
	//	sfxGroup.audioMixer.SetFloat("sfxVol", Mathf.Log10(volumeValue) * 20);
	//}

	public AudioSource GetSource(string sound)
    {
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return null;
		}

		return s.source;
	}

	public void StopAudio()
    {
		foreach(Sound s in sounds)
        {
            if (s.source.isPlaying)
            {
				s.source.Stop();
            }
        }
    }

	public void ToggleIsInTransition()
    {
		if (isInTransition)
			isInTransition = false;
		else
			isInTransition = true;
    }
}
