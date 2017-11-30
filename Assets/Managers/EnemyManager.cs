using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public List<GameObject> EnemyList = new List<GameObject>();

    public List<GameObject> AttackingList = new List<GameObject>();

    public List<Vector2> AttackSide = new List<Vector2>();


    private int _maxEnemiesAtOnce = 3;

    public int Atacking_Count
    {
        get
        {
            return AttackingList.Count;
        }
    }
    
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
    bool last_instantiated = false;

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

                    //DontDestroyOnLoad(go);

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


    public int EnemyCount
    {
        get
        {
            return EnemyList.Count;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Atacking_Count < _maxEnemiesAtOnce)
        {
            if (!last_instantiated)
            {
                if (Random.Range(0, 3) == 0 && !CheckForEnemies(false, 1))
                    CheckForEnemies(true, 1);
            }
            else
            {
                if (Random.Range(0, 3) == 0 && !CheckForEnemies(true, 1))
                    CheckForEnemies(false, 1);
            }
        }
        if (AttackSide.Count > 1)
        {
            for (int i = AttackSide.Count -1; i >= 0; i--)
            {
                AttackSide.Remove(AttackSide[i]);
            }
        }
    }

    public void AddEnemy(GameObject e)
    {       
        EnemyList.Add(e);
        Debug.Log(EnemyCount);
    }

    public void AddEnemies(List<GameObject> es)
    {        
        EnemyList.AddRange(es);
    }

    bool CheckForEnemies(bool _smart, int qtd = 0)
    {
        int enemieswanted = qtd;
        if (qtd == 0 )
            enemieswanted = MaxEnemiesAtOnce - Atacking_Count ;

        foreach(GameObject e in EnemyList)
        {
            if (e == null)
                EnemyList.Remove(e);

            if (!e.GetComponent<Enemy>().IsAttacking)
            {
                if (e.GetComponent<Enemy>().Smart == _smart && enemieswanted != 0)
                {
                    StartToAttack(e);
                    enemieswanted--;
                }
            }
            if(enemieswanted == 0)
                return true;
        }
        return false;
    }

    public void RemoveEnemy(GameObject e)
    {
        StopAttack(e);
        EnemyList.Remove(e);
        AttackingList.Remove(e);

        Combo++;

        var playerPos = Player.Instance.transform.position;

        // Verifica se existe mais algum inimigo na lista
        if (!HasEnemies)
        {
            if (SceneManager.Instance.CurrentScene == null)
                return;

            SceneManager.Instance.CurrentScene.SceneCompleted();
        }
    }

    public void RemoveAllEnemies()
    {
        EnemyList.RemoveRange(0, EnemyList.Count);
    }

    public bool HasEnemies
    {
        get
        {
            if (EnemyCount != 0 || Atacking_Count != 0)
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
    
    void StartToAttack(GameObject e)
    {
        EnemyList.Remove(e);   
        e.GetComponent<Enemy>().IsAttacking = true;
        e.GetComponent<Enemy>()._StateMachine.ChangeState( new FollowPathState());
        AttackingList.Add(e);
    }
    void StopAttack(GameObject e)
    {
        AttackingList.Remove(e);

        if(e.GetComponent<Enemy>() != null)
        {
            e.GetComponent<Enemy>().IsAttacking = false;
            e.GetComponent<Enemy>()._StateMachine.ChangeState(new WanderAroundPathState());
            EnemyList.Add(e);
        }
    }


    public GameObject ClosestEnemy(Vector3 pos)
    {
        if (EnemyList.Count == 0)
            return null;

        GameObject closest = null;
        float closestdist = 1000;
        float dist = 0;
        foreach (GameObject g in EnemyList)
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


    public GameObject ClosestEnemyOtherThan(Vector3 pos,GameObject caller)
    {
        if (EnemyList.Count <= 1)
            return null;

        GameObject closest = null;
        float closestdist = 1000;
        float dist = 0;
        foreach (GameObject g in EnemyList)
        {
            if (g != caller)
            {
                dist = g.GetComponent<Enemy>().EnemyDistPlayer();
                if (closestdist > dist)
                {
                    closestdist = dist;
                    closest = g;
                }
            }
        }

        return closest;
    }

    public bool HasMoreThan(int n)
    {
        if (EnemyList.Count > n)
            return true;
        return false;
    }

    public void ConfuseAllEnemies()
    {
        foreach (GameObject g in AttackingList)
        {
            g.GetComponent<Enemy>()._StateMachine.ChangeState(new ConfusedState());
        }
    }


    GameObject FarthestEnemyAttacking(Vector2 pos)
    {
        GameObject toreturn = null;
        float biggestdist = 0;
        float newdist = 0;
        foreach(GameObject e in AttackingList)
        {
            newdist = ((Vector2)e.transform.position - pos).magnitude;
            if(newdist > biggestdist)
            {
                biggestdist = newdist;
                toreturn = e;
            }
        }
        return toreturn;
    }


    public void GotTooCloseFromPlayer(GameObject e)
    {
        Debug.Log("TOO CLOSE");
        GameObject farthest = FarthestEnemyAttacking(e.transform.position);
        StartToAttack(e);
        StopAttack(farthest);
    }

    

    bool HasAnotherOfType(int index)
    {
        for (int i = 0; i < AttackingList.Count; i++)
        {
            if (i != index)
            {
                if (AttackingList[i].GetComponent<Enemy>().GetClass.Type == AttackingList[index].GetComponent<Enemy>().GetClass.Type)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void AtkAreaUpdate(GameObject g)
    {
        if (AttackingList.Count <= 1)
            return;
        int list_index = AttackingList.IndexOf(g);

        if (!HasAnotherOfType(list_index))
            return;

        if(list_index == 0)
        {
            AttackSide.Add(-g.GetComponent<Enemy>().AtkAreaToEnemy());
            g.GetComponent<Enemy>().transform.GetChild(0).GetComponent<AttackPattern>().CanUpdate(true);
        }
        else
        {
            g.GetComponent<Enemy>().transform.GetChild(0).GetComponent<AttackPattern>().CanUpdate(false);
            Change(g);
        }
    }

    
    void Change(GameObject g)
    {
        List<Vector2> atklist = new List<Vector2>();


        if (g.GetComponent<Enemy>().GetClass.Type == CHESSPIECE.PAWN)
        {
            atklist.Add(new Vector2(1.2f, 1.2f));
            atklist.Add(new Vector2(-1.2f, 1.2f));
            atklist.Add(new Vector2(1.2f, -1.2f));
            atklist.Add(new Vector2(-1.2f, -1.2f));
        }
        else if (g.GetComponent<Enemy>().GetClass.Type == CHESSPIECE.TOWER)
        {
            atklist.Add(new Vector2(  0  , 5.2f));
            atklist.Add(new Vector2( 5.2f,  0  ));
            atklist.Add(new Vector2(-5.2f,  0  ));
            atklist.Add(new Vector2(  0  ,-5.2f));

        }
        bool isequal = false;
        for(int i =0;i<AttackSide.Count;i++)
        {
            if(AttackSide[i] == g.GetComponent<Enemy>().AtkAreaToEnemy())
            {
                isequal = true;
            }
        }

        float dist = Mathf.Infinity;
        int index = -1;
        List<Vector2> temp = new List<Vector2>();
        if(isequal)
        {
            for(int i = 0;i< atklist.Count;i++)
            {
                if(!AttackSide.Contains(atklist[i]))
                {
                    temp.Add(atklist[i]);
                }
            }   

            for(int i =0;i<temp.Count;i++)
            {
                Vector2 pos = (Vector2)Player.Instance.transform.position + temp[i];

                float novadist = (pos - (Vector2)g.GetComponent<Enemy>().transform.position).magnitude;

                if (dist > novadist)
                {
                    dist = novadist;
                    index = i;
                }
            }
            g.GetComponent<Enemy>().CurrentAtkArea = g.GetComponent<Enemy>().transform.GetChild(0).GetChild(index);
        }
    }


}
