using System;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
			vm.RegisterCommand('.', b => { write((char)b.Memory[b.MemoryPointer]); });
			vm.RegisterCommand('+', b => { b.Memory[b.MemoryPointer]++; });
			vm.RegisterCommand('-', b => { b.Memory[b.MemoryPointer]--; });
			vm.RegisterCommand(',', b => { b.Memory[b.MemoryPointer] = (byte)read(); });
			vm.RegisterCommand('>', b =>
			{
				b.MemoryPointer++;
				if (b.MemoryPointer > b.Memory.Length - 1)
					b.MemoryPointer -= b.Memory.Length;
			});
			vm.RegisterCommand('<', b =>
			{
				b.MemoryPointer--;
				if (b.MemoryPointer < 0)
					b.MemoryPointer += b.Memory.Length;
			});
			for (int i = 'A'; i < 'Z' + 1; i++)
			{
				var j = (char)i;
				vm.RegisterCommand(j, b => { b.Memory[b.MemoryPointer] = (byte)j; });
			}
			for (int i = 'a'; i < 'z' + 1; i++)
			{
				var j = (char)i;
				vm.RegisterCommand(j, b => { b.Memory[b.MemoryPointer] = (byte)j; });
			}
			for (int i = '0'; i < '9' + 1; i++)
			{
				var j = (char)i;
				vm.RegisterCommand(j, b => { b.Memory[b.MemoryPointer] = (byte)j; });
			}
		}
	}
}