using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumping : MonoBehaviour
{
    [Header("Gravity Properties")]
    public LayerMask groundMask;
    public float gravitationEffect;

    //�L�����v���p�e�B
    [Header("Character Properties")]
    public float playerThickness;

    //������
    public GameObject walkingSound;
    //�R���|�[�l���g�֌W
    CharacterController playerController;
    Gamepad gamepad;

    public bool isJumping;
    public bool isGrounded;
    Vector3 gravity;
    void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current != null)
            gamepad = Gamepad.current;
        //�n�ʂ̊m�F
        isGrounded = Physics.CheckSphere(transform.position, playerThickness, groundMask);
        //�W�����v����
        isJumping = gamepad.aButton.wasPressedThisFrame|| gamepad.aButton.isPressed;
    }
    
    void FixedUpdate()
    {
        //�n�ʂɂ��Ă��邩�ǂ����m�F
        if (isGrounded&&isJumping)
        {
            gravity.y = Mathf.Sqrt(-0.5f * gravitationEffect * 3f) * Time.deltaTime;
        }
 
        if (!isGrounded)
        {
            walkingSound.SetActive(false);
            //�d�͂̌v�Z
            gravity.y += gravitationEffect * Time.deltaTime * Time.deltaTime;
        }
        playerController.Move(gravity);
    }
}
