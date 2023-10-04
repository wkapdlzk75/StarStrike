using Newtonsoft.Json;
using UnityEngine;
public class JsonTest : MonoBehaviour
{
    public TestClass002[] TestVariable;
    private void Start()
    {
        var result = JsonConvert.SerializeObject(TestVariable);//Convert to json
        Debug.Log(result);
    }
}