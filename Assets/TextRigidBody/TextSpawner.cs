using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class TextSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _toSpawn;

    /// <summary>
    /// Spawna um texto com fisica
    /// </summary>
    /// <param name="pos">Posição para ser spawnado</param>
    /// <param name="dir">Direção da força</param>
    /// <param name="force">Força a ser aplicada</param>
    /// <param name="text">Texto a ser mostrado</param>
    /// <param name="scale">Escala do texto</param>
    /// <param name="timer">Tempo até o texto ser destruido, se for negativo, nunca destroi o texto</param>
    public GameObject Spawn(Vector2 pos, Vector2 dir, float force, string text, float scale = 1, float timer = 5)
    {
        var temp = Instantiate(_toSpawn, pos, Quaternion.identity, transform) as GameObject;
        
        temp.GetComponent<Text>().text = text;
        temp.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);

        temp.GetComponent<TextRigidBody>().AddForce(dir, force);

        if (timer > 0)
            Destroy(temp.gameObject, timer);

        return temp;
    }

    /// <summary>
    /// Spawna um texto com fisicas com parametros aleatórios, que será destruido em 5 segundos
    /// </summary>
    /// <param name="pos">Posição para ser spawnado</param>
    /// <param name="text">Texto a ser mostrado</param>
    /// <param name="scale">Escala do texto</param>
    public GameObject Spawn(Vector2 pos, string text, float scale = 1)
    {
        var temp = Instantiate(_toSpawn, pos, Quaternion.identity, transform) as GameObject;

        temp.GetComponent<Text>().text = text;
        temp.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);

        var dir = Random.Range(-0.5f, 0.5f);
        var force = Random.Range(1.0f, 5.0f);

        temp.GetComponent<TextRigidBody>().AddForce(new Vector2(dir, 1), force);

        Destroy(temp.gameObject, 5);

        return temp;
    }

    void Start()
    {

    }

    void Update()
    {
        //var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //pos.z = 0;

        //if(Input.GetMouseButtonDown(0))
        //{
        //    var randNum = ((int)Random.Range(0, 10)).ToString();
        //    var temp = Spawn(pos, randNum);
        //    Destroy(temp, 2);
        //}
    }
}
