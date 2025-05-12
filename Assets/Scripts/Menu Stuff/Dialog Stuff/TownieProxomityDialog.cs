using UnityEngine;

public class TownieProxomityDialog : MonoBehaviour
{
    private bool canOpenDialog = false;
    public GameObject dialogBox;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered townie proximity.");
            canOpenDialog = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited townie proximity.");
            canOpenDialog = false;
        }
    }

    private void Update()
    {
        if (canOpenDialog)
        {
            if (Input.GetMouseButtonDown(1))
            {
                dialogBox.SetActive(true);
            }
        }
    }

    
}
