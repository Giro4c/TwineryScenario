namespace Data.ReadModels
{
    public class ScenarioNodeReadModel
    {
        public string pid;
        public string name;
        public PositionReadModel position;
        public string text;
        public LinkReadModel[] links;
        public NodePropsReadModel props;
    }
}