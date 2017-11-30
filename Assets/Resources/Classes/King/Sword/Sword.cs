using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IAttack
{
    public float Speed = 5;
    public float RotSpeed = 360;

    public Vector2 Direction;

    protected float _time = 0;
    protected bool _damage = true;

    public GameObject SwordParticles;

    public BattlePiece _piece;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IStopDash>() != null)
        {
            Destroy(this.gameObject);
            return;
        }
        var obj = collision.GetComponent<IKillable>();
        if(obj != null && collision.GetComponent<BattlePiece>() != _piece)
        {
            if (collision.GetComponent<Enemy>() && _piece.GetComponent<Enemy>())
                return;

            obj.TakeDamage(Direction);
        }
    }

    // Use this for initialization
    void Start()
    {
        transform.Rotate(Vector3.forward, -45);

        // Toca o som
        SoundManager.Play("corte 2");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        transform.localPosition -= transform.right * Time.deltaTime * Speed;
        transform.Rotate(Vector3.forward, RotSpeed * Time.deltaTime * 2);

        _time += Time.deltaTime;

        if (_time > 0.3f / 2)
        {
            // Separa as partículas
            SwordParticles.transform.parent = null;

            Destroy(this.gameObject);
        }
    }
}
