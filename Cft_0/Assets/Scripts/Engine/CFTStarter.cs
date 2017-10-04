using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CFTBase
{
    public class CFTStarter : MonoBehaviour
    {

        private CFTEngine m_gameEngine;
        private void Awake()
        {
            m_gameEngine = gameObject.AddComponent<CFTEngine>();

            GameObject.DontDestroyOnLoad(this.gameObject);
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            m_gameEngine.Tick();
        }

        private void FixedUpdate()
        {
            m_gameEngine.FixedTick();
        }
    }

}
