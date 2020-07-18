using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LadderLoader : MonoBehaviour
{
    private BoxCollider2D SelfCollider;
    private BuoyancyEffector2D buoyancyEffector;
    private SpriteRenderer SelfSprite;
    private readonly float dSize = 1.5f;
    private readonly float dH = 0.45f;
    private void Awake()
    {
        SelfCollider = GetComponent<BoxCollider2D>();
        buoyancyEffector = GetComponent<BuoyancyEffector2D>();
        SelfSprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SelfCollider.size = new Vector2(SelfCollider.size.x, SelfSprite.size.y + dSize);
        buoyancyEffector.surfaceLevel = SelfSprite.size.y / 2 + dH;
    }
}
