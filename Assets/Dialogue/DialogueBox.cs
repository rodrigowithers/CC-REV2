﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    public Sprite Speaking;
    public string[] Text;
    public float Speed = 0.25f;
    public float VocalTone = 1;
    public float VocalRange = 0.01f;

    public UnityEvent EndOfDialogueEvent;
}

public class DialogueBox : MonoBehaviour
{
    public AudioSource Beep;
    public Image PressNext;

    public Image Speaking;
    public Text Text;

    private Image _background;

    public Dialogue[] Dialogues;
    public Dialogue CurrentDialogue;

    public DialogueTrigger CurrentTrigger;

    private float _speed;

    private void Awake()
    {
        _background = GetComponent<Image>();
    }

    private void Start()
    {
        Set(false);

        StartCoroutine(Blink());
    }

    private void Update()
    {
        if (Input.GetButtonUp("Hability") && PressNext.gameObject.activeSelf == false)
        {
            _speed = 0.01f;
        }
    }

    private bool Next()
    {
        PressNext.enabled = true;

        bool hasPressed = Input.GetButtonUp("Hability");

        if (hasPressed)
        {
            PressNext.enabled = false;
            return true;
        }
        else
        {
            return false;
        }

    }

    public void Set(bool state)
    {
        Text.text = "";
        Speaking.gameObject.SetActive(state);
        PressNext.gameObject.SetActive(false);

        _background.enabled = state;

        if (state)
        {
            Time.timeScale = 0;
            Player.Instance.enabled = false;
        }
        else
        {
            Time.timeScale = 1;
            Player.Instance.enabled = true;

            if (CurrentTrigger != null)
                CurrentTrigger.Exit();
        }
    }

    public IEnumerator Blink()
    {
        var time = 0.0f;
        while (true)
        {
            if (time >= 0.5f)
            {
                var col = PressNext.color;
                col.a = Mathf.Abs(col.a - 1);

                PressNext.color = col;

                time = 0;
            }

            time += Time.unscaledDeltaTime;
            yield return null;
        }

        yield return null;
    }

    public IEnumerator CDisplayText()
    {
        // Desabilita o Player
        Player.Instance.enabled = false;
        Player.Instance.GetComponent<CharacterSwitch>().enabled = false;

        // Cria um ponteiro de função para esperar a telca espaço
        System.Func<bool> PressedNext = new System.Func<bool>(Next);

        int currentDialogue = 0;
        while (currentDialogue < Dialogues.Length)
        {
            CurrentDialogue = Dialogues[currentDialogue];

            // Reseta o texto
            Text.text = "";

            // Troca a Imagem        
            if (Speaking == null)
                Speaking.enabled = false;
            else
                Speaking.sprite = CurrentDialogue.Speaking;

            int currentText = 0;

            while (currentText < CurrentDialogue.Text.Length)
            {

                _speed = CurrentDialogue.Speed;

                Text.text = "";
                string textString = CurrentDialogue.Text[currentText];


                for (int i = 0; i < textString.Length; i++)
                {
                    // Checa se não é um espaço
                    if (textString[i] == ' ')
                    {
                        Text.text += textString[i];
                        continue;
                    }
                    if (textString[i] == '[')
                    {
                        switch (textString[i + 1])
                        {
                            case 'w':
                                yield return new WaitForSecondsRealtime(0.5f);
                                i += 2;
                                break;
                        }
                        continue;
                    }

                    // Beeps.
                    float pitch = CurrentDialogue.VocalTone + Random.Range(-CurrentDialogue.VocalRange, CurrentDialogue.VocalRange);
                    Beep.pitch = pitch;
                    Beep.Play();

                    Text.text += textString[i];
                    yield return new WaitForSecondsRealtime(_speed);
                }

                PressNext.gameObject.SetActive(true);

                yield return new WaitUntil(PressedNext);

                PressNext.gameObject.SetActive(false);
                currentText++;
            }

            // Executa o evento de final de fala
            CurrentDialogue.EndOfDialogueEvent.Invoke();

            // Troca o Dialogue
            currentDialogue++;
        }

        // Limpa
        Text.text = "";

        // Retorna o Player
        Player.Instance.enabled = true;
        Player.Instance.GetComponent<CharacterSwitch>().enabled = true;

        Set(false);

        yield return null;
    }
}