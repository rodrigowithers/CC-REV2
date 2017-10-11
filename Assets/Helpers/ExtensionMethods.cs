using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    #region Vector2 Extensions
    /// <summary>
    /// Retorna os componentes X e Y do Vector 3 para uso no Vector 2
    /// </summary>
    public static Vector2 xy(this Vector3 vector)
    {
        return new Vector2(vector.x, vector.y);
    }

    /// <summary>
    /// Retorna os componentes X e Z do Vector 3 para uso no Vector 2
    /// </summary>
    public static Vector2 xz(this Vector3 vector)
    {
        return new Vector2(vector.x, vector.z);
    }

    /// <summary>
    /// Retorna os componentes Y e Z do Vector 3 para uso no Vector 2
    /// </summary>
    public static Vector2 yz(this Vector3 vector)
    {
        return new Vector2(vector.y, vector.z);
    }

    public static Vector2 CreateDirectionVector(this Vector2 from, Vector2 to)
    {
        return (to - from).normalized;
    }


    #endregion

    #region Vector3 Extensions

    /// <summary>
    /// Retorna exatamente o meio entre dois vetores
    /// </summary>
    public static Vector3 Middle(this Vector3 min, Vector3 max)
    {
        var x = (min.x + max.x) / 2;
        var y = (min.y + max.y) / 2;
        var z = (min.z + max.z) / 2;

        return new Vector3(x, y, z);
    }

    /// <summary>
    /// Usa um unico float para criar um vector 3 com os mesmos parametros
    /// </summary>
    public static void CreateFromFloat(this Vector3 vec, float value)
    {
        vec = new Vector3(value, value, value);
    }

    /// <summary>
    /// Cria um Vector 3 com um Vector 2, passando um Vector 3 para o Z
    /// </summary>
    public static Vector3 xyz(this Vector2 vector, Vector3 pos)
    {
        return new Vector3(vector.x, vector.y, pos.z);
    }

    /// <summary>
    /// Retorna o objeto do array passado mais proximo da posição atual
    /// </summary>
    public static GameObject FindClosest(this Vector3 vector, GameObject[] objects)
    {
        GameObject toReturn = null;
        float closest = Mathf.Infinity;

        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] == null)
                continue;

            if (Vector3.Distance(vector, objects[i].transform.position) < closest)
            {
                closest = Vector3.Distance(vector, objects[i].transform.position);
                toReturn = objects[i];
            }
        }

        return toReturn;
    }

    #endregion

    #region AnimationCurve Extensions

    /// <summary>
    /// Retorna, em segundos, o comprimento total da curva
    /// </summary>
    /// <param name="curve"></param>
    /// <returns></returns>
    public static float GetCurveLenght(this AnimationCurve curve)
    {
        return curve.keys[curve.keys.Length - 1].time;
    }

    #endregion

    #region Animator Extensions

    /// <summary>
    /// NÃO FUNCIONA
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="value"></param>
    public static void SetCurrentBool(this Animator animator, bool value)
    {
        var info = animator.GetCurrentAnimatorStateInfo(0);
        var name = info.fullPathHash;

        animator.SetBool(name, value);
    }

    public static void SetAllBools(this Animator animator, bool value)
    {
        foreach(AnimatorControllerParameter p in animator.parameters)
        {
            animator.SetBool(p.name, value);
        }
    }


    #endregion

    #region List<GameObject> Extensions

    public static List<GameObject> Invert(this List<GameObject> list)
    {
        List<GameObject> toReturn = new List<GameObject>();

        for (int i = 1; i <= list.Count; i++)
        {
            toReturn.Add(list[list.Count - i]);
        }

        return toReturn;
    }

    #endregion

    #region GameObject Extensions

    /// <summary>
    /// Retorna um array com todos os objetos filhos do objeto
    /// </summary>
    public static GameObject[] GetChildren(this GameObject obj)
    {
        List<GameObject> temp = new List<GameObject>(obj.transform.childCount);

        for (int i = 0; i < obj.transform.childCount; i++)
        {
            temp.Add(obj.transform.GetChild(i).gameObject);
        }

        return temp.ToArray();
    }

    public static T[] GetChildrenOfType<T>(this GameObject gameObject) where T : Component
    {
        List<T> temp = new List<T>(gameObject.transform.childCount);

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).GetComponent<T>())
                temp.Add(gameObject.transform.GetChild(i).GetComponent<T>());
        }

        return temp.ToArray();
    }

    public static void SelfDestroy(this GameObject obj)
    {
        Object.Destroy(obj);
    }

    #endregion

    #region Tranform Extensions

    public static Transform[] GetChildren(this Transform t)
    {
        List<Transform> temp = new List<Transform>(t.childCount);

        for (int i = 0; i < t.transform.childCount; i++)
        {
            temp.Add(t.GetChild(i));
        }

        return temp.ToArray();
    }

    public static Transform[] GetChildrenOfType(this Transform t, System.Type type)
    {
        List<Transform> temp = new List<Transform>(t.childCount);

        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).GetComponent(type))
                temp.Add(t.GetChild(i));
        }

        return temp.ToArray();
    }

    public static Transform GetChildOfType<T>(this Transform transform) where T : Component
    {
        Transform[] childs = transform.GetChildren();
        foreach(var c in childs)
        {
            if(c.GetComponent<T>() != null)
            {
                return c;
            }
        }

        return null;
    }

    #endregion
}
