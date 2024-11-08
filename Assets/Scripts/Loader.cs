using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    void Start()
    {
        var prefab = Resources.Load("GameObject_I_Want_To_Load");
        Instantiate(prefab);
    }
}
