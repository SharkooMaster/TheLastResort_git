using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;
using TMPro;

public class S_Item_Gun : MonoBehaviour
{
    public Item item;

    private void Start()
    {
        item = GetComponent<Item>();

        textAmmo = transform.parent.parent.GetComponent<S_playerCamera>().textAmmo;
        textTotAmmo = transform.parent.parent.GetComponent<S_playerCamera>().textTotAmmo;
    }

    public int damage = 15;
    public string type = "bolt";
    public float bulletForce = 2;

    public int totAmmo;
    public int ammo;
    public int mag;

    public float recoil = 1f;
    float nextFire = 0;

    public float reloadSpeed = 1f;
    public float nextReloadSpeed = 0;
    public bool isReload = false;

    public TMP_Text textAmmo;
    public TMP_Text textTotAmmo;

    private void Update()
    {
        if(item.isActive)
        {
            switch (type)
            {
                case "bolt":
                    if ((Input.GetMouseButtonDown(0) || Input.GetAxis("xbox shoot") > 0) && Time.time >= nextFire)
                    {
                        if (ammo != 0)
                        {
                            nextFire = Time.time + recoil;
                            shoot();
                            ammo -= 1;
                            
                            if (ammo == 0)
                            {
                                AudioSource.PlayClipAtPoint(emptySFX, transform.position);
                                GetComponent<S_Recoil>().gunState("empty");
                            }

                            GetComponent<S_Recoil>().trigger();
                        }
                        else
                        {
                            AudioSource.PlayClipAtPoint(emptySFX, transform.position);
                            GetComponent<S_Recoil>().gunState("empty");
                        }
                        textAmmo.text = ammo.ToString();
                        textTotAmmo.text = totAmmo.ToString();
                    }
                    break;
                case "auto":
                    if ((Input.GetMouseButton(0) || Input.GetAxis("xbox shoot") > 0) && Time.time >= nextFire)
                    {
                        if (ammo != 0)
                        {
                            nextFire = Time.time + recoil;
                            shoot();
                            ammo -= 1;
                            
                            if (ammo == 0)
                            {
                                AudioSource.PlayClipAtPoint(emptySFX, transform.position);
                                GetComponent<S_Recoil>().gunState("empty");
                            }

                            GetComponent<S_Recoil>().trigger();
                        }
                        else
                        {
                            AudioSource.PlayClipAtPoint(emptySFX, transform.position);
                        }
                        textAmmo.text = ammo.ToString();
                        textTotAmmo.text = totAmmo.ToString();
                    }
                    break;
                default:
                    break;
            }

            if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Reload"))
            {
                if (totAmmo > 0)
                {
                    nextReloadSpeed = Time.time + reloadSpeed;
                    AudioSource.PlayClipAtPoint(clipOutSFX, transform.position);
                    if (totAmmo >= mag && ammo < mag)
                    {
                        int c = mag - ammo;
                        ammo = mag;
                        totAmmo -= c;
                    }
                    else if(totAmmo < mag)
                    {
                        ammo = totAmmo;
                        totAmmo = 0;
                    }
                    GetComponent<S_Recoil>().gunState("");
                    isReload = true;
                    textAmmo.text = ammo.ToString();
                    textTotAmmo.text = totAmmo.ToString();
                }
            }

            if (Time.time >= nextReloadSpeed && isReload)
            {
                AudioSource.PlayClipAtPoint(clipInSFX, transform.position);
                isReload = false;
            }
        }
    }

    public AudioClip shootSFX;
    public AudioClip emptySFX;
    public AudioClip clipOutSFX;
    public AudioClip clipInSFX;
    public GameObject shotDecal;

    private Vector3 sprayOffset;
    public Vector3 sprayOffsetVal;

    void randSpray()
    {
        sprayOffset = new Vector3(UnityEngine.Random.Range(-sprayOffsetVal.x, sprayOffsetVal.x),
            UnityEngine.Random.Range(-sprayOffsetVal.y, sprayOffsetVal.y), 
            UnityEngine.Random.Range(-sprayOffsetVal.z, sprayOffsetVal.z));
    }

    public void playShot()
    {
        AudioSource.PlayClipAtPoint(shootSFX, transform.position);
    }

    public void shoot()
    {
        AudioSource.PlayClipAtPoint(shootSFX, transform.position);
        randSpray();

        RaycastHit hit;
        if (Physics.Raycast(transform.parent.parent.position + sprayOffset, transform.parent.parent.forward, out hit, 100))
        {
            Debug.DrawLine(transform.position, hit.point);
            if (hit.transform.tag == "Damagable" || hit.transform.tag == "Player")
            {
                hit.transform.GetComponent<S_Health>().damage(damage);
                print(hit.transform.tag);
            }
            if(hit.transform.GetComponent<Rigidbody>())
            {
                hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(transform.parent.parent.forward * bulletForce, hit.transform.position);
            }
            GameObject go = Instantiate(shotDecal);
            go.transform.position = hit.point;
            go.transform.LookAt(transform.parent.parent.position);
            StartCoroutine(killDecal(go));
        }
    }

    private IEnumerator killDecal(GameObject go)
    {
        yield return new WaitForSeconds(8);
        Destroy(go);
    }
}
