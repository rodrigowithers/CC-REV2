using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDash : Hability
{
    public float Distance = 5;
    public float Speed = 5;

    public bool Dodging = false;

    public WallDash(Piece piece) : base(piece)
    {
        Cost = 25;
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

    private Vector2 CheckDirection(Vector2 direction)
    {
        Vector2 toReturn = _piece.transform.position.xy() + direction * Distance;

        // Casta um raio, ve se bateu em algo que para o Dash
        var hits = Physics2D.RaycastAll(_piece.transform.position.xy(), direction, Distance);
        foreach (var hit in hits)
        {
            if (hit.collider.GetComponent(typeof(IStopDash)))
            {
                // Retorna a posição mais perto
                toReturn = hit.point - direction;
            }
        }

        return toReturn;
    }

    IEnumerator CDash(Vector2 direction)
    {
        Dodging = true;
        _piece.CanMove = false;

        Vector2 finalPos = _piece.transform.position.xy() + direction * Distance;
        Vector2 originalPos = _piece.transform.position.xy();

        // Toca a animação
        if(direction.y < 0)
        {
            _piece.GetComponent<ClassAnimator>().Play("HabilityDown", 0, true);
        }
        else if(direction.y > 0)
        {
            _piece.GetComponent<ClassAnimator>().Play("HabilityUp", 0, true);
        }
        else if (direction.x > 0)
        {
            _piece.GetComponent<ClassAnimator>().Play("HabilityRight", 0, true);
        }
        else if (direction.x < 0)
        {
            _piece.GetComponent<ClassAnimator>().Play("HabilityLeft", 0, true);
        }

        float time = 0;
        var pTime = 0.0f;

        while (time < 1)
        {
            pTime++;
            if (pTime >= 4)
            {
                pTime = 0;
                _piece.GetComponent<ParticleSystem>().Emit(1);
            }


            if (!TryLerp(originalPos, finalPos, time))
                break;

            time += 0.01f * Speed;
            yield return null;
        }

        Check4Walls();
        _piece.CanMove = true;
        Dodging = false;

        yield return null;
    }


    void Check4Walls()
    {
        Vector3 cam = Camera.main.transform.position;
        Vector3 pos = _piece.transform.position;
        if(pos.x > 16.5)
        {
            pos.x *= -1;
        }
        else if(pos.x < -16.5)
        {
            pos.x *= -1;
        }
        if (pos.y > cam.y + 8.5)
        {
            pos.y = cam.y - 8;
        }
        else if (pos.y < cam.y - 8.5)
        {
            pos.y = cam.y + 8;
        }

        _piece.transform.position = pos;

    }
    
}