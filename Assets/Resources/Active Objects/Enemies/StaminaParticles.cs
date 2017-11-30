using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class StaminaParticles : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private Vector3 _target;

    public List<ParticleCollisionEvent> CollisionEvents = new List<ParticleCollisionEvent>();

    public float Force = 1;

    bool go = false;

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
        go = true;
        yield return null;
    }

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        CollisionEvents = new List<ParticleCollisionEvent>();

        var collisions = _particleSystem.collision;
    }

    // Use this for initialization
    void Start()
    {
        var main = _particleSystem.main;
        main.simulationSpace = ParticleSystemSimulationSpace.World;

        _target = Player.Instance.transform.position;

        StartCoroutine(Wait());
    }


    private void Update()
    {
        if (Player.Instance == null)
            Destroy(this.gameObject);

        _target = Player.Instance.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!go)
            return;

        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[_particleSystem.particleCount];
        _particleSystem.GetParticles(particles);

        for (int i = 0; i < particles.Length; i++)
        {
            // Pega a particula atual
            var curParticle = particles[i];

            // Traça um vetor na direção desse objeto
            Vector2 distance = _target - curParticle.position;

            curParticle.velocity = distance.xyz(Vector3.zero).normalized * Force * Time.deltaTime;

            // Retorna a particula modificada ao array
            particles[i] = curParticle;
        }

        // Seta todas as particulas de volta
        _particleSystem.SetParticles(particles, particles.Length);
    }
}
