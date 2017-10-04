using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CFTBase
{

    public class CFTEngine : MonoBehaviour
    {

        public static CFTEngine instance;

        private CFTGame m_game = null;

        private void Awake()
        {
            instance = this;

            //TODO
            m_game = new CFTGame();
        }

        public void Tick()
        {
            m_game.Tick(Time.deltaTime);
        }

        public void FixedTick()
        {
            m_game.FixedTick(Time.fixedDeltaTime);
        }
    }
}