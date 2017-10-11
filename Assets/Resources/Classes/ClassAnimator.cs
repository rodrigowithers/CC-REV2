using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ClassAnimator : MonoBehaviour
{
    private Animator _animator;

    private bool Locked = false;

    private IEnumerator WaitForLock(AnimationClip clip)
    {
        yield return new WaitForSeconds(clip.length);
        Locked = false;
    }

    public void LoadAnimations(string path)
    {
        //_animations = Resources.Load<Animations>(path);
        _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(path);

        if (_animator.runtimeAnimatorController == null)
        {
            throw new System.Exception("Não encontrou Animation Controller.");
        }
    }

    public bool Play(string stateName, int layer = 0, bool lockAnimation = false)
    {
        if (Locked)
            return false;

        var currentClip = _animator.GetCurrentAnimatorStateInfo(layer);
        if (!currentClip.IsName(stateName))
        {
            _animator.Play(stateName, 0);

            if (lockAnimation)
            {
                Locked = true;

                var clip = _animator.GetCurrentAnimatorClipInfo(layer);

                StartCoroutine(WaitForLock(clip[0].clip));
            }
            return true;
        }
        return false;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
