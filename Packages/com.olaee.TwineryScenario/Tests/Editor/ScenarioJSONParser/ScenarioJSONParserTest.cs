using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TwineryScenario.Editor.Scripts.ScenarioParser;
using TwineryScenario.Runtime.Scripts.Core;
using UnityEditor;

namespace TwineryScenario.Editor.Tests.ScenarioParser
{
    public class ScenarioJSONParserTest
    {
        [Test]
        public void TestParseScenario()
        {


        }

        [Test]
        public void TestStoreInAssetDatabaseFull()
        {
            // Declare paths
            string basePath = "Packages/com.olaee.TwineryScenario/Resources/";
            string resourcePath = "Tests/ScriptableObjects/Creation/StoreFull";
            string testPathAll = basePath + resourcePath;
            
            // Declare test values
            Scenario testScenario = Resources.Load<Scenario>("Tests/ScriptableObjects/Constants/ScenarioTest_1");
            ScenarioJSONParser.ScenarioDirectories testDirectories = new ScenarioJSONParser.ScenarioDirectories(
                testPathAll, testPathAll, testPathAll, testPathAll, testPathAll, 
                testPathAll, testPathAll, testPathAll);
            
            // Delete all previously created assets in the initial path folder for the creation
            DeleteAllInFolder(testPathAll);
            
            // Call method: StoreInAssetDatabase(Scenario scenario, ScenarioDirectories directories)
            ScenarioJSONParser.StoreInAssetDatabase(testScenario, testDirectories);
            Assert.NotNull(Resources.Load<Scenario>(resourcePath + "Scenario-Test Scenario"));
            Assert.NotNull(Resources.Load<Link>(resourcePath + "Link-Test Scenario-0"));
            Assert.NotNull(Resources.Load<Node>(resourcePath + "Node Test 1-1-Test Scenario"));
            Assert.NotNull(Resources.Load<Node>(resourcePath + "Node Test 2-2-Test Scenario"));
            Assert.NotNull(Resources.Load<Props>(resourcePath + "Props-Test Scenario-0"));
            Assert.NotNull(Resources.Load<BaseDialogProps>(resourcePath + "BaseDialogProps-Test Scenario-1"));
            Assert.NotNull(Resources.Load<Emotion>(resourcePath + "Happy"));
            Assert.NotNull(Resources.Load<Person>(resourcePath + "Arthur-0"));

        }

        private void DeleteAllInFolder(string folder)
        {
            string[] toEmptyFolder = { folder };
            foreach (var asset in AssetDatabase.FindAssets("", toEmptyFolder))
            {
                var path = AssetDatabase.GUIDToAssetPath(asset);
                AssetDatabase.DeleteAsset(path);
            }
        }
        
        [Test]
        public void TestStoreInAssetDatabase()
        {
            // Create Object to store : Props
            Props props = Props.CreateProps("Test Creation");
            
            // Declare path
            string basePath = "Packages/com.olaee.TwineryScenario/Resources/";
            string resourcePath = "Tests/ScriptableObjects/Creation";
            string testAssetPath = basePath + resourcePath;
            string testAssetName = "TestCreation";
            
            // Delete already existing asset at path
            AssetDatabase.DeleteAsset(testAssetPath + "/" + testAssetName + ".asset");

            // Call method: StoreInAssetDatabase(Object asset, string assetPath, string assetName)
            ScenarioJSONParser.StoreInAssetDatabase(props, testAssetPath, testAssetName);

            // Assert File exists
            Props propsAssert = Resources.Load<Props>(resourcePath + "/" + testAssetName);
            Assert.NotNull(propsAssert);
            Assert.IsTrue(propsAssert.type == "Test Creation");

        }
        
    }
}