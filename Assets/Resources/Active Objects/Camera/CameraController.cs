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

    public void Shake()
    {
        if (_shaking)
            return;

        StartCoroutine(CShake());
    }

    private IEnumerator CShake()
    {
        _shaking = true;
        var time = 0.0f;

        var originalPos = transform.position;
        while (time < 0.1f)
        {
            transform.position += Random.insideUnitCircle.xyz(transform.position) / 5;
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
        if (_changingTarget)
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