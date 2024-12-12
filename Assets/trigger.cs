using UnityEngine;

public class TrainBoxHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is a box
        if (other.CompareTag("Box"))
        {
            // Parent the box to the train
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger is a box
        if (other.CompareTag("Box"))
        {
            // Unparent the box
            other.transform.SetParent(null);
        }
    }
}
