using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firestorm : MonoBehaviour
{
    public ParticleSystem ps;
    public Camera mainCamera;
    void Start()
    {
        float height = 2f * mainCamera.orthographicSize;
        float width = height * mainCamera.aspect;

        ps = GetComponent<ParticleSystem>();
        var shape = ps.shape;
        shape.scale = new Vector3(width, height, 1f);

        ALL_CONSUMING_INFERNO();
    }

    public void ALL_CONSUMING_INFERNO()
    {
        ps.Play();
    }
}
