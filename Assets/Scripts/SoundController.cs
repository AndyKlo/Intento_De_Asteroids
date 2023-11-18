using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController instance;
    private AudioSource m_audio;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        m_audio = GetComponent<AudioSource>();
    }
    public void EjecutarSonido(AudioClip sonido)
    {
        m_audio.PlayOneShot(sonido);
    }
}
