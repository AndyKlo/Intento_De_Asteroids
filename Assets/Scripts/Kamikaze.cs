using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : MonoBehaviour
{

    public float speed = 5f;
    public Transform target; // Asigna el objetivo (jugador) desde el editor.

    void Update()
    {
        MoveTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        // Mueve el enemigo hacia el objetivo.
        transform.Translate((target.position - transform.position).normalized * speed * Time.deltaTime);
    }


}
