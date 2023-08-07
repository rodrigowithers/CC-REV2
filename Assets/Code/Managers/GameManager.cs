﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameMode
    {
        NORMAL,
        HARD,
        QUEEN
    }

    private GameObject _player;
    public GameObject _Player
    {
        get
        {
            if(_player == null)
            {
                _player = Player.Instance.gameObject;
            }

            return _player;
        }
        set
        {
            _player = value;
        }
    }

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                if (FindObjectOfType<GameManager>() == null)
                {
                    GameObject go = new GameObject("Game Manager");
                    go.AddComponent<GameManager>();

                    DontDestroyOnLoad(go);

                    _instance = go.GetComponent<GameManager>();
                }
                else
                {
                    _instance = FindObjectOfType<GameManager>();
                }
            }

            return _instance;
        }
    }

    public bool Dead
    {
        set
        {
            if (value)
            {
                if(YouDied != null)
                {
                    YouDied.SetActive(true);
                }

                foreach(var enemy in EnemyManager.Instance.EnemyList)
                {
                    enemy.GetComponent<StateMachine>().SetCurrentState(new WanderState());

                    //Destroy(enemy.GetComponent<Piece>());
                    //Destroy(enemy.GetComponent<StateMachine>());
                }

                Time.timeScale = 0.0f;

                StartCoroutine(WaitAnyInput());
            }
        }
    }

    public bool Pause
    {
        set
        {
            if (value)
            {

            }
            else
            {

            }
        }
    }

    public GameObject YouDied;

    public List<Sprite> _Portraits = new List<Sprite>();

    public GameMode Mode = GameMode.NORMAL;

    private bool Next()
    {
        bool hasPressed = Input.GetButtonUp("Hability");
        return hasPressed;

    }

    private IEnumerator WaitAnyInput()
    {
        bool input = false;

        System.Func<bool> Pressed = new System.Func<bool>(Next);

        yield return new WaitForSecondsRealtime(1.0f);

        yield return new WaitUntil(Pressed);

        // Pega a cena atual
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.name);

        yield return null;
    }

    void Awake()
    {
        LoadAllSprites();
    }

    private void Start()
    {

    }

    public void LoadScene(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }

    void LoadAllSprites()
    {
        //Carregando Inimigos
        _Portraits.Add(Resources.Load<Sprite>("Active Objects/Enemies/Portraits/e_King_Normal"));
        _Portraits.Add(Resources.Load<Sprite>("Active Objects/Enemies/Portraits/e_Queen_Normal"));
        _Portraits.Add(Resources.Load<Sprite>("Active Objects/Enemies/Portraits/e_Tower_Normal"));
        _Portraits.Add(Resources.Load<Sprite>("Active Objects/Enemies/Portraits/e_Horse_Normal"));
        _Portraits.Add(Resources.Load<Sprite>("Active Objects/Enemies/Portraits/e_Bishop_Normal"));
        _Portraits.Add(Resources.Load<Sprite>("Active Objects/Enemies/Portraits/e_Pawn_Normal"));

        //Carregando Player
        _Portraits.Add(Resources.Load<Sprite>("Active Objects/Player/Character Switch/Portraits/King_Normal"));
        _Portraits.Add(Resources.Load<Sprite>("Active Objects/Player/Character Switch/Portraits/Queen_Normal"));
        _Portraits.Add(Resources.Load<Sprite>("Active Objects/Player/Character Switch/Portraits/Tower_Normal"));
        _Portraits.Add(Resources.Load<Sprite>("Active Objects/Player/Character Switch/Portraits/Horse_Normal"));
        _Portraits.Add(Resources.Load<Sprite>("Active Objects/Player/Character Switch/Portraits/Bishop_Normal"));
        _Portraits.Add(Resources.Load<Sprite>("Active Objects/Player/Character Switch/Portraits/Pawn_Normal"));

    }

    private void Update()
    {
        
    }

    //private Object[] _piecesprites;
    //[SerializeField]
    //private bool _menu;

    //private static float _oldTimeScale = 1.0f;
    //private float _oldVolume = 1.0f;

    //public static GameManager _instance = null;
    //List<Sprite> spritesbonitas = new List<Sprite>();

    //BnWImageEffect effect;

    //private GameObject _menuObject;


    //public GameObject _player; // Referencia para o jogador, para que esse possa ser mais facilmente encontrado nas outras classes

    //public GameObject Player
    //{
    //    get
    //    {
    //        if (_player == null)
    //            _player = Tag.FindUsingTag("Player");

    //        return _player;
    //    }
    //}

    //void Awake()
    //{
    //    if (_instance == null)
    //        _instance = this;
    //    else if (_instance != this)
    //        Destroy(gameObject);

    //    DontDestroyOnLoad(gameObject);
    //    LoadSprites();
    //}

    //public void InGameMenu()
    //{
    //    _menu = !_menu;

    //    // Encontra a musica
    //    var sound = GameObject.FindGameObjectWithTag("SpectrumAnaliser").GetComponent<AudioSource>();

    //    effect = Camera.main.GetComponent<BnWImageEffect>();

    //    if (_menu)
    //    {
    //        _oldTimeScale = Time.timeScale;
    //        _oldVolume = sound.volume;

    //        effect.enabled = true;
    //        _menuObject.SetActive(true);

    //        Time.timeScale = 0.0f;
    //        sound.volume = _oldVolume - 0.7f;
    //    }
    //    else
    //    {
    //        //effect.Enabled = false;
    //        effect.enabled = false;

    //        _menuObject.SetActive(false);

    //        sound.volume = _oldVolume;
    //        Time.timeScale = _oldTimeScale;
    //    }
    //}

    //public void GoToMainMenu()
    //{
    //    // Despausa o jogo
    //    if (_menu)
    //        InGameMenu();

    //    // Deleta tudo que tem que ser deletado
    //    //Destroy(GameManager._instance.gameObject);
    //    Destroy(PieceManager.Instance.gameObject);
    //    Destroy(WaveManager.Instance.gameObject);
    //    Destroy(Run.Instance.gameObject);

    //    SceneManager.LoadScene("Menu");
    //}

    //public void DisableMenu()
    //{
    //    // Encontra o Menu
    //    _menuObject = GameObject.FindGameObjectWithTag("Menu");
    //    _menuObject.SetActive(false);
    //}

    //// Use this for initialization
    //void Start()
    //{
    //    _player = Tag.FindUsingTag("Player");

    //    effect = Camera.main.GetComponent<BnWImageEffect>();

    //    if (effect != null)
    //        effect.enabled = false;
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    //#region SPRITE
    //void LoadSprites()
    //{
    //    _piecesprites = Resources.LoadAll<Sprite>("Sprites/Generic/peças");

    //    spritesbonitas.Add(Resources.Load<Sprite>("Sprites/pecasbonitas/rei"));
    //    spritesbonitas.Add(Resources.Load<Sprite>("Sprites/pecasbonitas/rainha"));
    //    spritesbonitas.Add(Resources.Load<Sprite>("Sprites/pecasbonitas/torre"));
    //    spritesbonitas.Add(Resources.Load<Sprite>("Sprites/pecasbonitas/cavalo"));
    //    spritesbonitas.Add(Resources.Load<Sprite>("Sprites/pecasbonitas/bispa"));
    //    spritesbonitas.Add(Resources.Load<Sprite>("Sprites/pecasbonitas/peao"));
    //}

    //public Sprite Getspritebonita(int i)
    //{
    //    return spritesbonitas[i];
    //}

    //public Sprite GetPieceSpriteWithName(string name)
    //{
    //    foreach (Sprite s in _piecesprites)
    //    {
    //        if (s.name == name)
    //            return (Sprite)s;
    //    }
    //    return null;
    //}


    //#endregion
}
