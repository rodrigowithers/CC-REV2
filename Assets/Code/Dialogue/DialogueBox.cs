﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    public Sprite Speaking;
    public int index = 0;
    /// <summary>
    /// Person goes from 0 - 6
    ///  0 = Player
    ///  1 = King
    ///  2 = Queen
    ///  3 = Tower
    ///  4 = Horse
    ///  5 = Bishop
    ///  6 = Pawn
    ///  To diferentiate between normal pieces and enemies put an "-" before the number
    /// </summary>
    public int person = 0;
    public string[] Text;
    public float Speed = 0.05f;
    public float VocalTone = 1;
    public float VocalRange = 0.01f;

    //public UnityEvent EndOfDialogueEvent;
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

    public List<UnityEvent> Nodes;
    public string Filename;

    public DialogueTrigger CurrentTrigger;

    public bool skipThis = false;

    private static DialogueBox _instance;
    public static DialogueBox Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DialogueBox>();
            }
            return _instance;
        }
    }

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

        if (Input.GetButtonUp("Cancel") && PressNext.gameObject.activeSelf == false)
        {
            skipThis = true;
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

            Player.Instance.StopMoving();
            Player.Instance.enabled = false;
            Player.Instance.GetComponent<PlayerController>().Enable = false;
            Player.Instance.GetComponent<Player>().CanMove = false;
            if (SoundManager.Instance != null)
                SoundManager.Instance.Mixer.FindSnapshot("Menus").TransitionTo(0.0f);
        }
        else
        {
            Time.timeScale = 1;
            Player.Instance.enabled = true;
            Player.Instance.GetComponent<PlayerController>().Enable = true;
            Player.Instance.GetComponent<Player>().CanMove = true;

            if(SoundManager.Instance != null)
                SoundManager.Instance.Mixer.FindSnapshot("Main").TransitionTo(0.0f);

            if (CurrentTrigger != null)
                CurrentTrigger.Exit();
        }
    }

    public void StartDoingTheThing()
    {
        StartCoroutine(CExecuteNodes());
    }

    public void GetDialogue(int index)
    {
        if (Filename == "")
            Filename = "0" + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        DialogueBox.Instance.CurrentDialogue = JsonHelper.Instance.Get(Filename, index);
    }

    public Sprite Adjust_Speaker_Img(int index)
    {
        int abs = Mathf.Abs(index);
        Sprite toreturn = GameManager.Instance._Portraits[0];

        switch (abs)
        {
            case 0:
                toreturn = null;
                break;
            case 1: // KING
                if (index > 0)
                    abs += 6;
                toreturn = GameManager.Instance._Portraits[abs - 1];
                break;
            case 2:// QUEEN
                if (index > 0)
                    abs += 6;

                toreturn = GameManager.Instance._Portraits[abs - 1];
                break;
            case 3:// TOWER
                if (index > 0)
                    abs += 6;

                toreturn = GameManager.Instance._Portraits[abs - 1];
                break;
            case 4:// HORSE
                if (index > 0)
                    abs += 6;

                toreturn = GameManager.Instance._Portraits[abs - 1];
                break;
            case 5:// BISHOP
                if (index > 0)
                    abs += 6;

                toreturn = GameManager.Instance._Portraits[abs - 1];
                break;
            case 6:// PAWN
                if (index > 0)
                    abs += 6;

                toreturn = GameManager.Instance._Portraits[abs - 1];
                break;
        }
        return toreturn;
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

    private float _waitTime = 0.0f;

    public void Wait(float seconds)
    {
        _waitTime = seconds;
    }

    private IEnumerator CWait()
    {
        Debug.Log("Waiting...");
        yield return new WaitForSecondsRealtime(_waitTime);
        _waitTime = 0.0f;
        yield return null;
    }

    private bool _executing = false;
    public IEnumerator CExecuteNodes()
    {
        // Checa se já está printando algo
        if (_executing)
            yield break;

        _executing = true;

        // Desabilita o Player
        Player.Instance.enabled = false;
        Player.Instance.GetComponent<CharacterSwitch>().enabled = false;

        // Cria um ponteiro de função para esperar a telca espaço
        System.Func<bool> PressedNext = new System.Func<bool>(Next);

        int currentNode = 0;
        while (currentNode < Nodes.Count)
        {
            // Checa se não é um dialogo
            if (Nodes[currentNode].GetPersistentMethodName(0) != "GetDialogue")
            {
                if (Nodes[currentNode].GetPersistentMethodName(0) == "Wait")
                {
                    Nodes[currentNode].Invoke();
                    yield return StartCoroutine(CWait());
                }
                else
                {
                    Nodes[currentNode].Invoke();
                }
            }
            else if(Nodes[currentNode].GetPersistentMethodName(0) == "GetDialogue" && (!Level.Instance.Completed && !skipThis))
            {
                // Se é um dialogo, pega o dialogo atual
                Nodes[currentNode].Invoke();

                // Reseta o texto
                Text.text = "";

                // Troca a Imagem        
                if (Speaking == null)
                    Speaking.enabled = false;
                else
                    Speaking.sprite = Adjust_Speaker_Img(CurrentDialogue.person);

                int currentText = 0;

                while (currentText < CurrentDialogue.Text.Length)
                {
                    if (skipThis)
                    {
                        Text.text = "";
                        currentText++;
                        continue;
                    }

                    _speed = CurrentDialogue.Speed;

                    Text.text = "";
                    string textString = CurrentDialogue.Text[currentText];


                    for (int i = 0; i < textString.Length; i++)
                    {
                        if (skipThis)
                        {
                            Text.text = "";
                            continue;
                        }

                        // Checa se não é um espaço
                        if (textString[i] == ' ')
                        {
                            Text.text += textString[i];
                            continue;
                        }
                        if (textString[i] == '#')
                        {
                            yield return new WaitForSecondsRealtime(0.1f);
                            //i++;

                            continue;
                        }
                        if (textString[i] == '$')
                        {
                            _speed /= 2;


                            continue;
                        }

                        if (textString[i] == '%')
                        {
                            _speed *= 2;


                            continue;
                        }
                        if (textString[i] == '+')
                        {
                            Camera.main.GetComponent<Flash>().StartFlash(true);
                            SoundManager.Play("flash", 0.5f);
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
            }

            // Troca o Node
            currentNode++;
            yield return null;
        }
        
        // Limpa
        Text.text = "";

        // Retorna o Player
        Player.Instance.enabled = true;
        Player.Instance.GetComponent<CharacterSwitch>().enabled = true;

        Set(false);
        _executing = false;

        skipThis = false;

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
                    if (textString[i] == '#')
                    {
                        //yield return new WaitForSecondsRealtime(0.5f);
                        i++;

                        continue;
                    }

                    // Beeps.
                    float pitch = CurrentDialogue.VocalTone + Random.Range(-0.25f, 0.25f);
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
            //CurrentDialogue.EndOfDialogueEvent.Invoke();

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
