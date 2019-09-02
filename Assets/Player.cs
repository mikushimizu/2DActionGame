using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
//コンポーネントを追加する
//セットアップのエラーを回避する

public class Player : MonoBehaviour {
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f; //頂点までの時間
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 6;

    float gravity;
    float jumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    Controller2D controller;
    // Use this for initialization
    void Start () {
        controller = GetComponent<Controller2D>();
        //物理の距離の公式から
        //Pow timeToJumpApexの2乗
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2); //x = 1/2 * a * t^2 をaで整理
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex; //v = a * t
        print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);
    }
	
	// Update is called once per frame
	void Update () {
        if (controller.collisions.above || controller.collisions.below) //上方か下方がtrue
        {
            velocity.y = 0;
        }
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
        }

        float targetVelocityX = input.x * moveSpeed; //そのうちでてくるでしょう
        velocity.x = input.x * moveSpeed;
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime, input);
    }
}
