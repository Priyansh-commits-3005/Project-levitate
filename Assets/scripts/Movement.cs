using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Movement : MonoBehaviour

{
    [SerializeField]AudioClip mainEngine;

    [SerializeField]float upvelocity = 0f;
    [SerializeField]float rotateVelocity = 0f;
    [SerializeField] ParticleSystem rightThruster;
    [SerializeField] ParticleSystem leftThruster;
    [SerializeField] ParticleSystem upThruster;

    Rigidbody rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        processThrust();
        processRotation();
    }
    void processThrust(){
        if (Input.GetKey(KeyCode.Space))
            startThrusting();
        else
        {
            stopThrusting();
        }



    }



    void processRotation(){
        if(Input.GetKey(KeyCode.A))
        {
            rotatingLeft();

        }
        else
        {  
            leftThruster.Stop();
        }


        if(Input.GetKey(KeyCode.D))
        {
            rotatingRight();

        }
        else 
        { 
            rightThruster.Stop(); 
        }



    }

    private void rotatingRight()
    {
        rotation(-rotateVelocity);
        if (!rightThruster.isPlaying)
        {
            rightThruster.Play();
        }
    }

    private void rotatingLeft()
    {
        rotation(rotateVelocity);
        if (!leftThruster.isPlaying)
        {
            leftThruster.Play();
        }
    }

    private void rotation(float rotationThrustVelcoity)

    {
        rb.freezeRotation = true; //pausing the rotation by physics
        transform.Rotate(rotationThrustVelcoity * Vector3.forward * Time.deltaTime);
        rb.freezeRotation = false; //replaying the physics rotation
    }
    private void stopThrusting()
    {
        upThruster.Stop();
        audioSource.Stop();
    }

    private void startThrusting()
    {
        if (!upThruster.isPlaying)
        {
            upThruster.Play();
        }
        rb.AddRelativeForce(upvelocity * Vector3.up * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }

}