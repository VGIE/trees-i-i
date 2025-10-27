using Lists;
using System.Collections;

public class ListNode<T>
{
    public T Value;
    public ListNode<T> Next = null;
    public ListNode<T> Previous = null;

    public ListNode(T value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

public class List<T> : IList<T>
{
    ListNode<T> First = null;
    ListNode<T> Last = null;
    int m_numItems = 0;

    public override string ToString()
    {
        ListNode<T> node = First;
        string output = "[";

        while (node != null)
        {
            output += node.ToString() + ",";
            node = node.Next;
        }
        output = output.TrimEnd(',') + "] " + Count() + " elements";

        return output;
    }

    public int Count()
    {
        //TODO #1: return the number of elements on the list
        return m_numItems;      
    }

    public T Get(int index)
    {
        //TODO #2: return the element on the index-th position. O if the position is out of bounds
        ListNode<T> node = First;
        int counter = 0;
        while (node != null && counter<index)
        {
            counter++;
            node = node.Next;
        }
        if (node == null) return default(T);
        return node.Value;
        
    }

    public void Add(T value)
    {
        //TODO #3: add a new integer to the end of the list
        ListNode<T> listNode = new ListNode<T>(value);
        m_numItems++;
        if (First == null)
        {
            First = listNode;
            Last = listNode;
            return;
        }
        listNode.Previous = Last;
        Last.Next = listNode;
        Last = listNode;
    }

    public T Remove(int index)
    {
        //TODO #4: remove the element on the index-th position. Do nothing if position is out of bounds
        if (index < 0 || index >= m_numItems) return default;
        T removedValue = default;
        if (index == 0)
        {
            removedValue = First.Value;

            First = First.Next;
            if (First != null)
                First.Previous = null;
            else
                Last = null;

            m_numItems--;
            return removedValue;
        }
       if (index == m_numItems - 1)
        {
            removedValue = Last.Value;

            Last = Last.Previous;
            if (Last != null)
                Last.Next = null;
            m_numItems--;
            return removedValue;
        }

        ListNode<T> node;
        if (index < m_numItems / 2)
        {
            node = First;
            for (int i = 0; i < index; i++)
                node = node.Next;
        }
        else
        {
            node = Last;
            for (int i = m_numItems - 1; i > index; i--)
                node = node.Previous;
        }

        removedValue = node.Value;
        node.Previous.Next = node.Next;
        node.Next.Previous = node.Previous;

        m_numItems--;
        return removedValue;
    }

    public void Clear()
    {
        //TODO #5: remove all the elements on the list
        First = null;
        Last = null;
        m_numItems = 0;
    }

    public IEnumerator GetEnumerator()
    {
        //TODO #6 : Return an enumerator using "yield return" for each of the values in this list
        ListNode<T> node = First;
        while (node != null)
        {
            yield return node.Value;
            node = node.Next;
        }        
    }
}