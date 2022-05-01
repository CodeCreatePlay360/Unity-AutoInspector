using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

           
namespace CodeCreatePlay
{
    namespace AutoEditor
    {
        public static class AutoEdCntrlBuilder
        {
            static FieldInfo info;

            public static void CreateControl(EditorFieldAttr attr, Type t, ref System.Object obj)
            {
                if (attr.CtrlType == ControlType.intField)
                {
                    FieldInfo info = t.GetField(attr.Name);
                    if (info != null)
                    {
                        int intVal = (int)info.GetValue(obj);
                        intVal = EditorGUILayout.IntField(attr.Name, intVal);
                        info.SetValue(obj, intVal);
                    }

                }
                else if (attr.CtrlType == ControlType.floatField)
                {
                    info = t.GetField(attr.Name);
                    if (info != null)
                    {
                        float floatVal = (float)info.GetValue(obj);
                        floatVal = EditorGUILayout.FloatField(attr.Name, floatVal);
                        info.SetValue(obj, floatVal);
                    }
                }
                else if (attr.CtrlType == ControlType.boolField)
                {
                    GUILayout.BeginHorizontal();
                    info = t.GetField(attr.Name);
                    bool boolField = false;
                    if (info != null)
                    {
                        boolField = (bool)info.GetValue(obj);
                        boolField = EditorGUILayout.Toggle("", boolField, GUILayout.Width(17));
                        GUILayout.Label(attr.Name);
                        info.SetValue(obj, boolField);
                    }
                    GUILayout.EndHorizontal();

                    if (boolField && attr.Message != "")
                        EditorGUILayout.HelpBox(attr.Message, MessageType.Info);
                }
                else if (attr.CtrlType == ControlType.vector2)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(2);
                    GUILayout.Label(attr.Name, GUILayout.MinWidth(125));
                    GUILayout.Space(-19);
                    info = t.GetField(attr.Name);
                    if (info != null)
                    {
                        Vector2 vector2 = (Vector2)info.GetValue(obj);
                        vector2 = EditorGUILayout.Vector2Field("", (Vector2)vector2);
                        info.SetValue(obj, (Vector2)vector2);
                    }
                    GUILayout.EndHorizontal();
                }
                else if (attr.CtrlType == ControlType.vector3)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(2);
                    GUILayout.Label(attr.Name, GUILayout.MinWidth(125));
                    GUILayout.Space(-19);
                    info = t.GetField(attr.Name);
                    if (info != null)
                    {
                        Vector3 vector3 = (Vector3)info.GetValue(obj);
                        vector3 = EditorGUILayout.Vector3Field("", (Vector3)vector3);
                        info.SetValue(obj, (Vector3)vector3);
                    }
                    GUILayout.EndHorizontal();
                }
                else if (attr.CtrlType == ControlType.boldLabel)
                {
                    GUILayout.Space(5f);
                    GUILayout.Label(attr.Name, AutoEditor.labelStyle_bold);
                    GUILayout.Space(3f);
                }
                else if (attr.CtrlType == ControlType.text)
                {
                    GUILayout.BeginHorizontal();
                    info = t.GetField(attr.Name);
                    if (info != null)
                    {
                        GUILayout.Label(attr.Name);
                        GUILayout.Space(-250f);
                        GUILayout.Label(info.GetValue(obj).ToString());
                    }
                    GUILayout.EndHorizontal();
                }
                else if (attr.CtrlType == ControlType.textControl)
                {
                    GUILayout.BeginHorizontal();

                    info = t.GetField(attr.Name);
                    if (info != null)
                    {
                        GUILayout.Space(2);
                        GUILayout.Label(attr.Name);
                        string stringCtrl = (string)info.GetValue(obj);
                        stringCtrl = GUILayout.TextField(stringCtrl);
                        info.SetValue(obj, stringCtrl);
                    }

                    GUILayout.EndHorizontal();
                }
                else if (attr.CtrlType == ControlType.tag)
                {
                    info = t.GetField(attr.Name);
                    if (info != null)
                    {
                        // GUILayout.Label(attr.Name);
                        string stringCtrl = (string)info.GetValue(obj);
                        stringCtrl = EditorGUILayout.TagField(attr.Name, stringCtrl);
                        info.SetValue(obj, stringCtrl);
                    }
                }
                else if (attr.CtrlType == ControlType.space)
                {
                    info = t.GetField(attr.Name);
                    if (info != null)
                    {
                        int intVal = (int)info.GetValue(obj);
                        GUILayout.Space(intVal);
                    }
                }
                else if (attr.CtrlType == ControlType.layerField)
                {
                    info = t.GetField(attr.Name);
                    if (info != null)
                    {
                        LayerMask mask = (LayerMask)info.GetValue(obj);
                        mask = EditorGUILayout.LayerField(attr.Name, (LayerMask)mask);
                        info.SetValue(obj, (LayerMask)mask);
                    }
                }
                else if (attr.CtrlType == ControlType.gameObject)
                {
                    info = t.GetField(attr.Name);
                    if (info != null)
                    {
                        GameObject go;
                        try
                        {
                            go = (GameObject)info.GetValue(obj);
                        }
                        catch (InvalidCastException)
                        {
                            info.SetValue(obj, null);
                            go = (GameObject)info.GetValue(obj);
                        }

                        go = (UnityEngine.GameObject)EditorGUILayout.ObjectField(attr.Name, (UnityEngine.GameObject)go,
                            typeof(GameObject), allowSceneObjects: false);

                        info.SetValue(obj, (UnityEngine.GameObject)go);
                    }
                }
                else if (attr.CtrlType == ControlType.texture2d)
                {
                    info = t.GetField(attr.Name);
                    if (info != null)
                    {
                        Texture2D texture = (Texture2D)info.GetValue(obj);
                        texture = (UnityEngine.Texture2D)EditorGUILayout.ObjectField(attr.Name, (UnityEngine.Texture2D)texture,
                            typeof(Texture2D), allowSceneObjects: false);
                        info.SetValue(obj, (UnityEngine.Texture2D)texture);
                    }
                }
                else if (attr.CtrlType == ControlType.unityTerrainField)
                {
                    info = t.GetField(attr.Name);
                    if (info != null)
                    {
                        Terrain terrain = (Terrain)info.GetValue(obj);
                        terrain = (Terrain)EditorGUILayout.ObjectField(attr.Name, (Terrain)terrain,
                            typeof(Terrain), allowSceneObjects: true);
                        info.SetValue(obj, (Terrain)terrain);
                    }
                }
                else if (attr.CtrlType == ControlType.intSlider)
                {
                    info = t.GetField(attr.Name);
                    if (info != null)
                    {
                        int intVal = (int)info.GetValue(obj);

                        // upcast EditorFieldControl to it's inherited FloatSliderAttr
                        IntSliderAttr upcastAttr = attr as IntSliderAttr;

                        intVal = EditorGUILayout.IntSlider(attr.Name, intVal, upcastAttr.minVal, upcastAttr.maxVal);
                        info.SetValue(obj, intVal);
                    }
                } 
                else if (attr.CtrlType == ControlType.floatSlider)
                {
                    info = t.GetField(attr.Name);
                    if (info != null)
                    {
                        float floatVal = (float)info.GetValue(obj);

                        // upcast EditorFieldControl to it's inherited FloatSliderAttr
                        FloatSliderAttr upcastAttr = attr as FloatSliderAttr;

                        floatVal = EditorGUILayout.Slider(attr.Name, floatVal, upcastAttr.minVal, upcastAttr.maxVal);
                        info.SetValue(obj, floatVal);
                    }
                }
                else if (attr.CtrlType == ControlType.color)
                {
                    info = t.GetField(attr.Name);
                    if (info != null)
                    {
                        Color c = (Color)info.GetValue(obj);
                        c = EditorGUILayout.ColorField(attr.Name, c);
                        info.SetValue(obj, (Color)c);
                    }
                }
                else if (attr.CtrlType == ControlType.transformsList)
                {
                    info = t.GetField(attr.Name);
                    if (info != null)
                    {
                        List<Transform> transforms = (List<Transform>)info.GetValue(obj);

                        if (transforms.Count == 0)
                        { transforms.Add(null); }

                        for (int i = 0; i < transforms.Count; i++)
                        {
                            GUILayout.BeginHorizontal();
                            GUILayout.Space(2);

                            transforms[i] = (Transform)EditorGUILayout.ObjectField(
                            transforms[i], typeof(Transform), allowSceneObjects:false);

                            Color oldColor = GUI.backgroundColor;
                            GUI.backgroundColor = Color.red;

                            // create a button to remove this transform.
                            if (transforms[i] != null && GUILayout.Button("-"))
                            {
                                transforms.RemoveAt(i); // on remove
                            }

                            GUI.backgroundColor = oldColor;
                            GUILayout.EndHorizontal();
                        }

                        // add a new null PaintMesh if count is > 0.
                        if (transforms.Count > 0 && transforms[^1] != null)
                            transforms.Add(null);

                        info.SetValue(obj, (List<Transform>)transforms);
                    }
                }
            }
        }


        public class AutoEditor
        {
            public static bool LAYOUT_VERTICAL = false;
            [System.NonSerialized] public static GUIStyle labelStyle_bold;
            [System.NonSerialized] public Color textColor = new (0.8f, 0.8f, 0.8f, 1);

            readonly Type type;
            System.Object obj;
            readonly bool createFoldOut = false;
            bool foldOutOpen = false;


            public AutoEditor(Type _type, ref System.Object _obj, bool _createFoldout = false)
            {
                type = _type;
                obj = _obj;
                createFoldOut = _createFoldout;
            }

            public void Build(float vOffset = 0, float hOffset = 0)
            {
                // create a title style-----------------
                labelStyle_bold = new GUIStyle(EditorStyles.label)
                {
                    fontStyle = FontStyle.Bold,
                    fontSize = 12
                };
                labelStyle_bold.normal.textColor = textColor;
                // --------------------------------------

                if (createFoldOut)
                {
                    // create a foldOut style
                    // -----------------------

                    _Build(vOffset, hOffset);

                    // revert to original foldout style

                }
                else
                {
                    _Build(vOffset, hOffset);
                }
            }

            private void _Build(float vOffset, float hOffset)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(hOffset);

                GUILayout.BeginVertical();

                foreach (FieldInfo field in type.GetFields())
                {
                    foreach (Attribute attr in field.GetCustomAttributes(true))
                    {
                        if (attr is EditorFieldAttr atr)
                        {
                            // 1 = start layout horizontal
                            if (atr.LayoutHorizontal == 1)
                            {
                                GUILayout.BeginHorizontal();
                                AutoEdCntrlBuilder.CreateControl(atr, type, ref obj);
                            }

                            // -1 = end layout horizontal
                            else if (atr.LayoutHorizontal == -1)
                            {
                                AutoEdCntrlBuilder.CreateControl(atr, type, ref obj);
                                GUILayout.EndHorizontal();
                            }
                            else
                            {
                                AutoEdCntrlBuilder.CreateControl(atr, type, ref obj);
                            }

                        }
                    }
                }

                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
            }
        }
         

        [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
        public class EditorFieldAttr : Attribute
        {
            public ControlType CtrlType { get; }

            public string Name { get; } = "";

            // 1 = start layout horizontal
            // 0 = keep layout horizontal
            // -1 = end layout horizontal
            // some other int = dont layout horizontal
            public int LayoutHorizontal { get; set; } = -1;

            public string Message { get; set; } = "";

            public EditorFieldAttr(ControlType t, string _name, int layoutHorizontal = -23, string msg = "")
            {
                CtrlType = t;
                Name = _name;
                LayoutHorizontal = layoutHorizontal;
                Message = msg;
            }
        }


        public class IntSliderAttr : EditorFieldAttr
        {
            public int minVal = 1;
            public int maxVal = 100;

            public IntSliderAttr(ControlType t, string _name, int min, int max) : base(t, _name)
            {
                minVal = min;
                maxVal = max;
            }
        }


        public class FloatSliderAttr : EditorFieldAttr
        {
            public float minVal = 0.5f;
            public float maxVal = 50f;

            public FloatSliderAttr(ControlType t, string _name, float min, float max) : base(t, _name)
            {
                minVal = min;
                maxVal = max;
            }
        }


        public enum ControlType
        {
            floatField,
            intField,
            boolField,
            vector2,
            vector3,
            boldLabel,
            text,
            textControl,
            space,
            layerField,
            tag,
            intSlider,
            floatSlider,
            color,
            gameObject,
            texture2d,
            unityTerrainField,
            transformsList,
            gameObjectsList,
        }
    }
}
