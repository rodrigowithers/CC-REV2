using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePull : Hability
{
    int life_captured = 0;

    public bool Dodging = false;

    GameObject _tiedTo = new GameObject();
    private float _tiedTime = 3.0f;

    private GameObject _rope;

    public RopePull(Piece piece) : base(piece)
    {
        Cost = 40;
        _piece = piece;
    }

    public override bool Use()
    {
        base.Use();

        // Pega a direção que a peça está se movendo
        var dir = _piece.MovementDirection;

        if (Dodging || dir.magnitude < 0.5f)
            return false;



        _piece.StartCoroutine(CChooseTarget());
        return true;
    }


    IEnumerator CChooseTarget()
    {

        if (EnemyManager.Instance.HasEnemies)
        {
            Vector3 pos = GameManager.Instance._Player.transform.position;
            _tiedTo = EnemyManager.Instance.ClosestEnemyOtherThan(pos,_piece.gameObject);

            if (_tiedTo.GetComponent<Enemy>().Life < 3)
                _tiedTo.GetComponent<Enemy>().Life++;

            _piece.StartCoroutine(CStayTied());
        }
        yield return null;
    }

    IEnumerator CStayTied()
    {
        if (_rope == null)
        {
            // Pega a Rope
            _rope = Resources.Load<GameObject>("Classes/Bishop/Rope");
        }

        GameObject obj = Object.Instantiate(_rope,_piece.transform);
        obj.name = "Rope";
        Debug.Log(obj.transform.position.y);

        var ln = obj.GetComponent<LineRenderer>();
        var time = 0.0f;
        ln.positionCount = 2;
        bool pull = true;

        while (time < _tiedTime)
        {
            if (_piece == null || _tiedTo == null)
            {
                time = _tiedTime;
                pull = false;
                Object.Destroy(obj);
            }
            else
            {
                ln.SetPosition(0, _piece.transform.position);
                ln.SetPosition(1, _tiedTo.transform.position);
            }

            time += Time.deltaTime;

            yield return null;
        }
        if (pull)
        {
            PullCloser();
        }
        if (obj != null)
        {
            Object.Destroy(obj);
        }
        yield return null;
    }
    

    void PullCloser()
    {
        Vector2 dirtopull = (_piece.transform.position - _tiedTo.transform.position).normalized;

        _tiedTo.GetComponent<BattlePiece>().Pushback(dirtopull, 30);
    }


}