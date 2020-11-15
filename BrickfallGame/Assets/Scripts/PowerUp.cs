using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float speed;

    public GameObject DeadZoneObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0f, -1f, 0f) * Time.deltaTime * speed);

        if (transform.position.y < DeadZoneObject.transform.position.y)
        {
            Destroy(gameObject);
        }
    }
}
