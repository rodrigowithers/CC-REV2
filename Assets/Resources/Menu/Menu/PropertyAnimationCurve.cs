using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe para animar qualquer propriedade de um script usando uma curva de animação
/// </summary>
public abstract class APropertyAnimationCurve : MonoBehaviour
{
    protected float _toAnimate; // Valor a ser modificado pela curva
    protected float _elapsedTime = 0; // Tempo que passou desde o começo da emulação

    public AnimationCurve AnimationCurve; // Curva usada para animar a propriedade selecionada
    [Range(0.1f, 10)] public float Speed = 1; // Velocidade sobre a qual a curva sera analisada
}
