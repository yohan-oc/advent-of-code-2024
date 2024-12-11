using System.ComponentModel.DataAnnotations;
using System.Data;
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
            Console.WriteLine("**** Day 09 - Part 01 ****");
            
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
            
            //Console.WriteLine("Total blocks: " + blocks.Count);

            for (int blockCount = 0; blockCount < spaceBlockSize; blockCount++)
            {
                var firstSpaceBlock = blocks.First(c => c.Value.Type == BlockType.Space);
                
                var lastFileBlock = blocks.Last(c => c.Value.Type == BlockType.File);

                blocks[firstSpaceBlock.Key] = lastFileBlock.Value;
                blocks[lastFileBlock.Key] = firstSpaceBlock.Value;
                
                //PrintBlocks(blocks);
                
                //Console.WriteLine("Rearranging: " + blockCount);
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
        
        public static void Part02()
        {
            Console.WriteLine("**** Day 09 - Part 02 ****");
            
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
                    blocks.Add(indexCount,
                        new Block
                        {
                            Type = BlockType.File, Id = blockIndex, Size = size
                        });
                    indexCount = indexCount + 10;
                    blockIndex++;
                }
                else
                {
                    if (size != 0)
                    {
                        blocks.Add(indexCount,
                            new Block
                            {
                                Type = BlockType.Space, Id = spaceIndex, Size = size
                            });
                        indexCount = indexCount + 10;
                        spaceIndex++;
                    }
                }
                
            }

            Console.WriteLine("Map completed");
            
            //PrintIndividualsBlocks(blocks);

            var defragging = true;
            
            while (defragging)
            {
                var orderedBlocks = blocks.OrderBy(c => c.Key);
                var lastFileBlock = 
                    orderedBlocks.Last(c => c.Value is { Type: BlockType.File, HasFragmented: false });
                
                var firstSpaceBlock =
                    orderedBlocks.FirstOrDefault(c => c.Value.Type == BlockType.Space 
                                                      && c.Value.Size >= lastFileBlock.Value.Size && !c.Value.HasFragmented);

                if (firstSpaceBlock.Value == null)
                {
                    var fileValue = lastFileBlock.Value;
                    fileValue.HasFragmented = true;
                    blocks[lastFileBlock.Key] = fileValue;
                }
                else
                {
                    var freeSpace = firstSpaceBlock.Value.Size - lastFileBlock.Value.Size;

                    if (freeSpace == 0)
                    {
                        var newFileValue = new Block
                        {
                            Type = lastFileBlock.Value.Type,
                            Size = lastFileBlock.Value.Size,
                            HasFragmented = true,
                            Id = lastFileBlock.Value.Id
                        };
                        blocks[firstSpaceBlock.Key] = newFileValue;
                        
                        var newSpaceValue = new Block
                        {
                            Type = firstSpaceBlock.Value.Type,
                            Size = firstSpaceBlock.Value.Size,
                            HasFragmented = true,
                            Id = firstSpaceBlock.Value.Id
                        };
                        blocks[lastFileBlock.Key] = newSpaceValue;
                        
                        // blocks[firstSpaceBlock.Key] = lastFileBlock.Value;
                        //
                        // var spaceValue = firstSpaceBlock.Value;
                        //
                        // blocks[lastFileBlock.Key] = spaceValue;
                    }
                    else
                    {
                        var remainingSpaceValue = new Block
                        {
                            Type = BlockType.Space,
                            Size = freeSpace,
                            HasFragmented = false,
                            Id = firstSpaceBlock.Value.Id
                        };
                        
                        var shiftingSpaceValue = new Block
                        {
                            Type = BlockType.Space,
                            Size = lastFileBlock.Value.Size,
                            HasFragmented = true,
                            Id = firstSpaceBlock.Value.Id
                        };
                        
                        var newFileValue = new Block
                        {
                            Type = lastFileBlock.Value.Type,
                            Size = lastFileBlock.Value.Size,
                            HasFragmented = true,
                            Id = lastFileBlock.Value.Id
                        };
                        
                        blocks[firstSpaceBlock.Key] = newFileValue;
                        
                        blocks[lastFileBlock.Key] = shiftingSpaceValue;
                        
                        blocks.Add(firstSpaceBlock.Key + 1, remainingSpaceValue);
                    }
                    
                }
                
                //PrintIndividualsBlocks(blocks);
                
                var remainingFileBlocks = 
                    blocks.Where(c => c.Value is { Type: BlockType.File, HasFragmented: false });
                
                Console.WriteLine($"Defragging... {DateTime.Now} Remaining: {remainingFileBlocks.Count()}" );

                if (remainingFileBlocks.Count() == 0)
                {
                    defragging = false;
                }
            }

            Int64 total = 0;
            
            int position = 0;
            
            var orderedB = blocks.OrderBy(c => c.Key);
            
            foreach (var block in orderedB)
            {
                if (block.Value.Type == BlockType.File)
                {
                    for (int blockCount = 0; blockCount < block.Value.Size; blockCount++)
                    {
                        Int64 sum = position * block.Value.Id;
                        Console.WriteLine($"{position} * {block.Value.Id} = {sum}");
                
                        total = total + sum;

                        position++;

                        //Console.WriteLine("Calculating: " + total);
                    }
                }
                
                if (block.Value.Type == BlockType.Space)
                {
                    position = position + block.Value.Size;
                }
            }
            
            PrintIndividualsBlocks(blocks);
            
            Console.WriteLine("check sum: " + total);
        }

        static void PrintIndividualsBlocks(Dictionary<int, Block> blocks)
        {
            //string newDiskMap = "";

            var orderedBlocks = blocks.OrderBy(c => c.Key);
            
            foreach (var block in orderedBlocks)
            {
                for (int blockCount = 0; blockCount < block.Value.Size; blockCount++)
                {
                    if (block.Value.Type == BlockType.File)
                    {
                        Console.Write(block.Value.Id);
                        //newDiskMap = newDiskMap + "x" + block.Value.Id;
                    }
                    else
                    {
                        Console.Write(".");
                        //newDiskMap = newDiskMap + ".";
                    }
                }
            }
            Console.WriteLine();
        }
    }

    public class Block
    {
        public BlockType Type { get; set; }

        public int Id { get; set; }

        public int Size { get; set; }

        public bool HasFragmented = false;
    }

    public enum BlockType
    {
        File,
        Space
    }
}