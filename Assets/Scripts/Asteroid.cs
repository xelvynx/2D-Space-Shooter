using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private int _speed = 10;
    [SerializeField] private GameObject _explosionPrefab;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1f) * Time.deltaTime * _speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position,Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject, .25f);
        }

    }
}
