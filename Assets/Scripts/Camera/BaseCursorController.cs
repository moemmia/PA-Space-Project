using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseCursorController : MonoBehaviour
{
    public Image mouse;
    
    protected RaycastHit _hit;

    protected virtual void Awake () {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    protected virtual void Update () {
        mouse.transform.position = Input.mousePosition;
    }
}
