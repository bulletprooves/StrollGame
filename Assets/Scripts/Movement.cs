using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    public Animator anim;
    public GameObject slash, blood;
    public float speed, rollingPower, rollingCool = 0f;
    float damageTakenCool = 0f, rollingCurrentTime = 0f, jumpingCool = 0f, knifeCool, selfHealCool, shieldCool; //롤링커런트타임은 구르기 하는 동안 애니메이션 재생시간
    public static float hp, shieldGauge;
    bool isWalking, jumpingAllow, isJumping, ouchAllow, selfHeal, hidingAllow, shieldRecharge;
    public static bool isRolling, rollingAllow, lookRight, damageTaken, knifeAllow, isHiding, shieldAllow;
    int shit;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        lookRight = true; isWalking = false; isRolling = false; rollingAllow = true; ouchAllow = false; knifeAllow = false; isJumping = false; isHiding = false; hidingAllow = false;
        knifeCool = 0f; hp = 20; shieldGauge = 10; shieldAllow = true; shieldRecharge = false;
    }


    void Update()
    {
        // 체력
        if (UI.isPaused == false)
        {
            if (hp < 20 && selfHeal == true)
            {
                hp += 0.05f;
            }
            else if (hp <= 0) Destroy(gameObject);
            // 공격받음
            if (damageTaken == true)
            {
                if (damageTakenCool < 1f)
                {
                    damageTakenCool += 8 * Time.fixedDeltaTime;
                    if (damageTakenCool >= 1f)
                    {
                        damageTaken = false;
                        damageTakenCool = 0f;
                    }
                }
            }
        }
        if (selfHeal == false)
        {
            if (selfHealCool < 1f)
            {
                selfHealCool += 0.2f * Time.fixedDeltaTime;
                if (selfHealCool >= 1f)
                {
                    selfHeal = true;
                }
            }
        }
        // 방어 게이지
        if (Gun.isShield == true)
        {
            shieldGauge -= 0.1f;
            shieldRecharge = false;
        }
        if (UI.isPaused == false)
        {
            if (shieldGauge > 0f && shieldRecharge == true && Gun.isShield == false)
            {
                shieldAllow = true;
                shieldGauge += 0.08f;
                if (shieldGauge > 10f)
                {
                    shieldAllow = true;
                    shieldGauge = 10f;
                }
            }
            else if (shieldGauge <= 0f)
            {
                shieldAllow = false;
                shieldGauge += 0.08f;
                if (shieldGauge > 10f)
                {
                    shieldAllow = true;
                    shieldGauge = 10f;
                }
            }
            if (shieldRecharge == false)
            {
                if (shieldCool < 2f)
                {
                    shieldCool += 0.5f * Time.fixedDeltaTime;
                    if (shieldCool >= 2f)
                    {
                        shieldRecharge = true;
                        shieldCool = 0f;
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        // 숨기
        anim.SetBool("isHiding", isHiding);
        if (Input.GetKey(KeyCode.W) && hidingAllow == true)
        {
            if (Gun.isShake == false) isHiding = true;
        }
        else
        {
            isHiding = false;
            // 걷기
            anim.SetBool("isWalking", isWalking);
            if (Input.GetKey(KeyCode.A) && Gun.isShield == false)
            {
                isWalking = true; lookRight = false;
                transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
            }
            else if (Input.GetKey(KeyCode.D) && Gun.isShield == false)
            {
                isWalking = true; lookRight = true;
                transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
            }
            else isWalking = false;
            // 회전
            if (lookRight == true)
            {
                Vector3 scaleF = transform.localScale;
                scaleF.x = 1;
                transform.localScale = scaleF;
            }
            else
            {
                Vector3 scaleF = transform.localScale;
                scaleF.x = -1;
                transform.localScale = scaleF;
            }
            // 닷지롤
            anim.SetBool("isRolling", isRolling);
            if (rollingCool <= 2f)
            {
                rollingCool += 2.5f * Time.deltaTime;
                if (rollingCool >= 2f)
                {
                    //rollingCool = 0f;
                    rollingAllow = true;
                }
            }
            if (lookRight == true && Input.GetKeyDown(KeyCode.I) && rollingAllow == true)
            {
                rb.AddForce(Vector3.right * rollingPower * 1.5f);
                isRolling = true; rollingCurrentTime = 0f; rollingAllow = false; rollingCool = 0f;
            }
            else if (lookRight == false && Input.GetKeyDown(KeyCode.I) && rollingAllow == true)
            {
                rb.AddForce(Vector3.left * rollingPower * 1.5f);
                isRolling = true; rollingCurrentTime = 0f; rollingAllow = false; rollingCool = 0f;
            }
            else
            {
                if (rollingCurrentTime < 2f)
                {
                    rollingCurrentTime += 5 * Time.fixedDeltaTime;
                    if (rollingCurrentTime >= 2f)
                    {
                        rollingCurrentTime = 0f;
                        isRolling = false; //jumpingAllow = true;
                    }
                }
            }
            // 점프
            anim.SetBool("isJumping", isJumping);
            if (Input.GetKeyDown(KeyCode.L) && jumpingAllow == true && rollingAllow == true)
            {
                rb.AddForce(Vector3.up * rollingPower * 1.9f); jumpingCool = 0f;
                jumpingAllow = false; isJumping = true;
            }
            if (jumpingCool <= 2f)
            {
                jumpingCool += 2f * Time.fixedDeltaTime;
                if (jumpingCool >= 0.8f) isJumping = false;
                if (jumpingCool >= 2f) jumpingAllow = true;
            }
            // 근접공격
            if (knifeAllow == true && knifeCool < 1f)
            {
                knifeCool += 6f * Time.fixedDeltaTime;
                if (knifeCool >= 1f)
                {
                    SoundManagers.PlayAudio("KnifeSlash");
                    Instantiate(slash, transform.position, Quaternion.identity);
                    knifeAllow = false;
                    knifeCool = 0f;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TwoOpp" && isRolling == false)
        {
            hp -= 2;
            damageTaken = true; selfHeal = false; selfHealCool = 0f;
            SoundManagers.PlayAudio("HitImpact");            
            Instantiate(blood, transform.position, Quaternion.identity);
            // 무작위비명소리
            /*
            shit = Random.Range(0, 30);
            if (shit == 0 || shit == 2) SoundManagers.PlayAudio("Ahyaya");
            else if (shit == 1 || shit == 3) SoundManagers.PlayAudio("Aww");
            else if (shit == 4) SoundManagers.PlayAudio("Ahaaha");
            else if (shit == 5 || shit == 6) SoundManagers.PlayAudio("Awthatshurt");
            else if (shit == 7 || shit == 8) SoundManagers.PlayAudio("Owow");
            */
        }
        if (other.gameObject.tag == "Enemy" && isRolling == false)
        {
            knifeAllow = true;
        }
        if (other.gameObject.tag.Equals("RifleItem"))
        {
            Gun.weaponNum = 1;
            UI.remainAmmo = 30;
        }
        if (other.gameObject.tag == "SmgItem")
        {
            Gun.weaponNum = 2;
            UI.remainAmmo = 50;
        }
        if (other.gameObject.tag == "SgItem")
        {
            Gun.weaponNum = 3;
            UI.remainAmmo = 12;
        }
        if (other.gameObject.tag == "Cylinder")
        {
            hidingAllow = true;
        }
        if (other.gameObject.tag == "FromTownStartToTown")
        {
            SceneManager.LoadScene("Town");
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Cylinder")
        {
            hidingAllow = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Cylinder"))
        {
            hidingAllow = false;
        }
    }
}
