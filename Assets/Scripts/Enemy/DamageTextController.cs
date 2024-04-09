using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Why not merge this class into DamageTextBehavior?
// Singleton
public class DamageTextController : SingletonMonoBehavior<DamageTextController>
{    
    public ObjectPooler Pooler {get; set;}

    private void Start()
    {
       Pooler = GetComponent<ObjectPooler>();
    }

}
