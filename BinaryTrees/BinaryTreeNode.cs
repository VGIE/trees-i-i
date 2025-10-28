
using System;
using System.Xml;
namespace BinaryTrees
{
    public class BinaryTreeNode<TKey, TValue> where TKey : IComparable<TKey>
    {
        public TKey Key;
        public TValue Value;
        public BinaryTreeNode<TKey, TValue> LeftChild;
        public BinaryTreeNode<TKey, TValue> RightChild;

        public BinaryTreeNode(TKey key, TValue value)
        {
            //TODO #1: Initialize member variables/attributes
            Key = key;
            Value = value;
            LeftChild = null;
            RightChild = null;
        }

        public string ToString(int depth)
        {
            string output = null;

            string leftSpace = null;
            for (int i = 0; i < depth; i++) leftSpace += " ";
            if (leftSpace != null) leftSpace += "->";

            if (Value != null)
                output += $"{leftSpace}[{Key.ToString()}-{Value.ToString()}]\n";

            if (LeftChild != null)
                output += $"{LeftChild?.ToString(depth + 1)}";

            if (RightChild != null)
                output += $"{RightChild?.ToString(depth + 1)}";

            return output;
        }

        public void Add(BinaryTreeNode<TKey, TValue> node)
        {
            //TODO #2: Add the new node following the order:
            //          -If the current node (this) has a higher key that the new node (use CompareTo()), the new node should be on this node's left.
            //              a) If the left child is null, the added node should be this node's left node side
            //              b) Else, we should ask the LeftChild to add it recursively
            //          -If the current node has a lower key that the new node (use CompareTo()), the new node should be on this node's right side.
            //          -If the current node and the new node have the same key, just update this node's value with the new node's value
            if (Key.CompareTo(node.Key) == -1)
            {
                if (LeftChild != null)
                {
                    LeftChild.Add(node);
                    return;
                }
                
                LeftChild = node;

            }
            else if (Key.CompareTo(node.Key) == 0)
            {
                Value = node.Value;
            }
            else
            {
                if (RightChild != null) 
                {
                    RightChild.Add(node);
                    return;
                }
                RightChild = node;
            }
        }

        public int Count()
        {
            //TODO #3: Return the total number of elements in this tree
            int count = 1;
            if (LeftChild != null) count += LeftChild.Count();
            if (RightChild != null) count += RightChild.Count();
            return count;
        }

        public int Height()
        {
            //TODO #4: Return the height of this tree

            int maxHeight;
            int LeftChildHeight = -1;
            int RightChildHeight = -1;
            if (LeftChild != null) LeftChildHeight = LeftChild.Height();
            if (RightChild != null) RightChildHeight = RightChild.Height();
            
            if (RightChildHeight > LeftChildHeight) maxHeight = RightChildHeight;
            else maxHeight = LeftChildHeight;
            
            return maxHeight + 1;            
        }

        public TValue Get(TKey key)
        {
            //TODO #5: Find the node that has this key:
            //          -If the current node (this) has a higher key that the new node (use CompareTo()), the key we are searching for should be on this node's left side.
            //              a) If the left child is null, return null. We haven't found it
            //              b) Else, we should ask the LeftChild to find the node recursively. It must be below LeftChild
            //          -If the current node has a lower key that the new node (use CompareTo()), the key should be on this node's right side.
            //          -If the current node and the new node have the same key, just return this node's value. We found it
            if (Key.CompareTo(key) == -1)
            {
                if (LeftChild != null) return LeftChild.Get(key);
            }
            else if (Key.CompareTo(key) == 0)
            {
                return Value;
            }
            else
            {
                if (RightChild != null) return RightChild.Get(key);
            }
            return default;
            
        }

        public BinaryTreeNode<TKey, TValue> Remove(TKey key)
        {

            if (LeftChild == null && RightChild == null)
            {
                if (Key.Equals(key)) return this;
            }
            else if (LeftChild == null && RightChild != null) RightChild.Remove(key);
            
            else if (LeftChild != null && RightChild == null) LeftChild.Remove(key);


            BinaryTreeNode<TKey, TValue> SaveTheChildrenFromLeft, DesintegratedNode;
            if (LeftChild.Key.Equals(key))
            {
                if (LeftChild.LeftChild == null && LeftChild.RightChild == null) return LeftChild;
                else if (LeftChild.LeftChild == null && LeftChild.RightChild != null)
                {
                    DesintegratedNode = LeftChild;
                    LeftChild = LeftChild.RightChild;
                    return DesintegratedNode;
                }
                else if (LeftChild.LeftChild != null && LeftChild.RightChild == null)
                {
                    DesintegratedNode = LeftChild;
                    LeftChild = LeftChild.LeftChild;
                    return DesintegratedNode;
                }
                SaveTheChildrenFromLeft = LeftChild.LeftChild;
                DesintegratedNode = LeftChild;
                LeftChild = LeftChild.RightChild;
                Add(SaveTheChildrenFromLeft);
                return DesintegratedNode;
            }
            else if (RightChild.Key.Equals(key))
            {
                if (RightChild.LeftChild == null && RightChild.RightChild == null) return RightChild;
                else if (RightChild.LeftChild == null && RightChild.RightChild != null)
                {
                    DesintegratedNode = RightChild;
                    RightChild = RightChild.RightChild;
                    return DesintegratedNode;
                }
                else if (RightChild.LeftChild != null && RightChild.RightChild == null)
                {
                    DesintegratedNode = RightChild;
                    RightChild = RightChild.LeftChild;
                    return DesintegratedNode;
                }
                SaveTheChildrenFromLeft = RightChild.LeftChild;
                DesintegratedNode = RightChild;
                RightChild = RightChild.RightChild;
                Add(SaveTheChildrenFromLeft);
                return DesintegratedNode;
            }
            else
            {
                if (LeftChild.Key.CompareTo(key) == 1)
                {
                    LeftChild.Remove(key);
                }
                else
                {
                    RightChild.Remove(key);
                }
            }
            return this;
        }

        public int KeysToArray(TKey[] keys, int index)
        {
            if (LeftChild != null)
                index = LeftChild.KeysToArray(keys, index);
            keys[index] = Key;
            index++;
            if (RightChild != null)
                index = RightChild.KeysToArray(keys, index);
            return index;
        }

        public int ValuesToArray(TValue[] values, int index)
        {
            if (LeftChild != null)
                index = LeftChild.ValuesToArray(values, index);
            values[index] = Value;
            index++;
            if (RightChild != null)
                index = RightChild.ValuesToArray(values, index);
            return index;
        }
    }
}