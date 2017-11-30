using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMode : Hability
{
    private bool _using = false;
    private float _cost = 20;

    private float _turretSpeed = 100;
    private float _turretAttackCost = 5;
    private float _turretAttackSpeed = 0.5f;

    private float _oldSpeed;
    private float _oldAttackCost;
    private float _oldAttackSpeed;

    private GameObject _particles;
    private GameObject _instancedParticles;

    public TurretMode(Piece piece) : base(piece)
    {
        Cost = 0;
    }

    public override bool Use()
    {
        base.Use();

        // Carrega as particulas
        if(_particles == null)
        {
            _particles = Resources.Load<GameObject>("Classes/Tower/TurretModeParticles");
        }

        // Checa se já estava em uso, se for o caso, não tira nenhum custo
        if (!_using)
        {
            _piece.StartCoroutine(Activate());
        }
        else
        {
            _using = false;

            _piece.Speed = _oldSpeed;
            _piece.GetComponent<Class>().AttackSpeed = _oldAttackSpeed;
            _piece.GetComponent<BattlePiece>().AttackCost = _oldAttackCost;

            Object.Destroy(_instancedParticles.gameObject);
        }

        return true;
    }

    private IEnumerator Activate()
    {
        // Stop Moving
        _piece.StopMoving();

        // Toca a animação
        _piece.GetComponent<ClassAnimator>().Play("HabilityDown", 0, true, true);

        // Espera a animação acabar
        yield return new WaitForSeconds(0.8f);

        // Pega as velocidades antigas
        _oldSpeed = _piece.Speed;
        _oldAttackCost = _piece.GetComponent<BattlePiece>().AttackCost;
        _oldAttackSpeed = _piece.GetComponent<Class>().AttackSpeed;

        _using = true;
        _piece.GetComponent<HabilityManager>().Energy -= _cost;

        // Seta as variáveis
        _piece.Speed = _turretSpeed;
        _piece.GetComponent<Class>().CurrentCooldown = 0;
        _piece.GetComponent<Class>().CanAttack = true;

        _piece.GetComponent<BattlePiece>().AttackCost = _turretAttackCost;
        _piece.GetComponent<Class>().AttackSpeed = _turretAttackSpeed;


        // Inicia a corotina
        _piece.StartCoroutine(CReduceStamina());

        // Seta as particulas
        if (_instancedParticles != null)
        {
            Object.Destroy(_instancedParticles.gameObject);
        }

        _instancedParticles = Object.Instantiate(_particles, _piece.transform, false);

        _piece.ResumeMove();

        yield return null;
    }

    private IEnumerator CReduceStamina()
    {
        while (_using)
        {
            _piece.GetComponent<HabilityManager>().Energy -= 0.5f;

            if(_piece.GetComponent<HabilityManager>().Energy <= 0)
            {
                _using = false;

                _piece.Speed = _oldSpeed;
                _piece.GetComponent<Class>().AttackSpeed = _oldAttackSpeed;
                _piece.GetComponent<BattlePiece>().AttackCost = _oldAttackCost;

                Object.Destroy(_instancedParticles.gameObject);

                yield break;
            }

            yield return null;
        }

        yield return null;
    }
}
