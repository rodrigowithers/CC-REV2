using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    public float Speed = 50;
    public Vector2 Direction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.GetComponent<IKillable>();
        if (obj != null && !collision.GetComponent<HarpoonShooter>())
        {
            var dir = (collision.transform.position - transform.position).normalized;

            obj.TakeDamage(dir);
            Destroy(this.gameObject);
        }

        if(collision.GetComponent<IStopDash>() != null)
        {
            Destroy(this.gameObject);
        }

    }

    // Use this for initialization
    void Start()
    {
        Destroy(this.gameObject, 5);

        transform.localRotation = Quaternion.FromToRotation(Vector3.up, Direction);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Direction.xyz(transform.position) * Speed * Time.deltaTime;
    }
}
