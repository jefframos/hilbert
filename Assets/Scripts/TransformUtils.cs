using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformUtils
{
	public static void InstantiateAndAdd(GameObject prefab, Transform parent)
    {
        GameObject go = GameObject.Instantiate(prefab, new Vector3(), Quaternion.identity);
        Transform t = go.transform;
        t.SetParent(parent, false);
    }
}
