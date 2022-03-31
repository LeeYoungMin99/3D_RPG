using System;
using System.Collections;
using UnityEngine;

public class PlayerTargetManager : TargetManager
{
    private EnemyHealthBar _enemyHealthBar;

    private void Awake()
    {
        _enemyTargetLayer = 1 << 3;

        _enemyHealthBar = GameObject.Find("Enemy Health Bar").GetComponent<EnemyHealthBar>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _enemyHealthBar.PlayerTargetManager = this;
    }

    protected override IEnumerator SearchTarget()
    {
        while (true == enabled)
        {
            Transform prevEnemyTarget = EnemyTarget;
            EnemyTarget = SearchTargetHelper(EMEMY_SEARCH_RADIUS, _enemyTargetLayer); ;

            if (EnemyTarget != prevEnemyTarget)
            {
                _enemyHealthBar.RefreshTarget();
            }

            yield return new WaitForSeconds(SEARCH_DELAY);
        }
    }
}
