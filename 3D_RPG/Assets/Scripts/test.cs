using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public RaycastHit _hit;
    [Range(0f, 5f)]
    [SerializeField] private float _distance = 0f;
    [SerializeField] private LayerMask _layerMask;

    private void LateUpdate()
    {
        if (Physics.Raycast(transform.position + new Vector3(0f, 2f, 0f), transform.up * -1, out _hit, _distance, _layerMask))
        {
            gameObject.transform.position = _hit.point;
        }
    }
}
