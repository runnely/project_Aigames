﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace K_PathFinder {
    [CustomPropertyDrawer(typeof(AreaAdvanced))]
    public class AdvancedAreaDrawer : PropertyDrawer {
        static string nameString = "name";
        static string colorString = "color";
        static string overridePriorityString = "overridePriority";
        static string costString = "cost";

        static GUIContent overridePriorityContent = new GUIContent("Priority");
        static GUIContent costContent = new GUIContent("Cost");
        static GUIContent colorContent = new GUIContent("Color");

        float l1Width = 45;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return (base.GetPropertyHeight(property, label) * 2) + 2;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty name = property.FindPropertyRelative(nameString);
            SerializedProperty color = property.FindPropertyRelative(colorString);
            SerializedProperty overridePriority = property.FindPropertyRelative(overridePriorityString);
            SerializedProperty cost = property.FindPropertyRelative(costString);

            Rect r1 = new Rect(position.x, position.y, position.width * 0.5f, position.height * 0.5f);
            Rect r2 = new Rect(position.x + (position.width * 0.5f), position.y, position.width * 0.5f, position.height * 0.5f);
            Rect r3 = new Rect(position.x, position.y + (position.height * 0.5f), position.width * 0.5f, position.height * 0.5f);
            Rect r4 = new Rect(position.x + (position.width * 0.5f), position.y + (position.height * 0.5f), position.width * 0.5f, position.height * 0.5f);


            float lw = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = l1Width;

            EditorGUI.LabelField(new Rect(r1.x, r1.y, l1Width, r1.height), "Name");
            EditorGUI.BeginChangeCheck();
            string s = EditorGUI.TextArea(new Rect(new Rect(r1.x + l1Width, r1.y, r1.width - l1Width, r1.height)), name.stringValue);
            if (EditorGUI.EndChangeCheck()) {
                name.stringValue = s;
            }

            EditorGUI.PropertyField(r2, overridePriority, overridePriorityContent);
            EditorGUI.PropertyField(r3, color, colorContent);
            EditorGUI.PropertyField(r4, cost, costContent);

            EditorGUIUtility.labelWidth = lw;

            //if (area.drawLabel & (label.text != string.Empty))
            //    position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            //int value = property.intValue;

            ////GUI.Label(new Rect(position.x, position.y, idWidth, position.height), value.ToString());

            //Area pathFinderArea = PathFinder.GetArea(value);
            //if (pathFinderArea == null) {
            //    property.intValue = 0;
            //    pathFinderArea = PathFinder.GetArea(0);
            //}

            //TextAnchor curAnchor = GUI.skin.box.alignment;
            //GUI.skin.box.alignment = TextAnchor.MiddleCenter;
            //Color curColor = GUI.color;
            //GUI.color = pathFinderArea.color;
            //GUI.Box(new Rect(position.x, position.y, colorBoxWidth, position.height), value.ToString());
            //GUI.color = curColor;
            //GUI.skin.box.alignment = curAnchor;

            //EditorGUI.BeginChangeCheck();
            //value = EditorGUI.IntPopup(
            //    new Rect(position.x + colorBoxWidth, position.y, position.width - colorBoxWidth, position.height),
            //    value, PathFinder.settings.areaNames, PathFinder.settings.areaIDs);
            //if (EditorGUI.EndChangeCheck()) {
            //    property.intValue = value;
            //}

            EditorGUI.EndProperty();

        }
    }

    [CustomPropertyDrawer(typeof(AreaAttribute))]
    public class AreaAttributeDrawer : PropertyDrawer {
        const float colorBoxWidth = 25f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            AreaAttribute area = attribute as AreaAttribute;

            EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType != SerializedPropertyType.Integer) {
                EditorGUI.LabelField(position, label.text, "Use this attribute with Integer");
            }
            else {
                if (PathFinder.settings == null) {
                    GUI.Label(position, "Waiting for PathFinder to init");
                    return;
                }

                if (area.drawLabel & (label.text != string.Empty))
                    position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

                int value = property.intValue;

                //GUI.Label(new Rect(position.x, position.y, idWidth, position.height), value.ToString());

                Area pathFinderArea = PathFinder.GetArea(value);
                if (pathFinderArea == null) {
                    property.intValue = 0;
                    pathFinderArea = PathFinder.GetArea(0);
                }

                TextAnchor curAnchor = GUI.skin.box.alignment;
                GUI.skin.box.alignment = TextAnchor.MiddleCenter;
                Color curColor = GUI.color;
                GUI.color = pathFinderArea.color;
                GUI.Box(new Rect(position.x, position.y, colorBoxWidth, position.height), value.ToString());
                GUI.color = curColor;
                GUI.skin.box.alignment = curAnchor;

                EditorGUI.BeginChangeCheck();
                value = EditorGUI.IntPopup(
                    new Rect(position.x + colorBoxWidth, position.y, position.width - colorBoxWidth, position.height),
                    value, PathFinder.settings.areaNames, PathFinder.settings.areaIDs);
                if (EditorGUI.EndChangeCheck()) {
                    property.intValue = value;
                }
            }

            EditorGUI.EndProperty();
        }
    }

    [CustomPropertyDrawer(typeof(TagAttribute))]
    public class MyTagAttributeDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            TagAttribute myTag = attribute as TagAttribute;

            EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType != SerializedPropertyType.String) {
                EditorGUI.LabelField(position, label.text, "Use this attribute with string");
            }
            else {
                if (myTag.drawLabel & (label.text != string.Empty))
                    position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

                EditorGUI.BeginChangeCheck();
                string value = EditorGUI.TagField(position, property.stringValue);
                if (EditorGUI.EndChangeCheck()) {
                    property.stringValue = value;
                }
            }

            EditorGUI.EndProperty();
        }
    }

    [CustomPropertyDrawer(typeof(LayerPFAttribute))]
    public class LayerPFAttributeDrawer : PropertyDrawer {   
        static string valueString = "value";
        static string bitmaskString = "BitMaskPF";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            LayerPFAttribute layer = attribute as LayerPFAttribute;

            EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType == SerializedPropertyType.Integer) {
                if (layer.drawLabel & (label.text != string.Empty))
                    position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

                if (PathFinder.settings == null)
                    EditorGUI.LabelField(position, label.text, "Waiting for PathFinder Initialization");
                else {
                    int value = property.intValue;
                    EditorGUI.BeginChangeCheck();
                    value = EditorGUI.IntPopup(position, value, PathFinder.settings.layerSellectorStrings, PathFinder.settings.layerSellectorInts);
                    if (EditorGUI.EndChangeCheck())
                        property.intValue = value;
                }
            }
            else if (property.type == bitmaskString) {
                if (PathFinder.settings == null)
                    EditorGUI.LabelField(position, label.text, "Waiting for PathFinder Initialization");
                else {
                    //boi. here we go
                    SerializedProperty value = property.FindPropertyRelative(valueString);
                    int workValue = 0;
                    int serializedValue = value.intValue;

                    int[] ints = PathFinder.settings.layerSellectorInts;

                    for (int i = 0; i < ints.Length; i++) {
                        if ((1 << ints[i] & serializedValue) != 0)
                            workValue |= 1 << i;
                    }

                    EditorGUI.BeginChangeCheck();
                    workValue = EditorGUI.MaskField(position, label, workValue, PathFinder.settings.layerSellectorStrings);

                    if (EditorGUI.EndChangeCheck()) {
                        serializedValue = 0;
                        for (int i = 0; i < ints.Length; i++) {
                            if ((1 << i & workValue) != 0)
                                serializedValue |= 1 << ints[i];
                        }

                        value.intValue = serializedValue;
                    }
                }
            }
            else {
                EditorGUI.LabelField(position, label.text, "Use this attribute with Integer or BitMaskPF type");
            }

            EditorGUI.EndProperty();
            
        }


        [CustomPropertyDrawer(typeof(BitMaskPF))]
        public class BitMaskPFDrawer : PropertyDrawer {
            //aw yeah
            static string[] bitStrings = new string[32] {
                "0", "1", "2", "3", "4", "5", "6", "7",
                "8", "9", "10", "11", "12", "13", "14", "15",
                "16", "17", "18", "19", "20", "21", "22", "23",
                "24", "25", "26", "27", "28", "29", "30", "31"
            };
            static string valueString = "value";

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
                EditorGUI.BeginProperty(position, label, property);
                SerializedProperty value = property.FindPropertyRelative(valueString);
                int serializedValue = value.intValue;

                EditorGUI.BeginChangeCheck();
                serializedValue = EditorGUI.MaskField(position, label, serializedValue, bitStrings);
                if (EditorGUI.EndChangeCheck()) 
                    value.intValue = serializedValue; 

                EditorGUI.EndProperty();
            }
        }
    }
    
}