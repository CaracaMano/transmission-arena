using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

enum AxisType {
  KeyOrMouseButton = 0,
  MouseMovement = 1,
  JoystickAxis = 2
};

class InputAxis {
  public string name = null;
  public string descriptiveName = null;
  public string descriptiveNegativeName = null;
  public string negativeButton = null;
  public string positiveButton = null;
  public string altNegativeButton = null;
  public string altPositiveButton = null;

  public float gravity = 0;
  public float dead = 0;
  public float sensitivity = 0;

  public bool snap = false;
  public bool invert = false;

  public AxisType type = 0;

  public int axis = 0;
  public int joyNum = 0;
}

public class InputManagerEditor : EditorWindow {
  string log;
  SerializedObject serializedObject;

  // Add menu named "My Window" to the Window menu
  [MenuItem("Window/InputManagerEditor")]
  static void Init() {
    // Get existing open window or if none, make a new one:
    InputManagerEditor window = (InputManagerEditor)EditorWindow.GetWindow(typeof(InputManagerEditor));
    window.Show();
  }

  void OnEnable() {
    serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
    log = "ok";
  }

  void OnGUI() {
    if (GUILayout.Button("Start")) {
      SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");
      axesProperty.ClearArray();
      SetMenuAxis();
      SetJoystickAxis();
      log = "Done";
    }

    ReadOnlyTextField("Output", log);
  }

  void SetJoystickAxis() {
    for (int i = 1; i <= 4; i++) {
      for (int j = 0; j <= 14; j++) {
        AddAxis(new InputAxis() {
          name = "j" + i + "a" + j.ToString(),
          dead = 0.4f,
          sensitivity = 1f,
          type = AxisType.KeyOrMouseButton,
          positiveButton = "joystick " + i + " button " + j,
          axis = j,
          joyNum = i,
        });
      }
    }
  }

  void SetMenuAxis() {
    AddAxis(new InputAxis() {
      name = "Horizontal",
      sensitivity = 3f,
      gravity = 3f,
      dead = 0.4f,
      type = AxisType.KeyOrMouseButton,
      negativeButton = "left",
      positiveButton = "right",
      altNegativeButton = "a",
      altPositiveButton = "d",
      axis = 1
    });
    AddAxis(new InputAxis() {
      name = "Horizontal",
      sensitivity = 3f,
      gravity = 3f,
      dead = 0.4f,
      type = AxisType.JoystickAxis,
      axis = 1
    });
    AddAxis(new InputAxis() {
      name = "Vertical",
      sensitivity = 3f,
      gravity = 3f,
      dead = 0.4f,
      type = AxisType.KeyOrMouseButton,
      negativeButton = "down",
      positiveButton = "up",
      altNegativeButton = "s",
      altPositiveButton = "w",
      axis = 2
    });
    AddAxis(new InputAxis() {
      name = "Vertical",
      sensitivity = 3f,
      gravity = 3f,
      dead = 0.4f,
      invert = true,
      type = AxisType.JoystickAxis,
      axis = 2
    });
    AddAxis(new InputAxis() {
      name = "Mouse X",
      sensitivity = 1f,
      type = AxisType.MouseMovement,
      axis = 1
    });
    AddAxis(new InputAxis() {
      name = "Mouse Y",
      sensitivity = 1f,
      type = AxisType.MouseMovement,
      axis = 2
    });
    AddAxis(new InputAxis() {
      name = "Submit",
      positiveButton = "return",
      altPositiveButton = "joystick button 0",
      sensitivity = 1000f,
      type = AxisType.KeyOrMouseButton,
      axis = 1
    });
    AddAxis(new InputAxis() {
      name = "Submit",
      positiveButton = "enter",
      altPositiveButton = "space",
      sensitivity = 1000f,
      type = AxisType.KeyOrMouseButton,
      axis = 1
    });
    AddAxis(new InputAxis() {
      name = "Cancel",
      positiveButton = "escape",
      altPositiveButton = "joystick button 1",
      sensitivity = 1000f,
      type = AxisType.KeyOrMouseButton,
      axis = 1
    });
  }

  void ReadOnlyTextField(string label, string text) {
    EditorGUILayout.BeginHorizontal();
    EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth - 4));
    EditorGUILayout.SelectableLabel(text, EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
    EditorGUILayout.EndHorizontal();
  }

  private SerializedProperty GetChildProperty(SerializedProperty parent, string name) {
    SerializedProperty child = parent.Copy();
    child.Next(true);
    do {
      if (child.name == name) return child;
    }
    while (child.Next(false));
    return null;
  }

  private bool AxisDefined(string axisName) {
    SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");

    axesProperty.Next(true);
    axesProperty.Next(true);
    while (axesProperty.Next(false)) {
      SerializedProperty axis = axesProperty.Copy();
      axis.Next(true);
      if (axis.stringValue == axisName) return true;
    }
    return false;
  }

  private void AddAxis(InputAxis axis) {
    //if (AxisDefined(axis.name)) return;

    SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");

    axesProperty.arraySize++;
    serializedObject.ApplyModifiedProperties();

    SerializedProperty axisProperty = axesProperty.GetArrayElementAtIndex(axesProperty.arraySize - 1);

    GetChildProperty(axisProperty, "m_Name").stringValue = axis.name;
    GetChildProperty(axisProperty, "descriptiveName").stringValue = axis.descriptiveName;
    GetChildProperty(axisProperty, "descriptiveNegativeName").stringValue = axis.descriptiveNegativeName;
    GetChildProperty(axisProperty, "negativeButton").stringValue = axis.negativeButton;
    GetChildProperty(axisProperty, "positiveButton").stringValue = axis.positiveButton;
    GetChildProperty(axisProperty, "altNegativeButton").stringValue = axis.altNegativeButton;
    GetChildProperty(axisProperty, "altPositiveButton").stringValue = axis.altPositiveButton;
    GetChildProperty(axisProperty, "gravity").floatValue = axis.gravity;
    GetChildProperty(axisProperty, "dead").floatValue = axis.dead;
    GetChildProperty(axisProperty, "sensitivity").floatValue = axis.sensitivity;
    GetChildProperty(axisProperty, "snap").boolValue = axis.snap;
    GetChildProperty(axisProperty, "invert").boolValue = axis.invert;
    GetChildProperty(axisProperty, "type").intValue = (int)axis.type;
    GetChildProperty(axisProperty, "axis").intValue = axis.axis - 1;
    GetChildProperty(axisProperty, "joyNum").intValue = axis.joyNum;

    serializedObject.ApplyModifiedProperties();
  }
}
