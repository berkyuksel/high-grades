using UnityEngine;
using TMPro;

public enum Operation { None, Add, Substract, Multiply, Divide };
public class Gate : MonoBehaviour
{
    [SerializeField] TextMeshPro label;
    public Operation Operation;
    public float Value;
    // Start is called before the first frame update
    void Start()
    {
        Config();
    }

    void Config()
    {
        char symbol = OperatorSymbol(Operation);
        label.text = symbol.ToString() + Value.ToString();
    }

    //Opearation character
    char OperatorSymbol(Operation operation)
    {
        switch (operation)
        {
            case Operation.Add:
                return '+';
            case Operation.Substract:
                return '-';
            case Operation.Multiply:
                return 'x';
            case Operation.Divide:
                return '/';
            default:
                return ' ';
        }
    }

    //Operation
    public int Calculate(int lh)
    {
        switch (Operation)
        {
            case Operation.Add:
                return Mathf.FloorToInt((float)lh + Value);
            case Operation.Substract:
                return Mathf.FloorToInt((float)lh - Value);
            case Operation.Multiply:
                return Mathf.FloorToInt((float)lh * Value);
            case Operation.Divide:
                return Mathf.FloorToInt((float)lh / Value);
            default:
                return lh;
        }
    }

    //Prevent multiple triggers
    public void Deactivate()
    {
        GetComponent<BoxCollider>().enabled = false;
    }
}
