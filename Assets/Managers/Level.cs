using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private static Level _instance;
    public static Level Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<Level>();
            }

            return _instance;
        }
    }

    // Pegar num JSON
    public bool Completed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
