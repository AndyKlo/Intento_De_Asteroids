using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaExtra : MonoBehaviour
{
    public AudioClip vidaExtra;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager game = GameManager.Instance;
        ScoreTracker score = ScoreTracker.instance;
        SoundController.instance.EjecutarSonido(vidaExtra);
        game.vidas++;
        score.lives++;

        Destroy(this.gameObject);
    }
}
