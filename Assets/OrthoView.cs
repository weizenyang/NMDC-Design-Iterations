using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrthoView : MonoBehaviour
{
    void Start()
    {
        // create orthographic matrix
        var matrix = Matrix4x4.Ortho(-4, 4, -2, 2, 1, 100);
        // will print:
        // 0.25000 0.00000  0.00000  0.00000
        // 0.00000 0.50000  0.00000  0.00000
        // 0.00000 0.00000 -0.02020 -1.02020
        // 0.00000 0.00000  0.00000  1.00000
        Debug.Log("projection matrix\n" + matrix);

        // get shader-compatible projection matrix value
        var shaderMatrix = GL.GetGPUProjectionMatrix(matrix, false);
        // on a Direct3D-like graphics API, will print:
        // 0.25000 0.00000  0.00000  0.00000
        // 0.00000 0.50000  0.00000  0.00000
        // 0.00000 0.00000 0.01010 1.01010
        // 0.00000 0.00000 0.00000 1.00000
        Debug.Log("shader projection matrix\n" + shaderMatrix);
    }
}
