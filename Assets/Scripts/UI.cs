using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    Vector3 hpScale, shieldScale;
    Text ammo;
    public GameObject pauseMenu, gameOverMenu, PressToStart, MainMenu, PlayList, lowHp, moreLowHp, killingSpreeList;
    public bool isHp = false, isShield = false, isAmmoCounter = false;
    public static bool isPaused;
    public static int remainAmmo = 0;
    void Start()
    {
        ammo = GetComponent<Text>();
        if (isHp == true) hpScale = transform.localScale;
        if (isShield == true) shieldScale = transform.localScale;
        isPaused = false;
        lowHp.SetActive(false);
        moreLowHp.SetActive(false);
    }

    void Update()
    {
        if (isAmmoCounter) ammo.text = "" + remainAmmo;
        if (isHp == true)
        {
            hpScale.x = 0.05f * Movement.hp; // 체력이 int로 20이라서 float에서의 1f로 바꾸기 위해 0.05를 곱해 1로만듬. 아래 쉴드 게이지도 같음
            transform.localScale = hpScale;
        }
        if (isShield == true)
        {
            shieldScale.x = 0.1f * Movement.shieldGauge;
            transform.localScale = shieldScale;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }
        if (Movement.hp > 10)
        {
            lowHp.SetActive(false);
            moreLowHp.SetActive(false);
        }
        if (Movement.hp <= 10)
        {
            lowHp.SetActive(true);
            moreLowHp.SetActive(false);
        }
        if (Movement.hp <= 5)
        {
            lowHp.SetActive(false);
            moreLowHp.SetActive(true);
        }
        // 게임오버
        if (Movement.hp <= 0)
        {
            lowHp.SetActive(false);
            moreLowHp.SetActive(false);
            GameOver();
        }
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void Quit()
    {
        gameOverMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("Main");
    }
    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void PressToStarted()
    {
        PressToStart.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void Play()
    {
        MainMenu.SetActive(false);
        PlayList.SetActive(true);
    }
    public void RobberyOne()
    {
        SceneManager.LoadScene("TownStart");
    }
    public void JohnOne()
    {
        SceneManager.LoadScene("Club");
    }
    public void KillingSpree()
    {
        MainMenu.SetActive(false);
        killingSpreeList.SetActive(true);
    }
}
