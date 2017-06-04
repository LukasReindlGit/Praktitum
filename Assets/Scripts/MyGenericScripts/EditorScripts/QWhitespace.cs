#if UNITY_EDITOR

using System;
using UnityEngine;
using UnityEditor;

/* Usage: put these above variables
 * [QDivider]
 * [QDivider("My header")] 
 * [QDivider("My header", "My subtitle")] 
 */

public class QDWhitespace : PropertyAttribute
{
    public float lines;



    public QDWhitespace(float lines)
    {
        this.lines = lines;
    }



    public QDWhitespace()
    {
        this.lines = 1.0f;
    }
}

[CustomPropertyDrawer(typeof(QDWhitespace))]
public class QDWhitespaceDrawer : DecoratorDrawer
{
    public override void OnGUI(Rect rect)
    {
        QDWhitespace att = attribute as QDWhitespace;

        //rect.y -= (EditorGUIUtility.singleLineHeight * 0.35f);


        // DIVIDER
        if (Event.current.type == EventType.Repaint)
        {
            GUIStyle subtitleStyle = new GUIStyle(GUI.skin.label);
            subtitleStyle.fontSize = 10;

            EditorGUI.LabelField(rect, "", subtitleStyle);

            //Alternative: Use a gui skinbox: GUI.skin.box.Draw(rect,GUIContent.none,0);

        }
    }

    public override float GetHeight()
    {
        QDWhitespace att = attribute as QDWhitespace;

        float headerHeight = EditorGUIUtility.singleLineHeight * att.lines;

        return headerHeight;
    }
}

#else

using System;
using UnityEngine;

public class QDivider : PropertyAttribute
{
    public string header;
    public string subtitle;

    public QDivider(string header, string subtitle) { }
    public QDivider(string header) { }
    public QDivider() { }
}

#endif