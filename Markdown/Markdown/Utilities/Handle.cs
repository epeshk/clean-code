using System;

namespace Markdown.Utilities
{
    public class Handle : IDisposable
    {
        private readonly Action exit;

        internal Handle(Action enter, Action exit)
        {
            this.exit = exit;
            enter();
        }

        public void Dispose()
        {
            exit();
        }
    }
}