using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doormovement : MonoBehaviour
{
    Vector3 open;
    Vector3 closed;
    public GameObject door;
    [SerializeField] private float reRollTime = 2f;

    private void Start()
    {
        open = door.transform.position + Vector3.up * 8;
        closed = door.transform.position;

        StartCoroutine(DoorOpen());
    }

    private IEnumerator DoorOpen()
    {
        int roll = 0;
        float time = 0;

        // Coroutine will inifinitely run
        while (true)
        {
            time = 0;
            roll = 0;

            // Run until the timer is greater than 1
            while (time < 1)
            {
                // Add time and move the door.
                time += Time.deltaTime;
                door.transform.position = Vector3.Lerp(closed, open, time);

                // Wait until the next frame
                yield return null;
            }

            // Randomly roll every reRollTime second to see if we can proceed
            while (roll < 80)
            {
                roll = Random.Range(0, 100);

                yield return new WaitForSecondsRealtime(reRollTime);
            }

            // We rolled high enough, so proceed and reset the roll
            roll = 0;

            // Run until time is 0
            while (time > 0)
            {
                // Move the door
                time -= Time.deltaTime;
                door.transform.position = Vector3.Lerp(closed, open, time);

                // Wait until next frame
                yield return null;
            }

            // Randomly roll every reRollTime second to see if we can proceed
            while (roll < 80)
            {
                roll = Random.Range(0, 100);

                yield return new WaitForSecondsRealtime(reRollTime);
            }
        }
    }
}
