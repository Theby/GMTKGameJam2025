using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource drums;
    public AudioSource harmonics;
    public AudioSource organ;
    public AudioSource whistle;
    public AudioSource sfxAudioSource;
    public AudioClip doorOpenSfx;
    public AudioClip inGoalSfx;
    public AudioClip levelCompleteSfx;
    public AudioClip pushSfx;
    public List<AudioClip> walkSfxs;
    public AudioClip loadNextStageSfx;
    public AudioClip moveUISfx;
    public AudioClip pressUISfx;
    public AudioClip backUISfx;

    int _walkIndex = 0;

    public static AudioManager instance;

    public void Awake()
    {
        instance = this;
    }

    public void Initialize()
    {
        drums.Play();
        harmonics.Play();
        organ.Play();
        whistle.Play();
    }

    public void PlayUIMusic()
    {
        harmonics.volume = 0.0f;
        whistle.volume = 0.0f;
    }

    public void PlayGameMusic()
    {
        harmonics.volume = 1.0f;
        whistle.volume = 1.0f;
    }

    public void PlayDoorOpenSfx()
    {
        sfxAudioSource.pitch = 1.0f;
        sfxAudioSource.PlayOneShot(doorOpenSfx);
    }

    public void PlayInGoalSfx()
    {
        sfxAudioSource.pitch = 1.0f;
        sfxAudioSource.PlayOneShot(inGoalSfx);
    }

    public void PlayLevelCompleteSfx()
    {
        sfxAudioSource.pitch = 1.0f;
        sfxAudioSource.PlayOneShot(levelCompleteSfx);
    }

    public void PlayPushSfx()
    {
        sfxAudioSource.clip = pushSfx;
        sfxAudioSource.pitch = Random.Range(0.9f, 1.1f);
        sfxAudioSource.Play();
    }

    public void PlayWalkSfx()
    {
        _walkIndex++;
        _walkIndex %= walkSfxs.Count;

        sfxAudioSource.clip = walkSfxs[_walkIndex];
        sfxAudioSource.pitch = Random.Range(0.9f, 1.1f);
        sfxAudioSource.Play();
    }

    public void LoadNewStageSfx()
    {
        sfxAudioSource.pitch = 1.0f;
        sfxAudioSource.PlayOneShot(loadNextStageSfx);
    }

    public void MoveUISfx()
    {
        sfxAudioSource.pitch = 1.0f;
        sfxAudioSource.PlayOneShot(moveUISfx);
    }

    public void PressUISfx()
    {
        sfxAudioSource.pitch = 1.0f;
        sfxAudioSource.PlayOneShot(pressUISfx);
    }

    public void BackUISfx()
    {
        sfxAudioSource.pitch = 1.0f;
        sfxAudioSource.PlayOneShot(backUISfx);
    }
}
