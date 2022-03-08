using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObject : MonoBehaviour
{
    private Transform _target;
    private Rigidbody _rigidbody;
    private float _speed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.LookAt(_target);
        Vector3 newVel = transform.forward * _speed;
        _rigidbody.velocity = newVel;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("데미지를 입히다");

        gameObject.SetActive(false);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}
