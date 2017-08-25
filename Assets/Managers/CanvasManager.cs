using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private static CanvasManager _instance;
    public static CanvasManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CanvasManager>();
            }

            return _instance;
        }
    }

    public bool Menu
    {
        set
        {
            if (value == true)
            {
                // Mostra o Plane de menu
            }
            else
            {
                // Mostra o Plane normal
            }
        }
    }

    private Player _player;

    [Header("Stamina")]
    public Image StaminaBar;
    public Color HabilityReady;
    public Color HabilityNotReady;

    [Header("Attack")]
    public Image AttackBar;
    public Color AttackReady;
    public Color AttackNotReady;

    public GameObject ScenarioPanel;
    public GameObject MenuPanel;
    public GameObject ChangePanel;

    private TextSpawner _textSpawner;

    public GameObject CurrentPanel
    {
        set
        {
            ScenarioPanel.SetActive(false);
            MenuPanel.SetActive(false);
            ChangePanel.SetActive(false);

            if (value != null)
                value.SetActive(true);
        }
    }

    private void UpdateStaminaBar()
    {
        if (StaminaBar == null)
            return;

        // Pega a Stamina atual do player
        var stamina = _player.GetComponent<HabilityManager>().Stamina;

        StaminaBar.fillAmount = stamina / 100;

        // Vê se a stamina atual é suficiente para executar a habilidade atual
        if (stamina >= _player.GetComponent<HabilityManager>().Hability.Cost)
        {
            // Seta a cor para Amarelo
            StaminaBar.color = HabilityReady;
        }
        else
        {
            // Seta a cor para Cinza
            StaminaBar.color = HabilityNotReady;
        }
    }

    private void UpdateAttack()
    {
        if (AttackBar == null)
            return;

        var attack = _player.Stamina;
        AttackBar.fillAmount = attack / 100;

        if (attack >= _player.AttackCost)
        {
            AttackBar.color = AttackReady;
        }
        else
        {
            AttackBar.color = AttackNotReady;
        }
    }

    private void UpdateScore()
    {

    }

    public void SpawnComboText(int combo, Vector2 pos)
    {
        string text = combo.ToString();

        _textSpawner.Spawn(pos, Vector2.up, 10, text);
    }

    private void Awake()
    {
        _textSpawner = GetComponent<TextSpawner>();
    }

    private void Start()
    {
        //_player = GameManager.Instance.Player.GetComponent<Player>();
        _player = Player.Instance;

        CurrentPanel = null;
    }

    private void Update()
    {
        // Checa se o Player está em alguma Scene
        //if (SceneManager.Instance.currentScene == null)
        //{
        //    transform.GetChildOfType<CanvasScenarioPanel>().gameObject.SetActive(false);
        //}
        //else
        //{
        //    transform.GetChildOfType<CanvasScenarioPanel>().gameObject.SetActive(true);
        //}

        UpdateStaminaBar();
        UpdateAttack();
        UpdateScore();
    }
}
