using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    // Start is called before the first frame update
    void Start()
    {
        //Take the current position = new position (0,0,0)
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

                           //Vector3(1,0,0) * 5 * real time
        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
    }
}
