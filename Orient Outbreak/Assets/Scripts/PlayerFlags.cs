using UnityEngine;

public class PlayerFlags : MonoBehaviour
{
    // Flag Items
    public bool hasMetTanod;

    private void Start()
    {
        hasMetTanod = false;
    }

    public void SwitchFlagValue(bool flag)
    {
        if(flag == true) { flag = false; }
        else { flag = true; }
    }
}
