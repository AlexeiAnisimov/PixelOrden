using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aim : MonoBehaviour
{
    float sin,cos;
    float distancex, distancey;
    float hypotenus;
    Vector2 posgg;
    int storona;
    public Transform arb;
    // Start is called before the first frame update
    void Start()
    {
        posgg = GameObject.Find("Player").transform.position;
        storona = GameObject.Find("Player").GetComponent<movement>().storona;
    }

    // Update is called once per frame
    void Update()
    {
        storona = GameObject.Find("Player").GetComponent<movement>().storona;
        posgg = GameObject.Find("Player").transform.position;
        Vector2 cursor = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        GameObject.Find("mis").transform.position = cursor;
        distancex = cursor.x - posgg.x;
        distancey = cursor.y - posgg.y;
        hypotenus = Mathf.Sqrt(distancex * distancex + distancey * distancey);
        sin = distancey / hypotenus;
        cos = distancex / hypotenus;
        Vector2 polet = cursor - (Vector2)transform.position;
        if (Mathf.Abs(sin) < 0.8f)
        {
            transform.position = new Vector2(posgg.x + 0.65f * cos, posgg.y + sin * 0.65f);
            //arb.rotation = Quaternion.Euler(0, 0, 90* sin*storona);
           //arb.localScale = new Vector2(arb.localScale.x, Mathf.Abs(arb.localScale.y) *storona);
        }
    }
}
