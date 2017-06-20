using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*
 * Q PROPERTY DRAWER
 * This API provides easy to use customizations for the inspector in Unity3D.
 * 
 * Styling: Each method (for which it makes sense) have an optional parameter style.
 * If no GUIStyle ist passed to the methods, the default GUIStyle is used. 
 * The QPropertyDrawer default style is not always the unity default one (E.G. some headers are bold by default)
 */

public class QPropertyDrawer : PropertyDrawer
{
    public bool useDefaultLineHeight = true;
    public float customLineHeight = EditorGUIUtility.singleLineHeight;

    public enum FieldLayoutType { Custom, line, column }
    public enum HorizontalAlignment { Left, Center, Right }
    public enum VerticalAlignment { Top, Center, Bottom }
    public enum Float { None, Inherit, Yes }

    public struct Layout
    {
        public float lines;
        public float spacingTop;
        public float spacingBottom;
    }




    private float m_LineCount = 0;
    private GUIContent m_content;
    private Rect m_anchorRect;
    public Stack<Rect> prefixedAnchorRects = new Stack<Rect>();

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return (useDefaultLineHeight ? EditorGUIUtility.singleLineHeight : customLineHeight) * (m_LineCount+1);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent content)
    {
       // base.OnGUI(position, property, label);
        m_anchorRect = new Rect(position);
        m_content = content;
        m_LineCount = 0;
    }


    // LINE FIELDS:

        /// <summary>
        /// Adds whitespace to the sequence.
        /// </summary>
    public void WhiteSpace(float lines)
    {
        m_LineCount = m_LineCount + lines;
    }


    // ToDO: Own alignment class to avoid irritation with prefixed dividers!
    /// <summary>
    /// Adds a horizontal divider to the sequence.
    /// </summary>
    public void Divider(QEditor.LineStyle lineStyle, float lineCount = 1, GUIStyle style = default(GUIStyle))
    {
        var linesAbove = 0.0f;
        var linesBeneath = 0.0f;

        if(lineCount != 1)
        {
            if (style != null)
            {
                switch (style.alignment)
                {
                    case TextAnchor.LowerCenter:
                        goto case TextAnchor.LowerLeft;
                    case TextAnchor.LowerRight:
                        goto case TextAnchor.LowerLeft;
                    case TextAnchor.LowerLeft:
                        linesAbove = lineCount - 1;
                        linesBeneath = 0;
                        style.alignment = TextAnchor.LowerLeft;
                        break;


                    case TextAnchor.UpperCenter:
                        goto case TextAnchor.UpperLeft;
                    case TextAnchor.UpperRight:
                        goto case TextAnchor.UpperLeft;
                    case TextAnchor.UpperLeft:
                        linesAbove = 0;
                        linesBeneath = lineCount - 1;
                        style.alignment = TextAnchor.UpperLeft;
                        break;


                    case TextAnchor.MiddleCenter:
                        goto case TextAnchor.MiddleLeft;
                    case TextAnchor.MiddleRight:
                        goto case TextAnchor.MiddleLeft;
                    case TextAnchor.MiddleLeft:
                        linesAbove = (lineCount - 1) / 2;
                        linesBeneath = linesAbove;
                        style.alignment = TextAnchor.MiddleLeft;
                        break;


                    default:
                        goto case TextAnchor.MiddleCenter;

                }
            }else
            {
                linesAbove = (lineCount - 1) / 2;
                linesBeneath = linesAbove;
            }
        }

        m_LineCount = m_LineCount + linesAbove;

        LabelFieldLine( QEditor.GetLineStroke(lineStyle), style);

        m_LineCount = m_LineCount + linesBeneath;

    }


    /// <summary>
    /// Adds a one line height, full line width label field with auto position to the sequence.
    /// </summary>
    public void PrefixLabelField(int id, GUIContent content, GUIStyle style = default(GUIStyle))
    {
        // Get anchor rect or prefixed anchor, if there is one on the stack
        var tempRect = new Rect((prefixedAnchorRects.Count == 0) ? m_anchorRect : prefixedAnchorRects.Peek());
        tempRect.position = new Vector2(m_anchorRect.x, m_anchorRect.y + EditorGUIUtility.singleLineHeight * m_LineCount);

        prefixedAnchorRects.Push(EditorGUI.PrefixLabel(tempRect, GUIUtility.GetControlID(FocusType.Passive), content, (style == default(GUIStyle) ? EditorStyles.label : style)));

    }

    public void PrefixLabelField(int id,  string label, GUIStyle style = default(GUIStyle))
    {
        PrefixLabelField(id, new GUIContent(label), style);
    }

    public void PrefixLabelField(int id, string label, string tooltip, GUIStyle style = default(GUIStyle))
    {
        PrefixLabelField(id, new GUIContent(label, tooltip), style);

    }


    // ToDo: custom prefix utlity with layer or column system toeasy jump between difeerent depths
    public void ExitPrefixedLabel()
    {
        prefixedAnchorRects.Pop();
    }


    /// <summary>
    /// Adds a one line height, full line width label field with auto position to the sequence.
    /// </summary>
    public void LabelFieldLine(GUIContent content, GUIStyle style = default(GUIStyle))
    {
        // Get anchor rect or prefixed anchor, if there is one on the stack
        var tempRect = new Rect((prefixedAnchorRects.Count == 0) ? m_anchorRect : prefixedAnchorRects.Peek());
        tempRect.position = new Vector2(m_anchorRect.x, m_anchorRect.y + EditorGUIUtility.singleLineHeight * m_LineCount);
        // Set height to single line
        tempRect.height = (useDefaultLineHeight ? EditorGUIUtility.singleLineHeight : customLineHeight);

        EditorGUI.LabelField(tempRect, content, (style == default(GUIStyle) ? EditorStyles.label : style));

        m_LineCount = m_LineCount + 1;
    }

    public void LabelFieldLine(string label, GUIStyle style = default(GUIStyle))
    {
        LabelFieldLine(new GUIContent(label), style);
    }

    public void LabelFieldLine(string label, string tooltip, GUIStyle style = default(GUIStyle))
    {
        LabelFieldLine(new GUIContent(label, tooltip), style);

    }


    /// <summary>
    /// Adds a one line height, full line width property field with a default label-to-propertyfield ratio with auto position to the sequence.
    /// </summary>
    public void PropertyFieldLine(SerializedProperty property, GUIContent content = default(GUIContent), GUIStyle style = default(GUIStyle))
    {
        // Get anchor rect or prefixed anchor, if there is one on the stack
        var tempRect = new Rect((prefixedAnchorRects.Count == 0) ? m_anchorRect : prefixedAnchorRects.Peek());
        tempRect.position = new Vector2(m_anchorRect.x, m_anchorRect.y + EditorGUIUtility.singleLineHeight * m_LineCount);
        // Set height to single line
        tempRect.height = (useDefaultLineHeight ? EditorGUIUtility.singleLineHeight : customLineHeight);

        EditorGUI.PropertyField(tempRect, property, content);

        m_LineCount = m_LineCount + 1;
    }

    public void PropertyFieldLine(SerializedProperty property, string label, GUIStyle style = default(GUIStyle))
    {
        PropertyFieldLine(property, new GUIContent(label), style);
    }

    public void PropertyFieldLine(SerializedProperty property, string label, string tooltip, GUIStyle style = default(GUIStyle))
    {
        PropertyFieldLine(property, new GUIContent(label, tooltip), style);
    }

    

}