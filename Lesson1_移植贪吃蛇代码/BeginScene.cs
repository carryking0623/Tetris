﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class BeginScene : BeginOrEndBaseScene
    {
        public BeginScene() {
            strTitle = "俄罗斯方块";
            strOne = "开始游戏";
        }
        public override void EnterJDoSomething()
        {
            //按J键做什么逻辑
            if(nowSelIndex == 0)
            {
                Game.ChangeScene(E_SceneType.Game);
            }else
            {
                Environment.Exit(0);
            }


        }
    }
}
