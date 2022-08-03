using System.Collections.Generic;
using UnityEngine;

public enum DialogName { None, Start, Gameover, Success };

public class DialogController : MonoBehaviour
{
    //Dialog containers/panels
    [SerializeField] GameObject startDialog;
    [SerializeField] GameObject gameoverDialog;
    [SerializeField] GameObject successDialog;

    //Dialogs dictionary
    Dictionary<DialogName, GameObject> dialogs;

    //Initialize
    private void Awake()
    {
        InitDictionary();
        CloseAll();
    }

    private void InitDictionary()
    {
        dialogs = new Dictionary<DialogName, GameObject>();
        dialogs.Add(DialogName.Start, startDialog);
        dialogs.Add(DialogName.Gameover, gameoverDialog);
        dialogs.Add(DialogName.Success, successDialog);
    }

    //Dialog management
    private GameObject DialogNamed(DialogName name)
    {
        if (dialogs == null)
            return null;
        return dialogs[name];
    }

    public void OpenDialog(DialogName name)
    {
        CloseAll(name);
        GameObject dialog = DialogNamed(name);
        if (dialog != null)
            dialog.SetActive(true);
    }

    public void CloseDialog(DialogName name)
    {
        GameObject dialog = DialogNamed(name);
        if (dialog != null)
            dialog.SetActive(false);
    }

    public void CloseAll(DialogName exclude = DialogName.None)
    {
        foreach (KeyValuePair<DialogName, GameObject> entry in dialogs)
        {
            if (entry.Key != exclude && entry.Value != null && entry.Value.activeInHierarchy)
                entry.Value.SetActive(false);
        }
    }
}
