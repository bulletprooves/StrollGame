using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    SpriteRenderer rend;
    Light light; Vector3 shellOut, barrel;
    Collider collider;
    public Sprite pistolIdle, pistolFired, knife, rifleIdle, rifleFired, shield
        , smgIdle, smgFired, sgIdle, sgFired;
    public GameObject bulletShell, bullet, bulletTwo, bulletRifle, bulletSixOne, bulletSixTwo, bulletSixThree;
    float noneCool, knifeCool, pistolCool, pistolRecoil, rifleCool, rifleRecoil, smgCool, sgCool;
    bool pistolAllow, rifleAllow, smgAllow, sgAllow;
    public static bool isShake, isShield;
    public static int weaponNum;
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        light = GetComponent<Light>();
        collider = GetComponent<Collider>();
        pistolFired = Resources.Load<Sprite>("PistolFired");
        pistolIdle = Resources.Load<Sprite>("PistolIdle");
        knife = Resources.Load<Sprite>("Knife");
        rifleIdle = Resources.Load<Sprite>("RifleIdle");
        rifleFired = Resources.Load<Sprite>("RifleFired");
        shield = Resources.Load<Sprite>("Shield");
        smgIdle = Resources.Load<Sprite>("SmgIdle");
        smgFired = Resources.Load<Sprite>("SmgFired");
        sgIdle = Resources.Load<Sprite>("SgIdle");
        sgFired = Resources.Load<Sprite>("SgFired");
        rend.sprite = pistolIdle; isShake = false; pistolAllow = true;
        light.intensity = 0; weaponNum = 0;
        if (weaponNum == 0) rend.sprite = pistolIdle;
        collider.enabled = false; isShield = false;
    }
    void FixedUpdate()
    {
        // 구르기에 관한 총 스크립트
        if (Movement.isRolling == true || Movement.isHiding == true)
        {
            Color color = rend.color;
            color.a = 0f;
            rend.color = color;
        }
        else
        {
            Color color = rend.color;
            color.a = 1f;
            rend.color = color;
            // 권총 쿨타임
            if (pistolAllow == false && weaponNum == 0)
            {
                if (pistolCool < 0.17f)
                {
                    pistolCool += 1f * Time.fixedDeltaTime;
                    if (pistolCool >= 0.17f)
                    {
                        pistolAllow = true;
                    }
                }
            }
            //  라이플 쿨타임
            if (rifleAllow == false && weaponNum == 1)
            {
                if (rifleCool < 0.1f)
                {
                    rifleCool += 1f * Time.fixedDeltaTime;
                    if (rifleCool >= 0.1f)
                    {
                        rifleAllow = true;
                    }
                }
            }
            // 기관단총 쿨타임
            if (smgAllow == false && weaponNum == 2)
            {
                if (smgCool < 0.08f)
                {
                    smgCool += 1f * Time.fixedDeltaTime;
                    if (smgCool >= 0.08f)
                    {
                        smgAllow = true;
                    }
                }
            }
            // 샷건 쿨타임
            if (sgAllow == false && weaponNum == 3)
            {
                if (sgCool < 0.4f)
                {
                    sgCool += 1f * Time.fixedDeltaTime;
                    if (sgCool >= 0.4f)
                    {
                        sgAllow = true;
                    }
                }
            }
            // 권총 사격
            if (weaponNum == 0)
            {
                // 방패
                if (Input.GetKey(KeyCode.O) && Movement.knifeAllow == false &&Movement.isRolling == false && pistolAllow == true && Movement.shieldAllow == true)
                {
                    rend.sprite = shield; isShield = true; collider.enabled = true;
                }
                else if (Input.GetKey(KeyCode.K) && pistolAllow == true && Movement.knifeAllow == false && weaponNum == 0)
                {
                    SoundManagers.PlayShotAudio("Pistol");
                    SoundManagers.PlayShotAudio("PistolFar");
                    noneCool = 0f; pistolAllow = false; pistolCool = 0f;
                    shellOut = transform.position; isShield = false; collider.enabled = false;
                    if (Movement.lookRight == true) barrel = new Vector3(transform.position.x + 1, transform.position.y, 0);
                    else if (Movement.lookRight == false) barrel = new Vector3(transform.position.x - 1, transform.position.y, 0);
                    Instantiate(bulletShell, shellOut, Quaternion.identity);
                    Instantiate(bullet, barrel, Quaternion.identity);
                }
                else
                {
                    rend.sprite = pistolIdle;
                    collider.enabled = false; isShield = false;
                    if (noneCool < 1f)
                    {
                        rend.sprite = pistolFired;
                        light.intensity = 1; isShake = true;
                        noneCool += 20 * Time.fixedDeltaTime;
                        if (noneCool >= 1f)
                        {
                            rend.sprite = pistolIdle; isShake = false;
                            light.intensity = 0;
                        }
                    }
                }
            }
            // 라이플 사격
            else if (weaponNum == 1)
            {
                if (UI.remainAmmo > 0) {
                    // 방패
                    if (Input.GetKey(KeyCode.O) && Movement.knifeAllow == false && Movement.isRolling == false && rifleAllow == true && Movement.shieldAllow == true)
                    {
                        rend.sprite = shield; isShield = true; collider.enabled = true;
                    }
                    else if (Input.GetKey(KeyCode.K) && rifleAllow == true && Movement.knifeAllow == false && weaponNum == 1)
                    {
                        SoundManagers.PlayShotAudio("Rifle");
                        SoundManagers.PlayShotAudio("RifleFar");
                        noneCool = 0f; rifleAllow = false; rifleCool = 0f;
                        shellOut = transform.position; isShield = false; collider.enabled = false;
                        if (Movement.lookRight == true) barrel = new Vector3(transform.position.x + 1, transform.position.y - 0.2f, 0);
                        else if (Movement.lookRight == false) barrel = new Vector3(transform.position.x - 1, transform.position.y - 0.2f, 0);
                        Instantiate(bulletShell, shellOut, Quaternion.identity);
                        Instantiate(bulletRifle, barrel, Quaternion.identity);
                        UI.remainAmmo -= 1;
                    }
                    else
                    {
                        rend.sprite = rifleIdle;
                        collider.enabled = false; isShield = false;
                        if (noneCool < 1f)
                        {
                            rend.sprite = rifleFired;
                            light.intensity = 1; isShake = true;
                            noneCool += 20 * Time.fixedDeltaTime;
                            if (noneCool >= 1f)
                            {
                                rend.sprite = rifleIdle; isShake = false;
                                light.intensity = 0;
                            }
                        }
                    }
                }
                else if (UI.remainAmmo <= 0) weaponNum = 0;
            }
            // 기관단총 사격
            else if (weaponNum == 2)
            {
                if (UI.remainAmmo > 0)
                {
                    // 방패
                    if (Input.GetKey(KeyCode.O) && Movement.knifeAllow == false && Movement.isRolling == false && smgAllow == true && Movement.shieldAllow == true)
                    {
                        rend.sprite = shield; isShield = true; collider.enabled = true;
                    }
                    else if (Input.GetKey(KeyCode.K) && smgAllow == true && Movement.knifeAllow == false && weaponNum == 2)
                    {
                        SoundManagers.PlayShotAudio("Silencer");
                        noneCool = 0f; smgAllow = false; smgCool = 0f;
                        shellOut = transform.position; isShield = false; collider.enabled = false;
                        if (Movement.lookRight == true) barrel = new Vector3(transform.position.x + 1, transform.position.y, 0);
                        else if (Movement.lookRight == false) barrel = new Vector3(transform.position.x - 1, transform.position.y, 0);
                        Instantiate(bulletShell, shellOut, Quaternion.identity);
                        Instantiate(bulletTwo, barrel, Quaternion.identity);
                        UI.remainAmmo -= 1;
                    }
                    else
                    {
                        rend.sprite = smgIdle;
                        collider.enabled = false; isShield = false;
                        if (noneCool < 1f)
                        {
                            rend.sprite = smgFired;
                            light.intensity = 0; isShake = true;
                            noneCool += 20 * Time.fixedDeltaTime;
                            if (noneCool >= 1f)
                            {
                                rend.sprite = smgIdle; isShake = false;
                                light.intensity = 0;
                            }
                        }
                    }
                }
                else if (UI.remainAmmo <= 0) weaponNum = 0;
            }
            // 샷건 사격
            else if (weaponNum == 3)
            {
                if (UI.remainAmmo > 0)
                {
                    // 방패
                    if (Input.GetKey(KeyCode.O) && Movement.knifeAllow == false && Movement.isRolling == false && sgAllow == true && Movement.shieldAllow == true)
                    {
                        rend.sprite = shield; isShield = true; collider.enabled = true;
                    }
                    else if (Input.GetKey(KeyCode.K) && sgAllow == true && Movement.knifeAllow == false && weaponNum == 3)
                    {
                        SoundManagers.PlayShotAudio("Sg");
                        SoundManagers.PlayShotAudio("SgFar");
                        noneCool = 0f; sgAllow = false; sgCool = 0f;
                        shellOut = transform.position; isShield = false; collider.enabled = false;
                        if (Movement.lookRight == true) barrel = new Vector3(transform.position.x + 1, transform.position.y, 0);
                        else if (Movement.lookRight == false) barrel = new Vector3(transform.position.x - 1, transform.position.y, 0);
                        Instantiate(bulletShell, shellOut, Quaternion.identity);
                        Instantiate(bulletSixOne, barrel, Quaternion.identity);
                        Instantiate(bulletSixTwo, barrel, Quaternion.identity);
                        Instantiate(bulletSixThree, barrel, Quaternion.identity);
                        UI.remainAmmo -= 1;
                    }
                    else
                    {
                        rend.sprite = sgIdle;
                        collider.enabled = false; isShield = false;
                        if (noneCool < 1f)
                        {
                            rend.sprite = sgFired;
                            light.intensity = 1; isShake = true;
                            noneCool += 20 * Time.fixedDeltaTime;
                            if (noneCool >= 1f)
                            {
                                rend.sprite = sgIdle; isShake = false;
                                light.intensity = 0;
                            }
                        }
                    }
                }
                else if (UI.remainAmmo <= 0) weaponNum = 0;
            }
            // 칼 꺼내고 넣기
            if (Movement.knifeAllow == true && Movement.isRolling == false && isShield == false)
            {
                rend.sprite = knife; knifeCool = 0f;
                if (knifeCool < 1f)
                {
                    knifeCool += 20 * Time.fixedDeltaTime;
                    if (knifeCool >= 1f)
                    {
                        if (weaponNum == 0) rend.sprite = pistolIdle;
                        else if (weaponNum == 1) rend.sprite = rifleIdle;
                        else if (weaponNum == 2) rend.sprite = smgIdle;
                        else if (weaponNum == 3) rend.sprite = sgIdle;
                    }
                }
            }
        }
        
    }
}
