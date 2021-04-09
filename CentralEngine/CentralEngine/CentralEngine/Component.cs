using System;
using System.Collections.Generic;
using System.Text;

namespace CentralEngine.CentralEngine
{
    public abstract class Component
    {
        public string ComponentID = "COMPONENT-BASE";
        
        public Component(string ComponentID)
        {
            this.ComponentID = ComponentID;
        }
    }
}
