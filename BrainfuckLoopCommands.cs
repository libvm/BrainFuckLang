using System.Collections.Generic;

namespace func.brainfuck
{
    public class BrainfuckLoopCommands
    {
        public static void RegisterTo(IVirtualMachine vm)
        {
            var stack = new Stack<int>();
            var positions = new Dictionary<int,int>();
            var len = vm.Instructions.Length;
            for (int i = 0; i < len; i++)
            {
                if (vm.Instructions[i] == '[')
                    stack.Push(i);
                if (vm.Instructions[i] == ']')
                    positions.Add(stack.Pop(), i);
            }
            var reversed = new Dictionary<int, int>();
            foreach (var pair in positions)
                reversed.Add(pair.Value, pair.Key);
            vm.RegisterCommand('[', b =>
            {
                if (b.Memory[b.MemoryPointer] == 0)
                    b.InstructionPointer = positions[b.InstructionPointer] - 1;
            });
            vm.RegisterCommand(']', b =>
            {
                if (b.Memory[b.MemoryPointer] != 0) 
                    b.InstructionPointer = reversed[b.InstructionPointer] - 1;
            });
        }
    }
}