using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(Text))]
public class TextRigidBody : MonoBehaviour
{
    private RectTransform _transform;

    private Vector2 _momentum;
    private Vector2 _gravity = new Vector2(0, -10);

    public float GravityScale = 1;

    public Vector2 Gravity
    {
        get
        {
            return _gravity;
        }

        set
        {
            _gravity = value;
        }
    }
    public Vector2 Momentum
    {
        get
        {
            return _momentum;
        }
    }

    /// <summary>
    /// Adiciona uma força instantanea no objeto
    /// </summary>
    /// <param name="direction">Direção da força</param>
    /// <param name="force">Força a ser aplicada na direção</param>
    public void AddForce(Vector2 direction, float force)
    {
        _momentum += direction * force;
    }

    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        // Atualiza o momentum
        _momentum += _gravity * GravityScale * Time.deltaTime;

        // Aplica o momentum
        _transform.anchoredPosition += _momentum;
    }
}