using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop_Normal : Class
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
            return 0.25f;
        }
    }

    public override void Attack(Vector2 direction)
    {
        base.Attack(direction);

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


    public override void Start()
    {
        Type = CHESSPIECE.BISHOP;
        // Carreaga a Area de Ataque
        this.AttackArea = Resources.Load<GameObject>("Classes/Bishop/NormalAttackArea");

        // Instancia a Area de Ataque
        Instantiate(AttackArea, transform, false);

        this.AttackArea.transform.localPosition = Vector3.zero;
        // Adiciona a Habilidade correspondente
        GetComponent<HabilityManager>().Hability = new Dash(this.GetComponent<Piece>());

        base.Start();
    }

    void Update()
    {
        base.Update();
    }
}
