using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{

    Camera FPSCamera;
    public GameObject throwablePrefab;
    private GameObject sphere = null;
    public float throwForce = 50f;
    public float maxThrowForce = 4f;

    private float holdDownTimer;
    // Start is called before the first frame update
    void Start()
    {
        FPSCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            holdDownTimer = Time.time;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            float totalTime = Time.time - holdDownTimer;
            Throw(CalculateHoldDownForce(totalTime * 2f));
        }
    }

    private void Throw(float throwForce)
    {
        if (sphere != null)
        {
            Destroy(sphere);
        }

        sphere = Instantiate(throwablePrefab, transform.position, Quaternion.identity);
        sphere.GetComponent<Rigidbody>().AddForce(FPSCamera.transform.forward * throwForce, ForceMode.Impulse);
    }

    private float CalculateHoldDownForce(float holdTime)
    {
        float holdTimeNormalized = Mathf.Clamp01(holdTime / maxThrowForce);
        float force = holdTimeNormalized * throwForce;
        return force;
    }
}
