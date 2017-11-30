using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceRun : MonoBehaviour {

    Image img;
    RectTransform rect;
    int next = 0;
	// Use this for initialization
	void Start () {
        img = GetComponent<Image>();

        rect = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		//if(rect.position.x >= 11)
  //      {
  //          next++;
  //          if (next >= 6)
  //              next = 0;

  //          img.sprite = sprites[next];
  //      }
	}

}
