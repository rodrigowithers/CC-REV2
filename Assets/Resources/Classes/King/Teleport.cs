using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Hability
{
    public float Distance = 5;
    public float Speed = 5;

    public bool Dodging = false;

    private GameObject _teleportIn;
    private GameObject _teleportOut;

    public Teleport(Piece piece) : base(piece)
    {
        Cost = 70;
    }

    public override bool Use()
    {
        base.Use();

        if (Dodging)
            return false;

        _piece.StartCoroutine(CTeleport());
        _piece.GetComponent<ClassAnimator>().Play("HabilityDown", 0, true);

        return true;
    }

    private GameObject Closest(List<Enemy> EnemyList)
    {
        GameObject closest = null;
        float closestdist = 1000;
        float dist = 0;
        foreach (var g in EnemyList)
        {
            dist = g.EnemyDistPlayer();
            if (closestdist > dist)
            {
                closestdist = dist;
                closest = g.gameObject;
            }
        }

        return closest;
    }

    private IEnumerator CTeleport()
    {
        GameObject target = null;
        if (_piece.GetComponent<Player>())
        {
            List<Enemy> enemies = new List<Enemy>(GameObject.FindObjectsOfType<Enemy>());
            target = Closest(enemies);
        }
        else if (_piece.GetComponent<Enemy>())
            target = GameManager.Instance._Player;
        if (target == null)
            yield break;

        // Carrega os teleportes do Resources
        _teleportIn = Resources.Load<GameObject>("Classes/King/TeleportIn");
        _teleportOut = Resources.Load<GameObject>("Classes/King/TeleportOut");

        Object.Instantiate(_teleportIn, _piece.transform.position, Quaternion.identity);

        _piece.CanMove = false;
        _piece.RigidBody.velocity = Vector2.zero;

        Vector3 dir = (target.transform.position - _piece.transform.position).normalized;
        
        Vector3 targetDir = target.GetComponent<Piece>().Direction;
        //Vector2 finalpos = target.transform.position - (2 * targetDir);

        Vector2 finalPos = target.transform.position + dir * 2;

        yield return new WaitForSeconds(0.25f);

        _piece.CanMove = true;

        _piece.transform.position = finalPos;       


        if (_piece.GetComponent<Player>())
            EnemyManager.Instance.ConfuseAllEnemies();
        else if (_piece.GetComponent<Enemy>())
            _piece.GetComponent<Unit>().RequestNewPathTo(target);

        Object.Instantiate(_teleportOut, finalPos, Quaternion.identity);

        yield return null;
    }

    //private Vector2 CheckDirection(Vector2 direction)
    //{
    //    Vector2 toReturn = _piece.transform.position.xy() + direction * Distance;

    //    // Casta um raio, ve se bateu em algo que para o Dash
    //    var hits = Physics2D.RaycastAll(_piece.transform.position.xy(), direction, Distance);
    //    foreach (var hit in hits)
    //    {
    //        if (hit.collider.GetComponent(typeof(IStopDash)))
    //        {
    //            // Retorna a posição mais perto
    //            toReturn = hit.point - direction;
    //        }
    //    }

    //    return toReturn;
    //}

    //IEnumerator CDash(Vector2 direction)
    //{
    //    Dodging = true;
    //    _piece.CanMove = false;

    //    Vector2 finalPos = CheckDirection(direction);
    //    Vector2 originalPos = _piece.transform.position.xy();

    //    float finalDistance = Vector2.Distance(_piece.transform.position.xy(), finalPos);
    //    float curDistance = 0;

    //    float time = 0;

    //    bool hitted = false;

    //    while (curDistance < finalDistance)
    //    {
    //        // Raycast para ver se bateu em algo
    //        Debug.DrawRay(_piece.transform.position.xy() + direction, direction, Color.red);

    //        if (!hitted)
    //        {
    //            var hits = Physics2D.RaycastAll(_piece.transform.position.xy() + direction, direction, 1);
    //            foreach (var hit in hits)
    //            {
    //                if (hit.collider.GetComponent<IKillable>() != null && hit.collider.gameObject != _piece)
    //                {
    //                    hitted = true;

    //                    hit.collider.GetComponent<IKillable>().TakeDamage(direction);
    //                    break;
    //                }
    //            }
    //        }


    //        curDistance = Vector2.Distance(_piece.transform.position.xy(), originalPos);

    //        _piece.transform.position = Vector3.Lerp(originalPos, finalPos, time);
    //        time += Time.deltaTime * Speed;

    //        yield return new WaitForEndOfFrame();
    //    }

    //    _piece.CanMove = true;
    //    Dodging = false;

    //    yield return null;
    //}

}
