using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Heap<T> where T :IHeapItem<T> {

    T[] items;
    int currentItemCount;

    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }
    public void Add(T item)
    {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        SortUp(item);
        currentItemCount++;
    }

    void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;

        while(true)
        {
            T parentItem = items[parentIndex];
            if(item.CompareTo(parentItem) > 0)
            {
                Swap(item,parentItem);
            }
            else
            {
                break;
            }
        }
    }

    void SortDown(T item)
    {
        while (true)
        {
            int LeftChildIndex = item.HeapIndex * 2 + 1;
            int RightChildIndex = item.HeapIndex * 2 + 2;
            int swapindex = 0;
            if(LeftChildIndex < currentItemCount)
            {
                swapindex = LeftChildIndex;
                if (RightChildIndex < currentItemCount)
                {
                    if (items[LeftChildIndex].CompareTo(items[RightChildIndex]) < 0)
                    {
                        swapindex = RightChildIndex;
                    }
                } 
                if(item.CompareTo(items[swapindex])<0)
                {
                    Swap(item,items[swapindex]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }

    void Swap(T A, T B)
    {
        items[A.HeapIndex] = B;
        items[B.HeapIndex] = A;

        int itemAindex = A.HeapIndex;
        A.HeapIndex = B.HeapIndex;
        B.HeapIndex = itemAindex;
    }

    public T RemoveFirst()
    {
        T firstitem = items[0];
        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);
        return firstitem;
    }

    public int Count {
        get { return currentItemCount; }
    }

    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    public bool Contains( T item)
    {
        return Equals(items[item.HeapIndex],item);
    }

}


public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex
    {
        get;
        set;
    }
}
