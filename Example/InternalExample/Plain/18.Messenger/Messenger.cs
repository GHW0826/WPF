using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger_IEventAggregator
{
    public class Messenger
    {
        private readonly List<Action<string>> _subscribers = new List<Action<string>>();

        public static Messenger Instance { get; } = new Messenger();

        public void Register<T>(Action<T> callback)
        {
            if (typeof(T) == typeof(string))
                _subscribers.Add(o => callback((T)(object)o));
        }

        public void Send(string message)
        {
            foreach (var sub in _subscribers)
                sub.Invoke(message);
        }
    }
}
