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
    }

    public void ALL_CONSUMING_INFERNO()
    {
        ps.Play();
    }

    private void Update()
    {
        transform.position = mainCamera.transform.position + new Vector3(0f, 0f, 5f);
    }
}
