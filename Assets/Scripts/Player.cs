using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    public NoiseMaker noiseMaker;

    private Transform tf;

    public float rotationSpeed = 1.0f;

    public float movementSpeed = 1.0f;

    public float m_health = 10;
    // Start is called before the first frame update
    void Start()
    {
        tf = gameObject.GetComponent<Transform>();
        noiseMaker = gameObject.GetComponent<NoiseMaker>();
        GameManager.instance.Player = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        noiseMaker.m_CurrentNoiseLevel = 0;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            noiseMaker.m_CurrentNoiseLevel += 1;
            tf.position += tf.up * movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            tf.position -= tf.up * movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            tf.position -= tf.right * movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            tf.position += tf.right * movementSpeed * Time.deltaTime;
        }

        
    }

    public void reduceHealth(float amountToReduce)
    {
        m_health -= amountToReduce;
        if (checkDead() == true)
        {
            Destroy(this.gameObject);
        }
    }

    private bool checkDead()
    {
        if (m_health > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}
