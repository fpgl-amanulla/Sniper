﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(MasterManager.Instance.mainCamera.transform.position + Vector3.forward * 180);
    }
}
