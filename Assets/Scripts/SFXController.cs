using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public static SFXController instance;

    [SerializeField] private AudioSource sfxObject;
    [SerializeField] private float volume;

    private List<AudioSource> sounds;
    
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            sounds = new List<AudioSource>();
        }
    }

    void Update()
    {
        sounds.RemoveAll(s => s == null); 
    }

    public void PlaySFX(AudioClip audioClip, Transform pos, float vol)
    {
        AudioSource audioSource = Instantiate(sfxObject, pos.position, Quaternion.identity);
        sounds.Add(audioSource);
        audioSource.clip = audioClip;
        audioSource.volume = vol;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    public void StopAll()
    {
        foreach (var sound in sounds)
        {
            sound.Stop();
        }
    }
}
