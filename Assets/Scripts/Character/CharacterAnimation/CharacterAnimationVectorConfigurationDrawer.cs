using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(CharacterAnimationVectorConfiguration))]
public class CharacterAnimationVectorConfigurationDrawer : QPropertyDrawer
{

   
    //Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

       


        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);


        base.OnGUI(position, property, label);
        

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Set anchor rect
        //anchorRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

        PropertyFieldLine(property.FindPropertyRelative("vectorBehaviour"));


        if ((CharacterAnimationVectorConfiguration.VectorBehaviour)property.FindPropertyRelative("vectorBehaviour").enumValueIndex != CharacterAnimationVectorConfiguration.VectorBehaviour.AlwaysFollow)
        {
            Divider(QEditor.LineStyle.Dashed50);

            LabelFieldLine("Transition Properties:", EditorStyles.boldLabel);
          
            PropertyFieldLine(property.FindPropertyRelative("vectorRotationTransitionSpeed"), new GUIContent("Rotation Transition Speed"));
            PropertyFieldLine(property.FindPropertyRelative("vectorRotationTransitionFunctionType"), new GUIContent("Rotation Transition Function"));
        }

      

        if ((CharacterAnimationVectorConfiguration.VectorBehaviour)property.FindPropertyRelative("vectorBehaviour").enumValueIndex != CharacterAnimationVectorConfiguration.VectorBehaviour.Custom)
        {
            Divider(QEditor.LineStyle.Dashed50);

            LabelFieldLine("Target Vector Properties:", EditorStyles.boldLabel);

            PropertyFieldLine(property.FindPropertyRelative("followVector"), new GUIContent("Follow this vector:"));

        }

        Divider(QEditor.LineStyle.Dashed50);


        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }

    //// Draw the property inside the given rect
    //public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //{
    //    // Using BeginProperty / EndProperty on the parent property means that
    //    // prefab override logic works on the entire property.
    //    EditorGUI.BeginProperty(position, label, property);

    //    // Draw label
    //    position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

    //    // Don't make child fields be indented
    //    var indent = EditorGUI.indentLevel;
    //    EditorGUI.indentLevel = 0;

    //    // Calculate rects
    //    var amountRect = new Rect(position.x, position.y, 30, position.height);
    //    var unitRect = new Rect(position.x + 35, position.y, 50, position.height);
    //    var nameRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);


    //    // Draw fields - passs GUIContent.none to each so they are drawn without labels
    //    EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);
    //    EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("unit"), GUIContent.none);
    //    EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);

    //    EditorGUI.IntField(amountRect, GUIContent.none, 0);

    //    // Set indent back to what it was
    //    EditorGUI.indentLevel = indent;

    //    EditorGUI.EndProperty();
    //}








    // OLD:

//   using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomPropertyDrawer(typeof(CharacterAnimationVectorConfiguration))]
//public class CharacterAnimationVectorConfigurationDrawer : QPropertyDrawer
//{
//    int lineCount;

//    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//    {
//        return Mathf.Max(base.GetPropertyHeight(property, label), EditorGUIUtility.singleLineHeight * (lineCount+1));
//    }
//    //Draw the property inside the given rect
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        lineCount = 0;
//        // Using BeginProperty / EndProperty on the parent property means that
//        // prefab override logic works on the entire property.
//        EditorGUI.BeginProperty(position, label, property);

//        // Draw label
//        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

//        // Don't make child fields be indented
//        var indent = EditorGUI.indentLevel;
//        EditorGUI.indentLevel = 0;

//        // Calculate rects
//        var tempRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        

//        // Draw fields - passs GUIContent.none to each so they are drawn without labels
//        EditorGUI.PropertyField(tempRect, property.FindPropertyRelative("vectorBehaviour"), GUIContent.none);
//        lineCount = lineCount + 1;

//        //[QDivider("Hallo", "Subheader")]
//        //int abc;

