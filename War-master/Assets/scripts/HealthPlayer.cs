using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    
   public Vector3 localScale;


    // Start is called before the first frame update
    void Start()
    {
        
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        localScale.x = Player1.currentHealthBar;
        transform.localScale = localScale;
    }

    
}
