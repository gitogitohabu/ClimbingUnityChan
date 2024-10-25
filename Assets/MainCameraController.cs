using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    // Unityちゃんのゲームオブジェクトを格納する変数
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // ゲーム開始時にUnityちゃんのゲームオブジェクトを探してplayer変数に格納
        this.player = GameObject.Find("UnityChan_footwork_0");
    }

    // Update is called once per frame
    void Update()
    {
        // Unityちゃんの現在の位置を取得
        Vector3 playerPos = this.player.transform.position;

        // カメラの位置を更新して、カメラがUnityちゃんのY座標に追従するようにする
        transform.position = new Vector3(transform.position.x, playerPos.y, transform.position.z);
    }
}