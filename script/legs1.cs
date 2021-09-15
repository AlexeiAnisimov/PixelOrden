using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class legs1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnCollisionExit2D(Collision2D sliding)
    {
       // Debug.Log(.gameObject.name);
        if (sliding.gameObject.name == "zemlia"||sliding.gameObject.name=="block")
        {
            GetComponentInParent<movement>().exitEarth();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponentInParent<movement>().padenie();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
       GetComponentInParent<movement>().naEarth();
    }
}
