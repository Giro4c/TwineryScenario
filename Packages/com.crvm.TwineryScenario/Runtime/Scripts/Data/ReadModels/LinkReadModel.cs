namespace Data.ReadModels
{
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
    }
}