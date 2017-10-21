using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


namespace CFTBase
{
    public class ControlKeys
    {
        //joystick
        public const int JOYSTICK_LEFT = 0;
        public const int JOYSTICK_RIGHT = 1;
        public const int JOYSTICK_COUNT = 2;

        //axis
        public const int AXIS_H = 0;
        public const int AXIS_V = 1;

        public const int AXIS_START = AXIS_H;
        public const int AXIS_END = AXIS_H;

        //action
        public const int ACTION_1 = 2;
        public const int ACTION_2 = 3;

        public const int ACTION_START = ACTION_1;
        public const int ACTION_END = ACTION_2;
        public const int ACTION_COUNT = 2;
        
        //all
        public const int KEYS_COUNT = 2;
    }

    public class ButtonData
    {
        public const float PRESSING_TIME = 0.5f;
        //true down flas up
        private bool m_buttonDown;
        public bool ButtonDown { get { return m_buttonDown; } }

        private bool m_lastButtonDown;
        public bool LastButtonDown { get { return m_lastButtonDown; } }

        private float m_lastButtonDownStartTime;
        public float LastButtonDownStartTime { get { return m_lastButtonDownStartTime; } }

        private float m_lastButtonDownTime;
        public float LastButtonDownTime { get { return m_lastButtonDownTime; } }

        public bool IsPressing()
        {
            if (m_buttonDown && m_lastButtonDownTime - m_lastButtonDownStartTime >= PRESSING_TIME)
                return true;

            return false;
        }

        public bool IsButtonReleased()
        {
            return !m_buttonDown && m_lastButtonDown;
        }

        public bool IsClicked()
        {
            return IsButtonReleased() && m_lastButtonDownTime - m_lastButtonDownStartTime < PRESSING_TIME;
        }

        public bool IsPressed()
        {
            return IsButtonReleased() && m_lastButtonDownTime - m_lastButtonDownStartTime >= PRESSING_TIME;
        }

        public ButtonData()
        {
            m_buttonDown = false;
            m_lastButtonDown = false;
            m_lastButtonDownStartTime = -1;
        }

        public void Update(float buttonState, float gameTime)
        {
            m_lastButtonDown = m_buttonDown;
            m_buttonDown = buttonState > 0;
            if (m_buttonDown)
            {
                m_lastButtonDownTime = gameTime;
                if (!m_lastButtonDown)
                    m_lastButtonDownStartTime = gameTime;
            }
        }
    }

    public class JoystickData
    {
        protected Vector3 m_dir;
        public Vector3 Dir
        {
            get { return m_dir; }
        }

        protected Vector3 m_lastDir;
        public Vector3 LastDir
        {
            get { return m_lastDir; }
        }

        public JoystickData()
        {
            m_dir = Vector3.zero;
            m_lastDir = Vector3.zero;
        }

        public void Update(float x, float z)
        {
            m_lastDir = m_dir;
            m_dir.x = x;
            m_dir.z = z;
        }
    }


    public class InputManager
    {
        static private class KeysMapping
        {
            static private bool m_initialized = false;
            static private string[] m_keysMapping = new string[ControlKeys.KEYS_COUNT];
            public static void Init()
            {
                m_keysMapping[ControlKeys.AXIS_H] = "Horizontal";
                m_keysMapping[ControlKeys.AXIS_V] = "Vertical";
                m_keysMapping[ControlKeys.ACTION_1] = "Fire1";
                m_keysMapping[ControlKeys.ACTION_2] = "Fire2";

                m_initialized = true;
            }

            public static string GetKeyName(int key)
            {
                if (!m_initialized)
                    Init();

                if (key < 0 || key >= ControlKeys.KEYS_COUNT)
                    return string.Empty;

                return m_keysMapping[key];
            }
        }

        private float[] m_keysDataCache = new float[ControlKeys.KEYS_COUNT];
        private float[] m_keysLastDataCache = new float[ControlKeys.KEYS_COUNT];
        private JoystickData[] m_joyStickData = new JoystickData[ControlKeys.JOYSTICK_COUNT];
        private ButtonData[] m_buttonData = new ButtonData[ControlKeys.ACTION_COUNT];

        public InputManager()
        {
            for (int i = 0; i < ControlKeys.JOYSTICK_COUNT; i ++)
            {
                m_joyStickData[i] = new JoystickData();
            }

            for (int i = 0; i < ControlKeys.ACTION_COUNT; i ++)
            {
                m_buttonData[i] = new ButtonData();
            }
        }

        public void Update(float gameTime, float deltaTime)
        {
            //axis
            for (int i = ControlKeys.AXIS_START; i <= ControlKeys.AXIS_END; i++)
            {
                m_keysLastDataCache[i] = m_keysDataCache[i];
                m_keysDataCache[i] = CrossPlatformInputManager.GetAxis(KeysMapping.GetKeyName(i));
            }

            //button
            for (int i = ControlKeys.ACTION_START; i <= ControlKeys.ACTION_END; i ++)
            {
                m_keysLastDataCache[i] = m_keysDataCache[i];
                m_keysDataCache[i] = CrossPlatformInputManager.GetButton(KeysMapping.GetKeyName(i)) ? 1 : 0;
            }

            //update joysticks
            for (int i = ControlKeys.JOYSTICK_LEFT; i < ControlKeys.JOYSTICK_COUNT; i ++)
            {
                //joystick == axis h, axis v
                m_joyStickData[i].Update(m_keysDataCache[i * 2], m_keysDataCache[i * 2 + 1]);
            }

            //update buttons
            for (int i = 0; i < ControlKeys.ACTION_COUNT; i ++)
            {
                m_buttonData[i].Update(m_keysDataCache[i + ControlKeys.ACTION_START], gameTime);
            }
        }
    }
}
