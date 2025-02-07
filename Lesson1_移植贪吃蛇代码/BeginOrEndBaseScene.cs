using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tetris
{
    abstract class BeginOrEndBaseScene : ISceneUpdate
    {
        protected int nowSelIndex = 0 ; // 当前选择的选项
        protected string strTitle; // 标题
        protected string strOne; // 第一个选项

        public abstract void EnterJDoSomething(); // 按下J键执行 某事

        public void Update()
        {
            // 开始和结束场景的 游戏逻辑 
            //选择当前的选项 ,然后 监听键盘输入
            
            // 显示标题
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(Game.w / 2 - strTitle.Length, 5);
            Console.WriteLine(strTitle);

            // 显示下方的选项
            Console.ForegroundColor = nowSelIndex == 0 ? ConsoleColor.Red :  ConsoleColor.White;
            Console.SetCursorPosition(Game.w / 2 - strOne.Length, 8);
            Console.WriteLine(strOne);
            Console.ForegroundColor = nowSelIndex == 1 ? ConsoleColor.Red : ConsoleColor.White;
            Console.SetCursorPosition(Game.w / 2 - 4, 10);
            Console.WriteLine("结束游戏");

            // 检测输入

            switch(Console.ReadKey(true).Key) 
            {
                case ConsoleKey.W:
                    -- nowSelIndex ; if (nowSelIndex < 0) nowSelIndex = 0;
                    break;
                case ConsoleKey.S:
                    ++ nowSelIndex ; if (nowSelIndex > 1 ) nowSelIndex = 1;
                    break;
                case ConsoleKey.J:
                    EnterJDoSomething();
                    break;

            }


        }

        public  BeginOrEndBaseScene() 
        { 

        }
    }
}
