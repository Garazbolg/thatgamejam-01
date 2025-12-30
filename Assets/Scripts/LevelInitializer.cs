using System;
using UnityEngine;

[ExecuteInEditMode]
public class LevelInitializer : MonoBehaviour
{
    private void Awake()
    {
        transform.localPosition = Vector3.zero;
    }
}
