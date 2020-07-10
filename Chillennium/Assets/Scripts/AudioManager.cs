using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{

    private AudioSource m_audioSource;
    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            //Destroy self if we already have audio manager
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        m_audioSource = GetComponent<AudioSource>();
    }
    
    public void playTrack(AudioClip track)
    {
        if (m_audioSource.clip.name != track.name)
        {
            m_audioSource.clip = track;
            m_audioSource.Play();
        }
        
    }
}
