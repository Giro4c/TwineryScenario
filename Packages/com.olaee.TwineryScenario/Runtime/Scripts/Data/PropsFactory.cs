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

        /// <summary>
        /// Reset value of all attributes in the class. Lists are cleared, singular class objects are changed to null.
        /// </summary>
        public void Clear()
        {
            if (personsList != null)
            {
                personsList.persons.Clear();
            }
            if (emotionsList != null)
            {
                emotionsList.emotions.Clear();
            }
        }
        
        /// <summary>
        /// Converts a GlobalPropsReadModel which can contain any data from any props type into the Props of type
        /// associated to the "type" attribute of the props.
        /// </summary>
        /// <param name="readModel">The Global props read model which contains all needed data for the conversion</param>
        /// <returns>A Props object with the node characteristics related to its type.</returns>
        public Props ConvertReadModel(GlobalPropsReadModel readModel)
        {
            if (readModel != null)
            {
                switch (readModel.type)
                {
                    // Base Dialog
                    case "Base Dialog":
                        // Verify that the person in the props exists.
                        // Search by name
                        Person person = personsList.GetPerson(readModel.speaker);
                
                        // If it does not exist in the list, creates a new person and adds it to the list
                        if (!person)
                        {
                            person = Person.CreatePerson(personsList.persons.Count, readModel.speaker);
                            personsList.persons.Add(person);
                        }
                
                        // Verify that the emotion in the props exists.
                        Emotion emotion = emotionsList.GetEmotion(readModel.emotion);
                        // If it does not exist in the list, creates a new emotion and adds it to the list
                        if (!emotion)
                        {
                            emotion = Emotion.CreateEmotion(readModel.emotion);
                            emotionsList.emotions.Add(emotion);
                        }
                
                        // Create the node props
                        BaseDialogProps baseDialogProps = BaseDialogProps.CreateBaseDialogProps(emotion, 
                            person);
                        return baseDialogProps;
                
                    case "Other":
                        break;
                }
            }

            Props props = Props.CreateProps("Base");
            return props;
        }
        
    }
}