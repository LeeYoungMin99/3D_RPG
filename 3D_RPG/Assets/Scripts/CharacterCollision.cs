using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    [Range(0f, 5f)]
    [SerializeField] private float _distance = 0f;
    [SerializeField] private LayerMask _layerMask;

    private RaycastHit _hit;

    public bool IsCorrecting = true;

    private void LateUpdate()
    {
        if (false == IsCorrecting)
        {
            return;
        }

        if (Physics.Raycast(transform.position + new Vector3(0f, 2f, 0f), transform.up * -1, out _hit, _distance, _layerMask))
        {
            gameObject.transform.position = _hit.point;
        }
    }
}
