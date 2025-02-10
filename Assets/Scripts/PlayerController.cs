using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 3.0f;

    // Start is called before the first frame update;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float horizInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizInput * speed * Time.deltaTime);

        float vertInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * vertInput * speed * Time.deltaTime);
    }
}