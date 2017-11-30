using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SceneBuilder))]
public class EnemyScene : Scene
{
    private SceneBuilder _sceneBuilder;
    private List<Enemy> _enemies = new List<Enemy>();

    private int _currentWave = 0;
    public int CurrentWave
    {
        get { return _currentWave; }
    }

    public bool SpawnWave()
    {
        _enemies = new List<Enemy>();
        _enemies = _sceneBuilder.Spawn(CurrentWave);

        if (_enemies.Count <= 0)
            return false;

        foreach (var e in _enemies)
        {
            EnemyManager.Instance.AddEnemy(e.gameObject);
        }

        //Debug.Log("Spawnando");

        _currentWave++;
        return true;
    }

    public override void SceneEnter()
    {
        base.SceneEnter();
        //SpawnWave();
    }

    public override bool SceneCompleted()
    {
        if (!SpawnWave())
        {
            SceneExit();
        }

        return false;
    }

    public override void SceneUpdate()
    {
        base.SceneUpdate();
    }

    public override void SceneExit()
    {
        //for (int i = 0; i < _enemies.Count; i++)
        //{
        //    if (_enemies[i] != null)
        //        _enemies[i].Die();
        //}

        _enemies = new List<Enemy>();

        base.SceneExit();
    }

    private void Awake()
    {
        _sceneBuilder = GetComponent<SceneBuilder>();
    }

    // Use this for initialization
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
