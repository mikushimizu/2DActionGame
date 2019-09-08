using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
//BoxCollider2DというUnity特有のコンポーネントを追加する

public class RaycastController : MonoBehaviour {
    public LayerMask collisionMask; //衝突用のマスク(レイヤー)を設定している

    public const float skinWidth = .015f; //キャラクターの衝突するスキン幅 数値制度の問題を回避する 赤い線がちょっと触れてるとこ
    public int horizontalRayCount = 4; //水平方向に飛ばすRayの数
    public int verticalRayCount = 4; //垂直方向に飛ばすRayの数 細かくとって

    [HideInInspector]
    public float horizontalRaySpacing; //水平方向のRayの幅
    [HideInInspector]
    public float verticalRaySpacing; //垂直方向のRayの幅

    [HideInInspector]
    public BoxCollider2D collider;
    public RaycastOrigins raycastOrigins;

    public virtual void Awake() {
        collider = GetComponent<BoxCollider2D> ();
    }

    public virtual void Start () {
        CalculateRaySpacing();
    }

    //RayCastの位置を更新
    public void UpdateRaycastOrigins()
    {
        //bounds 3次元空間範囲を表す
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth*-2); //

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    //Rayの間隔を計算
    public void CalculateRaySpacing()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    //構造体 クラスみたいなやつ
    //見やすくしてるだけ
    public struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
}