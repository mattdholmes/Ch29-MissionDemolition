using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// TODO: You must set the values for the enum
public enum GameMode
{
    idle,
    playing,
    levelEnd

}


// TODO: implement the MissionDemolition script
public class MissionDemolition : MonoBehaviour {
    static public MissionDemolition S;
    public GameObject[] castles;
    public Text gtLevel;
    public Text gtScore;
    public Vector3 castlepPos;
    public bool _______________;
    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Slingshot";
        // Use this for initialization
	void Start ()
    {
        S = this;
        level = 0;
        levelMax = castles.Length;
        StartLevel();
    }
	
	void StartLevel()
    {
        if (castle != null)
        {
            Destroy(castle);

        }
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject pTemp in gos) {
            Destroy(pTemp);
        }
        castle = Instantiate(castles[level]) as GameObject;
        castle.transform.position = castlepPos;
        shotsTaken = 0;
        SwitchView("Both");
        ProjectileLine.S.Clear();
        Goal.goalMet = false;
        ShowGT();
        mode = GameMode.playing;
    }

    void ShowGT()
    {


        gtLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        gtScore.text = "Shots Taken: " + shotsTaken;
    }

    void UpdateGUI()
    {

    }

    void Update()
    {
        ShowGT();
        if (mode == GameMode.playing && Goal.goalMet)
        {
            mode = GameMode.levelEnd;
            SwitchView("Both");
            Invoke("NextLevel", 2f);

        }

    }

    void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }

    private void OnGUI()
    {
        Rect buttonRect = new Rect((Screen.width / 2) - 50, 10, 100, 24);
        switch(showing)
        {
            case "Slingshot":
                if (GUI.Button(buttonRect, "Show Castle"))
                {
                    SwitchView("Castle");

                }
                break;
            case "Castle":
                if (GUI.Button(buttonRect, "Both"))
                {
                    SwitchView("Both");

                }
                break;
            case "Both":
                if (GUI.Button(buttonRect, "Show Slingshot"))
                {
                    SwitchView("Slingshot");

                }
                break;
        }
    }

    public void SwitchView(string eView = "")
    {
        S.showing = eView;
        switch (S.showing)
        {
            case "Slingshot":
                FollowCam.S.Poi = S.null;
                break;
            case "Castle":
                FollowCam.S.poi = S.castle;
                break;
            case "Both":
                FollowCam.S.poi = GameObject.Find("VeiwBoth");
                break;
        }
    }

    // Static method that allows code anywhere to increment shotsTaken
    public static void ShotFired()
    {
        S.shotsTaken++;
    }
}
