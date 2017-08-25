using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    private List<GameObject> _enemyList = new List<GameObject>();

    private List<GameObject> _enemiesAttacking = new List<GameObject>();

    private int _maxEnemiesAtOnce = 3;


    private int _combo = 0;
    public int Combo
    {
        get
        {
            return _combo;
        }

        set
        {
            _combo = value;
        }
    }

    private static EnemyManager _instance;
    public static EnemyManager Instance
    {
        get
        {
            if (_instance == null)
            {
                if (FindObjectOfType<EnemyManager>() == null)
                {
                    GameObject go = new GameObject("Enemy Manager");
                    go.AddComponent<EnemyManager>();

                    DontDestroyOnLoad(go);

                    _instance = go.GetComponent<EnemyManager>();
                }
                else
                {
                    _instance = FindObjectOfType<EnemyManager>();
                }
            }

            return _instance;
        }
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (_enemiesAttacking.Count < _maxEnemiesAtOnce)
        {

        }


    }

    public void AddEnemy(GameObject e)
    {
        //e.GetComponent<Enemy>().Direction = new Vector2(Random.Range(-6, 6), Random.Range(-4, 4));
        //e.GetComponent<Enemy>().StateMachine.ChangeState(new GoToState());
        _enemyList.Add(e);
    }

    public void AddEnemies(List<GameObject> es)
    {
        _enemyList.AddRange(es);
    }

    public void RemoveEnemy(GameObject e)
    {
        StopAttack(e);
        _enemyList.Remove(e);

        Combo++;

        var playerPos = Player.Instance.transform.position;

        CanvasManager.Instance.SpawnComboText(Combo, playerPos);

        // Verifica se existe mais algum inimigo na lista
        if (_enemyList.Count < 1)
        {
            if (SceneManager.Instance.CurrentScene == null)
                return;

            SceneManager.Instance.CurrentScene.SceneCompleted();
        }
    }

    public void RemoveAllEnemies()
    {
        RemoveAttackingEnemies();
        _enemyList.RemoveRange(0, _enemyList.Count);
    }
    public void RemoveAttackingEnemies()
    {
        _enemiesAttacking.RemoveRange(0, _enemiesAttacking.Count);
    }

    //public void MakeEveryoneAware()
    //{
    //    foreach (GameObject g in _enemyList)
    //    {
    //        Enemy e = g.GetComponent<Enemy>();
    //        if(!e.IsAware)
    //        {
    //            WaveManager.Instance.GetSceneScript().SeenPlayer(main_script.gameObject)
    //            //e.StateMachine.ChangeState(new AwareState());
    //        }
    //    }
    //}
    public bool HasEnemies
    {
        get
        {
            if (_enemyList.Count != 0)
            {
                return true;
            }
            return false;
        }
    }

    public void ChangeEnemyState(GameObject g, string s)
    {

    }


    public int MaxEnemiesAtOnce
    {
        get { return _maxEnemiesAtOnce; }
        set { _maxEnemiesAtOnce = value; }
    }


    //public int CurrentEnemiesAtOnce
    //{
    //    get { return _currentattacking; }
    //    set { _currentattacking = value; }
    //}


    void StartToAttack(GameObject e)
    {
        // _currentattacking++;
        //e.GetComponent<Enemy>().StateMachine.ChangeState(new newatkstate());
        _enemiesAttacking.Add(e);


    }
    void StopAttack(GameObject e)
    {
        if (_enemiesAttacking.Contains(e))
        {
            // _currentattacking--;
            _enemiesAttacking.Remove(e);
            //e.GetComponent<Enemy>().Direction = new Vector2(Random.Range(-6, 6), Random.Range(-4, 4));
            //e.GetComponent<Enemy>().StateMachine.ChangeState(new GoToState());
        }
    }


    public GameObject ClosestEnemy(Vector3 pos)
    {
        if (_enemyList.Count == 0)
            return null;

        GameObject closest = null;
        float closestdist = 1000;
        float dist = 0;
        foreach (GameObject g in _enemyList)
        {
            dist = g.GetComponent<Enemy>().EnemyDistPlayer();
            if (closestdist > dist)
            {
                closestdist = dist;
                closest = g;
            }
        }

        return closest;
    }
    public void ConfuseAllEnemies()
    {
        foreach (GameObject g in _enemyList)
        {
            g.GetComponent<Enemy>()._StateMachine.ChangeState(new ConfusedState());
        }
    }

}
