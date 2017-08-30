using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvincibleDash : Hability
{
    public float Distance = 3;
    public float Speed = 10;

    public bool Dodging = false;
    public float InvincibleTime = 3;
    Color originalcolor = new Color();

    private GameObject _shield;
    private GameObject _explosion;

    private GameObject _canvas;
    private Image _chargeBar;
    private float _charge = 0.0f;
    private int _chargeCount = 3;

    public InvincibleDash(Piece piece) : base(piece)
    {
        Cost = 30;

        // Instancia o Canvas
        _canvas = Object.Instantiate(Resources.Load<GameObject>("Classes/Pawn/InvincibleDash Canvas"), _piece.transform, false);
        _chargeBar = _canvas.transform.GetChild(1).GetComponent<Image>();
        _chargeBar.fillAmount = 0;
    }

    public override bool Use()
    {
        base.Use();

        originalcolor = _piece.GetColor();
        // Pega a direção que a peça está se movendo
        var dir = _piece.MovementDirection;

        if (Dodging || dir.magnitude < 0.5f)
            return false;

        if(_shield == null || _explosion == null)
        {
            _shield = Resources.Load<GameObject>("Classes/Pawn/PawnShield");
            _explosion = Resources.Load<GameObject>("Classes/Pawn/PawnExplosion");
        }

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

        float time = 0;
        var pTime = 0.0f;

        while (time < 1)
        {
            pTime++;
            if (pTime >= 3)
            {
                pTime = 0;
                _piece.GetComponent<ParticleSystem>().Emit(1);
            }

            if (!TryLerp(originalPos, finalPos, time))
                break;

            time += 0.01f * Speed;
            yield return null;
        }

        _charge += (1.0f / _chargeCount);
        _chargeBar.fillAmount = _charge;

        if (!_piece.IsInvincible && _charge >= 1.0f)
            _piece.StartCoroutine(CInvincibleTime());

        _piece.CanMove = true;
        Dodging = false;

        yield return null;
    }

    public void Explode(float radius = 1.0f)
    {
        DebugExtension.DebugCircle(_piece.transform.position, Vector3.forward, Color.red, radius, 5);

        // Instancia o Sistema da Partículas
        Object.Instantiate(_explosion, _piece.transform.position, Quaternion.identity);

        // Checa se houve colisão em um raio ao redor da explosão
        var hits = Physics2D.CircleCastAll(_piece.transform.position, radius, Vector2.zero);
        foreach (var hit in hits)
        {
            var obj = hit.collider.GetComponent<IKillable>();
            if (obj != null && hit.collider.gameObject != _piece.gameObject)
            {
                var dir = (hit.collider.transform.position - _piece.transform.position).normalized;
                obj.TakeDamage(dir, 10);
            }

            var atk = hit.collider.GetComponent<IAttack>();
            if(atk != null)
            {
                Object.Destroy(hit.collider.gameObject);
            }
        }

        // ScreenShake
        Camera.main.GetComponent<CameraController>().Shake();
    }

    IEnumerator CInvincibleTime()
    {
        _charge = 0;
        _chargeBar.fillAmount = _charge;

        _piece.SetColor(Color.white);

        _piece.IsInvincible = true;
        _piece.SetColor(Color.yellow);

        var oldSpeed = _piece.Speed;
        _piece.Speed = 200;

        var particles = Object.Instantiate(_shield, _piece.transform, false);

        var ps = particles.GetComponent<ParticleSystem>().emission.rateOverTime;
        ps.constant = 0;

        float time = 0;
        int counter = 0;

        while (time < InvincibleTime)
        {
            time += Time.deltaTime;

            DebugExtension.DebugCircle(_piece.transform.position, Vector3.forward, Color.yellow, 0.5f);

            // Circle Cast
            var hits = Physics2D.CircleCastAll(_piece.transform.position, 0.5f, Vector2.zero);
            foreach (var hit  in hits)
            {
                var obj = hit.collider.GetComponent<IAttack>();
                if (obj != null && hit.collider.GetComponent<Lance>().Piece.gameObject != _piece.gameObject)
                {
                    ps.constant += 10;

                    counter++;
                    break;
                }
            }

            yield return null;
        }



        if (_piece.GetComponent<Enemy>())
            _piece.SetColor(Color.red);
        else
            _piece.SetColor(Color.white);

        Object.Destroy(particles);

        if (counter > 1)
        {
            //Explode();
        }

        _piece.Speed = oldSpeed;
        _piece.IsInvincible = false;
        yield return null;
    }


}