using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    Vector3 movementVector;
    [SerializeField] Vector3 endingPosition = new Vector3(-2.5f, 12.75f, 0);
    [SerializeField][Range(0, 1)] float interval;
    [SerializeField] float period = 2;

    public float time = 0f;


    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        movementVector = endingPosition - startingPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }

        time += Time.deltaTime;
        interval = (time % period) / period;
        
        interval = SmoothStart(interval) + interval * (SmoothStop(interval) - SmoothStart(interval));

        movementVector = endingPosition - startingPosition;

        if ((int)(time / period) % 2 == 0)
        {
            transform.position = startingPosition + movementVector * interval;
        }
        else if ((int)(time / period) % 2 == 1)
        {
            transform.position = endingPosition - movementVector * interval;
        }
    }

    float SmoothStop (float x)
    {
        return 1 - (1 - x) * (1 - x);
    }

    float SmoothStart(float x)
    {
        return x * x;
    }
}
