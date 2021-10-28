using UnityEngine;

public class WipeTransition : MonoBehaviour
{
    public void OnWipe_End()
    {
        SceneLoader.instance.DeactivateCanvas();
    }

    public void OnWipe_Start()
    {
        //SceneLoader.instance.ActivateCanvas();
    }
}
