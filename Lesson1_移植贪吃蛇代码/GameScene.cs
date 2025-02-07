using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class GameScene : ISceneUpdate
    {
        Map map;
        BlockWorker blockWorker;

        #region Lesson 10 
        //Thread inputThread;
        //bool isRunning ;
        #endregion

        public GameScene()
        {
            map = new Map(this);
            blockWorker = new BlockWorker();
            //开启输入线程 检测输入 
            #region Lesson 10
            //isRunning = true ;
            //inputThread = new Thread(CheckInputThread);
            //inputThread.IsBackground = true;
            //inputThread.Start();
            #endregion

            InputThread.Instance.inputEvent += CheckInputThread; // 添加事件 


        }
        //输入线程 
        private void CheckInputThread()
        {
            //while (isRunning)
// 这里是 输入线程 每帧会执行的逻辑 ,不需要在函数内 死循环
                if (Console.KeyAvailable)// 有控制台输入的时候
                {
                    lock(blockWorker)
                    {
                        switch (Console.ReadKey(true).Key)
                        {
                            //键盘 左右键 控制 方块变形
                            case ConsoleKey.LeftArrow:
                                //判断是否能 变形
                                if (blockWorker.CanChange(E_Change_Type.Left, map))
                                    blockWorker.Change(E_Change_Type.Left);
                                break;
                            case ConsoleKey.RightArrow:
                                if (blockWorker.CanChange(E_Change_Type.Right, map))
                                    blockWorker.Change(E_Change_Type.Right);
                                break;
                            case ConsoleKey.A:
                                if (blockWorker.CanMoveRL(E_Change_Type.Left, map))
                                    blockWorker.MoveRL(E_Change_Type.Left);
                                break;
                            case ConsoleKey.D:
                                if (blockWorker.CanMoveRL(E_Change_Type.Right, map))
                                    blockWorker.MoveRL(E_Change_Type.Right);
                                break;
                            case ConsoleKey.S: // 加速向下罗
                                if (blockWorker.CanMove(map))
                                    blockWorker.AutoMove();
                                break;
                        }
                    }
                }
            //}
        }

        public void Update()
        {
            //锁里面 不要包含 休眠
            lock (blockWorker)
            {
                map.Draw(); // 地图绘制
                blockWorker.Draw();
                if (blockWorker.CanMove(map))
                    blockWorker.AutoMove();
            }

            Thread.Sleep(100);
        }

        //关闭线程
        public void StopThread()
        {
            //isRunning = false;
            //inputThread = null; // 删除线程

            InputThread.Instance.inputEvent -= CheckInputThread; //移除事件
        }

    }
}
