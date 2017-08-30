using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Flash : MonoBehaviour
{
    public Material Material;
    public AnimationCurve FlashCurve;
    public float Speed = 2.0f;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, Material);
    }

    private void OnDestroy()
    {
        Material.SetFloat("_Fade", 0);
    }

    public IEnumerator CFlash()
    {
        var time = 0.0f;
        Material.SetFloat("_Fade", 0);

        while (time < FlashCurve.GetCurveLenght())
        {
            time += Time.deltaTime * Speed;

            Material.SetFloat("_Fade", FlashCurve.Evaluate(time));

            yield return null;
        }

        Material.SetFloat("_Fade", 0);
        yield return null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            StartCoroutine(CFlash());
    }
}
