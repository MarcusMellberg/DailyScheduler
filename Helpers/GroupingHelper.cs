using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DailyScheduler.Helpers
{
    public class GroupingHelper<K, T> : ObservableCollection<T>
    {
        public K Key { get; private set; }

        public GroupingHelper(K key, IEnumerable<T> items)
        {
            Key = key;
            foreach (var item in items)
            {
                this.Items.Add(item);
            }
        }
    }
}
