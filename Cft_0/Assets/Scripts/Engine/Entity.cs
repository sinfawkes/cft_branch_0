using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CFTBase
{
    interface ITicker
    {
        void FixedTick();
        void Tick();
    }

    public class Entity : MonoBehaviour, ITicker
    {
        private static uint NextIndex = 0;
        private static uint GetNewIndex()
        {
            return NextIndex++;
        }

        public virtual void FixedTick()
        {

        }

        public virtual void Tick()
        {

        }
    }

}
