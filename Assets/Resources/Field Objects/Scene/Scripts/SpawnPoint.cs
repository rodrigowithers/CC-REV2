using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnPoint : MonoBehaviour
{
    public UnityEvent EnemyDieEvent;

    public SpawnInfo[] Enemies;

    private GameObject _enemy;

    private GameObject _effect;

    public bool UseEvent = false;

    public Enemy Spawn(int currentWave)
    {
        if (currentWave >= Enemies.Length)
            return null;

        if (Enemies[currentWave].name == "")
            return null;

        if (_enemy == null)
            return null;

        var enemy = Instantiate(_enemy, transform.position, Quaternion.identity);

        // Randomiza o inimigo entre Lesser = 0   /Normal = 1 /Greater = 2 
        //enemy.GetComponent<Enemy_Tower>()._Level = (LEVEL)0;//Random.Range(0,3);
        //PieceManager.Instance.DecideRandomClass(enemy.GetComponent<BattlePiece>());

        enemy.GetComponent<Enemy>().Smart = Enemies[currentWave].smart;

        if (UseEvent)
            enemy.GetComponent<Enemy>().DieEvent = EnemyDieEvent;

        string type = Enemies[currentWave].name;

        PieceManager.Instance.ChangeClass(enemy.GetComponent<BattlePiece>(), System.Type.GetType(type));

        Instantiate(_effect, transform.position, Quaternion.identity);



        return enemy.GetComponent<Enemy>();
    }

    public void FreeSpawn(string type)
    {
        var enemy = Instantiate(_enemy, transform.position, Quaternion.identity);


        //enemy.GetComponent<Enemy>().DieEvent = EnemyDieEvent;

        // Randomiza o inimigo entre Lesser = 0   /Normal = 1 /Greater = 2 
        enemy.GetComponent<Enemy_Tower>()._Level = (LEVEL)0;//Random.Range(0,3);
        PieceManager.Instance.ChangeClass(enemy.GetComponent<BattlePiece>(), System.Type.GetType(type));
        //PieceManager.Instance.DecideRandomClass(enemy.GetComponent<BattlePiece>());

        EnemyManager.Instance.AddEnemy(enemy);
    }

    // Use this for initialization
    void Start()
    {
        _effect = Resources.Load<GameObject>("Classes/King/TeleportIn");
        _enemy = Resources.Load<GameObject>("Active Objects/Enemies/Enemy");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
[System.Serializable]
public struct SpawnInfo
{
    public string name;
    public bool smart;
}
