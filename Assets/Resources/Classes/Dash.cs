using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Hability
{
    public float Distance = 4;
    public float Speed = 10;

    public bool Dodging = false;

    public Dash(Piece piece) : base(piece)
    {
        Cost = 20;
    }

    public override bool Use()
    {
        base.Use();

        // Pega a direção que a peça está se movendo
        var dir = _piece.MovementDirection;

        if (Dodging || dir.magnitude < 0.5f)
            return false;

        _piece.StartCoroutine(CDash(dir));
        return true;
    }

    IEnumerator CDash(Vector2 direction)
    {
        Dodging = true;
        _piece.CanMove = false;

        Vector2 finalPos = _piece.transform.position.xy() + direction * Distance;
        Vector2 originalPos = _piece.transform.position.xy();
        
        float time = 0;
        while (time < 1)
        {
            if (!TryLerp(originalPos, finalPos, time))
                break;

            time += 0.01f * Speed;
            yield return null;
        }

        _piece.CanMove = true;
        Dodging = false;

        yield return null;
    }
}
