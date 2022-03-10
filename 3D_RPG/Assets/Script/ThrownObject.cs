using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObject : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public Transform Target { private get; set; }
    public float Speed { private get; set; }
    public float Damage { private get; set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        transform.LookAt(Target);
        Vector3 newVel = transform.forward * Speed;
        _rigidbody.velocity = newVel;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Status>().TakeDamage(Damage);

        gameObject.SetActive(false);
    }
}
