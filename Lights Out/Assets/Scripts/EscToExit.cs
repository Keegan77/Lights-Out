using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscToExit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(FindObjectOfType<VisualColor>()?.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
