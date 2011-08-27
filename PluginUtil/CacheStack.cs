using System;
using System.Collections.Generic;

namespace Xenon.PluginUtil {
	public class CacheStack<T> : List<T> {
		public CacheStack(int _max, int _cleanupNum) : base(_max) {
			max = _max;
			cleanupNum = _cleanupNum;
		}
		
		private int max;
		private int cleanupNum;
		
		public void Push(T item) {
			if(Count >= max) {
				RemoveRange(0, Count - cleanupNum);
			}
			
			base.Add(item);
		}
		
		public T Pop() {
			if(Count == 0) return default(T);
			T t = base[Count - 1];
			RemoveAt(Count - 1);
			return t;
		}
		
		public T Peek() {
			if(Count == 0) return default(T);
			return base[Count - 1];
		}
		
		
	}
}

