using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScene
{
    void SceneEnter();
    void SceneUpdate();
    void SceneExit();

    bool SceneCompleted();
}
