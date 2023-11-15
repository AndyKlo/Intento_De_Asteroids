using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaExtra : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);

        GameManager game = GameManager.Instance;
        ScoreTracker score = ScoreTracker.instance;

        game.vidas++;
        score.lives++;
    }
}
