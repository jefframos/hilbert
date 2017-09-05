using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformUtils
{
	public static GameObject InstantiateAndAdd(GameObject prefab, Transform parent = null)
    {
        GameObject go = GameObject.Instantiate(prefab, new Vector3(), Quaternion.identity);
        Transform t = go.transform;
        if(parent != null)
        {
            t.SetParent(parent, false);

        }
        return go;
    }
}
