using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeDash : Hability
{
    public float Distance = 4;
    public float Speed = 10;

    public bool Dodging = false;

    List<Enemy> _tiedEnemies = new List<Enemy>();
    private float _tiedTime = 2.0f;

    private GameObject _rope;

    public RopeDash(Piece piece) : base(piece)
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
                        // hit.collider.gameObject.GetComponent<Piece>().Speed /= 2;
                        hit.collider.gameObject.GetComponent<Enemy>()._StateMachine.ChangeState(new TiedState(_tiedEnemies.Count));
                        if (!_tiedEnemies.Contains(hit.collider.GetComponent<Enemy>()))
                            _tiedEnemies.Add(hit.collider.GetComponent<Enemy>());
                        if (_tiedEnemies.Count == 1)
                            _piece.StartCoroutine(CTimerOut());

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
            ln.positionCount = _tiedEnemies.Count + 1;
            ln.SetPosition(0, _piece.transform.position);

            for (int i = 0; i < _tiedEnemies.Count; i++)
            {
                // Checa se o inimigo não morreu
                if(_tiedEnemies[i] == null)
                {
                    _tiedEnemies = null;
                    Object.Destroy(obj);
                    yield break;
                }

                ln.SetPosition(i + 1, _tiedEnemies[i].transform.position);
            }


            time += Time.deltaTime;
            yield return null;
        }



        Finished();

        Object.Destroy(obj);
        yield return null;
    }


    void Finished()
    {
        foreach (Enemy g in _tiedEnemies)
        {
            StateManager.Instance.AdjustFollow(g.GetComponent<Enemy>());

            if (_tiedEnemies.Count >= 2)
                g.TakeDamage(g.EnemyDirPlayer(), 30);
        }


        _tiedEnemies.RemoveRange(0, _tiedEnemies.Count);
    }

}
