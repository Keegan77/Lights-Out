using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformRightWhenStep : MonoBehaviour
{
    // Start is called before the first frame update
    bool steppedOn;
    public float movementCap;
    [SerializeField] private Transform playerTransform;
    PlayerController playerControl;
    void Start()
    {
        playerControl = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (steppedOn && transform.position.x < movementCap)
        {
            transform.Translate(new Vector3(2, 0, 0) * Time.deltaTime);
            if (playerControl.onMovingPlat)
            {
                playerTransform.transform.Translate(new Vector3(2, 0, 0) * Time.deltaTime);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            steppedOn = true;
        }
    }
}
