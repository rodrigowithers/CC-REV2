using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sicle : MonoBehaviour
{
    public float Speed = 5;
    public Vector2 Destination;
    public Vector2 Direction;

    public Transform Parent;

    protected float time = 0;
    protected bool _damage = false;
    protected bool _returning = false;

    protected Vector2 _originalPosition;

    protected LineRenderer _line;

    public BattlePiece _piece;

    private void Awake()
    {
        _line = GetComponent<LineRenderer>();
    }

    // Use this for initialization
    void Start()
    {
        Direction = (Destination - transform.position.xy()).normalized;

        _originalPosition = transform.position;
        transform.localRotation = Quaternion.Euler(0, 0, 45);
    }

    // Update is called once per frame
    void Update()
    {
        // Casta um circulo ao redor

        if (_returning)
        {
            DebugExtension.DebugCircle(transform.position + Direction.xyz(transform.position), Vector3.forward, Color.red, 0.5f);

            var hits = Physics2D.CircleCastAll(transform.position + Direction.xyz(transform.position), 0.5f, Vector2.zero);
            foreach (var hit in hits)
            {   
                if (hit.collider.GetComponent<IStopDash>() != null)
                {
                    Destroy(this.gameObject);
                }

                var obj = hit.collider.GetComponent<IKillable>();
                if (obj != null && _damage && hit.collider.GetComponent<BattlePiece>() != _piece)
                {
                    var dir = (Parent.position - transform.position).xy().normalized;

                    obj.TakeDamage(dir);
                    _damage = false;
                }
            }
        }


        // Atualiza o Line Renderer
        _line.SetPosition(0, Parent.position);
        _line.SetPosition(1, transform.position);
    }

    private void FixedUpdate()
    {
        if (_damage)
        {
            Destination = Parent.position;
            time += Time.deltaTime * Speed * 2;
        }
        else
        {
            time += Time.deltaTime * Speed;
        }

        transform.position = Vector3.Lerp(_originalPosition, Destination, time);

        if (time >= 1.0f && !_returning)   // INDO
        {
            _returning = true;
            _damage = true;
            time = 0;

            _originalPosition = transform.position;
            Destination = Parent.position;
        }
        else if (time >= 1.0f && _returning) // VOLTANDO
            Destroy(this.gameObject);
    }
}
