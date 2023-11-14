using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class ScoreTracker : MonoBehaviour
{
    public static ScoreTracker instance;
    public int score = 0;
    public int lives = 3;
    private bool nivel2 = false;
    private bool nivel3 = false;

    
    public TMPro.TextMeshProUGUI uiPuntaje;
    public TMPro.TextMeshProUGUI uiLives;
    public TMPro.TextMeshProUGUI uiLevels;

    public Button boton;
    public Image gameOverImage;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        StartGame();
    }
    public void Update()
    {
        uiPuntaje.text = score.ToString();
        uiLives .text = ("x" + lives.ToString());
    }
    public void AddScore(int amount)
    {
        score += amount;
        if (score >= 2000 && !nivel2)
        {
            nivel2 = true;
            Invoke(nameof(NivelDos), 1);
        }
        else if( score > 7000 && !nivel3)
        {
            nivel3 = true;
            NivelTres();
        }
    }
    public void Lives(int amount)
    {
        lives -= amount;
        if (lives <= 0){GameOver();}
    }
    public void NivelDos()
    {
        GameManager.Instance.ClearAsteroids();
        uiLevels.text = ("NIVEL 2");
        uiLevels.gameObject.SetActive(true);
        Invoke(nameof(DesactivarNivel), 3f);
    }
    public void NivelTres()
    {
        GameManager.Instance.ClearAsteroids();
        uiLevels.text = ("NIVEL 3");
        uiLevels.gameObject.SetActive(true);
        Invoke(nameof(DesactivarNivel), 3f);
    }
    public void StartGame()
    {
        uiLevels.text = ("NIVEL 1");
        uiLevels.gameObject.SetActive(true);
        Invoke(nameof(DesactivarNivel), 3f);
        GameManager.Instance.StartCoroutine(GameManager.Instance.BlinkPlayer());
        GameManager.Instance.ClearAsteroids();
        gameOverImage.gameObject.SetActive(false);
        boton.gameObject.SetActive(false);
        score = 0;
        lives = 3;
    }
    public void DesactivarNivel() { uiLevels.gameObject.SetActive(false); }
    public void GameOver() 
    {
        gameOverImage.gameObject.SetActive(true);
        boton.gameObject.SetActive(true);
    }
    public void PlayAgain()
    {
        GameManager game = GameManager.Instance;
        if (game != null)
        {
            game.vidas = 3;
        }
        GameManager.Instance.Respawn();
        StartGame();
    }
}
