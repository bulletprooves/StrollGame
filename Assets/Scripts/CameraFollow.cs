using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothDamp;
    Animator anim;
    float i, ii;

    void Start()
    {
        anim = GetComponent<Animator>();
        i = 0.05f; ii = 0.1f;
    }
    void FixedUpdate()
    {
        Vector3 dirPos = target.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, dirPos, smoothDamp);
        transform.position = smoothPos;
        transform.LookAt(target);
    }
    void Update()
    {
        if (Movement.lookRight == true) offset = new Vector3(-0.5f, 1, -9);
        else offset = new Vector3(0.5f, 1, -9);
        // 사격 후 화면 반동
        if (Gun.isShake == true)
        {
            i = Random.Range(-0.05f, 0.05f);
            transform.position = new Vector3(transform.position.x + i, transform.position.y + i, transform.position.z);
        }
        else if (Gun.isShake == false)
        {
            i = 0f;
            transform.position = transform.position;
        }
        // 공격 받고 화면 흔들림
        if (Movement.damageTaken == true)
        {
            ii = Random.Range(-0.25f, 0.25f);
            transform.position = new Vector3(transform.position.x + ii, transform.position.y + ii, transform.position.z);
        }
        else if (Movement.damageTaken == false)
        {
            ii = 0f;
            transform.position = transform.position;
        }
    }
}
