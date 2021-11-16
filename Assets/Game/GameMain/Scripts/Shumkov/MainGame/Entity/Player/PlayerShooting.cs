using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
public class PlayerShooting : MonoBehaviour
{
    //�e�̎��
    public GameObject[] bullet;
    public GameObject superBullet;
    //�e���������Ă���ꏊ
    public GameObject barrel;
    //�e�̃v���p�e�B
    public float attackDamage;
    public float attackSpeed;
    public Vector3 speed;

    public float powerUpTimer;
    float time;
 
    //�t���b�O
    public bool poweredUp;
    bool allowFire = true;

    //�G���E���{�[�i�X
    public float bonusSetting;


    
    //�����Ă���e�̏��
    int index = 0;
    public GameObject shootingBullet;
    //�Q�[���p�b�h
    Gamepad gamepad;

    //�O���ˑ��֌W
    UI_Time timeManager;
    public AudioSource audioShooting;
    void Start()
    {
        timeManager = GameObject.Find("GameManager").GetComponent<UI_Time>();
        audioShooting = GetComponent<AudioSource>();
        shootingBullet = bullet[0];
    }
    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current != null)
            gamepad = Gamepad.current;

        if (!timeManager.gameOver)
        {
            //�e�̐؂�ւ�
            if (gamepad.rightTrigger.wasPressedThisFrame)
            {
                OnButtonDown_rightChange();
            }
            if (gamepad.leftTrigger.wasPressedThisFrame)
            {
                OnBUttonDown_leftChange();
            }

            //����
            bool canShoot = gamepad.rightShoulder.wasPressedThisFrame;
            Shooting(canShoot);

            //�p���[�A�b�v�̊Ǘ�
            if (poweredUp)
            {
                shootingBullet = superBullet;
                time += Time.deltaTime;
                if (time >= powerUpTimer)
                {
                    shootingBullet = bullet[0];
                    poweredUp = false;
                    time = 0;
                    attackDamage *= 0.5f;
                }
            }
        }
        else StopAllCoroutines();
    }

    //����
    public void Shooting(bool canShoot)
    {
        if (allowFire && canShoot)
        {
            audioShooting.Play();
            GetComponent<Animator>().Play("Gunplay", 0);
            StartCoroutine(ShootingMethod());
        }
    }
    //������
    IEnumerator ShootingMethod()
    {
        allowFire = false;
        //���C�̔���
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        //�ړI�ɂ������m�F�̂���
        GameObject target = null;
        Vector3 targetPoint;
        
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
            if (hit.transform.gameObject.tag == "Enemy")
            {
                targetPoint = hit.point;
                target = hit.transform.gameObject;
            }
            else
                if (hit.transform.gameObject.tag == "Terrain")
            {
                targetPoint = hit.point;
            } 
        else target = null;
        }
        else
            targetPoint = ray.GetPoint(1000);
        //�e�̔���
        var shootBullet = Instantiate(shootingBullet, barrel.transform.position, barrel.transform.rotation);
        speed = (targetPoint - barrel.transform.position).normalized * 70f;
        //�e�̃v���p�e�B�̐ݒ�
        AttackDamage bulletProperty = shootBullet.GetComponent<AttackDamage>();
        bulletProperty.damage = attackDamage;
        bulletProperty.moveVector = targetPoint;
        bulletProperty.target = target;
        shootBullet.GetComponent<Rigidbody>().velocity = speed;
        //���A�j���[�V�����̃t���b�O
        GetComponent<Animator>().SetBool("shootingFlag", false);
        //�����[�h����
        yield return new WaitForSeconds(attackSpeed);
        allowFire = true;
    }

    public void OnBUttonDown_leftChange()
    {
        index--;
        if (index == -1)
            index = 2;
        shootingBullet = bullet[index];
    }
    public void OnButtonDown_rightChange()
    {
        index++;
        index %= bullet.Length;
        shootingBullet = bullet[index];
    }
}
