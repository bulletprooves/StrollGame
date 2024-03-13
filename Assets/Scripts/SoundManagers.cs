using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagers : MonoBehaviour
{
    public static AudioClip pistol, pistolFar, rifle, rifleFar, metal, reload, silencer, sg, sgFar, 
        cashChang, ohh, aww, ahaaha, ahyaya, hurt, owow, knifeSlash, hitImpact, hitLessImpact;
    static AudioSource audioSrc;

    void Start()
    {
        pistol = Resources.Load<AudioClip>("Pistol");
        pistolFar = Resources.Load<AudioClip>("PistolFar");
        rifle = Resources.Load<AudioClip>("Rifle");
        rifleFar = Resources.Load<AudioClip>("RifleFar");
        sg = Resources.Load<AudioClip>("Sg");
        sgFar = Resources.Load<AudioClip>("SgFar");
        cashChang = Resources.Load<AudioClip>("CashChang");
        ohh = Resources.Load<AudioClip>("Ohh");
        aww = Resources.Load<AudioClip>("Aww");
        ahaaha = Resources.Load<AudioClip>("Ahaaha");
        knifeSlash = Resources.Load<AudioClip>("KnifeSlash");
        ahyaya = Resources.Load<AudioClip>("Ahyaya");
        hurt = Resources.Load<AudioClip>("Awthatshurt");
        owow = Resources.Load<AudioClip>("Owow");
        hitImpact = Resources.Load<AudioClip>("HitImpact");
        hitLessImpact = Resources.Load<AudioClip>("HitLessImpact");
        metal = Resources.Load<AudioClip>("Metal");
        reload = Resources.Load<AudioClip>("Reload");
        silencer = Resources.Load<AudioClip>("Silencer");
        audioSrc = GetComponent<AudioSource>();
    }
    public static void PlayShotAudio(string clip)
    {
        switch (clip)
        {
            case "Pistol":
                audioSrc.PlayOneShot(pistol);
                break;

            case "PistolFar":
                audioSrc.PlayOneShot(pistolFar);
                break;

            case "Rifle":
                audioSrc.PlayOneShot(rifle);
                break;

            case "RifleFar":
                audioSrc.PlayOneShot(rifleFar);
                break;

            case "Reload":
                audioSrc.PlayOneShot(reload);
                break;

            case "Silencer":
                audioSrc.PlayOneShot(silencer);
                break;

            case "Sg":
                audioSrc.PlayOneShot(sg);
                break;

            case "SgFar":
                audioSrc.PlayOneShot(sgFar);
                break;

        }
    }

    public static void PlayAudio(string clip)
    {
        switch (clip)
        {
            case "CashChang":
                audioSrc.PlayOneShot(cashChang);
                break;

            case "Ohh":
                audioSrc.PlayOneShot(ohh);
                break;

            case "Aww":
                audioSrc.PlayOneShot(aww);
                break;

            case "Ahaaha":
                audioSrc.PlayOneShot(ahaaha);
                break;

            case "KnifeSlash":
                audioSrc.PlayOneShot(knifeSlash);
                break;

            case "Ahyaya":
                audioSrc.PlayOneShot(ahyaya);
                break;

            case "Awthatshurt":
                audioSrc.PlayOneShot(hurt);
                break;

            case "Owow":
                audioSrc.PlayOneShot(owow);
                break;

            case "HitImpact":
                audioSrc.PlayOneShot(hitImpact);
                break;

            case "HitLessImpact":
                audioSrc.PlayOneShot(hitLessImpact);
                break;

            case "Metal":
                audioSrc.PlayOneShot(metal);
                break;
        }
    }
}
