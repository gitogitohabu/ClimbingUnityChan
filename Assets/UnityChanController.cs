using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnityChanController : MonoBehaviour
{
    //�A�j���[�V�������邽�߂̃R���|�[�l���g������
    Animator animator;

    //Unity�������ړ�������R���|�[�l���g������
    Rigidbody2D rigid2D;

    // �W�����v�̗�
    float jumpForce = 500.0f;

    // ������
    float walkForce = 20.0f;

    // �ő���s���x
    float maxWalkSpeed = 2.0f;

    // �Q�[���N���A�̃e�L�X�g
    public Text gameclearText;

    // �Q�[���N���A�̔���
    private bool isGameClear = false;

    // �����̃I�[�f�B�I�\�[�X
    private AudioSource footstepAudio;

    // Start is called before the first frame update
    void Start()
    {
        // �A�j���[�^�̃R���|�[�l���g���擾����
        this.animator = GetComponent<Animator>();

        // Rigidbody2D�̃R���|�[�l���g���擾����
        this.rigid2D = GetComponent<Rigidbody2D>();

        // �t���[�����[�g
        Application.targetFrameRate = 50;

        // ClearText�I�u�W�F�N�g���擾
        this.gameclearText = GameObject.Find("GameClearText").GetComponent<Text>();

        // �ŏ��͔�\���ɂ���
        this.gameclearText.enabled = false;

        // �����̃I�[�f�B�I�\�[�X���擾
        this.footstepAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Y�������̑��x��0�̎��ɃX�y�[�X�L�[�������ꂽ�ꍇ��
        if (Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0)
        {
            // ������̗͂�������
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }

        // �ړ�����
        int key = 0;

        // �E���L�[�������ꂽ�ꍇ�͉E��
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;

        // �����L�[�������ꂽ�ꍇ�͍���
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;

        // x�������̌��݂̑��x
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        // x�������̑��x���ő���s���x�����������ꍇ��
        if (speedx < this.maxWalkSpeed)
        {
            // �͂������Ĉړ����x�𐧌䂷��
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        // ���E�̖��L�[��������Ă���ꍇ��
        if (key != 0)
        {
            // �L�����N�^�[�̌�����ύX����
            transform.localScale = new Vector3(key, 1, 1);
        }

        // X�������̑��x�ɉ����ăA�j���[�V�������x��ς���
        this.animator.speed = speedx / 2.0f;

        // �Q�[���N���A��ɃN���b�N���ꂽ��V�[���������[�h����
        if (isGameClear && Input.GetMouseButtonDown(0))
        {
            //SampleScene��ǂݍ���
            SceneManager.LoadScene("SampleScene");
        }

        // �������Đ�����
        if (speedx > 0 && this.rigid2D.velocity.y == 0 && !footstepAudio.isPlaying)
        {
            footstepAudio.Play();
        }

        // �������~����
        else if ((speedx == 0 || this.rigid2D.velocity.y != 0) && footstepAudio.isPlaying)
        {
            footstepAudio.Stop();
        }
    }

    // ����̃u���b�N�ɏՓ˂������ɃQ�[���N���A�̏������s��
    void OnCollisionEnter2D(Collision2D collision)
    {
        // ����̃u���b�N�ɐG�ꂽ�ꍇ
        if (collision.gameObject.tag == "GoalTag")
        {
            // �Q�[���N���A�̃e�L�X�g��\��
            this.gameclearText.enabled = true;

            // �Q�[���N���A
            isGameClear = true;
        }
    }
}