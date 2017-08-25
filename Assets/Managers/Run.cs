//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Run : MonoBehaviour
//{
//    private static Run _instance = null;
//    public static Run Instance
//    {
//        get
//        {
//            if (_instance == null)
//            {
//                var temp = GameObject.FindObjectOfType<Run>();

//                if (temp != null)
//                {
//                    _instance = temp;

//                    return _instance;
//                }

//                var obj = new GameObject("Run", typeof(Run));
//                _instance = obj.GetComponent<Run>();

//                DontDestroyOnLoad(obj);
//            }

//            return _instance;
//        }
//    }

//    public int SceneAmount = 6; // Quantidade de cenas no cenário
//    public int WaveGrowth = 1; // O quanto a quantidade de waves cresce por cada cena

//    public CHESSPIECE StartingClass;

//    public float DificultyGrowth = 1; // O quanto a dificuldade de cada cena cresce, por cena

//    private void Awake()
//    {
//        if (_instance == null)
//            _instance = this;
//    }


//    // Use this for initialization
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//}
