using UnityEngine;

class CharacterAnimID
{
    public static readonly int MOVE = Animator.StringToHash("Move");
    public static readonly int IS_ATTACKING = Animator.StringToHash("IsAttacking");
    public static readonly int IS_COOLDOWN = Animator.StringToHash("IsCooldown");
    public static readonly int USE_SKILL = Animator.StringToHash("UseSkill");
    public static readonly int IS_DAMAGE = Animator.StringToHash("IsDamage");
    public static readonly int IS_DIE = Animator.StringToHash("IsDie");
}
