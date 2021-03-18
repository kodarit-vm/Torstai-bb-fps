using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{

    Camera FPSCamera;
    public GameObject throwablePrefab;
    private GameObject sphere = null;
    public float throwForce = 80f;
    public float maxThrowForce = 4f;
    private float holdDownTimer;

    // Raycast
    public float range = 10f;
    public float pushForce = 20f;

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

        if (Input.GetButtonDown("Fire2"))
        {
            ProcessRayCast();
        }
    }

    private void ProcessRayCast()
    {
        RaycastHit hit;

        if (Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward, out hit, range))
        {
            Rigidbody rb = hit.collider.gameObject.GetComponent<Rigidbody>();
            if (rb == null)
            {
                return;
            }

            rb.AddForce(FPSCamera.transform.forward * pushForce, ForceMode.Impulse);
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
