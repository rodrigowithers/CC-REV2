using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public GameObject ExplosionParticles;

    public float Speed = 5;
    public Vector2 Destination;

    protected Vector2 _originalPosition;
    protected Vector2 _direction;

    protected float time = 0;

    protected float _radius = 2;

    public BattlePiece _piece;

    private void Explode()
    {
        DebugExtension.DebugCircle(transform.position, Vector3.forward, Color.red, _radius, 5);

        // Instancia o Sistema da Partículas
        Instantiate(ExplosionParticles, transform.position, Quaternion.identity);

        // Checa se houve colisão em um raio ao redor da explosão
        var hits = Physics2D.CircleCastAll(transform.position, _radius, Vector2.zero);
        foreach(var hit in hits)
        {
            var obj = hit.collider.GetComponent<IKillable>();
            if (obj != null)
            {
                var dir = (hit.collider.transform.position - transform.position).normalized;
                obj.TakeDamage(dir);
            }
        }

        // ScreenShake
        Camera.main.GetComponent<CameraController>().Shake();

        // Destroi
        Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        _originalPosition = transform.position;
        _direction = Destination - transform.position.xy();

        transform.localRotation = Quaternion.FromToRotation(Vector3.down, _direction);
    }

    // Update is called once per frame
    void Update()
    {
        // Casta um raio para frente
        Debug.DrawRay(transform.position, _direction / 5, Color.cyan);
        var hits = Physics2D.RaycastAll(transform.position, _direction / 5, 1);
        foreach (var hit in hits)
        {
            // Se bateu em qualquer coisa, explode

            if (hit.collider.GetComponent<IStopDash>() != null || hit.collider.GetComponent<IKillable>() != null && hit.collider.GetComponent<BattlePiece>() != _piece)
            {
                Explode();
            }
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(_originalPosition, Destination, time);

        time += Time.deltaTime * Speed;
        if (time >= 1.0f)
        {
            Explode();
        }
    }
}
