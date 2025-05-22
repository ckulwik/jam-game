using UnityEngine;

public class TownieProxomityDialog : MonoBehaviour
{
    public enum TownieId
    {
        Townie1,
        Townie2,
    }

    [System.Serializable]
    public class TownieDialog
    {
        public TownieId townieId;
        public string[] dialogLines;
    }

    public TownieId thisTownieId;
    private bool canOpenDialog = false;
    public GameObject dialogBox;
    private TownieDialog[] townieDialogs = new TownieDialog[]
    {
        new TownieDialog { townieId = TownieId.Townie1, dialogLines = new string[] { "Hello, traveler!", "I am townie number 1" } },
        new TownieDialog { townieId = TownieId.Townie2, dialogLines = new string[] { "Hi there!", "I am townie number 2" } },
    };

    private DialogController dialogController;

    private void Start()
    {
        dialogController = dialogBox.GetComponent<DialogController>();
    }


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
        if (canOpenDialog && Input.GetMouseButtonDown(1))
        {
            // Find the dialog lines for this townie
            foreach (var townieDialog in townieDialogs)
            {
                if (townieDialog.townieId == thisTownieId)
                {
                    dialogController.lines = townieDialog.dialogLines;
                    dialogBox.SetActive(true);
                    break;
                }
            }
        }
    }
}
