using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.IO;

public class mainUI : MonoBehaviour
{
    public Button startButton;

    float delta = 0f;

    //�f���V�[���ւ̈ڍs
    float demoTimer;

    //BGM�̐ݒ�
    public AudioSource bgmTitle;

    Gamepad gamepad;

    public GameObject fadeOut;

    public static float volValue;
    string path;

    [Header("Debug")]
    public bool debug = false;

    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + "/Settings";
        demoTimer = 0;

        if (File.Exists(path))
        {
            volValue = ReadSettings(path);
            bgmTitle.volume = volValue;
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current != null)
            gamepad = Gamepad.current;
        Debug.Log(demoTimer);


        //���͂��Ȃ��ꍇ�^�C�}�[���J�E���g

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

        //B�{�^���ŃQ�[���V�[����
        if (gamepad.bButton.wasPressedThisFrame)
        {
            OnButtonDown_Start();
        }

        //�X�^�[�g�{�^���������ŃQ�[�������(�f�o�b�O�p
        if (gamepad.startButton.isPressed)
        {
            delta += Time.deltaTime;

            if (delta > 3.0f)
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

    void WriteSettings(float volumeValue_)
    {
        if (!File.Exists(path))
            File.Create(path);
        string content =
            "GameVolume= " + volValue.ToString();

        File.WriteAllText(path, content);
    }

    float ReadSettings(string path_)
    {
        string text = File.ReadAllText(path_);
        var settingString = text.Split(" "[0]);
        float volume = float.Parse(settingString[1]);
        Debug.Log(volume);
        return volume;
    }

    void ApplicationQuit()
    {
        Application.Quit();
    }
}
