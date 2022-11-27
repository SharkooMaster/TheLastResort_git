using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_uiRenderView : MonoBehaviour
{
    [SerializeField] private float sensitivity;
    public bool isFocus = false;

    private float rotX;
    private float rotY;
    private float zoom;

    private float mX;
    private float mY;

    public GameObject toPreview;
    public Transform  spawn;

    public void preview(GameObject _go)
    {
        toPreview = _go;
        toPreview.transform.position = spawn.transform.position;

        zoom = 1;
    }

    private void Update()
    {
        if (isFocus)
        {
            if(Input.GetMouseButton(0))
            {
                mX += Input.GetAxis("Mouse X") * (sensitivity * Time.deltaTime);
                mY += Input.GetAxis("Mouse Y") * (sensitivity * Time.deltaTime);
            }

            if (Input.mouseScrollDelta.y > 0)
            {
                zoom += (zoom > 2f) ? 0 : 0.1f;
                scaleObj();
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                zoom -= (zoom < 0.5f) ? 0 : 0.1f;
                scaleObj();
            }
        }
    }

    private void scaleObj()
    {
        toPreview.transform.localScale = new
                Vector3(toPreview.transform.localScale.x * zoom,
                        toPreview.transform.localScale.y * zoom,
                        toPreview.transform.localScale.z * zoom);
    }
}
