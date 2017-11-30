using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextVisibilityAnimationCurve : APropertyAnimationCurve
{
    private Text _text;
    
    private void OnEnable()
    {
        _text = GetComponent<Text>();
    }

    private void Update()
    {
        // Pega a cor original para modificar
        Color original = _text.color;

        // Altera o valor de tempo
        _elapsedTime += Time.deltaTime * Speed;

        // Limita o tempo decorrido de acordo com o tamanho da AnimationCurve
        if (_elapsedTime > AnimationCurve.GetCurveLenght())
            _elapsedTime = 0;

        // Modifica valor alfa da cor original
        original.a = AnimationCurve.Evaluate(_elapsedTime);

        // Retorna a cor para o _text
        _text.color = original;
    }
}
