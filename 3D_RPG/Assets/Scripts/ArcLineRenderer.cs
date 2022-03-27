using UnityEngine;
using System.Collections;

public class ArcLineRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer _arclineRenderer;
    [SerializeField] private Transform _lineEndPosition;

    public int Angle { private get; set; }

    private void Awake()
    {
        _arclineRenderer.positionCount = Angle * 2 + 3;
    }

    private void OnEnable()
    {
        for (int i = -Angle + 1; i <= Angle + 1; ++i)
        {
            transform.localRotation = Quaternion.Euler(0f, i, 0f);

            _arclineRenderer.SetPosition(i + Angle, _lineEndPosition.position);
        }

        _arclineRenderer.SetPosition(0, transform.position);
        _arclineRenderer.SetPosition(_arclineRenderer.positionCount - 1, transform.position);

        transform.localRotation = Quaternion.identity;
    }
}
