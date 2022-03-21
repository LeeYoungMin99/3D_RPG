using UnityEngine;

public class PlayerSectorFormAttack : SectorFormAttackState
{
    private PlayerInput _input;
    private bool _isAuto = false;

    protected override void Start()
    {
        base.Start();

        _input = transform.parent.GetComponent<PlayerInput>();

        AutoButton autoButton = GameObject.Find("Auto Button").GetComponent<AutoButton>();
        _isAuto = autoButton.IsAuto;
        autoButton.OnClickEvent -= SetAuto;
        autoButton.OnClickEvent += SetAuto;
    }

    public override void UpdateState()
    {
        if (false == CheckComboPossible()) return;

        if (true == _input.Attack)
        {
            _animator.SetTrigger(CharacterAnimID.IS_ATTACKING);

            return;
        }

        if (true == _isAuto)
        {
            base.UpdateState();
        }
    }

    private void SetAuto(object sender, AutoButton.AutoButtonEventArgs args)
    {
        _isAuto = args.CurAuto;
    }
}
