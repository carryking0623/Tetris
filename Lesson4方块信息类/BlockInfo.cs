using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class BlockInfo
    {
        //方块信息坐标的容器
        private List<Position[]> list;
        
        public BlockInfo(E_DrawType type)
        {
            list = new List<Position[]>();
            //添加非 (0,0)的坐标
            switch(type) 
            {
                case E_DrawType.Cube:
                    list.Add(new Position[3] { 
                        new Position(2,0),
                        new Position(0,1),
                        new Position(2,1),
                    });
                    break;
                case E_DrawType.Line:
                    //第一种 形状 竖着的1
                    list.Add(new Position[3] {
                        new Position(0,-1),
                        new Position(0,1),
                        new Position(0,2),
                    });
                    //第二种 形状 横着的1
                    list.Add(new Position[3] {
                        new Position(-4,0),
                        new Position(-2,0),
                        new Position(2,0)
                    });
                    //第三种 形状 竖着的1
                    list.Add(new Position[3] {
                        new Position(0,-2),
                        new Position(0,-1),
                        new Position(0,1),
                    });
                    //第四种 形状 横着的1
                    list.Add(new Position[3] {
                        new Position(-2,0),
                        new Position(2,0),
                        new Position(4,0)
                    });
                    break;
                case E_DrawType.Tank:
                    //第一种 形状 T 凸的位置朝下
                    list.Add(new Position[3] {
                        new Position(-2,0),
                        new Position(2,0),
                        new Position(0,1),
                    });
                    //第二种 形状 凸的位置朝左
                    list.Add(new Position[3] {
                        new Position(0,-1),
                        new Position(-2,0),
                        new Position(0,1)
                    });
                    //第三种 形状 凸的位置朝上
                    list.Add(new Position[3] {
                        new Position(0,-1),
                        new Position(-2,0),
                        new Position(2,0),
                    });
                    //第四种 形状 凸的位置朝右
                    list.Add(new Position[3] {
                        new Position(0,-1),
                        new Position(2,0),
                        new Position(0,1)
                    });
                    break;
                case E_DrawType.Left_Ladder:
                    //第一种 形状 
                    list.Add(new Position[3] {
                        new Position(0,-1),
                        new Position(2,0),
                        new Position(2,1),
                    });
                    //第二种 形状 
                    list.Add(new Position[3] {
                        new Position(2,0),
                        new Position(-2,1),
                        new Position(0,1)
                    });
                    //第三种 形状 
                    list.Add(new Position[3] {
                        new Position(-2,-1),
                        new Position(-2,0),
                        new Position(0,1),
                    });
                    //第四种 形状 
                    list.Add(new Position[3] {
                        new Position(0,-1),
                        new Position(2,-1),
                        new Position(-2,0)
                    });
                    break;
                case E_DrawType.Right_Ladder:
                    //第一种 形状 
                    list.Add(new Position[3] {
                        new Position(0,-1),
                        new Position(-2,0),
                        new Position(-2,1),
                    });
                    //第二种 形状 
                    list.Add(new Position[3] {
                        new Position(2,0),
                        new Position(-2,-1),
                        new Position(0,-1)
                    });
                    //第三种 形状 
                    list.Add(new Position[3] {
                        new Position(2,-1),
                        new Position(2,0),
                        new Position(0,1),
                    });
                    //第四种 形状 
                    list.Add(new Position[3] {
                        new Position(0,1),
                        new Position(2,1),
                        new Position(-2,0)
                    });
                    break;
                case E_DrawType.Left_Long_Ladder:
                    //第一种 形状 
                    list.Add(new Position[3] {
                        new Position(0,-1),
                        new Position(0,1),
                        new Position(-2,-1),
                    });
                    //第二种 形状 
                    list.Add(new Position[3] {
                        new Position(2,0),
                        new Position(-2,0),
                        new Position(2,-1)
                    });
                    //第三种 形状 
                    list.Add(new Position[3] {
                        new Position(0,-1),
                        new Position(2,1),
                        new Position(0,1),
                    });
                    //第四种 形状 
                    list.Add(new Position[3] {
                        new Position(2,0),
                        new Position(-2,1),
                        new Position(-2,0)
                    });
                    break;
                case E_DrawType.Right_Long_Ladder:
                    //第一种 形状 
                    list.Add(new Position[3] {
                        new Position(0,-1),
                        new Position(0,1),
                        new Position(2,-1),
                    });
                    //第二种 形状 
                    list.Add(new Position[3] {
                        new Position(2,0),
                        new Position(-2,0),
                        new Position(2,1)
                    });
                    //第三种 形状 
                    list.Add(new Position[3] {
                        new Position(0,-1),
                        new Position(-2,1),
                        new Position(0,1),
                    });
                    //第四种 形状 
                    list.Add(new Position[3] {
                        new Position(2,0),
                        new Position(-2,-1),
                        new Position(-2,0)
                    });
                    break;
            }
        }

        /// <summary>
        /// 提供给外部 根据索引 快速获取位置偏移信息的
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Position[] this[int index]
        {
            get
            {
                if (index < 0) return list[0];
                else if(index >= list.Count) return list[list.Count-1];
                return list[index];
            }
        }

        /// <summary>
        /// 提供给外部 获取 形态有几种
        /// </summary>
        public int Count { get => list.Count; }
    }
}
