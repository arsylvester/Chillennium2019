using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitFall : MonoBehaviour
{
    public void wasHit()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
