using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    private bool _canMove = true;
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

    public float Speed;
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
    }

    protected void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {

    }

    private void Update()
    {

    }
}
