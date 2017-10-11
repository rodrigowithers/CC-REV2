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

    [Header("Health")]
    public Image HealthBar;

    [Header("Energy")]
    public Image EnergyBar;
    public Color HabilityReady;
    public Color HabilityNotReady;

    [Header("Stamina")]
    public Image StaminaBar;
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

    private void UpdateEnergyBar()
    {
        if (EnergyBar == null)
            return;

        // Pega a Stamina atual do player
        var energy = _player.GetComponent<HabilityManager>().Energy;

        EnergyBar.fillAmount = energy / 100;

        // Vê se a stamina atual é suficiente para executar a habilidade atual
        if (energy >= _player.GetComponent<HabilityManager>().Hability.Cost)
        {
            // Seta a cor para Amarelo
            EnergyBar.color = HabilityReady;
        }
        else
        {
            // Seta a cor para Cinza
            EnergyBar.color = HabilityNotReady;
        }
    }

    private void UpdateStaminaBar()
    {
        if (StaminaBar == null)
            return;

        var attack = _player.Stamina;
        StaminaBar.fillAmount = attack / 100;

        if (attack >= _player.AttackCost)
        {
            StaminaBar.color = AttackReady;
        }
        else
        {
            StaminaBar.color = AttackNotReady;
        }
    }

    private void UpdateHealthBar()
    {
        if (HealthBar == null)
            return;

        float max = _player.MaxLife;
        float cur = _player.Life;

        HealthBar.fillAmount = cur / max;
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

        UpdateEnergyBar();
        UpdateStaminaBar();
        UpdateHealthBar();

        UpdateScore();
    }
}
