using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZugManager : MonoBehaviour
{
    public static ZugManager zugmanager;
    private PlayerMovement player;

    private void Awake()
    {
        if(zugmanager ==null)
        {
            zugmanager = this;
        }
        else
        {
            Destroy(gameObject);
        }

        player = GetComponent<PlayerMovement>();
    }

    


}
