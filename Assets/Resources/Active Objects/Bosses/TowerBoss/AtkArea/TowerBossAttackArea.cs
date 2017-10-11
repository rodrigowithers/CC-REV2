using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBossAttackArea : MonoBehaviour
{
    private GameObject _explosion;
    private SpriteRenderer _sprite;
    
    public float ExplosionTimer = 1.0f;

    public AnimationCurve Curve;

    private IEnumerator CExplosionTimer()
    {
        var time = 0.0f;

        Color spriteColor = _sprite.color;

        while(time < ExplosionTimer)
        {
            // Faz a parada piscar
            _sprite.color = Color.Lerp(spriteColor, Color.white, Curve.Evaluate(time));

            time += Time.deltaTime;
            yield return null;
        }

        Explode();

        yield return null;
    }

    public void Explode()
    {
        Vector3 scale = transform.localScale;
        // Instancia as partículas, que também da o dano
        GameObject explosion = Instantiate(_explosion, transform.position, Quaternion.identity);
        explosion.transform.localScale = scale;
        explosion.GetComponent<TowerBossExplosion>().Scale = scale.x;
        //explosion.transform.lossyScale = scale;
        // Se destroi
        Destroy(this.gameObject);
    }

    private void Awake()
    {
        // Pega o Sprite
        _sprite = GetComponent<SpriteRenderer>();

        // Carrega a Explosão
        _explosion = Resources.Load<GameObject>("Active Objects/Bosses/TowerBoss/AtkArea/Explosion");

        if(_explosion == null)
        {
            throw new System.Exception("Não conseguiu carregar a explosão do boss");
        }
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(CExplosionTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
