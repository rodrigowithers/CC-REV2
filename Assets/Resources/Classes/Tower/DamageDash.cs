using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDash : Hability
{
    public float Distance = 5;
    public float Speed = 5;

    public bool Dodging = false;

    public DamageDash(Piece piece) : base(piece)
    {
        Cost = 30;
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

        bool hitted = false;
        float time = 0;

        float pTime = 0;

        while (time < 1)
        {
            pTime++;
            if (pTime >= 4)
            {
                pTime = 0;
                _piece.GetComponent<ParticleSystem>().Emit(1);
            }

            // CircleCast para ver se bateu em algo
            //Debug.DrawRay(_piece.transform.position.xy() + direction, direction, Color.red);

            DebugExtension.DebugCircle(_piece.transform.position.xy(), Vector3.forward, Color.red, 0.8f);

            if (!hitted)
            {
                var hits = Physics2D.CircleCastAll(_piece.transform.position, 0.8f, Vector2.zero);
                //var hits = Physics2D.RaycastAll(_piece.transform.position.xy() + direction, direction, 1);
                foreach (var hit in hits)
                {
                    if (hit.collider.GetComponent<IKillable>() != null && hit.collider.gameObject != _piece.gameObject)
                    {
                        hitted = true;

                        // Pega uma direção perpendicular à direção de dash
                        var dir = _piece.transform.right;

                        hit.collider.GetComponent<IKillable>().TakeDamage(dir, 15);
                        Camera.main.GetComponent<CameraController>().Shake();
                        break;
                    }
                }
            }

            if (!TryLerp(originalPos, finalPos, time))
            {
                break;
            }

            time += 0.01f * Speed;
            yield return null;
        }

        _piece.CanMove = true;
        Dodging = false;

        yield return null;
    }

}
