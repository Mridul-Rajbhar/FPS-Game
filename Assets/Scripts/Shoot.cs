using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    #region Private 
    AudioSource audioSource;
    SoundFiles soundFiles;
    bool shooting;
    EnemyAI enemy;
    float lastShot = 0.0f;
    int bulletsFired, bulletsLeft;
    
    AnimationScript animationScript;
    #endregion


    #region Public 

    public Image aim;
    public TMP_Text bulletsLeftText;
    public ParticleSystem muzzleEffect;
    int layerMask = (1<<12) | (1<<9) | (1<<11);
    public Transform attackPosition;
    public GameObject bulletHole, bullet, impactEffect;
    public Camera fpsCamera;
    public int bulletsPerTap, mazagine;
    public bool allowHoldButton;
    public float range, damage, Force, fireRate, timeBetweenBullets, spreadBullets;
    #endregion

    void Awake()
    {
        bulletsFired = 0;
        bulletsLeft = mazagine;
        animationScript = GetComponent<AnimationScript>();
        soundFiles = transform.root.GetComponent<SoundFiles>();
        audioSource = transform.root.GetComponent<AudioSource>();
    }


    private void Update()
    {
        Ray ray = fpsCamera.ViewportPointToRay( new Vector3(0.5f, 0.5f, 0f));
        Debug.DrawRay(fpsCamera.transform.position, fpsCamera.transform.forward * range, Color.red);
        if(Physics.Raycast(ray, range))
        {
            aim.color = Color.red;    
        }
        else
        {
            aim.color = Color.yellow;
        }

        bulletsLeftText.text = "Bullets Left: " + bulletsLeft + "/" + mazagine;

        if(bulletsLeft!=mazagine && Input.GetKey(KeyCode.Mouse1))
        {
            Invoke("Reload", 1f);
        }

        if (Time.time > fireRate + lastShot && bulletsLeft>0)
        {
            if (allowHoldButton)
            {
                shooting = Input.GetKey(KeyCode.Mouse0);
                
            }
            else
            {
                shooting = Input.GetKeyDown(KeyCode.Mouse0);
                
            }
            if (shooting)
            {
                muzzleEffect.Play();
                ShootBullet();
                if (allowHoldButton)
                    animationScript.holdFire(true);

            }
            
            if(!shooting && allowHoldButton)
            {
                muzzleEffect.Stop();
                animationScript.holdFire(false);
            }
        }

    
    }

    void ShootBullet()
    {

        if(transform.gameObject.name == "MachineGun")
        {
            soundFiles.MachineGun(audioSource);
        }
        else if (transform.gameObject.name == "SciFiGunLightBlack")
        {
            soundFiles.RiffleSound(audioSource);
        }
        else if (transform.gameObject.name == "LowPoly Scifi_Gun")
        {
            soundFiles.ShotGunSound(audioSource);
        }
        else if (transform.gameObject.name == "SciFiGun_Diffuse")
        {
            soundFiles.PistolSound(audioSource);
        }


            float x = Random.Range(-spreadBullets, spreadBullets);
        float y = Random.Range(-spreadBullets, spreadBullets);

        
        lastShot = Time.time;

        Vector3 target;
        Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range, layerMask))
        {
            target = hit.point;
            GameObject currentBulletHole;
            if (hit.transform.root.tag == "Enemy")
            {
                currentBulletHole = Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal), hit.transform);
                //currentBulletHole.transform.localScale = new Vector3(1f, 1f, 1f);
                hit.transform.root.GetComponent<EnemyAI>().TakeDamage(damage);
            }
            else
            { 
               currentBulletHole = Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));
            }
            

            GameObject tempEffect = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(tempEffect, 2f);
            Destroy(currentBulletHole, 3f);
        }
        else
        {
            target = ray.GetPoint(range);
        }

        Vector3 direction = target - attackPosition.position + new Vector3(x,y,0f);

        GameObject currentBullet = Instantiate(bullet, attackPosition.position, attackPosition.rotation);
        currentBullet.GetComponent<Rigidbody>().AddForce(direction * Force, ForceMode.Impulse);
        Destroy(currentBullet, 1.5f);
        bulletsFired++;
        bulletsLeft--;
        if (bulletsLeft > 0)
        {
            if (bulletsFired < bulletsPerTap)
            {
                Invoke("ShootBullet", timeBetweenBullets);
            }
            else
            {
                bulletsFired = 0;
            }
        }
        else
        {
            bulletsFired = 0;
        }
    }

    void Reload()
    {
        bulletsLeft = mazagine;
    }

}