using System;
using System.Linq;
using System.Collections.Generic;

namespace Xenon.PluginUtil {
	public class CacheHash<K, T> : Dictionary<K, TimeStampedVal<T>> {
		public CacheHash(int num, int trim) {
			maxItems = num;
			trimNum = trim;
		}
		
		private int maxItems;
		private int trimNum;
		
		public new T this[K key] {
			get {
				if(!base.ContainsKey(key)) return default(T);
				base[key].timestamp = DateTime.Now;
				return base[key].val;
			}
			set {
				base[key] = new TimeStampedVal<T>(value);
				if(Count > maxItems) {
					//var p = select item from this;
					int i = 0;
					foreach(K k in (from KeyValuePair<K, TimeStampedVal<T>> item in this orderby item.Value.timestamp ascending select item.Key)) {
						if(i > maxItems - trimNum) break;
						Remove(k);
						++i;
					}
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

