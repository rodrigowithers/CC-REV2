using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IAttack
{
    public float Speed = 5;
    public Vector2 Destination;

    protected Vector2 _originalPosition;
    protected Vector2 _direction;

    protected float time = 0;

    public BattlePiece _piece;

    // Use this for initialization
    void Start()
    {
        _originalPosition = transform.position;
        _direction = Destination - transform.position.xy();

        transform.localRotation = Quaternion.FromToRotation(Vector3.down, _direction);

        // Toca o som
        SoundManager.Play("tiro");
    }

    // Update is called once per frame
    void Update()
    {
        // Casta um raio para frente
        Debug.DrawRay(transform.position, _direction / 5, Color.cyan);
        var hits = Physics2D.RaycastAll(transform.position, _direction / 5, 1);
        foreach (var hit in hits)
        {
            if (hit.collider.GetComponent<IStopDash>() != null)
            {
                // Tira as particulas do parent
                if (transform.GetChild(0) != null && transform.GetChild(0).parent != null)
                    transform.GetChild(0).parent = null;

                Destroy(this.gameObject);
            }

            var obj = hit.collider.GetComponent<IKillable>();
            if (obj != null && hit.collider.GetComponent<BattlePiece>() != _piece)
            {
                if (hit.collider.GetComponent<Enemy>() && _piece.GetComponent<Enemy>())
                    continue;

                obj.TakeDamage(_direction, 5, 3);

                // Tira as particulas do parent
                if (transform.GetChild(0) != null)
                    transform.GetChild(0).parent = null;

                Destroy(this.gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(_originalPosition, Destination, time);

        time += Time.deltaTime * Speed;
        if (time >= 1.1f)
        {
            // Tira as particulas do parent
            transform.GetChild(0).parent = null;

            Destroy(this.gameObject);
        }
    }
}
