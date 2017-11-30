using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Piece : MonoBehaviour
{

    #region Variables
        #region PRIVATE
            protected Rigidbody2D _rigidBody;
            protected SpriteRenderer _renderer;

            private bool _canMove = true;
    #endregion
        #region PUBLIC
            public Vector2 MovementDirection;
            public Vector2 Direction;
            public float Speed;
            public bool IsInvincible = false;
        #endregion
    #endregion

    public Rigidbody2D RigidBody
    {
        get
        {
            return _rigidBody;
        }
    }

    public bool CanMove
    {
        get
        {
            return _canMove;
        }

        set
        {
            _canMove = value;
        }
    }

    protected void UpdateAnimator()
    {
        var anim = GetComponent<ClassAnimator>();
        if (anim == null)
            return; 

        // Usa o Movement Direction para dizer para onde a peça está se movendo
        if (MovementDirection.y > 0)
        {
            if (MovementDirection.x > 0.6f)
                anim.Play("WalkRight");
            else if (MovementDirection.x < -0.6f)
                anim.Play("WalkLeft");
            else
                anim.Play("WalkUp");
        }

        else if (MovementDirection.y < 0)
        {
            if (MovementDirection.x > 0.5f)
                anim.Play("WalkRight");
            else if (MovementDirection.x < -0.5f)
                anim.Play("WalkLeft");
            else
                anim.Play("WalkDown");
        }

        else if (MovementDirection.x > 0)
            anim.Play("WalkRight");

        else if (MovementDirection.x < 0)
            anim.Play("WalkLeft");

        else
            anim.Play("Idle");
    }

    public void Move(Vector2 input)
    {
        if (!CanMove)
            return;

        if (_rigidBody == null)
            _rigidBody = GetComponent<Rigidbody2D>();



        if (input.magnitude < 0.5f)
        {
            _rigidBody.velocity = Vector2.zero;
        }
        else
        {
            _rigidBody.velocity = input.normalized * Speed * Time.deltaTime;
            Debug.Log(_rigidBody.velocity);
        }

        MovementDirection = input.normalized;

        UpdateAnimator();
    }
    public void Move()
    {
        if (!CanMove)
            return;

        if (_rigidBody == null)
            _rigidBody = GetComponent<Rigidbody2D>();

        if (Direction.magnitude < 0.5f)
        {
            _rigidBody.velocity = Vector2.zero;
        }
        else
        {
            _rigidBody.velocity = Direction.normalized * Speed * Time.deltaTime;
        }

        MovementDirection = Direction.normalized;
        UpdateAnimator();
    }

    public void StopMoving()
    {
        CanMove = false;
        _rigidBody.velocity = Vector2.zero;
    }
    public void ResumeMove()
    {
        CanMove = true;
    }

    protected void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }


    // Alterna entre a cor da sprite e branco
    public IEnumerator CDamageFlash()
    {
        var renderer = GetComponent<SpriteRenderer>();
        var mat = renderer.material;

        float time = 0.0f;
        while (time < 1.0f)
        {
            mat.SetFloat("_Flash", time);

            time += Time.deltaTime * 6.0f;
            yield return null;
        };

        while (time > 0.0f)
        {
            mat.SetFloat("_Flash", time);

            time -= Time.deltaTime * 6.0f;
            yield return null;
        };

        mat.SetFloat("_Flash", 0);
        yield return null;
    }
    
    private void Start()
    {
        NewDirection();
    }
    public void NewDirection()
    {
        Direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
    }

    private void Update()
    {

    }



    public void Push(Vector2 dir , float mag = 10)
    {
        _rigidBody.AddForce(dir*mag);
    }

    public void SetColor(Color c)
    {
        if (_renderer == null)
            return;

        _renderer.color = c;
    }
    public Color GetColor()
    {
        if (_renderer == null)
            _renderer = GetComponent<SpriteRenderer>();

        return _renderer.color;
    }
    public Vector2 RandomDirection()
    {
        Vector2 toreturn = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        return toreturn;
    }
    //Retorna o angulo entre o inimigo e o player
    public float ThisAngleFromPlayer()
    {
        Vector2 normal = transform.position - Player.Instance.transform.position;
        return Vector2.Dot(new Vector2(1, 0), normal.normalized);
    }
}
