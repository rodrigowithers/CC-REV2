using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePull : Hability
{
    public float Distance = 4;
    public float Speed = 10;

    public bool Dodging = false;

    GameObject _tiedEnemy;
    private float _tiedTime = 2.0f;

    private GameObject _rope;

    public RopePull(Piece piece) : base(piece)
    {
        Cost = 30;
        _piece = piece;
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
        var pTime = 0.0f;

        while (time < 1)
        {
            pTime++;
            if (pTime >= 4)
            {
                pTime = 0;
                _piece.GetComponent<ParticleSystem>().Emit(1);
            }

            // Raycast para ver se bateu em algo
            Debug.DrawRay(_piece.transform.position.xy() + direction, direction, Color.red);

            if (!hitted)
            {
                var hits = Physics2D.CircleCastAll(_piece.transform.position.xy(), 1, Vector2.zero);
                foreach (var hit in hits)
                {
                    if (hit.collider.GetComponent<Enemy>() && hit.collider.tag != "Player")
                    {
                        _tiedEnemy = hit.collider.gameObject;

                        hitted = true;
                    }
                }
            }

            if (!TryLerp(originalPos, finalPos, time))
                break;

            time += 0.01f * Speed;
            yield return null;
        }

        _piece.CanMove = true;
        Dodging = false;

        yield return null;
    }

    IEnumerator CTimerOut()
    {
        if (_rope == null)
        {
            // Pega a Rope
            _rope = Resources.Load<GameObject>("Classes/Bishop/Rope");
        }

        GameObject obj = Object.Instantiate(_rope);

        var ln = obj.GetComponent<LineRenderer>();
        var time = 0.0f;

        while (time < _tiedTime)
        {
           
            

            time += Time.deltaTime;
            yield return null;
        }



        Finished();

        Object.Destroy(obj);
        yield return null;
    }


    void Finished()
    {
        
        


       
    }

}