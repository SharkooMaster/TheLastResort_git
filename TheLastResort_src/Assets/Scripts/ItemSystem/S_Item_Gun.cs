using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class S_Item_Gun : MonoBehaviour
{
    public Item item;

    private void Start()
    {
        item = GetComponent<Item>();
    }

    public int damage = 15;
    public string type = "bolt";

    public int ammo;
    public int mag;

    public float recoil = 1f;
    float nextFire = 0;

    private void Update()
    {
        if(item.isActive)
        {
            switch (type)
            {
                case "bolt":
                    if (Input.GetMouseButtonDown(0) && Time.time >= nextFire)
                    {
                        nextFire = Time.time + recoil;
                        shoot();
                        GetComponent<S_Recoil>().trigger();
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public AudioClip shootSFX;

    private void shoot()
    {
        AudioSource.PlayClipAtPoint(shootSFX, transform.position);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100))
        {
            if (hit.transform.tag == "Damagable")
            {
                hit.transform.GetComponent<S_Health>().damage(damage);
            }
        }
    }
}
