using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] private Transform _body;
    public void Rotate(Vector3 forward)
    {
        _body.forward = forward;
    }
}
