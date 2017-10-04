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

        public bool NeedUpdate = false;

        private uint m_index;
        public uint Index
        {
            get { return m_index; }
        }

        private Transform m_cachedTransform;
        public Transform CachedTransform
        {
            get
            {
                if (m_cachedTransform == null)
                    m_cachedTransform = this.transform;

                return m_cachedTransform;
            }
        }

        public Vector3 Position
        {
            get
            {
                return CachedTransform.position;
            }
            set
            {
                CachedTransform.position = value;
            }
        }

        public Quaternion Rotation
        {
            get
            {
                return CachedTransform.rotation;
            }
            set
            {
                CachedTransform.rotation = value;
            }
        }

        public Vector3 Forward
        {
            get
            {
                return CachedTransform.forward;
            }
            set
            {
                CachedTransform.forward = value;
            }
        }

        private void Awake()
        {
            m_index = GetNewIndex();
            OnAwake();
        }

        private void Start()
        {
            CFTGame.RegisterEntity(this);
            OnStart();
        }

        private void OnDestroy()
        {
            CFTGame.UnregisterEntity(this);
            OnOnDestroy();
        }

        protected virtual void OnAwake()
        {

        }

        protected virtual void OnStart()
        {

        }

        protected virtual void OnOnDestroy()
        {

        }

        public virtual void FixedTick()
        {

        }

        public virtual void Tick()
        {

        }

        public static Transform FindTransformByName(Transform root, string name)
        {
            Transform result = null;
            if (!string.IsNullOrEmpty(name))
            {
                Transform[] componentsInChildren = root.GetComponentsInChildren<Transform>(true);
                int num = 0;
                while (componentsInChildren != null && num < componentsInChildren.Length)
                {
                    if (componentsInChildren[num] != null && componentsInChildren[num].name == name)
                    {
                        result = componentsInChildren[num];
                        break;
                    }
                    num++;
                }
            }
            return result;
        }
    }

}
