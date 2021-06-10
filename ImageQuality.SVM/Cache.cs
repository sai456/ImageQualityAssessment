using System;
using System.Collections.Generic;
using System.Text;

// SVM LRU Cache (Least Recently Used)
namespace ImageQuality.SVM
{
    internal class Cache
    {
        private int _count;
        private long _size;
        

        private sealed class head_t
        {
            private Cache _enclosingInstance;
            public head_t(Cache enclosingInstance)
            {
                _enclosingInstance = enclosingInstance;
            }

            public Cache EnclosingInstance
            {
                get
                {
                    return _enclosingInstance;
                }
            }

            internal head_t prev, next;
            internal float[] data;
            internal int len;
        }

        private head_t[] head;
        private head_t lru_head;

        public Cache(int count, int size)
        {
            _count = count;
            _size = size;
            head = new head_t[_count];
            for (int i = 0; i < _count; i++)
                head[i] = new head_t(this);
            _size /= 4;
            _size -= _count * 4;
            lru_head = new head_t(this);
            lru_head.next = lru_head.prev = lru_head;
        }

        private void lru_delete(head_t h)
        {
            h.prev.next = h.next;
            h.next.prev = h.prev;
        }

        private void lru_insert(head_t h)
        {
            h.next = lru_head;
            h.prev = lru_head.prev;
            h.prev.next = h;
            h.next.prev = h;
        }

        private static void swap<T> (ref T lhs, ref T rhs)
        {
            T template = lhs;
            lhs = rhs;
            rhs = template;
        }

        public int GetData(int index, out float[] data, int len)
        {
            head_t h = head[index];
            if (h.len > 0)
                lru_delete(h);
            int more = len - h.len;
            if(more > 0)
            {
                // In LRU Cache Free Old Space and Allocate New Space
                while(_size < more)
                {
                    head_t old = lru_head.next;
                    lru_delete(old);
                    _size += old.len;
                    old.data = null;
                    old.len = 0;
                }
                // Allocate New Space
                float[] new_data = new float[len];
                if (h.data != null)
                    Array.Copy(h.data, 0, new_data, 0, h.len);
                h.data = new_data;
                _size -= more;
                swap(ref h.len, ref len);
            }

            lru_insert(h);
            data = h.data;
            return len;
        }

        public void SwapIndex(int i, int j)
        {
            if (i == j)
                return;

            if (head[i].len > 0)
                lru_delete(head[i]);
            if (head[j].len > 0)
                lru_delete(head[j]);

            swap(ref head[i].data, ref head[j].data);
            swap(ref head[i].len, ref head[j].len);
            if (head[i].len > 0)
                lru_insert(head[i]);
            if (head[j].len > 0)
                lru_insert(head[j]);

            if (i > j)
                swap(ref i, ref j);
            for(head_t h = lru_head.next; h!=lru_head; h = h.next)
            {
                if(h.len>i)
                {
                    if (h.len > j)
                        swap(ref h.data[i], ref h.data[j]);
                    else
                    {
                        //give up
                        lru_delete(h);
                        _size += h.len;
                        h.data = null;
                        h.len = 0; 
                    }
                }
            }

        }
        

    }
}
