using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [Header("Enemies")]
    public GameObject[] enemies;

    int enemytype;
    Animator animatedSpawn;
    // Start is called before the first frame update
    void Start()
    {
        animatedSpawn = GetComponent<Animator>();
        animatedSpawn.Play("Instantiated");
        enemytype = Random.Range(0, 101);
    }
    //�G��������āA�䗦�I�ɔ����������ɂȂ邽�߂ɉ��̃R�[�h���g���Ă��܂��B
    public void InstantiatingEnemy()
    {
        switch (enemytype)
        {
            case int n when n <= 25:
                    Instantiate(enemies[0], transform.position, transform.rotation);
                    break;
            case int n when n>25 && n<=100:
                    Instantiate(enemies[Random.Range(1, 4)], transform.position, transform.rotation);
                    break;
            default:
                    Debug.Log(enemytype + " is not initialized");
                    break;
        }
    }
    //�X�|�[����j������
    public void BeDestroyed()
    {
        Destroy(this.gameObject);
    }
    
}
