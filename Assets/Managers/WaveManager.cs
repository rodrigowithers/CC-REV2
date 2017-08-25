//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class WaveManager : MonoBehaviour
//{
//    private string _document_path = "Documents/SceneInfo";
//    private string _newPath;

//    private List<Wave> _waves = new List<Wave>();
//    private GameObject _scene;
//    private GameObject _sceneHard;

//    private GameObject _endUI;

//    private int _currentScene = 0; // Cena atual

//    // Singleton
//    private static WaveManager _instance = null;
//    public static WaveManager Instance
//    {
//        get
//        {
//            if(_instance == null)
//            {
//                // Tenta encontrar um objeto desse tipo já na cena
//                var temp = FindObjectOfType<WaveManager>();

//                if(temp == null)
//                {
//                    GameObject go = new GameObject("WaveManager");
//                    go.AddComponent<WaveManager>();
//                    DontDestroyOnLoad(go);

//                    _instance = go.GetComponent<WaveManager>();
//                }
//                else
//                {
//                    _instance = temp.GetComponent<WaveManager>();
//                }
//            }

//            return _instance;
//        }
//    }

//    public List<SceneScript> Scenes = new List<SceneScript>();

//    public int SceneAmount = 2; // Quantidade de cenas no cenário
//    public int WaveGrowth = 1; // O quanto a quantidade de waves cresce por cada cena

//    public CHESSPIECE StartingClass;

//    public Text CurSceneText;

//    [Range(0.1f, 1.0f)]
//    public float DificultyGrowth = 0.5f; // O quanto a dificuldade de cada cena cresce, por cena




//    public void TurnNextSceneIntoBoss()
//    {
//        Scenes[CurrentScene + 1].Dificulty = 0;
//        Scenes[CurrentScene + 1].WaveCount = 1;
//        Scenes[CurrentScene + 1].Boss = true;
//    }

//    public SceneScript  GetSceneScript()
//    {
//        return Scenes[CurrentScene].GetComponent<SceneScript>();
//    }


//    public int CurrentScene
//    {
//        get
//        {
//            return _currentScene;
//        }

//        set
//        {
//            var temp = Scenes[_currentScene];

//            // Desativa a cena atual
//            temp.SceneActive = false;
//            temp.ResetScene();

//            temp.gameObject.SetActive(false);

//            // Modifica o valor
//            _currentScene = value;

//            if (_currentScene <= 0)
//            {
//                _currentScene = 0;
//            }

//            var newTemp = Scenes[0];

//            // Verifica se essa é a ultima cena, se for, só desabilita todas por hora
//            if (_currentScene >= Scenes.Count && !_endUI.activeSelf)
//            {
//                Debug.Log("Não existem mais cenas");

//                _currentScene--;
//                _endUI.SetActive(true);
//                _endUI.GetComponent<HUD_EndGame>().Activate();
//            }

//            newTemp = Scenes[_currentScene];

//            //// Se a vida do player está maior que o total
//            //if (GameManager._instance.Player.GetComponent<Player>().Life > 3)
//            //    GameManager._instance.Player.GetComponent<Player>().Life = 3;

//            // Ativa a cena nova
//            newTemp.gameObject.SetActive(true);

//            // Seta o texto
//            CurSceneText.text = _currentScene.ToString();
//        }
//    }

//    /// <summary>
//    /// Retorna um array com as waves com a dificuldade passada
//    /// </summary>
//    public Wave[] GetWavesByDificulty(int dificulty)
//    {
//        List<Wave> toReturn = new List<Wave>();

//        foreach (Wave w in _waves)
//        {
//            if (w.Level <= dificulty)
//                toReturn.Add(w);
//        }

//        return toReturn.ToArray();
//    }

//    public int[] GetEnemiesFromWave(Wave wave)
//    {
//        List<int> toReturn = new List<int>();

//        int index = 0;

//        for (index = 0; index < wave.Bishop; index++)
//        {
//            toReturn.Add((int)CHESSPIECE.BISHOP);
//        }
//        for (index = 0; index < wave.Horse; index++)
//        {
//            toReturn.Add((int)CHESSPIECE.HORSE);
//        }
//        for (index = 0; index < wave.Kings; index++)
//        {
//            toReturn.Add((int)CHESSPIECE.KING);
//        }
//        for (index = 0; index < wave.Pawn; index++)
//        {
//            toReturn.Add((int)CHESSPIECE.PAWN);
//        }
//        for (index = 0; index < wave.Queen; index++)
//        {
//            toReturn.Add((int)CHESSPIECE.QUEEN);
//        }
//        for (index = 0; index < wave.Tower; index++)
//        {
//            toReturn.Add((int)CHESSPIECE.TOWER);
//        }

//        return toReturn.ToArray();
//    }

//    /// <summary>
//    /// Retorna a quantidade de inimigos em uma certa wave
//    /// </summary>
//    /// <param name="wave">A wave a ser checada</param>
//    /// <returns></returns>
//    public int WaveEnemies(Wave wave)
//    {
//        int toReturn = 0;

//        toReturn += (wave.Bishop + wave.Horse + wave.Kings + wave.Pawn + wave.Queen + wave.Tower);

//        return toReturn;
//    }

//    private void SpawnScenes()
//    {
//        // Spawna um GameObject vazio, que vai servir para organização
//        GameObject pivot = new GameObject("Scenes");

//        for (int i = 0; i < SceneAmount; i++)
//        {
//            Vector3 pos = Vector3.zero;
//            pos.y = 25 * i;

//            GameObject toInstantiate;

//            if (i > 5)
//                toInstantiate = _sceneHard;
//            else
//                toInstantiate = _scene;

//            var temp = Instantiate(toInstantiate, pos, Quaternion.identity, pivot.transform) as GameObject;
//            SceneScript curScene = temp.GetComponent<SceneScript>();

//            // Checa se é multiplo se 5
//            if (i % 5 == 0 && i != 0)
//            {
//                // É uma wave com Boss

//                curScene.SceneNumber = i;
//                curScene.WaveCount = 1;
//                curScene.Dificulty = 0;

//                curScene.Boss = true;
//            }
//            else
//            {
//                curScene.SceneNumber = i;
//                curScene.WaveCount = i * WaveGrowth;
//                curScene.Dificulty = Mathf.FloorToInt(i * DificultyGrowth);
//            }

//            curScene.pos = pos;
//            curScene.SpawnGround();

//            Scenes.Add(curScene);
//            curScene.gameObject.SetActive(false);
//        }

//        Scenes[0].gameObject.SetActive(true);
//        Scenes[0].SceneActive = true;
//    }

//    private void Awake()
//    {
//        if (_instance == null)
//        {
//            _instance = this;
//        }
//    }

//    void Start()
//    {
//        _scene = Resources.Load<GameObject>("Prefabs/Scene");
//        _sceneHard = Resources.Load<GameObject>("Prefabs/SceneHard");

//        var run = Run.Instance;

//        SceneAmount = run.SceneAmount;
//        WaveGrowth = run.WaveGrowth;
//        DificultyGrowth = run.DificultyGrowth;

//        _newPath = Application.streamingAssetsPath + "/SceneInfo";
//        XmlLoader.LoadWaves(_document_path, _waves);

//        SpawnScenes();
//        Camera.main.GetComponent<CameraSceneController>().SetPivots(Scenes.ToArray());

//        _endUI = Tag.FindUsingTag("UIEnd");
//        _endUI.SetActive(false);

//        CurSceneText = GameObject.FindGameObjectWithTag("StageNumber").GetComponent<Text>();
//    }
//}
