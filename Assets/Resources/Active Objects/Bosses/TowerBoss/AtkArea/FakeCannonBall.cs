using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCannonBall : MonoBehaviour
{
    public GameObject Pivot;

    public float Speed = 1;
    public Vector2 Destination;
    public bool special = false;
    GameObject explosion;

    protected Vector2 _originalPosition;

    public float time = 0.5f;

    GameObject cannonball;

    bool finished = false;
    bool started = false;
    bool hitpillar = false;
    GameObject pillarhit;


    // Use this for initialization
    void Start()
    {
        // Som
        SoundManager.Play("corte 2");
        SoundManager.Play("corte 1");

        _originalPosition = transform.position;
        if (special)
        {
            transform.localRotation = Quaternion.Euler(Vector3.up);
            StartCoroutine(CCannonBall_Trajectory_UP());
        }
        else
        {
            StartCoroutine(CCannonball_Trajectory());
        }

        explosion = Resources.Load<GameObject>("Field Objects/Explosion/Explosion");

    
        DebugExtension.DebugCircle(Destination, Vector3.forward, Color.magenta, 1, 1);



        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, (Destination - (Vector2)transform.position).normalized,20);
        foreach(RaycastHit2D hit in hits)
        {
            if(hit.collider.GetComponent<BossStopper>())
            {
                Destination = hit.point;
                pillarhit = hit.collider.gameObject;
                hitpillar = true;
            }
        }


        Destroy(this.gameObject, 5.0f);
    }


    void Explode()
    {
        GameObject E = Instantiate(explosion,transform.position,Quaternion.identity);
        E.transform.localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (hitpillar)
        {
            if (((Vector2)transform.position - Destination).magnitude < 0.3f)
            {
                if (pillarhit != null)
                {
                    pillarhit.GetComponent<BossStopper>().Life--;
                    pillarhit.GetComponent<BossStopper>().CorrectSprite();
                    finished = true;
                    Explode();
                }
            }
        }
        if (finished)
        {
            Destroy(this.gameObject);
            Explode();
        }
    }

    IEnumerator CCannonball_Trajectory()
    {
        Speed = (Destination - _originalPosition).magnitude / (time);
        Vector2 dir = (Destination - _originalPosition).normalized;

        float t = 0;

        while (t < time)
        {
            transform.Translate(dir * Speed * Time.deltaTime);
            t += Time.deltaTime;
            yield return null;
        }
        finished = true;
        yield return null;
    }



    IEnumerator CCannonBall_Trajectory_UP()
    {
        float t = 0;
        Speed = ((Camera.main.transform.position.y + 10) - transform.position.y) / time;
        Vector3 _up = Vector3.up * Speed * Time.deltaTime;

        while (t < time)
        {
            transform.Translate(_up*5);
            t += Time.deltaTime;
            yield return null;
        }

        //Destination = Pivot.transform.position;

        transform.position = new Vector2(Destination.x, transform.position.y);

        float dist = (transform.position - (Vector3)Destination).magnitude;
        transform.localRotation = Quaternion.Euler(Vector3.down);


        Speed = dist / time;
        Vector3 _down = Vector3.down * Speed * Time.deltaTime;

        while (dist > 0.6f)
        {
            //Debug.Log(dist);
            transform.Translate(_down);
            dist = (transform.position - (Vector3)Destination).magnitude;
            yield return null;
        }

        finished = true;
        yield return null;
    }

    //void OnCollisionEnter2D(Collision2D coll)
    //{
    //    if (coll.gameObject.GetComponent<BossStopper>())
    //    {
    //        coll.gameObject.GetComponent<BossStopper>().Life--;
    //        coll.gameObject.GetComponent<BossStopper>().CorrectSprite();
    //        finished = true;
    //    }            
    //}

    //public void DestroyCannonball()
    //{
       
    //}

}
