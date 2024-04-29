using TwineryScenario.Runtime.Scripts.Core;
using TwineryScenario.Runtime.Scripts.Data.ReadModels;

namespace TwineryScenario.Runtime.Scripts.Data
{
    public class PropsFactory
    {

        public Emotions emotionsList { get; set; }
        public Persons personsList { get; set; }

        public PropsFactory()
        {
            
        }
        
        public PropsFactory(Emotions emotionsList, Persons personsList)
        {
            this.emotionsList = emotionsList;
            this.personsList = personsList;
        }
        
        public Props ConvertReadModel(PropsReadModel readModel)
        {
            switch (readModel.type)
            {
                // Base Dialog
                case "Base Dialog":
                    BaseDialogPropsReadModel baseDialogReadModel = readModel as BaseDialogPropsReadModel;
                    if (baseDialogReadModel == null) return null;
                    // Verify that the person in the props exists.
                        // Search by name
                    Person person = personsList.GetPerson(baseDialogReadModel.speaker);
                
                    // If it does not exist in the list, creates a new person and adds it to the list
                    if (!person)
                    {
                        // person = Person.CreatePerson(int.Parse(tmpProps.speaker.id), tmpProps.speaker.name);
                        person = Person.CreatePerson(personsList.persons.Count, baseDialogReadModel.speaker);
                    }
                
                    // Verify that the emotion in the props exists.
                    Emotion emotion = emotionsList.GetEmotion(baseDialogReadModel.emotion);
                    // If it does not exist in the list, creates a new emotion and adds it to the list
                    if (!emotion)
                    {
                        emotion = Emotion.CreateEmotion(baseDialogReadModel.emotion);
                    }
                
                    // Create the node props
                    BaseDialogProps baseDialogProps = BaseDialogProps.CreateNodeProps(emotion, 
                        person);
                    return baseDialogProps;
            }

            Props props = Props.CreateProps(readModel.type);
            return props;
        }
        
        
        public Props ConvertReadModel(GlobalPropsReadModel readModel)
        {
            switch (readModel.type)
            {
                // Base Dialog
                case "Base Dialog":
                    if (readModel == null) return null;
                    // Verify that the person in the props exists.
                    // Search by name
                    Person person = personsList.GetPerson(readModel.speaker);
                
                    // If it does not exist in the list, creates a new person and adds it to the list
                    if (!person)
                    {
                        // person = Person.CreatePerson(int.Parse(tmpProps.speaker.id), tmpProps.speaker.name);
                        person = Person.CreatePerson(personsList.persons.Count, readModel.speaker);
                    }
                
                    // Verify that the emotion in the props exists.
                    Emotion emotion = emotionsList.GetEmotion(readModel.emotion);
                    // If it does not exist in the list, creates a new emotion and adds it to the list
                    if (!emotion)
                    {
                        emotion = Emotion.CreateEmotion(readModel.emotion);
                    }
                
                    // Create the node props
                    BaseDialogProps baseDialogProps = BaseDialogProps.CreateNodeProps(emotion, 
                        person);
                    return baseDialogProps;
            }

            Props props = Props.CreateProps(readModel.type);
            return props;
        }
        
    }
}