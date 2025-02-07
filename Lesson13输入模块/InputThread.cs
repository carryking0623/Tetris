using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class InputThread
    {
        //线程成员变量
        Thread inputThread;

        public event Action inputEvent;// 输入检测 事件


        private static InputThread instance = new InputThread();
        public static InputThread Instance
        {
            get { return instance; }
        }
        private InputThread() {
            inputThread = new Thread(InputCheck);
            inputThread.IsBackground = true;
            inputThread.Start();
        }

        private void InputCheck()
        {
            while(true)
            {
                inputEvent?.Invoke();// 存在输入检测 事件就会执行
            }
        }
    }
}
