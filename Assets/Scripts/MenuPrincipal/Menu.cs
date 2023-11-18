using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{/*
    public AudioClip menuMusic;

    public void Start()
    {
        SoundController.instance.EjecutarSonido(menuMusic);
    }
    */
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
    public void Salir()
    {
        Application.Quit();
    }
}
