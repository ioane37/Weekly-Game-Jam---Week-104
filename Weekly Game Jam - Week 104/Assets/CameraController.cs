﻿using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player = null;

    [SerializeField] float sensetivity = 10f;

    float pitch = 0;
    float yaw = 0;

    void Update()
    {
        transform.position = player.position;

        pitch -= Input.GetAxisRaw("Mouse Y") * sensetivity;
        yaw += Input.GetAxisRaw("Mouse X") * sensetivity;

        pitch = Mathf.Clamp(pitch, -80f, 85f);

        transform.eulerAngles = new Vector3(pitch, yaw);
    }
}