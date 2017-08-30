using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMode : Hability
{
    private bool _using = false;
    private float _cost = 30;

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
            // Pega as velocidades antigas
            _oldSpeed = _piece.Speed;
            _oldAttackCost = _piece.GetComponent<BattlePiece>().AttackCost;
            _oldAttackSpeed = _piece.GetComponent<Class>().AttackSpeed;

            _using = true;
            _piece.GetComponent<HabilityManager>().Stamina -= _cost;

            // Seta as variáveis
            _piece.Speed = _turretSpeed;
            _piece.GetComponent<BattlePiece>().AttackCost = _turretAttackCost;
            _piece.GetComponent<Class>().AttackSpeed = _turretAttackSpeed;

            // Inicia a corotina
            _piece.StartCoroutine(CReduceStamina());

            // Seta as particulas
            if(_instancedParticles != null)
            {
                Object.Destroy(_instancedParticles.gameObject);
            }

            _instancedParticles = Object.Instantiate(_particles, _piece.transform, false);
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

    private IEnumerator CReduceStamina()
    {
        while (_using)
        {
            _piece.GetComponent<HabilityManager>().Stamina -= 0.5f;

            if(_piece.GetComponent<HabilityManager>().Stamina <= 0)
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
