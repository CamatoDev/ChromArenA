using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float range = 10f;
    public float speed = 10f;
    private Vector3 _startPos;
    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
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
}
