using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class lightboys : MonoBehaviour
{
    bool vol = false;
    public bool lighton = true;
    Light2D light;
    public float speed=1;
    Vector3 napr=new Vector3(0,0,0);
    Vector2 nachcoord;
    // Start is called before the first frame update
    void Start()
    {
        if (lighton)
        {
            light = GetComponent<Light2D>();
            light.intensity = Random.Range(0.4f, 0.7f);
        }
        nachcoord = transform.position;
        StartCoroutine(re());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (lighton)
        {
            if (vol)
            {
                if (light.intensity < 0.7) light.intensity += 0.01f;
                else vol = false;
            }
            else
            {
                if (light.intensity > 0.4f) light.intensity -= 0.01f;
                else vol = true;
            }
        }
        transform.position += napr;
    }
    IEnumerator re()
    {
        if (Vector2.Distance(transform.position, nachcoord) < 0.1f*speed)
            napr = new Vector2(Random.Range(-0.003f, 0.003f), Random.Range(-0.003f, 0.003f)*speed);
        else napr = new Vector2(-Mathf.Sign(transform.position.x - nachcoord.x) * 0.0018f,- Mathf.Sign(transform.position.y - nachcoord.y) * 0.0018f)*speed;
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(re());
    }
}
