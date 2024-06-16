using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float widthImage;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float rest = (velocity * Time.time) % widthImage;

        transform.position = initialPosition + (rest * direction);
    }
}
