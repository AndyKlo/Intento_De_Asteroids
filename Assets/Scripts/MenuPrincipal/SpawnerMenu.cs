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
        InvokeRepeating(nameof(Spawn), 1f, 1.2f); //invoco repetidamente el método Spawn()
    }// invoco el método spawn durante cierto tiempo, y repetido cada ciertos segundos

    private void Spawn()
    {
        for (int i = 0; i < 1; i++)
        {
            //Genera una dirección de spawn aleatoria dentro de un círculo unitario y la normaliza
            //luego multiplica por 10 para obtener una dirección dentro de un círculo de radio 10 unidades.
            Vector3 spawnDirection = UnityEngine.Random.insideUnitCircle.normalized;

            //Calcula el punto de spawn sumando la posición actual del
            //objeto (transform.position) y la dirección de spawn.
            //básicamente lo mueve a la dirección aleatoria de 'spawnDirection'
            Vector3 spawnPoint = transform.position + (spawnDirection * 12f);

            //Genera una variación aleatoria en el ángulo de rotación dentro del rango
            float variance = UnityEngine.Random.Range(-15f, 15f);

            //Crea una rotación en el plano Z usando el ángulo de variación calculado.
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            //Instancia un nuevo asteroides en la posicion y rotación calculadas
            Asteroid asteroid = Instantiate(asteroidPrefab, spawnPoint, rotation);

            //asigna un tamaño aleatorio entre 0.5 y 2 unidades
            asteroid.size = UnityEngine.Random.Range(0.5f, 4f);

            //if (asteroid.isIndestructible) {gameObject.layer = LayerMask.NameToLayer("Asteroide");}
            //determina la trayectoria y le da dirección de movimiento del asteroide
            //crea un vector de trayectoria, asignado por la rotación y la dirección inversa al vector de spawn
            Vector3 trajectory = rotation * -spawnDirection;
            //envía el prefab para darle trayectoria, se aplica fuerza al rb multiplicado por la velocidad
            asteroid.AsteroidsMenu(trajectory);
        }
    }
}
