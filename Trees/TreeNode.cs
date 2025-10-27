
using System;
using System.Runtime.CompilerServices;
using Lists;

namespace Trees
{
    public class TreeNode<T>
    {
        private T Value;
        //TODO #1: Declare a member variable called "Children" as a list of TreeNode<T> objects
        private List<TreeNode<T>> Children;

        public TreeNode(T value)
        {
            //TODO #2: Initialize member variables/attributes
            Children = new List<TreeNode<T>>();
            Value = value;
        }

        public string ToString(int depth, int index)
        {
            //TODO #3: Uncomment the code below
            
            string output = null;
            string leftSpace = null;
            for (int i = 0; i < depth; i++) leftSpace += " ";
            if (leftSpace != null) leftSpace += "->";

            output += $"{leftSpace}[{Value}]\n";

            for (int childIndex = 0; childIndex < Children.Count(); childIndex++)
            {
                TreeNode<T> child = Children.Get(childIndex);
                output += child.ToString(depth + 1, childIndex);
            }
            return output;
        }

        public TreeNode<T> Add(T value)
        {
            //TODO #4: Add a new instance of class GenericTreeNode<T> with Value=value. Return the instance we just created
            TreeNode<T> newNode = new TreeNode<T>(value);
            Children.Add(newNode);
            return newNode;
            
        }

        public int Count()
        {
            //TODO #5: Return the total number of elements in this tree
            int count = 1;
            if (Children.Count() == 0) return count;
            for (int i = 0; i < Children.Count(); i++) count += Children.Get(i).Count();
            return count;
        }

        public int Height()
        {
            //TODO #6: Return the height of this tree
            if (Children.Count() == 0) return 0;

            int maxHeight = -1;
            
            for (int i = 0; i < Children.Count(); i++)
            {
                TreeNode<T> child = Children.Get(i);
                int childHeight = child.Height();

                if (childHeight > maxHeight) maxHeight = childHeight;
            }
            return maxHeight + 1;
        }

        public void Remove(T value)
        {
            //TODO #7: Remove the child node that has Value=value. Apply recursively
            for (int i = 0; i < Children.Count(); i++)
            {
                TreeNode<T> child = Children.Get(i);
                if (child.Value.Equals(value))
                {
                    Children.Remove(i);
                    return;
                }
                child.Remove(value);
            }

        }

        public TreeNode<T> Find(T value)
        {
            //TODO #8: Return the node that contains this value (it might be this node or a child). Apply recursively
            if (Value.Equals(value)) return this;
            for (int i = 0; i < Children.Count(); i++)
            {
                TreeNode<T> child = Children.Get(i);

                TreeNode<T> wanted = child.Find(value);
                if (wanted != null) return wanted;

            }
            return null;
        }


        public void Remove(TreeNode<T> node)
        {
            //TODO #9: Same as #7, but this method is given the specific node to remove, not the value
            for (int i = 0; i < Children.Count(); i++)
            {
                TreeNode<T> child = Children.Get(i);
                if (child == node)
                {
                    Children.Remove(i);
                    return;
                }
                child.Remove(node);
            }
        }
    }
}