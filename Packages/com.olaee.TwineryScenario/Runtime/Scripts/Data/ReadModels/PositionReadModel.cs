using System;

namespace TwineryScenario.Runtime.Scripts.Data.ReadModels
{
    [Serializable]
    public class PositionReadModel
    {
        public string x;
        public string y;
        
        public PositionReadModel()
        {
            
        }

        public PositionReadModel(string x, string y)
        {
            this.x = x;
            this.y = y;
        }
    }
}