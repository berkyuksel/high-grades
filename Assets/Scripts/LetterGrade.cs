using UnityEngine;
public enum Letter { F, D, C, B, A };

public class LetterGrade : MonoBehaviour
{
    public Letter Grade;
    public float CollectInterval = 0.1f; //seconds
    bool cooledDown = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Names.PlayerTag))
        {
            PlayerController pc = other.gameObject.GetComponent<PlayerController>();
            pc.Grade = Grade;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (cooledDown && other.CompareTag(Names.PlayerTag))
        {
            PlayerController pc = other.gameObject.GetComponent<PlayerController>();
            pc.Collect(1);
            cooledDown = false;
            Invoke("Cooldown", CollectInterval);
        }
    }

    void Cooldown()
    {
        cooledDown = true;
    }
}
