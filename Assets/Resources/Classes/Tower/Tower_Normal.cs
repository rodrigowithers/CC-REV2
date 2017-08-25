using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Normal : Class
{
    public override float MovementSpeed
    {
        get
        {
            return 250;
        }
    }
    public override float AttackSpeed
    {
        get
        {
            return 1.0f;
        }
    }
    public override float AttackDuration
    {
        get
        {
            return 0.5f;
        }
    }
    public override float StaminaRegenTimer
    {
        get
        {
            return 0.85f;
        }
    }

    public override string Hability
    {
        get
        {
            return "DamageDash";
        }
    }

    public override void Attack(Vector2 direction)
    {
        base.Attack(direction);

        if (_attackAreas == null)
            return;

        // Verifica qual delas está no angulo da direção
        foreach (var area in _attackAreas)
        {
            var areaPos = area.localPosition;
            var angle = Vector2.Angle(direction, areaPos);

            Debug.DrawRay(transform.position, direction, Color.red);

            if (direction.magnitude < 0.7f)
            {
                area.GetComponent<AttackArea>().Selected = false;
            }

            if (angle <= 30)
            {
                area.GetComponent<AttackArea>().Selected = true;

                // Checa se pode atacar
                if (CanAttack)
                {
                    // Reduz a Stamina do jogador
                    GetComponent<BattlePiece>().Stamina -= GetComponent<BattlePiece>().AttackCost;
                    GetComponent<BattlePiece>().CanRegen = false;

                    StartCoroutine(area.GetComponent<AttackArea>().CAttack());

                    CanAttack = false;
                }
            }
            else
            {
                area.GetComponent<AttackArea>().Selected = false;
            }
        }
    }

    void Start()
    {
        // Troca a Sprite para a Sprite de um Pawn
        Sprite s = Resources.Load<Sprite>("Sprites/tower");
        GetComponent<SpriteRenderer>().sprite = s;

        Type = CHESSPIECE.TOWER;
        // Carreaga a Area de Ataque
        this.AttackArea = Resources.Load<GameObject>("Classes/Tower/NormalAttackArea");

        // Instancia a Area de Ataque
        _attackArea = Instantiate(AttackArea, transform, false);

        // Adiciona a Habilidade correspondente
        GetComponent<HabilityManager>().Hability = new DamageDash(this.GetComponent<Piece>());

        base.Start();
    }

    void Update()
    {
        base.Update();
    }
}
