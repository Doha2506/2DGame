using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer2 : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 localScale2;

   // Player2Move obj =gameObject.AddComponent< Player2Move>();


    private void Start()
    {
        localScale2 = transform.localScale;
    }


   // Update is called once per frame
    private void Update()
    {
     //   Debug.Log("hi");
        localScale2.x = Player2.currentHealthBar;
        transform.localScale = localScale2;
    }

  
}
