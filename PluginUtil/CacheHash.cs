using System;
using System.Collections.Generic;

namespace PluginUtil {
	public class CacheHash<K, T> : Dictionary<K, TimeStampedVal<T>> {
		public CacheHash(int num, int trim) {
			maxItems = num;
		}
		
		private int maxItems;
		
		public new T this[K key] {
			get {
				if(!base.ContainsKey(key)) return default(T);
				base[key].timestamp = DateTime.Now;
				return base[key].val;
			}
			set {
				base[key] = new TimeStampedVal<T>(value);
				if(Count > maxItems) {
					
				}
			}
		}
	}
	
	public class TimeStampedVal<T> {
		public TimeStampedVal(T _val) { val = _val; timestamp = DateTime.Now; }
		public T val;
		public DateTime timestamp;
	}
}

