using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    Rigidbody rb;
    public GameObject flameMetal;
    public int objNum;
    public float shootRange, dirAndSpeed;
    // 0 = 탄피, 1 = 플레여탄환, 2 = 적군탄환, 3 = 돈아이템, 4 = 칼, 5 = 피와 플레임, 6 = 총기 겟
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(objNum == 0) // 탄피
        {
            int i = Random.Range(-30, 30);
            rb.AddForce(Vector3.up * 100);
            rb.AddForce(Vector3.right * i);
        }
    }
    void FixedUpdate()
    {
        if (objNum == 0 || objNum == 5) // 탄피 또는 피
        {
            Destroy(gameObject, 3f);
        }
        else if (objNum == 1) // 나의 권총과 라이플 탄환
        {
            if (Movement.lookRight == true)
            {
                rb.velocity = new Vector2(60, 0);
                Destroy(gameObject, 0.2f);
            }
            else if (Movement.lookRight == false)
            {
                rb.velocity = new Vector2(-60, 0);
                Destroy(gameObject, 0.2f);
            }
        }
        else if (objNum == 2) // 적 권총
        {
            if (Enemy.lookRight == true)
            {
                rb.velocity = new Vector2(12, 0);
                Destroy(gameObject, 1.6f);
            }
            else if (Enemy.lookRight == false)
            {
                rb.velocity = new Vector2(-12, 0);
                Destroy(gameObject, 1.6f);
            }
        }
        else if (objNum == 4) // 칼
        {
            if (Movement.lookRight == true)
            {
                rb.velocity = new Vector2(14, 0);
                Destroy(gameObject, 0.1f);
            }
            else if (Movement.lookRight == false)
            {
                rb.velocity = new Vector2(-14, 0);
                Destroy(gameObject, 0.1f);
            }
        }
        else if (objNum == 7) // 샷건 탄알
        {
            if (Movement.lookRight == true)
            {
                rb.velocity = new Vector2(60, 0);
                Destroy(gameObject, 0.16f);
            }
            else if (Movement.lookRight == false)
            {
                rb.velocity = new Vector2(-60, 0);
                Destroy(gameObject, 0.16f);
            }
        }
        else if (objNum == 8) // 샷건 탄알
        {
            if (Movement.lookRight == true)
            {
                rb.velocity = new Vector2(60, 0);
                Destroy(gameObject, 0.1f);
            }
            else if (Movement.lookRight == false)
            {
                rb.velocity = new Vector2(-60, 0);
                Destroy(gameObject, 0.1f);
            }
        }
        else if (objNum == 9) // 샷건 탄알
        {
            if (Movement.lookRight == true)
            {
                rb.velocity = new Vector2(60, 0);
                Destroy(gameObject, 0.06f);
            }
            else if (Movement.lookRight == false)
            {
                rb.velocity = new Vector2(-60, 0);
                Destroy(gameObject, 0.06f);
            }
        }
        else if (objNum == 10) // 기관단총 탄알
        {
            if (Movement.lookRight == true)
            {
                rb.velocity = new Vector2(60, 0);
                Destroy(gameObject, 0.18f);
            }
            else if (Movement.lookRight == false)
            {
                rb.velocity = new Vector2(-60, 0);
                Destroy(gameObject, 0.18f);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy" && (objNum == 1 || objNum == 4 || objNum == 7 || objNum == 8 || objNum == 9 || objNum == 10))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Player" && objNum == 2)
        {
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "Metal" && (objNum == 1 || objNum == 2 || objNum == 4 || objNum == 7 || objNum == 8 || objNum == 9 || objNum == 10))
        {
            SoundManagers.PlayAudio("Metal");
            Instantiate(flameMetal, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Player" && objNum == 3)
        {
            SoundManagers.PlayAudio("CashChang");
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Player" && objNum == 6)
        {
            SoundManagers.PlayShotAudio("Reload");
            Destroy(gameObject);
        }
    }
}