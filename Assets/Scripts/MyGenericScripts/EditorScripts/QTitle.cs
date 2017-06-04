#if UNITY_EDITOR

using System;
using UnityEngine;
using UnityEditor;

/* Usage: put these above variables
 * [QDivider]
 * [QDivider("My header")] 
 * [QDivider("My header", "My subtitle")] 
 */

public class QTitle : PropertyAttribute 
{
    public string header;
    public string subtitle;

    public QTitle(string header, string subtitle)
	{
		this.header = header;
		this.subtitle = subtitle;
	}

	public QTitle(string header)
	{
		this.header = header;
		this.subtitle = null;
	}



    public QTitle()
	{
		this.header = null;
		this.subtitle = null;
	}
}

[CustomPropertyDrawer (typeof(QTitle))]
public class QTitleDrawer : DecoratorDrawer 
{
	public override void OnGUI (Rect rect) 
	{
        QTitle att = attribute as QTitle;

        rect.y -= (EditorGUIUtility.singleLineHeight * 0.35f);

        //TITLE
        if (!String.IsNullOrEmpty(att.header))
		{
            // A header generates a spacing to clearly seperate from the content above
            rect.y += (EditorGUIUtility.singleLineHeight * 0.5f);

            GUIStyle headerStyle = new GUIStyle (GUI.skin.label);
			headerStyle.fontSize = 11;
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
    }

	public override float GetHeight()
	{
        QTitle att = attribute as QTitle;

		float headerHeight = 0;
        
		if( !String.IsNullOrEmpty(att.header) )
		{
			headerHeight += EditorGUIUtility.singleLineHeight*1.0f;
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