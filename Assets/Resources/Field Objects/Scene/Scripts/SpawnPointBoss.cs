using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnPointBoss : MonoBehaviour
{
    public GameObject Boss;

    private GameObject _effect;

    public UnityEvent DieEvent;

    public void Spawn()
    {
        _effect = Resources.Load<GameObject>("Classes/King/TeleportIn");
        var obj = Instantiate(Boss, transform.position, Quaternion.identity);
        obj.GetComponent<Boss>().DieEvent = DieEvent;
    }

    // Use this for initialization
    void Start()
    {

    }
}