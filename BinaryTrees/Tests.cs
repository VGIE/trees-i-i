using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BinaryTrees
{
    public class SpeedMeasure
    {
        public bool Success { get; set; }
        public double Time { get; set; }
    }
    public static class Tests
    {
        public static bool TestBinaryTree(Action<string> onProgress, Action<string> onError)
        {
            BinaryTree<int, string> tree = new BinaryTree<int, string>();

            onProgress("Testing Add()...");
            tree.Add(3, "hiru");
            tree.Add(7, "zazpi");
            tree.Add(11, "hamaika");
            tree.Add(2, "bi");
            tree.Add(1, "bat");
            tree.Add(4, "lau");
            tree.Add(15, "hamabost");
            tree.Add(5, "bost");
            tree.Add(13, "hamairu");
            tree.Add(6, "sei");
            tree.Add(8, "zortzi");
            tree.Add(9, "bereratzi");
            tree.Add(10, "hamar");
            tree.Add(12, "hamabi");
            tree.Add(14, "hamalau");

            string asString = tree.ToString();
            if (asString == null)
            {
                onError("Error. ToString() returned null. Did you forget to uncomment ToString()?");
                return false;
            }

            for (int i = 1; i < 16; i++)
            {
                if (asString == null || !asString.Contains($"[{i}-"))
                {
                    onError($"Error. Value {i} wasn't added to the tree:");
                    return false;
                }
            }
            onProgress("Ok");

            onProgress($"Initial tree:\n{tree.ToString()}");

            onProgress("Testing Count()...");
            int count = tree.Count();
            if (count != 15)
            {
                onError($"Error. Count() returned {count} instead of 15");
                return false;
            }
            onProgress("Ok");

            onProgress("Testing Height()...");
            int height = tree.Height();
            if (height != 5)
            {
                onError($"Error. Height() returned {height} instead of 5");
                return false;
            }
            onProgress("Ok");

            onProgress("Testing Get()...");
            string value = tree.Get(12);
            if (value != "hamabi")
            {
                onError($"Error. Get(12) returned {value} instead of \"hamabi\"");
                return false;
            }
            value = tree.Get(14);
            if (value != "hamalau")
            {
                onError($"Error. Get(14) returned {value} instead of \"hamalau\"");
                return false;
            }
            value = tree.Get(4);
            if (value != "lau")
            {
                onError($"Error. Get(4) returned {value} instead of \"lau\"");
                return false;
            }
            onProgress("Ok");

            onProgress("Testing Add() with duplicated key...");
            tree.Add(6, "six");
            count = tree.Count();
            if (count != 15 || tree.Get(6) != "six")
            {
                onError($"Error. Get(6) after Add(6,\"six\") returned \"{tree.Get(6)}\" instead of \"six\"");
                return false;
            }
            onProgress("Ok");


            onProgress("Testing Remove() with leaf nodes...");
            tree.Remove(6);
            int newCount = tree.Count();
            if (count != newCount + 1)
            {
                onError($"Error. Remove failed to remove leaf node");
                return false;
            }
            onProgress("Ok");

            onProgress("Testing Remove() with non-existing node...");
            tree.Remove(6);
            newCount = tree.Count();
            int expectedNodeCount = count - 1;
            if (expectedNodeCount != newCount)
            {
                onError($"Error. {newCount} instead of {expectedNodeCount} nodes");
                return false;
            }
            onProgress("Ok");

            onProgress("Testing Remove() with interior node with only left child...");
            tree.Remove(15);
            newCount = tree.Count();
            expectedNodeCount = count - 2;
            if (expectedNodeCount != newCount)
            {
                onError($"Error. {newCount} instead of {expectedNodeCount} nodes");
                return false;
            }
            onProgress("Ok");

            onProgress("Testing Remove() with interior node with only right child...");
            tree.Remove(8);
            newCount = tree.Count();
            expectedNodeCount = count - 3;
            if (expectedNodeCount != newCount)
            {
                onError($"Error. {newCount} instead of {expectedNodeCount} nodes");
                return false;
            }
            onProgress("Ok");

            onProgress("Testing Remove() with interior node with both left and right children...");
            tree.Remove(7);
            newCount = tree.Count();
            expectedNodeCount = count - 4;
            if (expectedNodeCount != newCount)
            {
                onError($"Error. {newCount} instead of {expectedNodeCount} nodes");
                return false;
            }
            onProgress("Ok");

            onProgress("Testing Remove() with root node...");
            tree.Remove(3);
            newCount = tree.Count();
            expectedNodeCount = count - 5;
            if (expectedNodeCount != newCount)
            {
                onError($"Error. {newCount} instead of {expectedNodeCount} nodes");
                return false;
            }
            onProgress("Ok");

            onProgress("Testing Keys()...");
            int[] keys = tree.Keys();
            foreach (int key in keys)
                onProgress(key.ToString());
            onProgress(null);

            onProgress("Testing Values()...");
            string[] values = tree.Values();
            foreach (string val in values)
                onProgress(val);
            onProgress(null);

            onProgress("Testing Balance()...");
            int oldHeight = tree.Height();
            int oldCount = tree.Count();
            tree.Balance();
            int newHeight = tree.Height();
            newCount = tree.Count();
            if (oldHeight <= newHeight || oldCount != newCount)
            {
                onError($"Balance() didn't work. Height before is {oldHeight} and after is {newHeight}. Count is {newCount} instead of {oldCount}");
                return false;
            }
            onProgress("Ok");

            onProgress("Tree after balancing:");
            onProgress(tree.ToString());


            return true;
        }



        public static bool MeasureBinaryTreeSpeed(Action<string> onProgress, Action<string> onError)
        {
            onProgress("Measuring speed");

            int numSamples = 1000000;
            Random randomGenerator = new Random();
            Dictionary<int, int> solutions = new Dictionary<int, int>();
            BinaryTree<int, int> tree = new BinaryTree<int, int>();

            for (int i = 0; i < numSamples; i++)
            {
                int number = randomGenerator.Next(0, 10000);
                tree.Add(number, number * number);
                solutions[number] = number * number;
            }

            foreach (int number in solutions.Keys)
            {
                if (tree.Get(number) != solutions[number])
                    return false;

                tree.Remove(number);
            }

            return true;
        }
    }
}