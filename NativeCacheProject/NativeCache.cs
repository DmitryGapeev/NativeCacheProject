using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmsDataStructures
{

  public class NativeCache<T>
  {
    public int size;
    public string[] slots;
    public T[] values;
    public int[] hits;

    private int step = 3;

    public NativeCache(int sz)
    {
      size = sz < 10  ? 10 : sz;
      slots = new string[size];
      values = new T[size];
      hits = new int[size];
    }

    public int HashFun(string key)
    {
      if (key == null)
        return -1;

      int sumBytes = 0;
      byte[] valuesBytes = Encoding.UTF8.GetBytes(key);

      foreach (byte item in valuesBytes)
        sumBytes += item;

      return sumBytes % slots.Length;
    }

    private int SeekPosition(string key)
    {
      int position = HashFun(key);
      for (int i = 0; i < slots.Length; i++)
      {
        if (slots[position] == null || slots[position] == key)
          return position;

        position += step;
        position %= slots.Length;
      }

      return -1;
    }

    public bool IsExist(string key)
    {
      if (key == null)
        return false;

      return GetKeyPosition(key) > 0;
    }

    private int GetKeyPosition(string key)
    {
      int keyPosition = HashFun(key);

      for (int i = 0; i < slots.Length; i++)
      {
        if (slots[keyPosition] == null)
          return -1;
        if (slots[keyPosition] == key)
          return keyPosition;

        keyPosition += step;
        keyPosition %= slots.Length;
      }

      return -1;
    }

    private int GetMinHitSlotPosition()
    {
      int minHit = int.MaxValue;
      int position = 0;
      for (int i = 0; i < hits.Length; i++)
      {
        if (hits[i] < minHit)
        {
          minHit = hits[i];
          position = i;
        }
      }

      return position;
    }

    public void Put(string key, T value)
    {
      if (key == null)
        return;

      int position = SeekPosition(key);

      if (position == -1)
      {
        int minHitPosition = GetMinHitSlotPosition();
        values[minHitPosition] = default(T);
        slots[minHitPosition] = null;
        hits[minHitPosition] = 0;

        Put(key, value);
      }
      else
      {
        if (slots[position] == null)
          slots[position] = key;

        values[position] = value;
      }
    }

    public T Get(string key)
    {
      if (key == null)
        return default(T);

      int position = GetKeyPosition(key);

      if (position != -1 && slots[position] != null)
      {
        hits[position]++;
        return values[position];
      }

      return default(T);
    }
  }
}