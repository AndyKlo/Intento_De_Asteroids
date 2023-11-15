using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Player player;
    public ParticleSystem explosion;
    public float tiempoInvulnerable = 3f;
    public int vidas = 3;

    public VidaExtra vidaPrefab;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AsteroidDeath(Asteroid asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();
    }
    public void PlayerDeath()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        
        vidas--;
        if (vidas >= 0)
        {
            Invoke(nameof(Respawn), 3f); //Respawneo en 3segundos
        }
    }
    public void Respawn()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.layer = LayerMask.NameToLayer("Invulnerable");
        player.gameObject.SetActive(true);
        StartCoroutine(BlinkPlayer());
        Invoke(nameof(TurnLayer), tiempoInvulnerable);
    }
    public void NewGame()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.layer = LayerMask.NameToLayer("Invulnerable");
        player.gameObject.SetActive(true);
        Invoke(nameof(TurnLayer), tiempoInvulnerable);
        ClearAsteroids();
        ClearVidaExtras();
    }
    private void TurnLayer()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }
    public IEnumerator BlinkPlayer()
    {
        SpriteRenderer playerRenderer = player.GetComponent<SpriteRenderer>();
        Color originalColor = playerRenderer.color;
        float blinkDuration = tiempoInvulnerable;
        float blinkSpeed = 10f; 
        float elapsedTime = 0f;

        while (elapsedTime < blinkDuration)
        {
            playerRenderer.color = Color.Lerp(originalColor, Color.clear, Mathf.PingPong(elapsedTime * blinkSpeed, 1));
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        playerRenderer.color = Color.white;
    }
    public void ClearAsteroids()
    {
        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();
        
        foreach (Asteroid asteroid in asteroids)
        {
            Destroy(asteroid.gameObject);
        }
    }
    public VidaExtra VidaEx(Vector2 position)
    {
        VidaExtra vida = Instantiate(vidaPrefab, position, this.transform.rotation);
        return vida;
    }
    public void ClearVidaExtras()
    {
        VidaExtra[] vidas = FindObjectsOfType<VidaExtra>();

        foreach (VidaExtra vid in vidas)
        {
            Destroy(vid.gameObject);
        }
    }
}
