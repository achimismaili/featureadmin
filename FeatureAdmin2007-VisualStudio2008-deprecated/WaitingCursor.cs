using System;
using System.Windows.Forms;

namespace CursorUtil
{
    public class WaitingCursor : IDisposable
    {
        private Cursor _previousCursor;
        bool _disposed = false;
        public WaitingCursor()
        {
            _previousCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
        }
        /// <summary>
        /// Public implementation of Dispose pattern callable by consumers
        /// Usual pattern
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Protected implementation of Dispose pattern
        /// Usual pattern
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) { return; }
            Cursor.Current = _previousCursor;
            _disposed = true;
        }
    }
}
