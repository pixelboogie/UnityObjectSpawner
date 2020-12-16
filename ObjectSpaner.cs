using UnityEditor;
using UnityEngine;

public class AdvancedObjectSpawner : EditorWindow
{

    string objectBaseName = "";
    int objectID = 1;
    GameObject objectToSpawn;
    float objectScale; 
    float spawnRadius = 5f;

    Transform objectContainer;
    bool appendID;
    float minScaleVal = 1f;
    float maxScaleVal = 3f;
    float minScaleLimit = 0.5f;
    float maxScaleLimit = 5f;



[MenuItem("My Tools/Advanced Object Spawner")]
public static void ShowWindow()
{
    GetWindow(typeof(AdvancedObjectSpawner)); // GetWindow inherited from EditorWindow class
}

private void OnGUI()
 {
    GUILayout.Label("Spawn New Object", EditorStyles.boldLabel);

    EditorGUILayout.Space(); // add a space
    objectToSpawn = EditorGUILayout.ObjectField("Prefab to Spawn", objectToSpawn, typeof(GameObject), false) as GameObject;
    objectContainer = EditorGUILayout.ObjectField("Object Container", objectContainer, typeof(Transform), true) as Transform;
    EditorGUILayout.HelpBox("Object Container not required.", MessageType.None, false);

    EditorGUILayout.Space(); 

    objectBaseName = EditorGUILayout.TextField("Base Name", objectBaseName);

    appendID = EditorGUILayout.BeginToggleGroup("Append Numerical ID", appendID);
    EditorGUI.indentLevel++;
    objectID = EditorGUILayout.IntField("Object ID", objectID);
    EditorGUI.indentLevel--;
    EditorGUILayout.EndToggleGroup();

    EditorGUILayout.Space();

    spawnRadius = EditorGUILayout.FloatField("Spawn Radius", spawnRadius);

    EditorGUILayout.Space();

    GUILayout.Label("Object Scale");

    EditorGUILayout.BeginHorizontal();
    EditorGUILayout.PrefixLabel("Min Limit: " + minScaleLimit);
    EditorGUILayout.MinMaxSlider(ref minScaleVal, ref maxScaleVal, minScaleLimit, maxScaleLimit);
    EditorGUILayout.PrefixLabel("Max Limit: " + maxScaleLimit);
    EditorGUILayout.EndHorizontal();

    EditorGUILayout.BeginHorizontal();
    EditorGUILayout.LabelField("Min Value: " + minScaleVal.ToString());
    EditorGUILayout.LabelField("Max Value: " + maxScaleVal.ToString());
    EditorGUILayout.EndHorizontal();

    EditorGUILayout.Space();

    EditorGUI.BeginDisabledGroup(objectToSpawn == null || objectBaseName == string.Empty || (objectContainer != null && EditorUtility.IsPersistent(objectContainer)));

  
    if (GUILayout.Button("Spawn Object"))
    {
        SpawnObject();
    }
 
    EditorGUI.EndDisabledGroup();
    
    EditorGUILayout.Space();

   if(objectToSpawn == null)
     {
         EditorGUILayout.HelpBox("Place a GameObject in the 'Prefab to Span' field.", MessageType.Warning);
     }
     if (objectBaseName == string.Empty)
     {
         EditorGUILayout.HelpBox("Assign a base name to the object to be spawned.", MessageType.Warning);
     }
     if (objectContainer != null && EditorUtility.IsPersistent(objectContainer))
     {
         EditorGUILayout.HelpBox("Object Container must be a scene object.", MessageType.Warning);
     }

 }

    private void SpawnObject()
    {
    Vector2 spawnCircle = Random.insideUnitCircle * spawnRadius;
    Vector3 spawnPos = new Vector3(spawnCircle.x, 0f, spawnCircle.y);
    objectScale = Random.Range(minScaleVal, maxScaleVal);

    string objName = objectBaseName;
    if(appendID)
    {
        objName += objectID.ToString();
        objectID++;
    }
    GameObject newObject = Instantiate(objectToSpawn, spawnPos, Quaternion.identity, objectContainer);
    newObject.name = objName;
    newObject.transform.localScale = Vector3.one * objectScale;
    }

}
