using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Blackout : MonoBehaviour
{
    public Image Image;
    //public Material Material;
    public float Speed = 1.0f;

    public UnityEvent Event;

    //private void OnRenderImage(RenderTexture source, RenderTexture destination)
    //{
    //    Graphics.Blit(source, destination, Material);
    //}

    private void OnDestroy()
    {
        StopAllCoroutines();
        Image.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        //Material.SetFloat("_Black", 0);
    }

    public void SetBlack()
    {
        Image.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
    }

    public void White()
    {
        StartCoroutine(CWhite());
    }

    private IEnumerator CWhite()
    {
        for (float i = 1; i > 0; i -= Time.deltaTime * Speed)
        {
            Image.color = new Color(0.0f, 0.0f, 0.0f, i);
            yield return new WaitForSecondsRealtime(0.016f);
        }

        yield return null;
    }

    public void Black()
    {
        StartCoroutine(CBlack());
    }

    private IEnumerator CBlack()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime * Speed)
        {
            Image.color = new Color(0.0f, 0.0f, 0.0f, i);
            yield return new WaitForSecondsRealtime(0.016f);
        }

        Event.Invoke();
        yield return null;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
