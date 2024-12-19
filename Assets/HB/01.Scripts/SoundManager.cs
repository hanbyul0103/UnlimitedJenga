using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public Sound[] bgmSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        PlayBGM("BGM");
    }

    public void PlayBGM(string name)
    {
        Sound sound = Array.Find(bgmSounds, x => x.name == name);

        if (sound != null)
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfxSounds, x => x.name == name);

        if (sound != null)
        {
            sfxSource.PlayOneShot(sound.clip);
        }
    }

    public void ToggleBGM()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void StopSFX()
    {
        sfxSource?.Stop();
    }

    public void StopMusci()
    {
        musicSource?.Stop();
    }

    public bool IsBGMOn() => !musicSource.mute;
    public bool IsSFXOn() => !sfxSource.mute;
}
