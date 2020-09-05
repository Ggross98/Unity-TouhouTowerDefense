using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource MusicPlayer;
    public AudioSource audioPrefab;
    [HideInInspector]
    public AudioSource[] SoundPlayer;

    public float musicVolume = 0.5f;
    public float soundVolume = 0.5f;

    public static AudioClip se_hit, se_invalid;

    void Awake()
    {
        if(instance!= null && instance != this)
        {
            Destroy(this.gameObject);

        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
            CreateSoundPlayer(100);
            SetVolume();
        }
    }

    void Start()
    {
        
        se_hit = Resources.Load<AudioClip>("se_hit");
        se_invalid = Resources.Load<AudioClip>("se_invalid");

    }

    public void PlayMusic(string name)
    {
        if(!MusicPlayer.isPlaying)
        {
            AudioClip clip = Resources.Load<AudioClip>("SoundEffects/"+name);
            //Debug.Log("bgm :" + clip);
            MusicPlayer.clip = clip;
            MusicPlayer.Play();
        }
    }

    public void PlaySound(string name)
    {
        AudioClip c = Resources.Load<AudioClip>("SoundEffects/" + name);
        for(int i =0;i<SoundPlayer.Length; i++)
        {
            if(SoundPlayer [i]!= null && !SoundPlayer [i].isPlaying)
            {
                SoundPlayer[i].clip = c;
                SoundPlayer[i].Play();
                return;
            }
        }
    }

    public void PlaySound(AudioClip aclip)
    {
        for (int i = 0; i < SoundPlayer.Length; i++)
        {
            if (SoundPlayer[i] != null && !SoundPlayer[i].isPlaying)
            {
                SoundPlayer[i].clip = aclip;
                SoundPlayer[i].Play();
                return;
            }
        }
    }

    public void SetVolume()
    {
        MusicPlayer.volume = musicVolume;
        if(SoundPlayer != null)
        {
            foreach(AudioSource a in SoundPlayer)
            {
                if (a != null) a.volume = soundVolume;
            }
        }
    }

    public void StopMusic()
    {
        MusicPlayer.Stop();
    }

    public void CreateSoundPlayer(int count)
    {
        SoundPlayer = new AudioSource[count];

        for(int i = 0; i < count; i++)
        {
            AudioSource source = Instantiate(audioPrefab, transform);
            SoundPlayer[i] = source;
        }

    }
}
