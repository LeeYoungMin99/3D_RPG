using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObject : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public Transform Target { private get; set; }
    public float Speed { private get; set; }
    public float Damage { private get; set; }

    [Range(0f, 5f)]
    [SerializeField] private float _time = 2f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        StartCoroutine(DisableObjectWaitForSeconds(_time));
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
    private IEnumerator DisableObjectWaitForSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

}
