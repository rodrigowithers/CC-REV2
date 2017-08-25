using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hability
{
    protected Piece _piece;

    public float Cost;

    public Hability(Piece piece)
    {
        _piece = piece;
    }

    public virtual bool Use()
    {
        return true;
    }

    /// <summary>
    /// Tenta fazer um lerp para a posição
    /// </summary>
    /// <param name="originalPos">Posição de começo do lerp</param>
    /// <param name="finalPos">Posição final do lerp</param>
    /// <param name="percentage">Porcentagem do lerp</param>
    /// <returns>Retorna falso se não conseguiu concluir o trajeto</returns>
    protected bool TryLerp(Vector2 originalPos, Vector2 finalPos, float percentage)
    {
        var nextPos = originalPos + ((finalPos - originalPos) * percentage);

        // Checa se vai dar ruim
        var hits = Physics2D.CircleCastAll(nextPos, _piece.GetComponent<CircleCollider2D>().radius, Vector3.forward);
        foreach (var hit in hits)
        {
            if (hit.collider.GetComponent<IStopDash>() != null)
            {
                return false;
            }
        }

        _piece.transform.position = nextPos.xyz(_piece.transform.position);
        return true;
    }

    //protected Vector2 CheckDirection(Vector2 direction)
    //{
    //    Vector2 toReturn = _piece.transform.position.xy() + direction * Distance;

    //    // Casta um raio, ve se bateu em algo que para o Dash
    //    var hits = Physics2D.RaycastAll(_piece.transform.position.xy(), direction, Distance);
    //    foreach (var hit in hits)
    //    {
    //        if (hit.collider.GetComponent(typeof(IStopDash)))
    //        {
    //            DebugExtension.DebugCircle(hit.point, Vector3.forward, Color.red, 0.2f, 5);

    //            // Retorna a posição mais perto
    //            toReturn = hit.point - direction * _piece.GetComponent<CircleCollider2D>().radius;
    //            DebugExtension.DebugCircle(toReturn, Vector3.forward, Color.blue, 0.2f, 5);
    //        }
    //    }

    //    return toReturn;
    //}
}
