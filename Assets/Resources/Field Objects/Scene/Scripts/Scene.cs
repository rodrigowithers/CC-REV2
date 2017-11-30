using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scene : MonoBehaviour, IScene, IStopDash
{
    protected List<Collider2D> _colliders;

    public bool Entered = false;

    public UnityEvent SceneExitEvent;
    public UnityEvent SceneEnterEvent;

    public virtual void SceneEnter()
    {
        if (Entered)
            return;

        Entered = true;

        foreach (var col in _colliders)
        {
            col.enabled = true;
        }


        SceneManager.Instance.CurrentScene = this;

        // Seta o Canvas
        CanvasManager.Instance.CurrentPanel = CanvasManager.Instance.ScenarioPanel;

        // Muda o Target da camera
        Camera.main.GetComponent<CameraController>().Target = transform;

        // Restaura stamina e habilidade do Player
        Player.Instance.GetComponent<Player>().Stamina = 100;
        Player.Instance.GetComponent<HabilityManager>().Energy = 100;

        // Executa o evento de entrada
        SceneEnterEvent.Invoke();
    }

    public virtual void SceneExit()
    {
        // Remove os Colliders
        foreach (var col in _colliders)
        {
            col.enabled = false;
        }

        // Seta o Canvas
        CanvasManager.Instance.CurrentPanel = null;

        // Retorna o controle de câmera
        Camera.main.GetComponent<CameraController>().Target = Player.Instance.transform;

        // Seta como null a cena atual no SceneManager
        SceneManager.Instance.currentScene = null;

        // Executa o evento de saida da cena
        SceneExitEvent.Invoke();
    }

    IEnumerator InvokeEvent()
    {
        yield return new WaitForSeconds(0.5f);
        SceneExitEvent.Invoke();
    }

    public virtual bool SceneCompleted()
    {
        //if (Input.GetKey(KeyCode.K))
        //    return true;

        return false;
    }

    public virtual void SceneUpdate()
    {
        //// Checa se completou
        //if (SceneCompleted())
        //{
        //    SceneExit();
        //}
    }

    protected void Start()
    {
        _colliders = new List<Collider2D>(GetComponents<Collider2D>());
        foreach (var col in _colliders)
        {
            col.enabled = false;
        }
    }

    void Update()
    {

    }
}
