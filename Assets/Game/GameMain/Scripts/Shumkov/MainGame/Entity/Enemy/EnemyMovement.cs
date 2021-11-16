using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("Range Settings")]
    public float sightRange;
    public float attackRange;
    public float walkingRange;
    [Header("Player Detection")]
    public LayerMask playerMask;
    public Transform player;

    public bool isAttacking = false;
    NavMeshAgent agent;
    Animator animator;
    UI_Time time;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        time = GameObject.Find("GameManager").GetComponent<UI_Time>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!time.gameOver)
        {
            //�v���C���[���݂��邩�ǂ����m�F
            bool playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
            //�U���ł��邩�ǂ����m�F
            bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);



            //�L�����������@-���@
            if (!playerInSightRange && !playerInAttackRange && !isAttacking) AI_IdleMovement(walkingRange);
            //�߂����U�����͂��Ȃ��@�[���@
            if (playerInSightRange && !playerInAttackRange && !isAttacking) AI_ActiveMovement();
            //�߂��@�[�� 
            if ((playerInSightRange && playerInAttackRange) || isAttacking)
            {
                animator.SetBool("isAttacking", true);
                agent.velocity = Vector3.zero;
                transform.LookAt(player.transform);
            }
        }
        else Destroy(gameObject);
    }

        Vector3 walkPoint;
        bool walkPointSet;
    public void AI_IdleMovement(float walkingRange)
    {
        //�܂��A�ړ��ʒu���Ȃ�������A���̈ʒu�����܂��傤�B
        if (!walkPointSet) SearchWalkPoint(walkingRange);
        //�ړ��悪���܂�����A�ړ�����
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            animator.SetBool("isMoving", true);
        }
        //�ړ���܂ł̋���
        Vector3 distance = transform.position - walkPoint;
        //��������A�V�����ړ�������B
        if (distance.magnitude < 0.1f)
        {
            walkPointSet = false;
            animator.SetBool("isMoving", false);
        }
    }
    private void SearchWalkPoint(float walkingRange)
    {
        LayerMask groundMask = LayerMask.GetMask("Ground");
        //�����_���ȕ���
        float randomZ = Random.Range(-walkingRange, walkingRange);
        float randomX = Random.Range(-walkingRange, walkingRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        NavMeshHit hit;

        //���̈ʒu�̉��ɒn�ʂ���������A���̏ꏊ���ړ���Ƃ��ĔF�߂�
        if (NavMesh.SamplePosition(walkPoint, out hit, 1f, NavMesh.AllAreas))
        {
            Debug.Log(hit.position);
            Debug.DrawRay(hit.position, Vector3.up, Color.red, 10f);
            walkPoint = hit.position;
            walkPointSet = true;
        }
    }

    public void AI_ActiveMovement()
    {
        //�ǂ�
        agent.SetDestination(player.position);
        animator.SetBool("isMoving", true);
    }
}
