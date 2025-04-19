using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxVisualizer : MonoBehaviour
{
    [SerializeField] private Vector3 boxSize;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(gameObject.transform.position, boxSize);
    }
}
