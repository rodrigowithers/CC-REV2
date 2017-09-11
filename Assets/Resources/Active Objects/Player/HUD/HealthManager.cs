using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public GameObject Health;
    public GameObject BlueHealth;

    private List<GameObject> InstancedHealth;

    private static HealthManager _instance;
    public static HealthManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<HealthManager>();
            }

            return _instance;
        }
    }

    public void TookDamage()
    {
        var life = Player.Instance.Life;
        for (int i = 0; i < InstancedHealth.Count; i++)
        {
            if (i >= life)
            {
                Destroy(InstancedHealth[i].gameObject);
                InstancedHealth.RemoveAt(i);

                continue;
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        InstancedHealth = new List<GameObject>();

        // Inicialização da vida
        for (int i = 0; i < Player.Instance.MaxLife; i++)
        {
            InstancedHealth.Add(Instantiate(Health, transform));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
