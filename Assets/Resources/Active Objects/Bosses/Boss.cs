using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


[RequireComponent(typeof(HabilityManager))]
public class Boss : Piece, IKillable
{
    public HabilityManager _habilityManager;
    public StateMachine _stateMachine;
    public GameObject _atkArea;
    public GameObject _lifebar_prefab;

    public GameObject _lifebar;
    Image _barimage;
    public int _life = 20;
    protected int _maxlife = 20;

    public UnityEvent DieEvent;

    // Use this for initialization
    public void Start()
    {
        _stateMachine = GetComponent<StateMachine>();
        _habilityManager = GetComponent<HabilityManager>();
        _stateMachine.StartManchine(this);

        _lifebar_prefab = Resources.Load<GameObject>("Active Objects/Bosses/HealthBoss");
        GameObject sceneUI = GameObject.FindGameObjectWithTag("ScenarioUI");

        _lifebar = GameObject.Instantiate<GameObject>(_lifebar_prefab);

        if (_life != null)
        {
            _lifebar.transform.SetParent(sceneUI.transform);
            _lifebar.transform.localScale = Vector2.one;
            _lifebar.transform.localPosition = new Vector2(500, 470);
            _lifebar.transform.parent = sceneUI.transform;
            //_lifebar.SetActive(false);
            _barimage = _lifebar.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        }


    }
    public void Update()
    {
        if (_lifebar == null || _barimage == null)
            return;
        if (_lifebar.active)
        {
            float cur = _life;
            float max = _maxlife;
            _barimage.fillAmount = cur / max;
        }

    }

    public IEnumerator HitStun(float time = 0.5F)
    {
        throw new NotImplementedException();
    }

    public void Die()
    {
        if (DieEvent != null)
            DieEvent.Invoke();
        SceneManager.Instance.CurrentScene.SceneExit();

        Destroy(_stateMachine);

        //Destroy(_atkArea);
        Destroy(_lifebar);

        GetComponent<ClassAnimator>().Play("Die", 0, true, true);

        Destroy(this);

        Destroy(GetComponent<ClassAnimator>(), 3);
        Destroy(GetComponent<Animator>(), 3.5f);

        //Destroy(t, 3);

        //var sprite = GetComponent<SpriteRenderer>().sprite;

        //GameObject go = new GameObject("Boss Sprite");
        //go.AddComponent<SpriteRenderer>();

        //go.GetComponent<SpriteRenderer>().sprite = sprite;
        //go.transform.position = transform.position;

        //Destroy(this.gameObject);
    }

    public void TakeDamage(Vector2 direction, float force = 10, int dmg = 1)
    {
        if (IsInvincible)
            return;

        _life -= dmg;
        StartCoroutine(CDamageFlash());

        SoundManager.Play("hit");

        if (_life <= 0)
            Die();

        //HitStun();

    }



}
