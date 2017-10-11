using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform InicialTarget;

    private Transform _target;
    public float Speed = 1;

    public bool LockY = false;
    public bool LockX = true;

    private bool _changingTarget = false;
    private bool _shaking = false;

    private bool _zooming = false;

    private bool GridCreated = false;

    void Start()
    {
        if (InicialTarget != null)
            Target = InicialTarget;
    }

    void Update()
    {

    }

    public Transform Target
    {
        get
        {
            return _target;
        }
        set
        {
            StartCoroutine(CChangeTarget(value));
        }
    }

    public void Shake(float multiplier = 0.2f)
    {
        if (_shaking)
            return;

        StartCoroutine(CShake(multiplier));
    }

    public void Zoom(Vector2 position)
    {
        if (_zooming)
            return;

        StartCoroutine(CZoom(position));
    }

    private IEnumerator CZoom(Vector2 position)
    {
        _zooming = true;

        Time.timeScale = 0.1f;
        var time = 0.0f;

        //Camera c = Camera.main;

        yield return new WaitForSecondsRealtime(1.0f);
            
        //var oldPos = transform.position;
        //var oldSize = c.orthographicSize;
        //var newSize = 9.0f;

        //var speed = 0.5f;

        // Indo

        //while(time < 1.0f)
        //{
        //    // Lerp da posição
        //    //transform.position = Vector3.Lerp(transform.position, position.xyz(transform.position), time);

        //    // Lerp do tamanho
        //    //c.orthographicSize = Mathf.Lerp(c.orthographicSize, newSize, time);

        //    time += Time.unscaledDeltaTime * speed;

        //    yield return null;
        //}

        //yield return new WaitForSecondsRealtime(0.5f);

        // Voltando

        //time = 0.0f;
        //while (time < 1.0f)
        //{
        //    // Lerp da posição
        //    //transform.position = Vector3.Lerp(transform.position, oldPos, time);

        //    // Lerp do tamanho
        //    //c.orthographicSize = Mathf.Lerp(c.orthographicSize, oldSize, time);

        //    time += Time.unscaledDeltaTime * speed * 2;

        //    yield return null;
        //}

        //c.orthographicSize = oldSize;

        _zooming = false;

        Time.timeScale = 1.0f;
        yield return null;
    }

    private IEnumerator CShake(float multiplier)
    {
        _shaking = true;
        var time = 0.0f;

        var originalPos = transform.position;
        while (time < 0.1f)
        {
            transform.position += Random.insideUnitCircle.xyz(transform.position) * multiplier;
            transform.position = Vector3.Lerp(transform.position, originalPos, 0.5f);

            time += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
        _shaking = false;
        yield return null;
    }

    private IEnumerator CChangeTarget(Transform newTarget)
    {
        _changingTarget = true;
        float time = 0.0f;

        while (time < 1.0f)
        {
            var newPos = Vector2.Lerp(transform.position.xy(), newTarget.position.xy(), time);

            if (LockX)
                newPos.x = transform.position.x;

            if (LockY)
                newPos.y = transform.position.y;

            transform.position = newPos.xyz(transform.position);

            time += 0.01f * Speed;
            yield return null;
        }

        _target = newTarget;

        if (_target.GetComponent<IScene>() != null)
        {
            Debug.Log("Criando Grid");
            GetComponent<Grid>().CreateNewGrid();
        }

        if (_target.GetComponent<EnemyScene>() != null)
        {
            //Debug.Log("Spawnando");
            _target.GetComponent<EnemyScene>().SpawnWave();
        }


        _changingTarget = false;
        yield return null;
    }

    private void FixedUpdate()
    {
        if (_changingTarget || _zooming)
            return;

        if (_target == null)
            return;

        // Faz um Lerp até o Target, no eixo Y
        var newPos = Vector2.Lerp(transform.position.xy(), _target.position.xy(), Time.deltaTime * Speed);

        if (LockX)
            newPos.x = transform.position.x;

        if (LockY)
            newPos.y = transform.position.y;

        transform.position = newPos.xyz(transform.position);
    }
}



////Cria uma nova Grid para o PathFinding assim que o jogador 
////entra em alguma cena

//GetComponent<Grid>().CreateNewGrid();