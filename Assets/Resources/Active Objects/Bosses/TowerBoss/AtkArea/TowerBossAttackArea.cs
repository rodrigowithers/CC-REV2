using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBossAttackArea : MonoBehaviour
{
    public GameObject CannonBall;
    GameObject _own_cannon_ball;
    private GameObject _explosion;
    private SpriteRenderer _sprite;
    public bool special = false;
    
    public float ExplosionTimer = 1.0f;

    public AnimationCurve Curve;

    private TowerBoss _boss;

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
        // Toca o som
        SoundManager.Play("boom");

        // Shake
        Camera.main.GetComponent<CameraController>().Shake(0.2f * transform.localScale.x);

        Vector3 scale = transform.localScale;
        // Instancia as partículas, que também da o dano
        GameObject explosion = Instantiate(_explosion, transform.position, Quaternion.identity);
        explosion.transform.localScale = scale;
        explosion.GetComponent<TowerBossExplosion>().Scale = scale.x;
        if (scale.x > 2)
            explosion.GetComponent<TowerBossExplosion>().damage = 5;
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
        // Toca o som
        SoundManager.Play("cannon");

        CannonBall = Resources.Load<GameObject>("Active Objects/Bosses/TowerBoss/AtkArea/FakeCannonBall");
        CannonBall.transform.localScale = transform.localScale * 2;
        _boss = FindObjectOfType<TowerBoss>();
        StartCoroutine(CExplosionTimer());

        _own_cannon_ball = Instantiate(CannonBall, _boss.transform.position, Quaternion.identity);
        _own_cannon_ball.GetComponent<FakeCannonBall>().Destination = transform.position;
        _own_cannon_ball.GetComponent<FakeCannonBall>().Pivot = gameObject;
        if (special)
        {
            _own_cannon_ball.GetComponent<FakeCannonBall>().time = ExplosionTimer / 2;
            _own_cannon_ball.GetComponent<FakeCannonBall>().special = true;
        }
        else
        {
            _own_cannon_ball.GetComponent<FakeCannonBall>().time = ExplosionTimer * 1.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_own_cannon_ball == null)
        {
            StopCoroutine(CExplosionTimer());
            Destroy(this.gameObject);
        }

    }
}
