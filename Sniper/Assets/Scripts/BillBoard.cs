using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(Manager.Instance.mainCamera.transform.position + Vector3.one);
    }
}
