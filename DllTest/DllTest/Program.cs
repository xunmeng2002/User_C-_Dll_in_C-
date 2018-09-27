using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DllTest
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public delegate int MyFunc(int a, int b);
    struct FuncCallback
    {
        public MyFunc sum_func;
        public MyFunc minus_func;
        public MyFunc multi_func;
        public MyFunc divide_func;
    }
    class Program
    {
        private const string DLL_NAME = "MyDll.dll";
        [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int sum(int a, int b);
        [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int sum_1(ref int a, ref int b);
        [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int sum_2(MyFunc my_sum, int a, int b);

        [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void register_spi(IntPtr func_callback);
        [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int call_sum(int a, int b);
        [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int call_minus(int a, int b);
        [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int call_multi(int a, int b);
        [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int call_divide(int a, int b);

        static FuncCallback m_func_callback;
        static IntPtr m_func_callback_point;
        public static int my_sum(int a, int b)
        {
            return a + b;
        }
        public static int my_minus(int a, int b)
        {
            return a - b;
        }
        public static int my_multi(int a, int b)
        {
            return a * b;
        }
        public static int my_divide(int a, int b)
        {
            return a / b;
        }
        static void Main(string[] args)
        {
            m_func_callback = new FuncCallback();
            m_func_callback.sum_func = new MyFunc(my_sum);
            m_func_callback.minus_func = new MyFunc(my_minus);
            m_func_callback.multi_func = new MyFunc(my_multi);
            m_func_callback.divide_func = new MyFunc(my_divide);
            int size = Marshal.SizeOf(m_func_callback);
            m_func_callback_point = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(m_func_callback, m_func_callback_point, true);
            register_spi(m_func_callback_point);

            Console.WriteLine("sum: " + sum(3, 5).ToString());
            int a = 10, b = 20;
            Console.WriteLine("sum_1: " + sum_1(ref a, ref b).ToString());
            Console.WriteLine("sum_2: " + sum_2(m_func_callback.sum_func, 9, 2).ToString());
            Console.WriteLine("call_sum: " + call_sum(1, 2).ToString());
            Console.WriteLine("call_minus: " + call_minus(1, 2).ToString());
            Console.WriteLine("call_multi: " + call_multi(1, 2).ToString());
            Console.WriteLine("call_divide: " + call_divide(1, 2).ToString());

            Console.Read();
        }
    }
}
