using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;


    private CinemachineVirtualCamera virtualCamera;
    private CinemachineFramingTransposer transposer;


    [Header("Camera distance")]
    [SerializeField] private bool canChangeCameraDistance;
    [SerializeField] private float distanceChangeRate;
    [SerializeField] private float targetCameraDistance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.LogWarning("You had more than one Camera Manager");
            Destroy(gameObject);
        }


        virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        transposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

    }

    private void Update()
    {
        UpdateCameraDistance();
    }

    private void UpdateCameraDistance()
    {
        if (canChangeCameraDistance == false)
            return;

        float currentDistnace = transposer.m_CameraDistance;

        if (Mathf.Abs(targetCameraDistance - currentDistnace) < .01f)
            return;
        
        transposer.m_CameraDistance =
            Mathf.Lerp(currentDistnace, targetCameraDistance, distanceChangeRate * Time.deltaTime);
    }

    public void ChangeCameraDistance(float distance, float newChangeRate = .25f)
    {
        distanceChangeRate = newChangeRate;
        targetCameraDistance = distance;
    }
    public void ChangeCameraTarget(Transform target,float cameraDistance = 10,float newLookAheadTime = 0)
    {
        virtualCamera.Follow = target;
        transposer.m_LookaheadTime = newLookAheadTime;
        ChangeCameraDistance(cameraDistance);
    }

}
