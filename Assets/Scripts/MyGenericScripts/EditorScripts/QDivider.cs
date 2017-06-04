#if UNITY_EDITOR

using System;
using UnityEngine;
using UnityEditor;

/* Usage: put these above variables
 * [QDivider]
 * [QDivider("My header")] 
 * [QDivider("My header", "My subtitle")] 
 */

public class QDivider : PropertyAttribute 
{
    public string header;
    public string subtitle;
    public const string divider = "____________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________";


    public QDivider(string header, string subtitle)
	{
		this.header = header;
		this.subtitle = subtitle;
	}

	public QDivider(string header)
	{
		this.header = header;
		this.subtitle = null;
	}



    public QDivider()
	{
		this.header = null;
		this.subtitle = null;
	}
}

[CustomPropertyDrawer (typeof(QDivider))]
public class QDividerDrawer : DecoratorDrawer 
{
	public override void OnGUI (Rect rect) 
	{
        QDivider att = attribute as QDivider;

        rect.y -= (EditorGUIUtility.singleLineHeight * 0.35f);

        //TITLE
        if (!String.IsNullOrEmpty(att.header))
		{
            // A header generates a spacing to clearly seperate from the content above
            rect.y += (EditorGUIUtility.singleLineHeight * 0.5f);

            GUIStyle headerStyle = new GUIStyle (GUI.skin.label);
			headerStyle.fontSize = 15;
			headerStyle.fontStyle = FontStyle.Bold;

			EditorGUI.LabelField(rect,att.header,headerStyle);

			rect.y += EditorGUIUtility.singleLineHeight * 1.1f;
		}

		//SUBTITLE
		if(!String.IsNullOrEmpty(att.subtitle))
		{
			GUIStyle subtitleStyle = new GUIStyle (GUI.skin.label);
			subtitleStyle.fontSize = 10;

			EditorGUI.LabelField(rect,att.subtitle,subtitleStyle);

			rect.y += EditorGUIUtility.singleLineHeight * 0.3f;
		}

        // DIVIDER
		if(Event.current.type == EventType.Repaint) 
		{
            GUIStyle subtitleStyle = new GUIStyle(GUI.skin.label);
            subtitleStyle.fontSize = 10;

            EditorGUI.LabelField(rect, QDivider.divider, subtitleStyle);

            //Alternative: Use a gui skinbox: GUI.skin.box.Draw(rect,GUIContent.none,0);

        }
    }

	public override float GetHeight()
	{
        QDivider att = attribute as QDivider;

		float headerHeight = EditorGUIUtility.singleLineHeight;
        
		if( !String.IsNullOrEmpty(att.header) )
		{
			headerHeight += EditorGUIUtility.singleLineHeight*1.6f;
		}

		if( !String.IsNullOrEmpty(att.subtitle) )
		{
			headerHeight += EditorGUIUtility.singleLineHeight*0.3f;
		}

		return /*base.GetHeight()*/ + headerHeight;
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