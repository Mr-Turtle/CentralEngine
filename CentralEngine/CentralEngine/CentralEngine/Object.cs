using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace CentralEngine.CentralEngine
{
    public class Object
    {
        public string ObjectName;
        public Vector2 ObjectPosition;
        public Vector2 ObjectScale;

        public List<Component> Components;

        public Object(string ObjectName, Vector2 ObjectPosition, Vector2 ObjectScale)
        {
            this.ObjectName = ObjectName;
            this.ObjectPosition = ObjectPosition;
            this.ObjectScale = ObjectScale;

            this.Components = new List<Component>();

            CentralEngine.RegisterObject(this);
        }

        public void AddComponent(Component component)
        {
            this.Components.Add(component);
        }

        public void RemoveComponent(Component component)
        {
            this.Components.Remove(component);
        }

        public Component GetComponent(string componentID)
        {
            foreach(Component component in this.Components)
            {
                if(component.ComponentID == componentID)
                {
                    return component;
                }
            }

            return null;
        }

        public Component GetComponent(int index)
        {
            return this.Components[index];
        }

    }
}
