using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King_Normal : Class
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
            return 0.8f;
        }
    }
    public override float AttackDuration
    {
        get
        {
            return 0.25f;
        }
    }

    public override string Hability
    {
        get
        {
            return "Teleport";
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

            if (angle <= 20)
            {
                area.GetComponent<AttackArea>().Selected = true;

                // Checa se pode atacar
                if (CanAttack)
                {
                    // Toca a Animação
                    if (area.position.x > transform.position.x) // Direita
                        GetComponent<ClassAnimator>().Play("AttackRight", 0, true);

                    if (area.position.x < transform.position.x) // Esquerda
                        GetComponent<ClassAnimator>().Play("AttackLeft", 0, true);

                    if (area.position.y > transform.position.y) // Cima
                        GetComponent<ClassAnimator>().Play("AttackUp", 0, true);

                    if (area.position.y < transform.position.y) // Baixo
                        GetComponent<ClassAnimator>().Play("AttackDown", 0, true);

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


    public override void Start()
    {
        // Carrega um ClassAnimator da classe novo
        gameObject.AddComponent<ClassAnimator>();
        gameObject.GetComponent<ClassAnimator>().LoadAnimations("Classes/King/Animations/KingAnimationController");

        Type = CHESSPIECE.KING;
        // Carreaga a Area de Ataque
        this.AttackArea = Resources.Load<GameObject>("Classes/King/NormalAttackArea");

        // Instancia a Area de Ataque
        _attackArea = Instantiate(AttackArea, transform, false);

        this.AttackArea.transform.localPosition = Vector3.zero;
        // Adiciona a Habilidade correspondente
        GetComponent<HabilityManager>().Hability = new Teleport(this.GetComponent<Piece>());

        base.Start();
    }

    void Update()
    {
        base.Update();
    }
}
