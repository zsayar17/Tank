using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class GameTour
{
    public Team[] Teams;
    public static int currentTeamIndex;

    public void Awake()
    {
        if (Teams == null) return;

        for (int i = 0; i < Teams.Length; i++)
            Teams[i].SetTeamMembersInfo(i);
    }

    public void Update()
    {
        if (Teams == null || !Teams[currentTeamIndex].AllDone) return; // Eğer takımın tüm üyeleri hareket etmediyse return

        Teams[currentTeamIndex].Refresh();
        currentTeamIndex = (currentTeamIndex + 1) % Teams.Length;
    }
}


public enum SituationType
{
    Done,
    Team
}

[System.Serializable]
public class Team
{
    public List<BaseObject> Objects;
    int teamIndex;
    int doneCount;

    public void AddTeamMember(BaseObject baseObject)
    {
        if (Objects == null) return;

        Objects.Add(baseObject);
        baseObject.team = this;
        SetTag(baseObject, SituationType.Team);
    }

    public void SetTeamMembersInfo(int teamIndex)
    {
        if (Objects == null) return;

        this.teamIndex = teamIndex;
        foreach (var obj in Objects)
        {
            obj.team = this;
            SetTag(obj, SituationType.Team);
        }
    }

    public void SetDone(BaseObject baseObject)
    {
        if (baseObject.CompareTag("Done")) return;

        SetTag(baseObject, SituationType.Done);
        doneCount++;
    }

    public bool IsDone(BaseObject baseObject)
    {
        return baseObject.CompareTag("Done");
    }

    public void Refresh()
    {
        foreach (var obj in Objects) SetTag(obj, SituationType.Team);
        doneCount = 0;
    }

    void SetTag(BaseObject baseObject, SituationType situation)
    {
        if (situation == SituationType.Done) baseObject.tag = "Done";
        else baseObject.tag = "Team" + teamIndex;
    }

    public bool IsEnemy(GameObject baseObject)
    {
        string tag;

        if (baseObject == null) return false;

        tag = baseObject.tag;
        return tag != "Done" && tag != "Team" + teamIndex;
    }

    public bool AllDone => doneCount == Objects.Count;
}