//        if ((CharacterAnimationVectorConfiguration.VectorBehaviour)property.FindPropertyRelative("vectorBehaviour").enumValueIndex != CharacterAnimationVectorConfiguration.VectorBehaviour.AlwaysFollow)
//        {
//            //GUILayout.Box("", new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(1) });
//            //tempRect.position = new Vector2(position.x, position.y + EditorGUIUtility.singleLineHeight * lineCount);
//            //var temptempRect = tempRect;
//            //temptempRect.height = 2;
//            //GUI.skin.box.Draw(temptempRect, GUIContent.none,10);
//            //lineCount = lineCount + 1;

//            tempRect.position = new Vector2(position.x, position.y + EditorGUIUtility.singleLineHeight * lineCount);
//            EditorGUI.LabelField(tempRect, "----------------------------------------------------------------------------------------", EditorStyles.label);
//            lineCount = lineCount + 1;

//            tempRect.position = new Vector2(position.x, position.y + EditorGUIUtility.singleLineHeight * lineCount);
//            EditorGUI.LabelField(tempRect, "Transition Properties:", EditorStyles.boldLabel);
//            lineCount = lineCount + 1;



//            tempRect.position = new Vector2(position.x, position.y + EditorGUIUtility.singleLineHeight * lineCount);
//            EditorGUI.PropertyField(tempRect, property.FindPropertyRelative("vectorRotationTransitionSpeed"), new GUIContent("Rotation Transition Speed"));
//            lineCount = lineCount + 1;

//            tempRect.position = new Vector2(position.x, position.y + EditorGUIUtility.singleLineHeight * lineCount);
//            EditorGUI.PropertyField(tempRect, property.FindPropertyRelative("vectorRotationTransitionFunctionType"), new GUIContent("Rotation Transition Function"));
//            lineCount = lineCount + 1;
//        }

      

//        if ((CharacterAnimationVectorConfiguration.VectorBehaviour)property.FindPropertyRelative("vectorBehaviour").enumValueIndex != CharacterAnimationVectorConfiguration.VectorBehaviour.Custom)
//        {
//            tempRect.position = new Vector2(position.x, position.y + EditorGUIUtility.singleLineHeight * lineCount);
//            EditorGUI.LabelField(tempRect, "----------------------------------------------------------------------------------------", EditorStyles.label);
//            lineCount = lineCount + 1;

//            tempRect.position = new Vector2(position.x, position.y + EditorGUIUtility.singleLineHeight * lineCount);
//            EditorGUI.LabelField(tempRect, "Target Vector Properties:", EditorStyles.boldLabel);
//            lineCount = lineCount + 1;

//            tempRect.position = new Vector2(position.x, position.y + EditorGUIUtility.singleLineHeight * lineCount);
//            EditorGUI.PropertyField(tempRect, property.FindPropertyRelative("followVector"), new GUIContent("Follow this vector:"));
//            lineCount = lineCount + 1;

//        }

//        tempRect.position = new Vector2(position.x, position.y + EditorGUIUtility.singleLineHeight * lineCount);
//        EditorGUI.LabelField(tempRect, "----------------------------------------------------------------------------------------", EditorStyles.label);
//        lineCount = lineCount + 1;


//        // Set indent back to what it was
//        EditorGUI.indentLevel = indent;

//        EditorGUI.EndProperty();
//    }

//    //// Draw the property inside the given rect
//    //public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    //{
//    //    // Using BeginProperty / EndProperty on the parent property means that
//    //    // prefab override logic works on the entire property.
//    //    EditorGUI.BeginProperty(position, label, property);

//    //    // Draw label
//    //    position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

//    //    // Don't make child fields be indented
//    //    var indent = EditorGUI.indentLevel;
//    //    EditorGUI.indentLevel = 0;

//    //    // Calculate rects
//    //    var amountRect = new Rect(position.x, position.y, 30, position.height);
//    //    var unitRect = new Rect(position.x + 35, position.y, 50, position.height);
//    //    var nameRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);


//    //    // Draw fields - passs GUIContent.none to each so they are drawn without labels
//    //    EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);
//    //    EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("unit"), GUIContent.none);
//    //    EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);

//    //    EditorGUI.IntField(amountRect, GUIContent.none, 0);

//    //    // Set indent back to what it was
//    //    EditorGUI.indentLevel = indent;

//    //    EditorGUI.EndProperty();
//    //}
//}



   
}


