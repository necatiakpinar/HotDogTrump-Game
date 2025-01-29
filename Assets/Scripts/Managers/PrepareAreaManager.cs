using System.Collections;
using System.Collections.Generic;
using Managers;
using Misc;
using UnityEngine;

public class PrepareAreaManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var bread = EventManager.OnSpawnFromBreadPool.Invoke(IngredientType.HamburgerBread, Vector3.zero, Quaternion.identity, null);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
