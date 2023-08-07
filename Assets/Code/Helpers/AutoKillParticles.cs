using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class AutoKillParticles : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        // Pega a LifeTime do sistema
        float lifetime = GetComponent<ParticleSystem>().main.startLifetime.constant;
        Destroy(this.gameObject, lifetime);
    }
}
