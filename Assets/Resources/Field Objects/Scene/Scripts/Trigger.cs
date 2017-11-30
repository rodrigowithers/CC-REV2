using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Trigger : MonoBehaviour
{
    public UnityEvent Event;

    private void OnEnable()
    {
        if (!GetComponent<BoxCollider2D>().isTrigger)
            GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            Event.Invoke();
        }
    }
}
