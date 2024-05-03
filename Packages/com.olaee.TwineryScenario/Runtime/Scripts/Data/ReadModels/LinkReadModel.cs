using System;

namespace TwineryScenario.Runtime.Scripts.Data.ReadModels
{
    [Serializable]
    public class LinkReadModel
    {
        /// <summary>
        /// The display name of the link
        /// </summary>
        public string name;
        
        /// <summary>
        /// The name of the node the link points to
        /// </summary>
        public string link;
        
        /// <summary>
        /// The pid/identifier of the node the links points to
        /// </summary>
        public string pid;

        public LinkReadModel()
        {
            
        }

        public LinkReadModel(string name, string link, string pid)
        {
            this.name = name;
            this.link = link;
            this.pid = pid;
        }
    }
}