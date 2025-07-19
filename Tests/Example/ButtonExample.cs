using UnityEngine;
using UnityGameLib.Attribute;

public class ButtonExample : MonoBehaviour
{
    [Button]
    public void TestFunction()
    {
        Debug.Log("TestFunction");
    }
    
    [Button]
    public void TestFunctionWithParameters( string param )
    {
        Debug.Log($"TestFunctionWithParameters: {param}");
    }
    
    [Button(ButtonName = "RENAME")]
    public void TestFunctionRename()
    {
        Debug.Log("TestFunctionRename");
    }
    
    [Button]
    public void TestFunctionResource([ResourceList(Path = "Assets/UnityGameLib/Tests/Example/Resource/Button", Pattern = "A*.json")] string file)
    {
        Debug.Log("TestFunctionResource: " + file);
    }
}
