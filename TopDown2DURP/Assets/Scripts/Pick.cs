using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour, Interactive
{
    public void Interact()
    {
        Destroy(gameObject);
    }
}
