using UnityEngine;

public class Character : MonoBehaviour
{
    protected bool stun = false;
    public void Stun()
    {
        stun = true;
    }
}