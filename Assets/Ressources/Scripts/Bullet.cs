using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float range = 10f;
    public float speed = 10f;
    private Vector3 _startPos;
    //private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        //rend = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(_startPos, transform.position);

        if (distance <= range)
            transform.position += transform.forward * Time.deltaTime * speed;
        else
            Destroy(gameObject, 0.1f);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Obstacle"))
    //    {
    //        Debug.Log("Cible touché...");
    //        other.transform.GetComponent<Renderer>().material.color = rend.material.color;
    //        Destroy(gameObject);
    //    }
    //}
}
