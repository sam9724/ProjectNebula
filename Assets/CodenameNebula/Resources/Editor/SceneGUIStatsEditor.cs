using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;
using System.Reflection;

[CustomEditor(typeof(IBaseCharacter), true)]
public class SceneGUIStatsEditor : Editor
{

    FieldInfo[] fields;

    public void OnEnable()
    {
        
    }

    private void OnSceneGUI()
    {
        fields = fields ?? typeof(CharacterStats).GetFields();

        IBaseCharacter character = target as IBaseCharacter;
        GameObject go = (target as MonoBehaviour).gameObject;
        CharacterStats stats = character.CharStats;
        Color originalHandleColor = Handles.color;

        Handles.color = Color.magenta;
        Handles.ArrowHandleCap(0, go.transform.position, go.transform.rotation, stats.speed, EventType.Repaint);
        Handles.color = originalHandleColor;

        Handles.BeginGUI();                                                                                 
        Vector2 guiPoint = HandleUtility.WorldToGUIPoint(go.transform.position);

        // generic GUI creation for any number of fields in CharacterStats class.
        for (int i = 0; i < fields.Length; i++)
        {
            string propertyName = fields[i].Name;

            Rect rect = new Rect(guiPoint.x - 100, guiPoint.y - (150 + i * 21), 100, 20);
            string val = GUI.TextField(rect, propertyName + ": " + stats.GetType().GetField(propertyName).GetValue(stats), new GUIStyle(GUI.skin.box));
            if (float.TryParse(Regex.Match(val, @"\d+").Value, out float newVal))
                stats.GetType().GetField(propertyName).SetValue(stats, newVal);
        }

        Handles.EndGUI();

        if(character is IBaseEnemy) // Logic specific to Enemy AIs
        {
            IBaseEnemy enemy = character as IBaseEnemy;
            /*if (enemy && enemy.waypoints.Length > 1)
            {
                originalHandleColor = Handles.color;                                                      //Like Enabled, we must perserve the original handle color!
                Handles.color = Color.green;                                                                    //Set the color of all 
                for (int i = 0; i < enemy.waypoints.Length - 1; i++)                                               //Length minus one, because the last one doesnt have a "next"
                    if (enemy.waypoints[i] && enemy.waypoints[i + 1])
                        Handles.DrawLine(enemy.waypoints[i].position, enemy.waypoints[i + 1].position);                   //Draw a line from i to i+1
                Handles.DrawLine(enemy.waypoints[enemy.waypoints.Length - 1].position, enemy.waypoints[0].position);     //Draw line from last element back to first element
                Handles.color = originalHandleColor;                                                            //Reset the handle back to the original!
            }*/

            /*Handles.BeginGUI();                                                                                //Begin 2D gui handle, specifies that this is on the scene
            for (int i = 0; i < enemy.waypoints.Length; i++)
            {
                if (!enemy.waypoints[i])
                    continue;
                Vector2 guiPoint2 = HandleUtility.WorldToGUIPoint(enemy.waypoints[i].position);                     //Convert a space in the world to the 2D GUI space
                Rect rect = new Rect(guiPoint2.x - 50f, guiPoint2.y - 40, 100, 20);                               //Create a scalable rect (In this case, hardcoded size)
                GUI.Box(rect, "Waypoint: " + i);                                                                //Create a GUI text box with the rect size
            }
            Handles.EndGUI();*/
        }
        //Debug.Log("OnSceneGUI called");
        //ProcessInput(Event.current);
    }

    /*void ProcessInput(Event e)
    {
        //Debug.Log(e.ToString());
        Dictionary<string, bool> inputValues = new Dictionary<string, bool>();

        inputValues["turnLeft"] = e.type == EventType.KeyDown && e.keyCode == KeyCode.A;
        inputValues["turnRight"] = e.type == EventType.KeyDown && e.keyCode == KeyCode.D;
        inputValues["moveForward"] = e.type == EventType.KeyDown && e.keyCode == KeyCode.W;
        inputValues["moveBackward"] = e.type == EventType.KeyDown && e.keyCode == KeyCode.S;

        InputManager.Instance.SetValuesFromEditor(inputValues);
    }*/
}
