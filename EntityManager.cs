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
        private WindowEventTable eventDestinations;

        public EntityManager(WindowEventTable eventDestinations)
        {
            this.eventDestinations = eventDestinations;
        }

        public void Add(Entity entity)
        {
            entities.Add(entity);
            BindEvents(entity);
            entity.Initialize();
            // Sort the list so that entities with the highest depth values come first
            entities.Sort((e1, e2) => { return e2.Depth - e1.Depth; });
        }

        public ReadOnlyCollection<Entity> List()
        {
            foreach (Entity entity in entities.Where(e => e.PendingDeletion))
            {
                UnbindEvents(entity);
            }
            entities.RemoveAll(e => e.PendingDeletion);
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
