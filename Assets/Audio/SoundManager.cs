using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public List<AudioClip> Clips;

    public AudioMixer Mixer;

    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager>();

                if (_instance == null)
                {
                    GameObject go = new GameObject("SoundManager");
                    go.AddComponent<SoundManager>();
                    DontDestroyOnLoad(go);

                    _instance = go.GetComponent<SoundManager>();
                }
                else
                {
                    _instance = FindObjectOfType<SoundManager>();
                }
            }
            return _instance;
        }
    }

    //public void Play(string name, bool loop = false)
    //{
    //    var sources = GetComponents<AudioSource>();

    //    foreach (var source in sources)
    //    {
    //        if (source.clip.name == name)
    //        {
    //            source.Play();
    //            source.loop = loop;
    //        }
    //    }
    //}

    public static void Play(string name, float volume = 1.0f, bool loop = false)
    {
        var sources = Instance.GetComponents<AudioSource>();

        if (sources == null)
        {
            throw new System.Exception("Não encontrou componentes do tipo AudioSource");
        }

        foreach (var source in sources)
        {
            if (source.clip.name == name)
            {
                source.volume = volume;
                source.Play();
                source.loop = loop;
            }
        }
    }

    public void PlayOneShot(string name)
    {
        var sources = GetComponents<AudioSource>();

        foreach (var source in sources)
        {
            if (source.clip.name == name)
            {
                source.Play();
                source.loop = false;
            }
        }
    }

    public void Loop(string name)
    {
        var sources = GetComponents<AudioSource>();

        foreach (var source in sources)
        {
            if (source.clip.name == name)
            {
                source.Play();
                source.loop = true;
            }
        }
    }

    public void Stop(string name)
    {
        var sources = GetComponents<AudioSource>();

        foreach (var source in sources)
        {
            if (source.clip.name == name)
            {
                source.Stop();
                source.loop = false;
            }
        }
    }
    
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < Clips.Count; i++)
        {
            gameObject.AddComponent<AudioSource>();
            var curAudioSource = GetComponents<AudioSource>()[i];

            curAudioSource.clip = Clips[i];
            curAudioSource.playOnAwake = false;
        }

        Mixer = Resources.Load<AudioMixer>("AudioMixer");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
