using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private Dictionary<string, Sound> soundsMap = new Dictionary<string, Sound>();

    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;

            soundsMap.Add(s.name, s);
        }
    }

    private void Start()
    {
        Play("Background");
    }

    public void Play (string name)
    {
        if (soundsMap.ContainsKey(name))
        {
            soundsMap[name].source.Play();
        }
    }
}
