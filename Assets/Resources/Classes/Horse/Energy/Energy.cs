using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public Vector2 Direction;

    public float StepTime = 2f; // O tempo que o ataque fica em cada passo do path

    protected int _index = 0;
    protected Vector2[] _path; // O conjunto de offsets que esse ataque ira seguir

    protected float time = 0.0f;

    public BattlePiece _piece;

    // Use this for initialization
    void Start()
    {
        _path = new Vector2[4];

        // Cria o path baseado na direção
        if (Direction.x > 0 && Direction.y > 0) // Superior Direito
        {
            _path[0] = new Vector2(0.0f, 1.3f);
            _path[1] = new Vector2(0.0f, 1.3f);
            _path[2] = new Vector2(0.0f, 1.3f);
            _path[3] = new Vector2(1.3f, 0.0f);
        }
        if (Direction.x < 0 && Direction.y > 0) // Superior Esquerdo
        {
            _path[0] = new Vector2(0.0f, 1.3f);
            _path[1] = new Vector2(0.0f, 1.3f);
            _path[2] = new Vector2(0.0f, 1.3f);
            _path[3] = new Vector2(-1.3f, 0.0f);
        }
        if (Direction.x > 0 && Direction.y < 0) // Inferior Direito
        {
            _path[0] = new Vector2(0.0f, -1.3f);
            _path[1] = new Vector2(0.0f, -1.3f);
            _path[2] = new Vector2(0.0f, -1.3f);
            _path[3] = new Vector2(1.3f, 0.0f);
        }
        if (Direction.x < 0 && Direction.y < 0) // Inferior Esquerdo
        {
            _path[0] = new Vector2(0.0f, -1.3f);
            _path[1] = new Vector2(0.0f, -1.3f);
            _path[2] = new Vector2(0.0f, -1.3f);
            _path[3] = new Vector2(-1.3f, 0.0f);
        }
    }


    private void Walk()
    {
        _index++;

        if (_index >= _path.Length)
        {
            Destroy(this.gameObject);
        }
        else
        {
            time = 0;
            transform.position += _path[_index].xyz(transform.position);
        }
    }


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        // Checa colisões
        DebugExtension.DebugCircle(transform.position, Vector3.forward, Color.red, 0.5f);

        var hits = Physics2D.CircleCastAll(transform.position, 0.5f, Vector2.zero);
        foreach (var hit in hits)
        {
            if (hit.collider.GetComponent<IStopDash>() != null)
            {
                Destroy(this.gameObject);
            }

            var obj = hit.collider.GetComponent<IKillable>();
            if (obj != null && hit.collider.GetComponent<BattlePiece>() != _piece)
            {
                var dir = hit.transform.position - transform.position;
                obj.TakeDamage(dir);

                Destroy(this.gameObject);
            }
        }


        if (time > StepTime) // Anda para o próximo lugar
        {
            Walk();
        }
    }
}
