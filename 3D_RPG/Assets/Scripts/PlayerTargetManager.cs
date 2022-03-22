using System;
using System.Collections;
using UnityEngine;

public class PlayerTargetManager : TargetManager
{
    [Header("NPC Target Setting")]
    [SerializeField] private float _NPCSearchRadius = 5f;
    [SerializeField] private LayerMask _NPCTargetMask;

    private EnemyHealthBar enemyHealthBar;
    public Transform NPCTarget { get; private set; }

    private void Awake()
    {
        enemyHealthBar = GameObject.Find("Enemy Health Bar").GetComponent<EnemyHealthBar>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        enemyHealthBar.PlayerTargetManager = this;
    }

    protected override IEnumerator SearchTarget()
    {
        while (true == enabled)
        {
            Transform prevEnemyTarget = EnemyTarget;
            EnemyTarget = SearchTargetHelper(_enemySearchRadius, _enemyTargetLayer); ;

            if (EnemyTarget != prevEnemyTarget)
            {
                enemyHealthBar.RefreshTarget();
            }

            NPCTarget = SearchTargetHelper(_NPCSearchRadius, _NPCTargetMask);

            yield return new WaitForSeconds(_searchDelay);
        }
    }
}
