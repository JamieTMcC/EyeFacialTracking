using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    void DestroyMe()
    {
        Destroy(this.gameObject);
    }
}