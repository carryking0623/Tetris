using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class EndScene : BeginOrEndBaseScene
    {
        public EndScene()
        {
            strTitle = "游戏结束";
            strOne = "回到开始界面";
        }

        public override void EnterJDoSomething()
        {
            if(nowSelIndex == 0)
            {
                Game.ChangeScene(E_SceneType.Begin);
            }else
            {
                Environment.Exit(0);
            }

        }
    }
}
