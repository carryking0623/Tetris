using System;
using System.Collections;
using System.Numerics;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using System.Reflection;
using System.Threading.Tasks.Sources;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Tetris
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Game g = new Game();
            g.Start();
        }
    }



}