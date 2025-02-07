using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    /// <summary>
    /// 变形左右枚举 ,决定顺时针 还是 逆时针
    /// </summary>
    enum E_Change_Type
    {
        Left ,Right,
    }
    class BlockWorker : IDraw
    {
        private List<DrawObject> blocks; // 方块们
        private Dictionary<E_DrawType, BlockInfo> blockInfoDic;//记录各个方块的形态信息
        private BlockInfo nowBlockInfo; // 记录 方块(随机创建出来的)的具体信息

        private int nowInfoIndex; // 当前的形态索引

        public BlockWorker() 
        {
            //初始化砖块信息
            blockInfoDic = new Dictionary<E_DrawType, BlockInfo>() {
                {E_DrawType.Cube,new BlockInfo(E_DrawType.Cube) },
                {E_DrawType.Line,new BlockInfo(E_DrawType.Line) },
                {E_DrawType.Left_Ladder,new BlockInfo(E_DrawType.Left_Ladder) },
                {E_DrawType.Right_Ladder,new BlockInfo(E_DrawType.Right_Ladder) },
                {E_DrawType.Tank,new BlockInfo(E_DrawType.Tank) },
                {E_DrawType.Left_Long_Ladder,new BlockInfo(E_DrawType.Left_Long_Ladder) },
                {E_DrawType.Right_Long_Ladder,new BlockInfo(E_DrawType.Right_Long_Ladder) },
            };
            RandomCreateBlock();
        }

        public void Draw()
        {
            for(int i=0;i < blocks.Count; i++) blocks[i].Draw(); 
        }

        /// <summary>
        /// 随机创建方块
        /// </summary>
        public void RandomCreateBlock()
        {
            //随机 方块类型
            Random r = new Random();
            E_DrawType type = (E_DrawType)r.Next(1, 8);

            // 每次新建一种 砖块 就是创建 4个小正方形
            blocks = new List<DrawObject>() {
                new DrawObject(type),
                new DrawObject(type),
                new DrawObject(type),
                new DrawObject(type),
            };
            //blocks = Enumerable.Range(0, 4)
            //       .Select(_ => new DrawObject(type))
            //       .ToList();
            //初始化 方块位置
            // 原点位置(0,0)
            blocks[0].pos = new Position(24, -2);

            //其余 3个方块的位置
            //取出方块的形态信息 ,然后存起来,用于之后的变形
            nowBlockInfo = blockInfoDic[type];
            //随机几种形态中的 一种来设置方块的信息
            nowInfoIndex = r.Next(0,nowBlockInfo.Count);
            //取出其中 一种形态的坐标信息
            Position[] pos  =  nowBlockInfo[nowInfoIndex];

            for(int i=0;i <  pos.Length;i++)
            {
                //取出来的 pos 是相对于 原点的坐标 ,需要加上 原点的坐标
                blocks[i + 1].pos = pos[i] + blocks[0].pos;
            }

        }
        #region Lesson6 变形相关
        //擦除方块的方法
        public void ClearDraw()
        {
            for(int i=0;i < blocks.Count;i ++)
            {
                blocks[i].ClearDraw();
            }
        }

        //变形相关方法
        public void Change(E_Change_Type type)
        {
            // 清除之前的 方块
            ClearDraw();
            switch(type)
            {
                case E_Change_Type.Left:
                    nowInfoIndex = (nowInfoIndex - 1 + nowBlockInfo.Count ) % nowBlockInfo.Count;
                    break;
                case E_Change_Type.Right:
                    nowInfoIndex = (nowInfoIndex + 1) % nowBlockInfo.Count;
                    break;
            }
            // 得到索引的目的 是得到对应形态 的位置偏移信息
            // 用于设置另外的 三个小方块
            Position[] pos = nowBlockInfo[nowInfoIndex];
            // 将另外的三个小方块 进行设置
            for (int i = 0; i < pos.Length; i++)
            {
                //取出来的 pos 是相对于 原点的坐标 ,需要加上 原点的坐标
                blocks[i + 1].pos = pos[i] + blocks[0].pos;
            }

            // 改变之后 再绘制
            Draw();
        }

        /// <summary>
        /// 判断 方块 能否变形
        /// </summary>
        /// <param name="type"> 变形方向</param>
        /// <param name="map"> 地图信息</param>
        /// <returns></returns>
        public bool CanChange(E_Change_Type type,Map map)
        {
            // 变化 临时变量
            int nowIndex = nowInfoIndex;
            switch (type)
            {
                case E_Change_Type.Left:
                    nowIndex = (nowIndex - 1 + nowBlockInfo.Count) % nowBlockInfo.Count;
                    break;
                case E_Change_Type.Right:
                    nowIndex = (nowIndex + 1) % nowBlockInfo.Count;
                    break;
            }
            Position[] nowPos = nowBlockInfo[nowIndex];
            Position tempPos;
            // 将另外的三个小方块 进行设置
            for (int i = 0; i < nowPos.Length; i++)
            {
                //取出来的 nowPos 是相对于 原点的坐标 ,需要加上 原点的坐标
                tempPos = nowPos[i] + blocks[0].pos;
                // 判断 左右和下边界
                if (tempPos.x  < 2 
                    || tempPos.x >= Game.w - 2 
                    || tempPos.y >= map.h) return false;
            }
            //判断是否和 地图上的 动态方块 重合
            for (int i = 0; i < nowPos.Length; i++)
            {
                //取出来的 nowPos 是相对于 原点的坐标 ,需要加上 原点的坐标
                tempPos = nowPos[i] + blocks[0].pos;
                for(int j=0; j< map.dynamicWalls.Count; j ++)
                {
                    if(tempPos == map.dynamicWalls[j].pos) return false;
                }
            }
            return true;
        }
        #endregion

        #region Lesson7 方块左右移动
        /// <summary>
        /// 左右移动
        /// </summary>
        /// <param name="type"></param>
        public void MoveRL(E_Change_Type type)
        {
            // 清除之前的 方块
            ClearDraw();
            //左动  x-2 ,y   右动 x+2, y
            Position movePos= new Position(type == E_Change_Type.Left ? - 2 : 2 ,0);
            // 遍历 我的所有小方块
            for(int i = 0;i < blocks.Count;i++) 
            {
                blocks[i].pos +=  movePos; 
            }
            // 改变之后 再绘制
            Draw();
        }

        /// <summary>
        /// 移动之前判断 能否移动
        /// </summary>
        /// <returns></returns>
        public bool CanMoveRL(E_Change_Type type,Map map)
        {
            Position movePos = new Position(type == E_Change_Type.Left ? -2 : 2, 0);
            Position tempPos; 
            // 遍历 我的所有小方块
            // 先 算 计算量小的 静态方块, 然后再算动态方块 ,在动态方块多的时候能减少计算量
            for (int i = 0; i < blocks.Count; i++)
            {
                tempPos = blocks[i].pos + movePos;
                //判断是否 和 左右方块重合 
                if (tempPos.x < 2 || tempPos.x >= Game.w -2 ) return false;
            }

            for (int i = 0; i < blocks.Count; i++)
            {
                tempPos = blocks[i].pos + movePos;
                //判断 是否和 动态方块重合
                for(int j =0;j < map.dynamicWalls.Count; j ++)
                {
                    if(tempPos == map.dynamicWalls[j].pos) return false;
                }
            }

            return true;
        }

        #endregion

        #region Lesson8 方块自动向下移动
        /// <summary>
        /// 判断是否 可以继续 向下移动
        /// </summary>
        /// <returns></returns>
        public bool CanMove(Map map)
        {
            // 向下的 位移
            Position downMove = new Position(0, 1);
            Position tempPos;
            // 判断是否和 下边界重合 
            for(int i=0;i < blocks.Count;i ++)
            {
                tempPos = blocks[i].pos + downMove;
                if (tempPos.y >= map.h)
                {
                    map.AddWalls(blocks);
                    RandomCreateBlock();
                    return false;
                }
            }
            for (int i = 0; i < blocks.Count; i++)
            {
                tempPos = blocks[i].pos + downMove;
                // 判断是否和 动态方块重合
                for (int j =0;j < map.dynamicWalls.Count;j++)
                {
                    if (tempPos == map.dynamicWalls[j].pos)
                    {
                        map.AddWalls(blocks);
                        RandomCreateBlock();
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 自动移动
        /// </summary>
        public void AutoMove()
        {
            
            ClearDraw();
            // 向下的 位移
            Position downMove = new Position(0, 1);
            for(int i=0 ;i<blocks.Count;i++)
            {
                blocks[i].pos += downMove;
            }
            Draw();
        }
        #endregion



    }
}
