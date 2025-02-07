using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    enum E_SceneType
    {
        Begin,
        Game,
        End
    }
    class Game
    {
        // 游戏窗口 宽高
        public const int w = 50;
        public const int h = 35;

        public static ISceneUpdate nowScene;// 当前选中的场景

        public void Init() 
        { 
            Console.CursorVisible = false;
            Console.SetWindowSize(w, h);
            Console.SetBufferSize(w, h);    
            
            ChangeScene(E_SceneType.Begin);
        }
        public void Start()// 游戏开始的方法
        {
            while (true) // 游戏主循环 主要负责游戏场景逻辑的更新
            {
                if(nowScene != null) // 游戏场景不为空就更新
                {
                    nowScene.Update();  
                }
            }
        }
        public static void ChangeScene(E_SceneType type)
        {
            // 切换场景前 ,应该把上一个 场景的绘制内容 擦掉
            Console.Clear();
            switch (type) 
            {
                case E_SceneType.Begin:
                    nowScene  = new BeginScene();
                    break;
                case E_SceneType.Game: 
                    nowScene = new GameScene();
                    break;
                case E_SceneType.End:
                    nowScene = new EndScene();
                    break;
            }
            
        }

        public Game()
        {
            Init();
        }

    }


}
