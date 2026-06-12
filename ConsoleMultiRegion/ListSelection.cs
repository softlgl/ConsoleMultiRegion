using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMultiRegion
{
    class ListSelection
    {
        public static void Run(string[] args)
        {
            // 准备列表数据
            string[] options = {
                "1. 启动系统服务",
                "2. 停止系统服务",
                "3. 重启数据库",
                "4. 清理系统缓存",
                "5. 查看运行日志",
                "6. 退出程序"
            };

            Console.WriteLine("=== 系统运维控制台 ===");
            Console.WriteLine("请使用 ↑/↓ 方向键选择，按 Enter 确认：\n");

            // 调用自定义列表选择方法
            int selectedIndex = ShowSelectionMenu(options);

            // 清理屏幕并输出最终结果
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"您最终选择了: {options[selectedIndex]}");
            Console.ResetColor();
        }

        /// <summary>
        /// 显示交互式列表菜单
        /// </summary>
        static int ShowSelectionMenu(string[] options)
        {
            // 1. 隐藏光标，防止光标在重绘时乱闪
            Console.CursorVisible = false;

            int selectedIndex = 0;
            // 记录列表开始绘制的 Y 轴坐标（行号）
            int startY = Console.CursorTop;

            while (true)
            {
                // 2. 重绘整个列表
                for (int i = 0; i < options.Length; i++)
                {
                    // 将光标移动到当前选项所在的行首
                    Console.SetCursorPosition(0, startY + i);

                    // 【关键】清空当前行：防止上一次的高亮背景色或长文本残留
                    Console.ResetColor();
                    // 用空格填满当前行（减去1是为了防止换行导致屏幕滚动）
                    Console.Write(new string(' ', Console.WindowWidth - 1));

                    // 重新回到行首准备写字
                    Console.SetCursorPosition(0, startY + i);

                    // 3. 根据是否选中，设置不同的样式
                    if (i == selectedIndex)
                    {
                        // 选中状态：黑字白底，前面加个 ">" 箭头
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write($" > {options[i]} ");
                    }
                    else
                    {
                        // 未选中状态：默认颜色，前面留空对齐
                        Console.Write($"   {options[i]} ");
                    }

                    // 恢复默认颜色
                    Console.ResetColor();
                }

                // 4. 捕获键盘输入 (true 表示不在屏幕上打印按键字符)
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                // 5. 处理按键逻辑
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    selectedIndex--;
                    // 如果到了顶部，循环到底部
                    if (selectedIndex < 0) selectedIndex = options.Length - 1;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    selectedIndex++;
                    // 如果到了底部，循环到顶部
                    if (selectedIndex >= options.Length) selectedIndex = 0;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    // 恢复光标显示
                    Console.CursorVisible = true;
                    // 将光标移动到列表下方，避免后续输出覆盖菜单
                    Console.SetCursorPosition(0, startY + options.Length + 1);
                    return selectedIndex; // 返回选中的索引
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    // 可选：按 Esc 取消退出
                    Console.CursorVisible = true;
                    Environment.Exit(0);
                }
            }
        }
    }
}
