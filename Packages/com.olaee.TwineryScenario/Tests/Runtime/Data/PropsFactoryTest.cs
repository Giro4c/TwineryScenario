using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TwineryScenario.Runtime.Scripts.Core;
using TwineryScenario.Runtime.Scripts.Data;
using TwineryScenario.Runtime.Scripts.Data.ReadModels;
using UnityEngine;
using UnityEngine.TestTools;

namespace TwineryScenario.Runtime.Tests.Data
{
    public class PropsFactoryTest
    {

        private PropsFactory GetInitPropsFactory()
        {
            // Declare attributes values
            Emotions emotions = Resources.Load<Emotions>("Tests/ScriptableObjects/Change/EmotionsListTest_1");
            Persons persons = Resources.Load<Persons>("Tests/ScriptableObjects/Change/PersonsListTest_1");
            
            // Return new props factory using the full constructor
            return new PropsFactory(emotions, persons);

        }
        
        [Test]
        public void TestConstructorEmpty()
        {
            // Call empty constructor
            PropsFactory propsFactory = new PropsFactory();

            // Verify default values
            Assert.Null(propsFactory.emotionsList);
            Assert.Null(propsFactory.personsList);
            
        }
        
        [Test]
        public void TestConstructorFull()
        {
            // Declare test values
            Emotions emotions = Resources.Load<Emotions>("Tests/ScriptableObjects/Change/EmotionsListTest_1");
            Persons persons = Resources.Load<Persons>("Tests/ScriptableObjects/Change/PersonsListTest_1");

            // Call full constructor
            PropsFactory propsFactory = new PropsFactory(emotions, persons);

            // Verify initialized values
            Assert.NotNull(propsFactory.emotionsList);
            Assert.IsTrue(propsFactory.emotionsList == emotions);
            Assert.IsTrue(propsFactory.emotionsList.emotions.Count == emotions.emotions.Count);
            Assert.NotNull(propsFactory.personsList);
            Assert.IsTrue(propsFactory.personsList == persons);
            Assert.IsTrue(propsFactory.personsList.persons.Count == persons.persons.Count);
            
        }
        
        [Test]
        public void TestClear()
        {
            // Get initialized props factory
            PropsFactory propsFactory = GetInitPropsFactory();
            
            // Add an item for each (in case lists are already empty)
            propsFactory.emotionsList.emotions.Add(Resources.Load<Emotion>("Tests/ScriptableObjects/Constants/EmotionTest_1"));
            Assert.IsFalse(propsFactory.emotionsList.emotions.Count == 0);
            propsFactory.personsList.persons.Add(Resources.Load<Person>("Tests/ScriptableObjects/Constants/PersonTest_1"));
            Assert.IsFalse(propsFactory.personsList.persons.Count == 0);

            // Call clear method
            propsFactory.Clear();
            
            // Verify changed values
            Assert.IsTrue(propsFactory.emotionsList.emotions.Count == 0);
            Assert.IsTrue(propsFactory.personsList.persons.Count == 0);
            
        }

        [Test]
        public void TestConvertReadModelBase()
        {
            // Create props factory and clears the referenced objects (lists)
            PropsFactory propsFactory = GetInitPropsFactory();
            propsFactory.Clear();
            
            // Declare global read model
            GlobalPropsReadModel readModel = new GlobalPropsReadModel();
            readModel.type = "Base";
            
            // Get props from conversion
            Props props = propsFactory.ConvertReadModel(readModel);
            
            // Assert correct values
            Assert.NotNull(props);
            Assert.IsTrue(props.type == readModel.type);
            
            // Verify that lists have not been changed
            Assert.IsTrue(propsFactory.emotionsList.emotions.Count == 0);
            Assert.IsTrue(propsFactory.personsList.persons.Count == 0);

        }
        
        [Test]
        public void TestConvertReadModelBaseDialog()
        {
            // Create props factory and clears the referenced objects (lists)
            PropsFactory propsFactory = GetInitPropsFactory();
            propsFactory.Clear();
            
            // Declare global read model
            GlobalPropsReadModel readModel = new GlobalPropsReadModel();
            readModel.type = "Base Dialog";
            readModel.speaker = "Arthur";
            readModel.emotion = "Happy";
            int nextPersonId = propsFactory.personsList.persons.Count;
            
            // Get props from conversion
            Props props = propsFactory.ConvertReadModel(readModel);
            
            // Assert correct values
            Assert.NotNull(props);
            Assert.IsTrue(props.type == readModel.type);
            
            // Cast to specific props type : BaseDialogProps
            BaseDialogProps baseDialogProps = props as BaseDialogProps;
            
            // Assert correct values
            Assert.NotNull(baseDialogProps);
            Assert.IsTrue(baseDialogProps.emotion.emotionName == readModel.emotion);
            Assert.IsTrue(baseDialogProps.speaker.id == nextPersonId);
            Assert.IsTrue(baseDialogProps.speaker.name == readModel.speaker);
            
            // Verify that lists have been changed
            Assert.IsTrue(propsFactory.emotionsList.emotions.Count == 1);
            Assert.NotNull(propsFactory.emotionsList.GetEmotion(readModel.emotion));
            Assert.IsTrue(propsFactory.personsList.persons.Count == 1);
            Assert.NotNull(propsFactory.personsList.GetPerson(readModel.speaker));
        }

        [Test]
        public void TestConvertReadModelUnknown()
        {
            // Create props factory and clears the referenced objects (lists)
            PropsFactory propsFactory = GetInitPropsFactory();
            propsFactory.Clear();
            
            // Declare global read model
            GlobalPropsReadModel readModel = new GlobalPropsReadModel();
            readModel.type = "Unknown";
            
            // Get props from conversion
            Props props = propsFactory.ConvertReadModel(readModel);
            
            // Assert correct values
            Assert.NotNull(props);
            Assert.IsTrue(props.type == "Base");
            
            // Verify that lists have not been changed
            Assert.IsTrue(propsFactory.emotionsList.emotions.Count == 0);
            Assert.IsTrue(propsFactory.personsList.persons.Count == 0);
        }
        
    }
}