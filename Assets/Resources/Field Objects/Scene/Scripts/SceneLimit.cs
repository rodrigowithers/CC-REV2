using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLimit : MonoBehaviour
{
    private Scene _scene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            // Checa se já está na cena, se está, não faz nada
            if (_scene.Entered)
                return;

            transform.parent.GetComponent<Scene>().SceneEnter();
        }
    }

    private void Start()
    {
        _scene = transform.parent.GetComponent<Scene>();
    }
}
