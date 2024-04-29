using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSerialJSON : MonoBehaviour
{

    [SerializeField] private TextAsset testText;
    
    // Start is called before the first frame update
    void Start()
    {
        TestJSONInheritWithType();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TestJSONInheritWithType()
    {
        Debug.Log("Test Json utility with Type");
        Debug.Log("Test Base ----------------------");
        TestSerialBase baseTest1 = JsonUtility.FromJson<TestSerialBase>(testText.text);
        Debug.Log(baseTest1);
        Debug.Log(baseTest1.basic);
        
        Debug.Log("Test Inherit ------------------------");
        TestSerialInherit inheritTest2 = JsonUtility.FromJson<TestSerialInherit>(testText.text);
        Debug.Log(inheritTest2);
        Debug.Log(inheritTest2.basic);
        Debug.Log(inheritTest2.inherit);
        Debug.Log(inheritTest2.num);
        
        Debug.Log("Test Inherit to Base to Inherit ----------------------");
        TestSerialBase baseTest2 = JsonUtility.FromJson<TestSerialInherit>(testText.text);
        TestSerialInherit inheritTest1 = (TestSerialInherit) baseTest2;
        Debug.Log(inheritTest1);
        Debug.Log(inheritTest1.basic);
        Debug.Log(inheritTest1.inherit);
        Debug.Log(inheritTest1.num);
        
        Debug.Log("Test Base to Inherit ----------------------");
        TestSerialInherit inheritTest3 = baseTest1 as TestSerialInherit;
        Debug.Log(inheritTest3);
        Debug.Log(inheritTest3.basic);
        Debug.Log(inheritTest3.inherit);
        Debug.Log(inheritTest3.num);
        
    }
    
}
