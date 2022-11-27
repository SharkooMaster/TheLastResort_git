using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Recoil : MonoBehaviour
{
    public Vector3 pos0;
    public Vector3 pos1;

    public float recoilSpeed;

    Item item;

    private void Start()
    {
        item = GetComponent<Item>();
    }

    public void trigger()
    {
        Debug.Log("RECOIL NEEDED");
        ejectBullet();
    }

    public GameObject bullet;
    public GameObject bulletPos;

    public void ejectBullet()
    {
        GameObject go = Instantiate(bullet);
        go.transform.position = bulletPos.transform.position;

        go.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(20f, 1f, 0), ForceMode.Impulse);
        StartCoroutine(_destroy(4, go));
    }

    IEnumerator _destroy(int sec ,GameObject go)
    {
        yield return new WaitForSeconds(sec);
        Destroy(go);
    }
}
