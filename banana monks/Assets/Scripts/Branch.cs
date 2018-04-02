using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour {
    public bool isBroken = false;

    public void Break()
    {
        // play animation
        isBroken = true;
    }
}
