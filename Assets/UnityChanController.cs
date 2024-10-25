using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnityChanController : MonoBehaviour
{
    //アニメーションするためのコンポーネントを入れる
    Animator animator;

    //Unityちゃんを移動させるコンポーネントを入れる
    Rigidbody2D rigid2D;

    // ジャンプの力
    float jumpForce = 500.0f;

    // 歩く力
    float walkForce = 20.0f;

    // 最大歩行速度
    float maxWalkSpeed = 2.0f;

    // ゲームクリアのテキスト
    public Text gameclearText;

    // ゲームクリアの判定
    private bool isGameClear = false;

    // 足音のオーディオソース
    private AudioSource footstepAudio;

    // Start is called before the first frame update
    void Start()
    {
        // アニメータのコンポーネントを取得する
        this.animator = GetComponent<Animator>();

        // Rigidbody2Dのコンポーネントを取得する
        this.rigid2D = GetComponent<Rigidbody2D>();

        // フレームレート
        Application.targetFrameRate = 50;

        // ClearTextオブジェクトを取得
        this.gameclearText = GameObject.Find("GameClearText").GetComponent<Text>();

        // 最初は非表示にする
        this.gameclearText.enabled = false;

        // 足音のオーディオソースを取得
        this.footstepAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Y軸方向の速度が0の時にスペースキーが押された場合は
        if (Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0)
        {
            // 上方向の力をかける
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }

        // 移動方向
        int key = 0;

        // 右矢印キーが押された場合は右に
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;

        // 左矢印キーが押された場合は左に
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;

        // x軸方向の現在の速度
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        // x軸方向の速度が最大歩行速度よりも小さい場合は
        if (speedx < this.maxWalkSpeed)
        {
            // 力を加えて移動速度を制御する
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        // 左右の矢印キーが押されている場合は
        if (key != 0)
        {
            // キャラクターの向きを変更する
            transform.localScale = new Vector3(key, 1, 1);
        }

        // X軸方向の速度に応じてアニメーション速度を変える
        this.animator.speed = speedx / 2.0f;

        // ゲームクリア後にクリックされたらシーンをリロードする
        if (isGameClear && Input.GetMouseButtonDown(0))
        {
            //SampleSceneを読み込む
            SceneManager.LoadScene("SampleScene");
        }

        // 足音を再生する
        if (speedx > 0 && this.rigid2D.velocity.y == 0 && !footstepAudio.isPlaying)
        {
            footstepAudio.Play();
        }

        // 足音を停止する
        else if ((speedx == 0 || this.rigid2D.velocity.y != 0) && footstepAudio.isPlaying)
        {
            footstepAudio.Stop();
        }
    }

    // 特定のブロックに衝突した時にゲームクリアの処理を行う
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 特定のブロックに触れた場合
        if (collision.gameObject.tag == "GoalTag")
        {
            // ゲームクリアのテキストを表示
            this.gameclearText.enabled = true;

            // ゲームクリア
            isGameClear = true;
        }
    }
}