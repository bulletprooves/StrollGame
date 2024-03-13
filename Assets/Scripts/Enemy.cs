using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;
    Light light;
    AudioSource audioSource;
    public Transform target;
    public GameObject bullet, copCorpse, blood;
    bool detected, fireAllow, isWalking, isRolling, rollingAllow, jumping; //파여얼로우가 트루여야 발사
    public float detectRange, rangeToOpenFire, fireRate, speed, rollingPower; // 디텍트렌지는 적이 따라오는 최초 시작 거리, 렌지투오픈파여는 발사 사정권
    float fireRecoil, rollingCool, rollingCurrentTime, i, ii; // i 는 fireRate의 변하지 않는 고정 값 저장소, ii는 빛의 유지정도의 타임델타 값 저장소
    float intermRate = 0;
    public int objNum, hp;
    public static bool lookRight;

    void Start()
    {
        light = GetComponent<Light>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        fireRecoil = 0f; ii = 0f; i = fireRate; rollingCurrentTime = 3f; rollingCool = 3f;
        fireAllow = false; detected = false; isWalking = false; rollingAllow = true; jumping = false;
    }

    void Update()
    {
        
        if (hp <= 0)
        {
            SoundManagers.PlayAudio("Ohh");
            Instantiate(copCorpse, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        // 적군의 감지
        if (Vector3.Distance(transform.position, target.position) <= detectRange)
        {
            detected = true;
            if (transform.position.x < target.position.x)
            {
                Vector3 scaleF = transform.localScale;
                scaleF.x = 1;
                transform.localScale = scaleF;
                lookRight = true;
            }
            else
            {
                Vector3 scaleF = transform.localScale;
                scaleF.x = -1;
                transform.localScale = scaleF;
                lookRight = false;
            }
            if (Vector3.Distance(transform.position, target.position) <= rangeToOpenFire && Vector3.Distance(transform.position, target.position) > rangeToOpenFire -12f)
            {
                fireAllow = true;
            }
            else fireAllow = false;
        }
        else detected = false;
        // 적군 사정권
        if (fireAllow == true && UI.isPaused == false)
        {
            if (fireRate > 0f)
            {
                fireRate -= (intermRate + 1) * Time.fixedDeltaTime;
                if (fireRate <= 0f)
                {
                    ii = 0f;                
                    SoundManagers.PlayShotAudio("PistolFar");
                    if (objNum == 0) SoundManagers.PlayShotAudio("Pistol");
                    else if (objNum == 1) SoundManagers.PlayShotAudio("Rifle");
                    else if (objNum == 2) SoundManagers.PlayShotAudio("Silencer");
                    Instantiate(bullet, transform.position, Quaternion.identity);
                    //audioSource.Play();
                    fireRate = i; intermRate = Random.Range(-0.5f, 2);
                }
            }
        }
        else if (fireAllow == false) fireRate = i;
    }

    void FixedUpdate()
    {
        // 걷는 모션
        anim.SetBool("isWalking", isWalking);
        if (Vector3.Distance(transform.position, target.position) <= detectRange && Vector3.Distance(transform.position, target.position) >= rangeToOpenFire)
        {
            if (lookRight == true) transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
            else if (lookRight == false) transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
            isWalking = true;
        }
        else if (Vector3.Distance(transform.position, target.position) < rangeToOpenFire - 9f)
        {
            if (lookRight == true) transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
            else if (lookRight == false) transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
            isWalking = true;
        }
        else isWalking = false;
        // 구르기 모션
        anim.SetBool("isRolling", isRolling);
        if (rollingCool <= 3f)
        {
            rollingAllow = false;
            rollingCool += 1f * Time.deltaTime;
            if (rollingCool >= 3f)
            {
                //rollingCool = 0f;
                rollingAllow = true;
            }
        }
        if (rollingCurrentTime < 2f)
        {
            rollingCurrentTime += 6 * Time.fixedDeltaTime;
            if (rollingCurrentTime >= 2f)
            {
                rollingCurrentTime = 0f;
                isRolling = false;
            }
        }
        // 구르기 조건
        if (Vector3.Distance(transform.position, target.position) < 2.5f)
        {
            if (lookRight == true && rollingAllow == true)
            {
                rb.AddForce(Vector3.left * rollingPower); isRolling = true; rollingCool = 0f; rollingCurrentTime = 0f;
            }
            else if (lookRight == false && rollingAllow == true)
            {
                rb.AddForce(Vector3.right * rollingPower); isRolling = true; rollingCool = 0f; rollingCurrentTime = 0f;
            }
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Three")
        {
            hp -= 3;
            Instantiate(blood, transform.position, Quaternion.identity);
            SoundManagers.PlayAudio("HitLessImpact");
        }
        if (other.gameObject.tag == "Two")
        {
            hp -= 2;
            Instantiate(blood, transform.position, Quaternion.identity);
            SoundManagers.PlayAudio("HitLessImpact");
        }
        if (other.gameObject.tag == "Six")
        {
            hp -= 6;
            Instantiate(blood, transform.position, Quaternion.identity);
            SoundManagers.PlayAudio("HitLessImpact");
        }
        if (other.gameObject.tag.Equals("Ten"))
        {
            hp -= 25;
            Instantiate(blood, transform.position, Quaternion.identity);
        }
        if (other.gameObject.tag == "Metal")
        {
            rb.AddForce(Vector3.up * 1200);
        }
    }
}