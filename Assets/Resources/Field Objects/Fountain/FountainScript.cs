using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FountainScript : MonoBehaviour {

    public Sprite bluefountain;
    SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        //bluefountain = Resources.Load<Sprite>("Field Objects/tiles para o jogo chess champions/fountain blue");
        sr = GetComponent<SpriteRenderer>();
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeFountain()
    {
        //GetComponent<SpriteRenderer>().sprite = bluefountain;
        StartCoroutine(CClearFountain());
    }

    IEnumerator CClearFountain()
    {
        //Instantiate(bluefountain,transform.position,Quaternion.identity);
        float t = 0;
        while (t < 0.3f)
        {
            Color newest = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - Time.deltaTime);
            sr.color = newest ;
            t += Time.deltaTime;
            yield return null;
        }
        sr.sprite = bluefountain;
        t = 0;
        while (t < 0.3f)
        {
            Color newest = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a + Time.deltaTime);
            sr.color = newest;
            t += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }
}
