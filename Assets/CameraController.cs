using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Cinemachine.CinemachineVirtualCamera cam;
    private float camDistance = 18f;

    // Update is called once per frame
    void Update()
    {
        HandleZoom();
    }

    private void HandleZoom()
    {
        float zoomChangeAmount = 200f;

        if(Input.mouseScrollDelta.y > 0)
        {
            camDistance -= zoomChangeAmount * Time.deltaTime;
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            camDistance += zoomChangeAmount * Time.deltaTime;
        }

        camDistance = Mathf.Clamp(camDistance, 5, 18);

        cam.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_CameraDistance = camDistance;
    }

}
