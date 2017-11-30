using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    //public Image LoadBar;
    //public Gradient Gradient;

    //public bool Change = true;
    //public float MaxTime = 100.0f;            // Tempo maximo da barra

    //private AsyncOperation _operation;
    //private bool _done = false;               // Done da barra
    //float time = 0;


    //public void Load(string sceneName)
    //{
    //    StartCoroutine(LoadAsync(sceneName));
    //}

    //private void Start()
    //{
    //    StartCoroutine(LoadAsync("tests"));
    //}

    //public void ChangeScene()
    //{
    //    Change = true;
    //}

    public void LoadLevel(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }

    //private void Update()
    //{
    //    //if (Input.anyKeyDown)
    //    //if (time >= MaxTime)
    //    //    Change = true;
    //}

    //private IEnumerator LoadAsync(string sceneName)
    //{
    //    LoadBar.fillAmount = 0;

    //    _operation.allowSceneActivation = false;

    //    while (!_done)
    //    {
    //        time += Time.deltaTime / MaxTime;

    //        var curValue = Mathf.Clamp01(_operation.progress / 0.9f);
    //        LoadBar.fillAmount = Mathf.Lerp(0, curValue, time);
    //        LoadBar.color = Gradient.Evaluate(time);

    //        _done = (LoadBar.fillAmount >= 1);

    //        yield return null;
    //    }

    //    while (!_operation.isDone)
    //    {
    //        _operation.allowSceneActivation = Change;

    //        yield return null;
    //    }

    //    yield return null;
    //}
}
