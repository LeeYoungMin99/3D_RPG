using UnityEngine;
using System.Collections;

public class ArcLineRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer _arclineRenderer;
    [SerializeField] private Transform _leftLine;
    [SerializeField] private Transform _rightLine;
    [SerializeField] private Transform _lineEndPosition;

    public int Angle { private get; set; }

    private void Awake()
    {
        _arclineRenderer.positionCount = Angle * 2 + 1;
    }

    private void OnEnable()
    {
        for (int i = -Angle; i <= Angle; ++i)
        {
            transform.localRotation = Quaternion.Euler(0f, i, 0f);

            _arclineRenderer.SetPosition(i + Angle, _lineEndPosition.position);
        }

        transform.localRotation = Quaternion.identity;
        _leftLine.localRotation = Quaternion.Euler(0f, -Angle, 0f);
        _rightLine.localRotation = Quaternion.Euler(0f, Angle, 0f);
    }
}
