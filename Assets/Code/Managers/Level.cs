using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
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

    public string Name = "1";
    public bool Completed;

    private void OnEnable()
    {
        Completed = Resources.Load<LevelsAsset>("Level Data").Check(Name);
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
