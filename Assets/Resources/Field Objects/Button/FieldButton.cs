using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FieldButton : MonoBehaviour
{
    public bool Pressed = false;

    public UnityEvent Events;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() && !Pressed)
        {
            Pressed = true;

            // Passa pela lista de Events, executando cada um
            if (Events != null)
                Events.Invoke();
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
