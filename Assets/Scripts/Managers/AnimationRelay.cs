using UnityEngine;

public class AnimationRelay : MonoBehaviour
{
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private Enemy enemy;

    public void PlayerDealDamage()
    {
        playerAttack.DealDamage();
    }

    public void EnemyDealDamage()
    {
        enemy.DealDamage(1);
    }

    public void EnemyAttackFinished()
    {
        enemy.EndAttack();
    }
}
