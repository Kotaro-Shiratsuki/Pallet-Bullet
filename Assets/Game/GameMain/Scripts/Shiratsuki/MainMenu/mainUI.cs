using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.IO;

public class mainUI : MonoBehaviour
{
    //public GameObject panelStart;//�X�^�[�g���
    public GameObject panelOption;//�I�v�V�������
    public Button startButton;

    float delta = 0f;

    //�f���V�[���ւ̈ڍs
    float demoTimer;

    //option��ʐ؂�ւ��̗P�\����
    bool isOption = false;
    float changeTime = 0;

    Gamepad gamepad;

    public GameObject fadeOut;

    public static float volValue;
    string path;

    [Header("Debug")]
    public bool debug = false;

    // Start is called before the first frame update
    void Start()
    {
        demoTimer = 0;
            
    }

    
    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current != null)
            gamepad = Gamepad.current;
        Debug.Log(demoTimer);
        Option();


        //�f���V�[���ւ̑J��
        if (!isOption)//���͂��Ȃ��ꍇ�^�C�}�[���J�E���g
        {
            if (Input.anyKey)
            {
                demoTimer = 0f;
            }
            else
            {
                demoTimer += Time.deltaTime;


                if (demoTimer > 10.0f)//10�b�ŃV�[���ڍs
                {
                    SceneManager.LoadScene(2);
                }
            }
        }
        else
        {
            demoTimer = 0f;
        }

        if(gamepad.bButton.wasPressedThisFrame)
        {
            OnButtonDown_Start();
        }

        if(gamepad.startButton.isPressed)
        {
            delta += Time.deltaTime;

            if(delta > 3.0f)
            {
                delta = 0;
                ApplicationQuit();
            }
        }


    }
    //start�{�^���ŃQ�[���V�[���֑J��
    public void OnButtonDown_Start()
    {
        OnButtonDown_start();
    }
    public void OnButtonDown_start()
    {
        fadeOut.SetActive(true);
    }
    //option�����
    public void OnButtonDown_close()
    {
        panelOption.SetActive(false);
    }

    //�I�v�V������ʂ��Ǘ�����֐�
    public void Option()
    {
        // Esc�L�[�ŃI�v�V�������
        if (gamepad.bButton.wasPressedThisFrame && !isOption&& debug)
        {
            panelOption.SetActive(true);
            demoTimer = 0;
            isOption = true;
        }

        if (isOption)
        {
            changeTime += Time.deltaTime;

            if (changeTime > 0.05 && gamepad.bButton.wasPressedThisFrame)
            {
                panelOption.SetActive(false);
                changeTime = 0;
                demoTimer = 0;
                isOption = false;
            }
        }
    }


    void ApplicationQuit()
    {
        Application.Quit();
    }
}
