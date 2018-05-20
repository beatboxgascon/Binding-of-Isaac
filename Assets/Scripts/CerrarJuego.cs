using UnityEngine;
public class CerrarJuego : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit ();
#endif
    }
}

