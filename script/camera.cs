using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class camera : MonoBehaviour
{
    public GameObject player;
    //bool dostatochno = true;
    float speed=0;
    int last = 1;
    Vector2 pos;
    Vector3 tempvector;
    float forlevelx;
    float forlevely;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        if (Application.loadedLevelName != "level3")
        {
            forlevelx = 1.5f;
            forlevely = 0.3f;
        }
        /*else
        {
            forlevelx = 1f;
            forlevely = 1f;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Sign(player.GetComponent<movement>().tempvector.x)!=0)
        {
            Vector3 otvinta = new Vector3(player.transform.position.x + forlevelx* Mathf.Sign(player.GetComponent<movement>().tempvector.x), player.transform.position.y+forlevely, transform.position.z);
            if (Mathf.Sign(player.GetComponent<movement>().tempvector.x) != last)
            {
                last = (int)Mathf.Sign(player.GetComponent<movement>().tempvector.x);
                speed = 0.5f;
            }
            pos = Vector2.Lerp(transform.position, otvinta, Time.deltaTime*speed);
            if (speed < 1.5f) speed += 0.01f;
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }
        //if (Vector2.Distance(transform.position, player.position) > 0.8|| Mathf.Abs(transform.position.y- player.position.y) > 0.65f) dostatochno = false;
        //else if (Vector2.Distance(transform.position, player.position) < 0.1) dostatochno = true;
        if (Input.GetButton("Vertical"))
        {
            tempvector = Vector3.right * Input.GetAxis("Vertical");
            if (GetComponent<Camera>().orthographicSize > 1f && Mathf.Sign(tempvector.x)==1)
                GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize - 0.015f;
            if (GetComponent<Camera>().orthographicSize < 5f && Mathf.Sign(tempvector.x) == -1)
                GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize + 0.015f;
        }
    }
}
