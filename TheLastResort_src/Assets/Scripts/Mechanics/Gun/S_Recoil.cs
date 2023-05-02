using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Recoil : MonoBehaviour
{
    public Vector3 barrelPos0;
    public float barrelPos1;
    Vector3 barrelPos2;

    public float recoilSpeed;
    [SerializeField] GameObject barrel;
    [SerializeField] GameObject body;

    Item item;

    private void Start()
    {
        item = GetComponent<Item>();
    }

    public void trigger()
    {
        Debug.Log("RECOIL NEEDED");
        if (!isEmpty)
        {
            isRecoilBarrel = true;
            barrelPos0 = transform.localPosition;
            barrelPos2 = barrelPos0 + (transform.worldToLocalMatrix.MultiplyVector(transform.forward) * barrelPos1);
            RecoilFire();
            barrelAnim(isRecoilBarrel);
        }
        ejectBullet();
    }

    bool isRecoilBarrel = false;

    private void Update()
    {
        if ((barrel.transform.localPosition.z - barrelPos2.z) <= 0.1f)
        {
            isRecoilBarrel=false;
        }
        barrelAnim(isRecoilBarrel);

        // Recoil

        targetRot = Vector3.Lerp(targetRot, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRot = Vector3.Slerp(currentRot, targetRot, snapThresh * Time.fixedDeltaTime);

        //transform.parent.parent.GetComponent<S_playerCamera>().rotH += currentRot.x;
        if(transform.parent.parent.GetComponent<S_playerCamera>())
            transform.parent.parent.GetComponent<S_playerCamera>().rotV += currentRot.x;
    }

    bool isEmpty = false;

    private void barrelAnim(bool isBegin)
    {
        if (isBegin && !isEmpty)
        {
            barrel.transform.localPosition = new Vector3(0,0, Mathf.Lerp(barrel.transform.localPosition.z, barrelPos2.z, recoilSpeed * Time.deltaTime));
            body.transform.localPosition = new Vector3(0, 0, Mathf.Lerp(body.transform.localPosition.z, barrelPos2.z - 0.1f, recoilSpeed * .5f * Time.deltaTime));
        }
        else if (!isBegin && !isEmpty)
        {
            barrel.transform.localPosition = new Vector3(0, 0, Mathf.Lerp(barrel.transform.localPosition.z, 0, recoilSpeed * Time.deltaTime));
            body.transform.localPosition = new Vector3(0, 0, Mathf.Lerp(body.transform.localPosition.z, 0, recoilSpeed * .5f * Time.deltaTime));
        }
    }

    public void gunState(string a)
    {
        switch (a)
        {
            case "empty":
                barrel.transform.localPosition = transform.localPosition + (transform.worldToLocalMatrix.MultiplyVector(transform.forward) * barrelPos1);
                isEmpty = true;
                break;
            default:
                barrel.transform.localPosition = Vector3.zero;
                isEmpty = false;
                break;
        }
    }

    public GameObject bullet;
    public GameObject bulletPos;
    public float bulletEjectionSpeed;

    public void ejectBullet()
    {
        GameObject go = Instantiate(bullet);
        go.transform.position = bulletPos.transform.position;

        go.GetComponent<Rigidbody>().AddRelativeForce(bulletPos.transform.localPosition - (bullet.transform.worldToLocalMatrix.MultiplyVector(-bulletPos.transform.forward) * bulletEjectionSpeed * Time.deltaTime), ForceMode.Impulse);
        StartCoroutine(_destroy(4, go));
    }

    IEnumerator _destroy(int sec ,GameObject go)
    {
        yield return new WaitForSeconds(sec);
        Destroy(go);
    }

    // ### Recoil ###

    // Rotation
    private Vector3 currentRot;
    private Vector3 targetRot;

    // HipFire
    [SerializeField] private float recoilX;
    [SerializeField] private float recoilY;
    [SerializeField] private float recoilZ;

    // Settings
    [SerializeField] private float snapThresh;
    [SerializeField] private float returnSpeed;

    public void RecoilFire()
    {
        targetRot += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }

}
