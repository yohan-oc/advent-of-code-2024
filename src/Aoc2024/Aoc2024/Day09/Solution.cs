using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Aoc2024.Day09
{
    public static class Solution
    {
        public static void Part01()
        {
            Console.WriteLine("**** Day 09 ****");
            
            string diskMap = File.ReadAllLines("Day09/input.txt")[0];

            var blocks = new Dictionary<int, Block>();
            
            int blockIndex = 0;
            int spaceIndex = 0;
            int indexCount = 0;
            
            for (int index = 0; index < diskMap.Length; index++)
            {
                var size = int.Parse(diskMap[index].ToString());
                if (index % 2 == 0)
                {
                    for (int i = 0; i < size; i++)
                    {
                        blocks.Add(indexCount,
                            new Block
                            {
                                Type = BlockType.File, Id = blockIndex//, Size = size
                            });
                        indexCount++;
                    }
                    blockIndex++;
                }
                else
                {
                    if (size != 0)
                    {
                        for (int i = 0; i < size; i++)
                        {
                            blocks.Add(indexCount,
                                new Block
                                {
                                    Type = BlockType.Space, Id = spaceIndex //, Size = size
                                });
                            indexCount++;
                        }
                        spaceIndex++;
                    }
                }
                
            }

            Console.WriteLine("Map completed");
            //string newDiskMap = "";

            // foreach (var block in blocks)
            // {
            //     if (block.Value.Type == BlockType.File)
            //     {
            //         Console.Write(block.Value.Id);
            //         newDiskMap = newDiskMap + "x" + block.Value.Id;
            //     }
            //     else
            //     {
            //         Console.Write(".");
            //         newDiskMap = newDiskMap + ".";
            //     }
            // }
            
            //Console.WriteLine();
            
            //Console.WriteLine("Individual blocks: " + newDiskMap);

            int spaceBlockSize = blocks.Count(c => c.Value.Type == BlockType.Space);

            for (int blockCount = 0; blockCount < spaceBlockSize; blockCount++)
            {
                var firstSpaceBlock = blocks.First(c => c.Value.Type == BlockType.Space);
                
                var lastFileBlock = blocks.Last(c => c.Value.Type == BlockType.File);

                blocks[firstSpaceBlock.Key] = lastFileBlock.Value;
                blocks[lastFileBlock.Key] = firstSpaceBlock.Value;
                
                //PrintBlocks(blocks);
                
                Console.WriteLine("Rearranging: " + blockCount);
            }

            
            //Console.WriteLine("New blocks");
           
            //Console.WriteLine("Re arranged blocks: " + newDiskMap);

            Int64 total = 0;
            foreach (var block in blocks)
            {
                if (block.Value.Type == BlockType.Space)
                {
                    break;
                }

                Int64 sum = block.Key * block.Value.Id;
                //Console.WriteLine("memory sum: " + sum);
                
                total = total + sum;
                
                //Console.WriteLine("Calculating: " + total);
            }
            
            Console.WriteLine("check sum: " + total);
        }
        
        public static void Part02()
        {
            string[] lines = File.ReadAllLines("Day09/input.txt");

            Int64 rowLengthIndex = lines.Length - 1;
            Int64 colLengthIndex = lines[0].Length - 1;
            
            Console.WriteLine("Day 09: ");
        }

        static void PrintBlocks(Dictionary<int, Block> blocks)
        {
            string newDiskMap = "";
            foreach (var block in blocks)
            {
                // for (int blockCount = 0; blockCount < block.Value.Size; blockCount++)
                // {
                    if (block.Value.Type == BlockType.File)
                    {
                        Console.Write(block.Value.Id);
                        newDiskMap = newDiskMap + "x" + block.Value.Id;
                    }
                    else
                    {
                        Console.Write(".");
                        newDiskMap = newDiskMap + ".";
                    }
                //}
            }
            Console.WriteLine();

        }
    }

    public class Block
    {
        public BlockType Type { get; set; }

        public int Id { get; set; }

        //public int Size { get; set; }
    }

    public enum BlockType
    {
        File,
        Space
    }
}