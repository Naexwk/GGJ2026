using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static AIManager instance;
    public Transform[] guardPois;
    public Transform[] staffPois;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);

        GameObject[] array = GameObject.FindGameObjectsWithTag("GuardPOI");
        guardPois = new Transform[array.Length];
        for(int i = 0; i < array.Length; i++)
        {
            guardPois[i] = array[i].transform;
        }

        array = GameObject.FindGameObjectsWithTag("StaffPOI");
        staffPois = new Transform[array.Length];
        for(int i = 0; i < array.Length; i++)
        {
            staffPois[i] = array[i].transform;
        }
    }


}