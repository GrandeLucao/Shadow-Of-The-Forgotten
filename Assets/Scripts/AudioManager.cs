using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    private int lvln;


    void Awake()
    {
        lvln= SceneManager.GetActiveScene().buildIndex;
        if(instance==null)
        {
            instance=this;
        }else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source=gameObject.AddComponent<AudioSource>();
            s.source.clip=s.clip;

            s.source.volume=s.volume;
            s.source.loop=s.loop;
            s.source.panStereo=s.panStereo;
            s.source.spatialBlend=s.spatialBlend;
        }
    }

    void Start()
    {
        if(lvln==1)
            Play("BGM");
    }

    public void Play (string name)
    {
        Sound s=Array.Find(sounds, sound => sound.name==name);
        if(s==null)
        {
            return;
        }
        s.source.Play();
    }

    public void Stop (string name)
    {
        Sound s=Array.Find(sounds, sound => sound.name==name);
        if(s==null)
        {
            return;
        }
        s.source.Stop();
    }
}
