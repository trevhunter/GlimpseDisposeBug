using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlimpseDisposeBug.Models
{
    public class DisposableIndexModel : IDisposable 
    {

        public string SomeProperty
        {
            get
            {
                ThrowIfDisposed();
                return "Some Value";
            }
        }

        private bool _isDisposed;

        private void ThrowIfDisposed()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException("I'm disposed");
            }
        }

        public void Dispose()
        {
            _isDisposed = true;
        }
    }
}