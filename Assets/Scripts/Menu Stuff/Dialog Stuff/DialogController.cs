using UnityEngine;
using TMPro;
using System.Collections;

public class DialogController : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public string[] lines;
    public float letterRenderTime;

    private int index;

    void OnEnable()
    {
        dialogText.text = "";
        StartDialog();
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (dialogText.text == lines[index])
            {
                NextLine();
            }
            else {
                StopAllCoroutines();
                dialogText.text = lines[index];
            }
        }
    }

    void StartDialog()
    {
     index = 0;  
     dialogText.text = ""; 
     StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char letter in lines[index].ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(letterRenderTime);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogText.text = "";
            StartCoroutine(TypeLine());
        }
        else {
            gameObject.SetActive(false);
        }
    }
}
