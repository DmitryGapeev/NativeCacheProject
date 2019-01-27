using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsDataStructures
{
  class Program
  {
    static void Main(string[] args)
    {
      TestNativeCache();

      Console.ReadKey();
    }

    static void TestNativeCache()
    {
      NativeCache<string> cache = new NativeCache<string>(10);

      cache.Put("facebook", "http://facebook.com");
      cache.Put("google", "http://google.com");
      cache.Put("vk", "http://vk.com");
      cache.Put("mail", "http://mail.ru");
      cache.Put("yandex", "http://yandex.ru");
      cache.Put("yahoo", "http://yahoo.com");
      cache.Put("bing", "http://bing.com");
      cache.Put("microsoft", "http://microsoft.com");
      cache.Put("oracle", "http://oracle.com");

      Console.WriteLine(new string('=', 50));
      ShowCacheInfo(cache);
      cache.Put("github", "http://github.com");
      Console.WriteLine(new string('=', 50));
      ShowCacheInfo(cache);

      Console.WriteLine(new string('=', 50));

      Console.WriteLine("test read from cache");
      Console.WriteLine();
      for (int i = 0; i < cache.slots.Length; i++)
      {
        for (int j = cache.slots.Length - i; j > 0; j--)
          cache.Get(cache.slots[i]);
      }

      ShowCacheInfo(cache);

      Console.WriteLine();
      cache.Put("amazon", "http://amazon.com");
      ShowCacheInfo(cache);
      Console.WriteLine();
      cache.Put("twitter", "http://twitter.com");
      ShowCacheInfo(cache);
      cache.Get("twitter");
      Console.WriteLine();
      ShowCacheInfo(cache);
      cache.Get(null);
      Console.WriteLine();
      ShowCacheInfo(cache);
    }

    static void ShowCacheInfo(NativeCache<string> cache)
    {
      for (int i = 0; i < cache.size; i++)
      {
        Console.WriteLine(cache.slots[i] + " "+cache.values[i] + " " + cache.hits[i]);
      }
    }
    
  }
}
