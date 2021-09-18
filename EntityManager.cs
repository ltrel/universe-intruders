// EntityManager.cs
// Created on: 2021-09-15
// Author: Leo Treloar

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace UniverseIntruders
{
    class EntityManager
    {
        private List<Entity> entities = new List<Entity>();
        private List<Entity> pendingAdds = new List<Entity>();
        private List<Entity> pendingRemoves = new List<Entity>();

        private WindowEventTable eventDestinations;

        public EntityManager(WindowEventTable eventDestinations)
        {
            this.eventDestinations = eventDestinations;
        }

        public void Add(Entity entity)
        {
            pendingAdds.Add(entity);
        }

        public void Remove(Entity entity)
        {
            pendingRemoves.Add(entity);
        }

        public ReadOnlyCollection<Entity> List()
        {
            // Handle pending entity additions
            foreach (Entity entity in pendingAdds)
            {
                entities.Add(entity);
                BindEvents(entity);
                entity.Initialize();
            }
            if (pendingAdds.Count > 0)
            {
                entities.Sort((e1, e2) => { return e2.Depth - e1.Depth; });
                pendingAdds.Clear();
            }

            // Handle pending entity deletions
            foreach (Entity entity in pendingRemoves)
            {
                UnbindEvents(entity);
                entities.Remove(entity);
            }
            pendingRemoves.Clear();

            return entities.AsReadOnly();
        }

        public void RebindEvents(Entity entity)
        {
            UnbindEvents(entity);
            BindEvents(entity);
        }

        private void BindEvents(Entity entity)
        {
            eventDestinations.KeyPressed += entity.EventHandlers.KeyPressed;
            eventDestinations.Closed += entity.EventHandlers.Closed;
        }

        private void UnbindEvents(Entity entity)
        {
            eventDestinations.KeyPressed -= entity.EventHandlers.KeyPressed;
            eventDestinations.Closed -= entity.EventHandlers.Closed;
        }
    }
}
