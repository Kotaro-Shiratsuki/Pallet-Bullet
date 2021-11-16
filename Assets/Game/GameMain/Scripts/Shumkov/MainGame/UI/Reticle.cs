using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Reticle : MonoBehaviour
{


    //UI�X�v���C�g�̎��
    public Sprite[] reticle;
    public Sprite[] indicator;



    //�j������I�u�W�F�N�g
    public Image reticleImage;
    public Image indicatorImage;
    public GameObject hpBar;
    public GameObject waveCnt;

    //�O���ˑ��֌W
    UI_Time uiTime;
    public PlayerShooting playerShoot;

    private void Start()
    {
        uiTime = GameObject.Find("GameManager").GetComponent<UI_Time>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!uiTime.gameOver)
        {
            //�����Ă���e�ɂ����
            ColorTypes.ColorType color = playerShoot.shootingBullet.GetComponent<AttackDamage>().bulletColor;
            //UI�̐F���Ⴄ
            //��
            if (color == ColorTypes.ColorType.Red)
            {
                indicatorImage.sprite = indicator[0];
                reticleImage.sprite = reticle[0];
            }
            //��
            if (color == ColorTypes.ColorType.Blue)
            {
                indicatorImage.sprite = indicator[1];
                reticleImage.sprite = reticle[1];
            }
            //��
            if (color == ColorTypes.ColorType.Green)
            {
                indicatorImage.sprite = indicator[2];
                reticleImage.sprite = reticle[2];
            }
            //��
            if (color == ColorTypes.ColorType.Super)
            {
                indicatorImage.sprite = indicator[3];
                reticleImage.sprite = reticle[3];
            }
        }
        else
        {
            //�j��
            if(waveCnt != null)
            {
                Destroy(waveCnt);
            }
            if(hpBar != null)
            {
                Destroy(hpBar);
            }
            if (reticleImage != null)
            {
                Destroy(reticleImage.gameObject);
                reticleImage = null;
            }
            if (indicatorImage != null)
            {
                Destroy(indicatorImage.gameObject);
                indicatorImage = null;
            }
        }
    }
}
