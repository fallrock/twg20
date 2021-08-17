using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCloner : MonoBehaviour
{

    public GameObject target;

    void Start()
    {
        
    }

    void Update()
    {
      gameObject
        .GetComponent<CloneReproductionSystem>()
        .RecordPlayerPosition(target.transform.position);
    }
}
