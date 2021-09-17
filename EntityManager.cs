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

        public void Add(Entity entity)
        {
            entities.Add(entity);
            entity.Initialize();
            // Sort the list so that entities with the highest depth values come first
            entities.Sort((e1, e2) => { return e2.Depth - e1.Depth; });
        }

        public ReadOnlyCollection<Entity> List()
        {
            entities.RemoveAll(e => e.PendingDeletion);
            return entities.AsReadOnly();
        }
    }
}
