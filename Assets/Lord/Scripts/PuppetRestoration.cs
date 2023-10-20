using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetRestoration : MonoBehaviour
{
    public PuppetRestorationState CurrentState = PuppetRestorationState.NeedWork;
    [SerializeField] private List<RestorationStage> restorationStages = new();
    private bool isRestoring;
    private float currentTime = 0f;
    private int currentStateIndex;
    private bool haveStageToDo = true;

    private void Start()
    {
        foreach (RestorationStage stage in restorationStages)
        {
            stage.Done = false;
            if (stage.Obj)
                stage.Obj.SetActive(false);
        }
    }

    private void Update()
    {
        if (isRestoring)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= restorationStages[currentStateIndex].DoTime)
            {
                restorationStages[currentStateIndex].Done = true;
                if (restorationStages[currentStateIndex].Obj)
                    restorationStages[currentStateIndex].Obj.SetActive(true);
                currentTime = 0;
                isRestoring = false;
                CurrentState = PuppetRestorationState.NeedWork;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
            DoRestoration(PuppetRestorationState.SeperateClothing);
    }

    public void DoRestoration(PuppetRestorationState state)
    {
        foreach (RestorationStage stage in restorationStages)
            if (!stage.Done)
                haveStageToDo = true;

        if (isRestoring)
            Debug.Log("Currently doing puppet restoring! wait");
        if (!haveStageToDo)
        {
            Debug.Log("You have completed all Puppet stages!");
            CurrentState = PuppetRestorationState.Done;
        }

        if (!isRestoring && haveStageToDo)
        {
            currentStateIndex = getStateIndex(state);
            if (currentStateIndex == -1)
            {
                Debug.Log("Wrong stage to do! Stage not found");
                return;
            }

            if (!restorationStages[currentStateIndex].Done)
            {
                CurrentState = restorationStages[currentStateIndex].State;
                isRestoring = true;
            }
        }
    }

    private int getStateIndex(PuppetRestorationState state)
    {
        for (int i = 0; i < restorationStages.Count; i++)
            if (restorationStages[i].State == state)
                return i;

        return -1;
    }

    public List<RestorationStage> GetStages()
    {
        return restorationStages;
    }
}

[System.Serializable]
public class RestorationStage
{
    public string Title;
    public PuppetRestorationState State;
    public GameObject Obj;
    public float DoTime;
    public bool Done;
    // add like value and goal 
    // from inventory to here and sum or decrese it
}

public enum PuppetRestorationState
{
    FixingString,
    RemovingNails,
    SeperateLimbs,
    SeperateClothing,
    ArrangeParts,
    RemovingHair,
    RemovingHat,
    ClothCleaning,
    ClothRepair,
    Reassembling,
    NeedWork,
    Done
}