using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CFTBase
{
    public class CFTGame
    {
        private static Dictionary<uint, ITicker> TickerList = new Dictionary<uint, ITicker>();
        private static Dictionary<uint, Entity> EntityList = new Dictionary<uint, Entity>();

        public static void RegisterEntity(Entity entity)
        {
            if (entity.NeedUpdate)
            {
                TickerList.Add(entity.Index, entity);
            }

            EntityList.Add(entity.Index, entity);
        }

        public static void UnregisterEntity(Entity entity)
        {
            if (entity.NeedUpdate)
            {
                TickerList.Remove(entity.Index);
            }

            EntityList.Remove(entity.Index);
        }

        public void Init()
        {

        }

        public void Reset()
        {
            TickerList.Clear();
            EntityList.Clear();
        }

        public void Tick(float deltaTime)
        {

        }

        public void FixedTick(float deltaTime)
        {

        }

    }
}