using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn_Normal : Class
{
    public override float MovementSpeed
    {
        get
        {
            return 300;
        }
    }
    public override float AttackSpeed
    {
        get
        {
            return 0.5f;
        }
    }
    public override float AttackDuration
    {
        get
        {
            return 0.2f;
        }
    }


    public override string Hability
    {
        get
        {
            return "InvincibleDash";
        }
    }

    public override void Attack(Vector2 direction)
    {
        base.Attack(direction);

        if (_attackAreas == null)
            return;

        // Verifica qual das areas de ataque está no angulo da direção
        foreach (var area in _attackAreas)
        {
            var areaPos = area.localPosition;
            var angle = Vector2.Angle(direction, areaPos);

            Debug.DrawRay(transform.position, direction, Color.red);

            if (direction.magnitude < 0.7f)
            {
                area.GetComponent<AttackArea>().Selected = false;
                continue;
            }

            if (angle <= 30)
            {
                area.GetComponent<AttackArea>().Selected = true;

                // Checa se pode atacar
                if (CanAttack)
                {
                    // Toca a Animação
                    if (area.position.x > transform.position.x) // Direita
                    {
                        if (area.position.y > transform.position.y) // Cima
                            GetComponent<ClassAnimator>().Play("AttackUpRight", 0, true);
                        else                                  // Baixo
                            GetComponent<ClassAnimator>().Play("AttackDownRight", 0, true);
                    }
                    else                                      // Esquerda
                    {
                        if (area.position.y > transform.position.y) // Cima
                            GetComponent<ClassAnimator>().Play("AttackUpLeft", 0, true);
                        else                                  // Baixo
                            GetComponent<ClassAnimator>().Play("AttackDownLeft", 0, true);
                    }

                    // Reduz a Stamina do jogador
                    GetComponent<BattlePiece>().Stamina -= GetComponent<BattlePiece>().AttackCost;
                    GetComponent<BattlePiece>().CanRegen = false;

                    StartCoroutine(area.GetComponent<AttackArea>().CAttack());
                    CanAttack = false;

                    GetComponent<BattlePiece>().StopMoving();
                    GetComponent<BattlePiece>().hitStun(AttackDuration);
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
        // Carrega um ClassAnimator da classe novo
        gameObject.AddComponent<ClassAnimator>();
        gameObject.GetComponent<ClassAnimator>().LoadAnimations("Classes/Pawn/Animations/PawnAnimationController");

        Type = CHESSPIECE.PAWN;

        if (GetComponent<Enemy_Tower>() != null)
        {
            GetComponent<Enemy_Tower>()._Type = Type;
        }

        // Carreaga a Area de Ataque
        this.AttackArea = Resources.Load<GameObject>("Classes/Pawn/NormalAttackArea");

        // Instancia a Area de Ataque
        _attackArea = Instantiate(AttackArea, transform, false);

        // Adiciona a Habilidade correspondente
        GetComponent<HabilityManager>().Hability = new InvincibleDash(this.GetComponent<Piece>());

        base.Start();
    }

    void Update()
    {
        base.Update();
    }
}
