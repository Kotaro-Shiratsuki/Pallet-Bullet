using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyAttack : MonoBehaviour
{
    //�U���v���p�e�B
    [Header("Damage Property")]
    public Collider attackCollider;
    public int damage;
    //�R���|�l���g
    Animator animator;
    public Transform player;
    //�O��
    EnemyMovement movement;
    [Header("Used in \"Enemy Movement\"")]
    public bool isAttackingPlayer;
    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        animator = GetComponent<Animator>();
        attackCollider.GetComponent<AttackDamage>().damage = damage;
    }
    //�A�j���[�V�������g���āA�U�����܂�
    public void StopMovement()
    {
        animator.SetBool("isMoving", false);
        movement.isAttacking = true;
    }
    public void ResumeMovement()
    {
        animator.SetBool("isAttacking", false);
        movement.isAttacking = false;
    }
    //�U���ڐG��L���ɂ���
    public void ActivateAtack()
    {
    
        isAttackingPlayer = true;
        attackCollider.enabled = true;
    }
    //�U���ڐG�𖳌��ɂ���
    public void DeactivateAtack()
    {

        isAttackingPlayer = false;
        attackCollider.enabled = false;
        animator.SetBool("isAttacking", false);
    }
}
