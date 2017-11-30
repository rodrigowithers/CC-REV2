using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Movement
{
    public float walk_x;
    public float walk_y;
    public int walk_dir;
    public float duration;
    // direçoes 0 - esquerda ,
    //1 - cima ,
    //2 -  direita ,
    //3 - baixo
}
[RequireComponent(typeof(ClassAnimator))]
public class MovimentationScript : MonoBehaviour {

    [Header("File")]
    public string file;
    public int _class = 1;
    List<Movement> movements = new List<Movement>();
    ClassAnimator class_anim;
    int next_mov = 0;
    bool finished = false;

    // Use this for initialization
	void Start () {
        class_anim = GetComponent<ClassAnimator>();
        movements = JsonHelper.Instance.Retrieve_Movement(file);
        class_anim.LoadAnimations(GetAnimationPath());

	}
    string GetAnimationPath()
    {
        string piece = "King";
       
        switch(_class)
        {
            case 1:piece = "King";
                break;
            case 2:piece = "Queen";
                break;
            case 3:piece = "Tower";
                break;
            case 4:piece = "Horse";
                break;
            case 5:piece = "Bishop";
                break;
            case 6:piece = "Pawn";
                break;
        }
        string _path = "Classes/" + piece + "/Animations/" + piece + "AnimationController";
        return _path;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void NextMove()
    {
        if (next_mov >= movements.Count)
        {
            finished = true;
            return;
        }
        
       
        next_mov++;
    }


}
