using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class SpawnerMenu : MonoBehaviour
{
    public Asteroid asteroidPrefab;
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 1f, 1.2f); //invoco repetidamente el m�todo Spawn()
    }// invoco el m�todo spawn durante cierto tiempo, y repetido cada ciertos segundos

    private void Spawn()
    {
        for (int i = 0; i < 1; i++)
        {
            //Genera una direcci�n de spawn aleatoria dentro de un c�rculo unitario y la normaliza
            //luego multiplica por 10 para obtener una direcci�n dentro de un c�rculo de radio 10 unidades.
            Vector3 spawnDirection = UnityEngine.Random.insideUnitCircle.normalized;

            //Calcula el punto de spawn sumando la posici�n actual del
            //objeto (transform.position) y la direcci�n de spawn.
            //b�sicamente lo mueve a la direcci�n aleatoria de 'spawnDirection'
            Vector3 spawnPoint = transform.position + (spawnDirection * 12f);

            //Genera una variaci�n aleatoria en el �ngulo de rotaci�n dentro del rango
            float variance = UnityEngine.Random.Range(-15f, 15f);

            //Crea una rotaci�n en el plano Z usando el �ngulo de variaci�n calculado.
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            //Instancia un nuevo asteroides en la posicion y rotaci�n calculadas
            Asteroid asteroid = Instantiate(asteroidPrefab, spawnPoint, rotation);

            //asigna un tama�o aleatorio entre 0.5 y 2 unidades
            asteroid.size = UnityEngine.Random.Range(0.5f, 4f);

            //if (asteroid.isIndestructible) {gameObject.layer = LayerMask.NameToLayer("Asteroide");}
            //determina la trayectoria y le da direcci�n de movimiento del asteroide
            //crea un vector de trayectoria, asignado por la rotaci�n y la direcci�n inversa al vector de spawn
            Vector3 trajectory = rotation * -spawnDirection;
            //env�a el prefab para darle trayectoria, se aplica fuerza al rb multiplicado por la velocidad
            asteroid.AsteroidsMenu(trajectory);
        }
    }
}
