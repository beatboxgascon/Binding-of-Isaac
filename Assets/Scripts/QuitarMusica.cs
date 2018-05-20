using UnityEngine;
public class QuitarMusica : MonoBehaviour
{
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Music"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Music"));
        }
    }
    void Update()
    {

    }
}