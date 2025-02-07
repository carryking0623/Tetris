using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class Map : IDraw
    {
        //固定墙壁
        private List<DrawObject> walls = new List<DrawObject>();
        // 动态墙壁
        public List<DrawObject> dynamicWalls = new List<DrawObject>();

        #region Lesson6 为了让外部快速得到地图边界 
        public int w;  // 动态墙壁的 宽容量,一行可以有多少个
        public int h;
        #endregion

        #region Lesson11 跨层
        private int[] recordInfo;//记录每一行有多少个小方块 索引对应行号
        #endregion

        private GameScene nowGameScene;



        public Map(GameScene gameScene )
        {
            this.nowGameScene = gameScene;
            h = Game.h - 6;
            w = 0; 
            recordInfo = new int[h];  // 每行的 计数初始化  0~h-1 行
            //绘制 横向的固定墙壁
            for(int i=0;i < Game.w;i += 2)
            {
                walls.Add(new DrawObject(E_DrawType.Wall, i, h));
                ++ w; 
            }
            w -= 2;  
            for (int i = 0; i < h ; i ++)
            {
                walls.Add(new DrawObject(E_DrawType.Wall, 0, i));
                walls.Add(new DrawObject(E_DrawType.Wall, Game.w - 2, i));
            }

        }

        public void Draw()
        {
            // 绘制 固定墙壁
            for(int i=0;i < walls.Count;i ++) {
                walls[i].Draw();
            }
            // 绘制 动态墙壁
            for (int i = 0; i < dynamicWalls.Count; i++)
            {
                dynamicWalls[i].Draw();
            }
        }
        //清除 动态墙壁
        public void ClearDraw()
        {
            for (int i = 0; i < dynamicWalls.Count; i++)
            {
                dynamicWalls[i].ClearDraw();
            }
        }


        /// <summary>
        /// 提供给外部 动态添加 墙壁
        /// </summary>
        /// <param name="walls"></param>
        public void AddWalls(List<DrawObject> walls) 
        {
            for(int i =0;i < walls.Count;i ++)
            {
                walls[i].ChangeType(E_DrawType.Wall); // 将方块的类型 改成墙壁类型
                dynamicWalls.Add(walls[i]);  // 将 方块添加到 动态墙壁中

                //在动态墙壁添加处 发现位置 顶满了就结束
                if (walls[i].pos.y <= 0)
                {
                    //关闭输入线程
                    nowGameScene.StopThread();
                    // 场景 切换到结束场景
                    Game.ChangeScene(E_SceneType.End);
                    return;
                }


                //添加动态墙壁的计数 ,根据索引得到行
                // h: Game.h - 6 , y 最大为Game.h - 7 
                recordInfo[h - 1  - walls[i].pos.y ] += 1 ;
            }
            // 检测移除 ,先擦后画
            ClearDraw();
            CheckClear();
            Draw();
        }

        /// <summary>
        /// 检测是否夸层
        /// </summary>
        public void CheckClear()
        {
            List<DrawObject> delList = new List<DrawObject>();//待删除方块的列表

            for(int i=0;i < recordInfo.Length;i ++) { // 枚举行
                //如果这一行满了 ,小方块计数 == w ( w - 2 ,这里的w已经去掉了左右两边的固定墙壁) 
                if (recordInfo[i] == w)
                {
                    // 1 这行的所有小方块 移除
                    
                    for(int j =0;j < dynamicWalls.Count; j ++)
                    {
//当前通过动态方块的y计算它在哪一行 如果行号和 当前记录索引一致 就证明 应该移除
                        if (i == h - 1 - dynamicWalls[j].pos.y  )
                        {
                            //移除这个方块, 为了安全移除 添加一个记录列表
                            delList.Add(dynamicWalls[j]);
                        }
                        // 2 在这行上的所有小方块 下一个单位
                        //如果当前的 这个位置 是该行以上 那就该小方块 下移一格
                        else if (h - 1 - dynamicWalls[j].pos.y > i)
                        {
                            ++dynamicWalls[j].pos.y;
                        }
                    }
                    //移除 待删除的 小方块
                    for(int j=0;j < delList.Count; j ++)
                    {
                        dynamicWalls.Remove(delList[j]);
                    }
                    // 3 记录小方块数量的数组从上到下迁移
                    for(int j = i;j < recordInfo.Length - 1 ; j ++)
                    {
                        recordInfo[j] = recordInfo[j + 1 ];
                    }
                    //置空最顶的计数
                    recordInfo[recordInfo.Length - 1] = 0;
                    //垮掉一行后 再次去从头检测是否 跨层
                    CheckClear(); // 通过递归检测 剩余的跨层 
                    break;
                }


            }


        }

    }
}
