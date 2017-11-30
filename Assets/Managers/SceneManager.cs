using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private static SceneManager _instance;
    public static SceneManager Instance
    {
        get
        {
            if(_instance == null)
            {
                if(FindObjectOfType<SceneManager>() == null)
                {
                    GameObject go = new GameObject("SceneManager");
                    go.AddComponent<SceneManager>();

                    DontDestroyOnLoad(go);

                    _instance = go.GetComponent<SceneManager>();
                }
                else
                {
                    _instance = FindObjectOfType<SceneManager>();
                }
            }

            return _instance;
        }
    }

    public GameObject ScenePrefab;
    private List<IScene> _scenes = new List<IScene>();

    public IScene currentScene;
    public IScene CurrentScene
    {
        get
        {
            return currentScene;
        }

        set
        {
            currentScene = value;
        }
    }

    private int _currentSceneIndex = 0;
    public int CurrentSceneIndex
    {
        get { return _currentSceneIndex; }
        set
        {
            // Checa se exitem cenas nesse indice
            if (value >= _scenes.Count)
            {
                // Colocar lógica de fim de jogo aqui
                throw new System.IndexOutOfRangeException("Fim das cenas atingido");
            }
            else
            {
                // Executa o Exit da cena anterior
                Debug.Log("Exiting Scene " + _currentSceneIndex);
                _scenes[_currentSceneIndex].SceneExit();

                // Troca de cena
                _currentSceneIndex = value;

                // Executa o Enter da cena atual
                Debug.Log("Entering Scene " + _currentSceneIndex);
                _scenes[_currentSceneIndex].SceneEnter();
            }
        }
    }


    void StartArcade()
    {
        for (int i = 0; i < Random.Range(1, 5); i++)
        {
            var scene = Instantiate(ScenePrefab, Vector3.zero, Quaternion.identity);
            scene.name = "Scene " + i;

            _scenes.Add(scene.GetComponent<IScene>());
        }

        // Inicia a primeira cena
        _scenes[CurrentSceneIndex].SceneEnter();
    }

    void UpdateArcade()
    {
        _scenes[CurrentSceneIndex].SceneUpdate();
    }

    void UpdateChapter()
    {
        if(currentScene != null)
            CurrentScene.SceneUpdate();
    }

    void Start()
    {

    }

    void Update()
    {
        UpdateChapter();
    }
}
