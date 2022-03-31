using System;

public class PlayerDeath : State
{
    //private DeathEventArgs _deathEvent = new DeathEventArgs();

    //public event EventHandler<DeathEventArgs> DeathEvent;

    protected override void Awake()
    {
        _stateTag = EStateTag.Death;

        //_deathEvent.GameObject = gameObject;

        //CharacterStatus characterStatus = GetComponent<CharacterStatus>();

        //characterStatus.OnDeathEvent -= ChangeCharacter;
        //characterStatus.OnDeathEvent += ChangeCharacter;

        base.Awake();
    }

    //private void ChangeCharacter(object sender, EventArgs args)
    //{
    //    DeathEvent?.Invoke(this, _deathEvent);
    //}
}
