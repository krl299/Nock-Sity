using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureModels : MonoBehaviour
{
    float yheight = 0;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    public void CreateModel(GameObject model)
    {
        var structure = Instantiate(model, transform);
        yheight = model.transform.position.y;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="rotation"></param>
    public void SwapModel(GameObject model, Quaternion rotation)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        var structure = Instantiate(model, transform);
        structure.transform.localPosition = new Vector3(0, yheight, 0);
        structure.transform.localRotation = rotation;
    }
}
