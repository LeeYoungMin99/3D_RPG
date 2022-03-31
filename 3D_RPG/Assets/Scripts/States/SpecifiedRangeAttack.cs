using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecifiedRangeAttack : RangeAttack
{
    [SerializeField] private GameObject _rangeCircle;
    [Header("Dolly Camera Setting")]
    [SerializeField] private GameObject _dollyCamera;
    [SerializeField] private GameObject _dollyTrack;

    private Camera _mainCamera;
    private CameraRotator _cameraRotator;
    private GameObject _skillButtons;
    private bool _isClick = false;

    private static readonly Vector3 HIDE_UI_POSITION_VECTOR = new Vector3(0f, -200, 0f);

    private void DisableDollyCamera()
    {
        _isClick = true;

        Time.timeScale = 1f;

        _skillButtons.transform.position -= HIDE_UI_POSITION_VECTOR;
        _dollyCamera.SetActive(false);
        _cameraRotator.LockCameraPosition = false;
    }

    protected override void Awake()
    {
        base.Awake();

        _mainCamera = Camera.main;

        Transform field = GameObject.Find("Field").transform;
        _skillButtons = field.Find("Canvas").Find("Skill Button").gameObject;

        _rangeCircle = Instantiate(_rangeCircle, field);
        _dollyCamera = Instantiate(_dollyCamera, field);
        _dollyTrack = Instantiate(_dollyTrack, transform);

        _cameraRotator = GameObject.Find("Follow Camera").GetComponent<CameraRotator>();
        _dollyCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>().
            m_Path = _dollyTrack.GetComponent<CinemachineSmoothPath>();

        _target = _rangeCircle.transform;
        _target.localScale *= _hitRadius;
    }

    protected override IEnumerator Attack()
    {
        RaycastHit hit;

        while (false == _isClick)
        {
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                _target.position = hit.point + CORRECT_TARGET_POSITION_VECTOR;
            }

            if (false == _isPlayableCharacter || true == _isAuto)
            {
                _target.position = _targetManager.EnemyTarget.position + CORRECT_TARGET_POSITION_VECTOR;

                DisableDollyCamera();

                if (_attackDelayTime > 0f) yield return new WaitForSeconds(_attackDelayTime);

                break;
            }

            if (false == _isPlayableCharacter || false == _playerInput.Attack) yield return null;
            else
            {
                DisableDollyCamera();

                if (_attackDelayTime > 0f) yield return new WaitForSeconds(_attackDelayTime);

                break;
            }
        }

        AttackHelper(_target.position);

        _rangeCircle.SetActive(false);

        CalculateObjectPoolIndex();
    }

    public override void EnterState()
    {
        _isClick = false;

        Time.timeScale = 0f;

        _skillButtons.transform.position += HIDE_UI_POSITION_VECTOR;
        _dollyCamera.SetActive(true);
        _rangeCircle.SetActive(true);
        _cameraRotator.LockCameraPosition = true;

        base.EnterState();
    }
}
