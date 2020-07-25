using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.Experimental.Rendering.Universal;
using Light2D = UnityEngine.Experimental.Rendering.Universal.Light2D;

public class LavaLoader : MonoBehaviour
{
    private BuoyancyEffector2D effector;
    private ParticleSystem particles;
    private Light2D freeFormLight;
    private SpriteRenderer sprite;
    private void Awake()
    {
        effector = GetComponent<BuoyancyEffector2D>();
        sprite = GetComponent<SpriteRenderer>();
        particles = GetComponent<ParticleSystem>();
        freeFormLight = transform.GetChild(0).GetComponent<Light2D>();
        effector.surfaceLevel = sprite.size.y / 2f + 0.15f;
        var pShape = particles.shape;
        pShape.scale = new Vector3(sprite.size.x, 1, 1);
        pShape.position = new Vector3(0, sprite.size.y / 2f + 0.24f, 0);
        freeFormLight.transform.localScale = new Vector3(sprite.size.x / 3.0285f, sprite.size.y / 7.605033f, 1);
    }
}
