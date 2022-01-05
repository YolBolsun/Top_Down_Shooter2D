using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.MonoBehaviours;

public class SetDesiredCameraDistance : MonoBehaviour
{
    [SerializeField] private CameraFollow cameraFollow = null;
    [SerializeField] private Transform followTransform = null;
    [SerializeField] private float zoom = 50f;

    private void Start()
    {
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        followTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (followTransform == null)
        {
            Debug.LogError("followTransform is null! Intended?");
            cameraFollow.Setup(() => Vector3.zero, () => zoom, true, true);
        }
        else
        {
            cameraFollow.Setup(() => followTransform.position, () => zoom, true, true);
        }
    }
}
