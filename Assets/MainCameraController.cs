using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    // Unity�����̃Q�[���I�u�W�F�N�g���i�[����ϐ�
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // �Q�[���J�n����Unity�����̃Q�[���I�u�W�F�N�g��T����player�ϐ��Ɋi�[
        this.player = GameObject.Find("UnityChan_footwork_0");
    }

    // Update is called once per frame
    void Update()
    {
        // Unity�����̌��݂̈ʒu���擾
        Vector3 playerPos = this.player.transform.position;

        // �J�����̈ʒu���X�V���āA�J������Unity������Y���W�ɒǏ]����悤�ɂ���
        transform.position = new Vector3(transform.position.x, playerPos.y, transform.position.z);
    }
}