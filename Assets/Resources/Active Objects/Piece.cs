using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{

    #region Variables
        #region PRIVATE
            private Rigidbody2D _rigidBody;
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

    public void Move(Vector2 input)
    {
        if (!CanMove)
            return;

        if (input.magnitude < 0.5f)
        {
            _rigidBody.velocity = Vector2.zero;
        }
        else
        {
            _rigidBody.velocity = input.normalized * Speed * Time.deltaTime;
        }

        MovementDirection = input.normalized;
    }
    public void Move()
    {
        if (!CanMove)
            return;

        if (Direction.magnitude < 0.5f)
        {
            _rigidBody.velocity = Vector2.zero;
        }
        else
        {
            _rigidBody.velocity = Direction.normalized * Speed * Time.deltaTime;
        }

        MovementDirection = Direction.normalized;
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

    private void Start()
    {
        NewDirection();
    }
    public void NewDirection()
    {
        Direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        //Debug.Log(Direction);
    }

    private void Update()
    {

    }


    public void SetColor(Color c)
    {
        _renderer.color = c;
    }
    public Color GetColor()
    {
        return _renderer.color;
    }

}
