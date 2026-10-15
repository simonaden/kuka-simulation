using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonoBehaviourScript : MonoBehaviour
{

    public ArticulationBody[] joints = new ArticulationBody[6];
    public float[] targetAngles = new float[6];

    public ArticulationBody baseBody;
    public float[] stiffness  = { 1000000, 675000, 90000, 2250, 1125, 225 };
    public float[] damping    = { 130000, 90000, 12000, 300, 150, 30 };
    public float[] forceLimit = { 15000, 18000, 15000, 5750, 5700, 2800 };


    public float[] maxSpeed   = { 80, 60, 60, 90, 80, 130 };  // Grad pro Sekunde (Werte der URDF)
    public float[] homeAngles = { 0, -90, 90, 0, 0, 0 };      // Startpose
    float[] driveAngles = new float[6];                        // Ziel, das wirklich ans Gelenk geht


    public bool loop = true;                      // Programm wiederholen?
    public float pauseSeconds = 0.5f;             // Pause nach jeder Pose

    List<float[]> poses = new List<float[]>();    // die gespeicherten Posen
    bool playing = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        baseBody.immovable = true;

        foreach (ArticulationBody ab in GetComponentsInChildren<ArticulationBody>())
            ab.useGravity = false;

        for (int i = 0; i < joints.Length; i++)
        {
            ArticulationDrive drive = joints[i].xDrive;
            drive.stiffness = stiffness[i];
            drive.damping = damping[i];
            drive.forceLimit = forceLimit[i];
            joints[i].xDrive = drive;
        }

        //homing
        for (int i = 0; i < joints.Length; i++)
            targetAngles[i] = homeAngles[i];

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < joints.Length; i++)
        {
            ArticulationDrive drive = joints[i].xDrive;
            driveAngles[i] = Mathf.MoveTowards(driveAngles[i], targetAngles[i], maxSpeed[i] * Time.fixedDeltaTime);
            drive.target = driveAngles[i];
            joints[i].xDrive = drive;
        }
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 250, 220), GUI.skin.box);

        for (int i = 0; i < joints.Length; i++)
        {
            ArticulationDrive drive = joints[i].xDrive;

            GUILayout.BeginHorizontal();
            GUILayout.Label("A" + (i + 1), GUILayout.Width(25));
            targetAngles[i] = GUILayout.HorizontalSlider(targetAngles[i], drive.lowerLimit, drive.upperLimit);
            GUILayout.Label(targetAngles[i].ToString("F0") + "°", GUILayout.Width(45));
            GUILayout.EndHorizontal();
        }

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Start"))
        {
            for (int i = 0; i < joints.Length; i++)
                targetAngles[i] = homeAngles[i];
        }
        if (GUILayout.Button("Speichern") && !playing)
            poses.Add((float[])targetAngles.Clone());
        if (GUILayout.Button("Play") && !playing && poses.Count > 0)
            StartCoroutine(PlayProgram());
        if (GUILayout.Button("Stopp"))
        {
            StopAllCoroutines();
            playing = false;
        }
        if (GUILayout.Button("Löschen") && !playing)
            poses.Clear();
        GUILayout.EndHorizontal();

        GUILayout.Label("Posen: " + poses.Count);

        GUILayout.EndArea();
    }

    IEnumerator PlayProgram()
    {
        playing = true;
        do
        {
            foreach (float[] pose in poses)
            {
                for (int i = 0; i < joints.Length; i++)
                    targetAngles[i] = pose[i];

                yield return new WaitUntil(PoseReached);
                yield return new WaitForSeconds(pauseSeconds);
            }
        } while (loop);
        playing = false;
    }

    bool PoseReached()
    {
        for (int i = 0; i < joints.Length; i++)
            if (Mathf.Abs(driveAngles[i] - targetAngles[i]) > 0.5f)
                return false;
        return true;
    }
}
