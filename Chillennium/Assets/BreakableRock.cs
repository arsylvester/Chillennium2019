using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableRock : MonoBehaviour
{
    public void dead()
    {
        Destroy(gameObject);
    }
}
