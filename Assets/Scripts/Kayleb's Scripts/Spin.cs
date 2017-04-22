﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

    public float rotateSpeed = 1;

	void Update () {
        transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
    }
}
