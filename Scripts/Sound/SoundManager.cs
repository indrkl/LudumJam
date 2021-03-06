﻿using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

	public AudioSource efxSource;
	public AudioSource musicSource;
	public AudioSource tempSource;
	public AudioSource tempSfxSource;

	public static SoundManager instance = null;
	public float fadeSpeed = 1.0f;
	public AudioClip newTheme = null;
	public AudioClip newSfx;

	public float lowPitchRange = 0.95f;
	public float highPitchRange = 1.05f;

	public float epsilon = 0.05f;



	// Use this for initialization
	void Awake () 
	{

		if (instance == null) {
			instance = this;
		} else if (instance == this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);

		tempSource.volume = 0f;
	
	}

	void Update()
	{
		FadeTheme (newTheme, Time.deltaTime);
	}

	public void PlaySingle(AudioClip clip) 
	{
		efxSource.clip = clip;
		efxSource.Play ();
	}

	public void RandomizeSfx(params AudioClip[] clips)
	{
		int randomIndex = Random.Range (0, clips.Length);
		float randomPitch = Random.Range (lowPitchRange, highPitchRange);

		efxSource.pitch = randomPitch;
		efxSource.clip = clips [randomIndex];
		efxSource.Play ();

	}

	public void FadeTheme (AudioClip newTheme, float dt)
	{
		// This gets called in update function.
		// If new clip is requested
		if (tempSource.clip != newTheme) {
			// if there is a clip playing already
			if (tempSource.clip != null) 
			{
				musicSource.clip = tempSource.clip;
				musicSource.Play ();
				musicSource.time = tempSource.time;
				musicSource.volume = tempSource.volume;
				tempSource.volume = 0f;
			}
			// Set the new theme, and start fading in
			tempSource.clip = newTheme;
			tempSource.Play ();

		}

		// if theme has been set already
		if (tempSource.clip != null) {
			tempSource.volume = Mathf.Lerp(tempSource.volume, 1.0f, fadeSpeed * dt);
			musicSource.volume = Mathf.Lerp (musicSource.volume, 0f, fadeSpeed * dt);
		} 
	}

	public void FadeSfx(AudioClip newSfx, float dt) 
	{
		float maxSound = 0.5f;
		// This gets called in update function.
		// If new clip is requested
		if (tempSfxSource.clip != newSfx) {
			// if there is a clip playing already
			if (tempSfxSource.clip != null) 
			{
				efxSource.clip = tempSfxSource.clip;
				efxSource.Play ();
				efxSource.time = tempSfxSource.time;
				efxSource.volume = tempSfxSource.volume;
				tempSfxSource.volume = 0f;
			}
			// Set the new theme, and start fading in
			tempSfxSource.clip = newSfx;
			tempSfxSource.Play ();

		}

		// if theme has been set already
		if (tempSfxSource.clip != null) {
			tempSfxSource.volume = Mathf.Lerp(tempSfxSource.volume, maxSound, fadeSpeed * dt);
			efxSource.volume = Mathf.Lerp (efxSource.volume, 0f, fadeSpeed * dt);
		}
	}
}
