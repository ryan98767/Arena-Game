using UnityEngine;
using PlayerMovementNameSpace;

public class AnimationRelay : MonoBehaviour
{
    [SerializeField] protected PlayerAttack playerAttack;
    [SerializeField] protected OldEnemy enemy;
    [SerializeField] protected GameObject portalTarget;
    protected GameObject cutscene;

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

    public void SpawnPortalTarget()
    {
        Instantiate(portalTarget, transform.position, Quaternion.identity);

        PlayerMovement playerMove = portalTarget.GetComponent<PlayerMovement>();
        playerMove.enabled = false;

        playerAttack = portalTarget.GetComponent<PlayerAttack>();
        playerAttack.enabled = false;

        cutscene = GameObject.FindWithTag("Cutscene");
        TutorialNPC tutNPC = cutscene.GetComponent<TutorialNPC>();
        tutNPC.StartCutscene();
    }

    public void DestroyPortal()
    {
        Destroy(gameObject);
    }
}
