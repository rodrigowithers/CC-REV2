using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour, IStopDash
{
    public Sprite Broken;
    public ParticleSystem _particles;

    private bool _done = false;

    public void Break()
    {
        // Libera partículas
        _particles.Emit(50);

        // Troca a Sprite
        GetComponent<SpriteRenderer>().sprite = Broken;

        // Toca um som
        SoundManager.Play("pedra lascando");

        Destroy(this);
    }

    private void Awake()
    {

    }
}
