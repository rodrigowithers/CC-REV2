using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen_Normal : Class
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

    public override string Hability
    {
        get
        {
            return "WallDash";
        }
    }

    private Transform _lastAttackArea;
    private float _charge = 0;

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

                // Verifica se é a mesma area de ataque do antigo frame
                if (_lastAttackArea != area)
                {
                    _lastAttackArea = area;
                    _charge = 0;
                }
                else
                {
                    // Checa se pode atacar
                    if (CanAttack)
                    {
                        //print(_charge);

                        // Checa a carga do arco
                        if (_charge >= 16)
                        {
                            _charge = 0;
                            GetComponent<Piece>().Speed = MovementSpeed;

                            // Reduz a Stamina do jogador
                            GetComponent<BattlePiece>().Stamina -= GetComponent<BattlePiece>().AttackCost;
                            GetComponent<BattlePiece>().CanRegen = false;

                            StartCoroutine(area.GetComponent<AttackArea>().CAttack());
                            CanAttack = false;
                        }
                        else
                        {
                            if(_charge == 0)
                            {
                                if(area.position.y > transform.position.y)
                                {
                                    GetComponent<ClassAnimator>().Play("AttackUp", 0, true);
                                }
                                else if (area.position.y < transform.position.y)
                                {
                                    GetComponent<ClassAnimator>().Play("AttackDown", 0, true);
                                }
                                else if(area.position.x > transform.position.x)
                                {
                                    GetComponent<ClassAnimator>().Play("AttackRight", 0, true);
                                }
                                else if (area.position.x < transform.position.x)
                                {
                                    GetComponent<ClassAnimator>().Play("AttackLeft", 0, true);
                                }
                            }

                            Camera.main.GetComponent<CameraController>().Shake(Mathf.Min(0.1f, 0.01f + (_charge / 100)));

                            GetComponent<Piece>().Speed = 150;
                            _charge++;

                        }
                    }
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
        // Troca a Sprite para a Sprite de um Pawn
        //Sprite s = Resources.Load<Sprite>("Sprites/queen");
        //GetComponent<SpriteRenderer>().sprite = s;

        // Carrega o Animator
        gameObject.AddComponent<ClassAnimator>();
        gameObject.GetComponent<ClassAnimator>().LoadAnimations("Classes/Queen/Animations/QueenAnimationController");

        Type = CHESSPIECE.QUEEN;
        // Carreaga a Area de Ataque
        this.AttackArea = Resources.Load<GameObject>("Classes/Queen/NormalAttackArea");

        // Instancia a Area de Ataque
        _attackArea = Instantiate(AttackArea, transform, false);

        this.AttackArea.transform.localPosition = Vector3.zero;
        // Adiciona a Habilidade correspondente
        GetComponent<HabilityManager>().Hability = new WallDash(this.GetComponent<Piece>());

        base.Start();
    }

    void Update()
    {
        base.Update();
    }
}