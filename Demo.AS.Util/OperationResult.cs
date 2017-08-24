using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.AS.Util
{
    public class OperationResult
    {
        public OperationResult()
        {
            this.Success = true;
        }
        public bool Success { get; private set; }

        private Exception _mainException;
        public Exception MainException
        {
            get { return _mainException; }
            set
            {
                _mainException = value;
                this.Success = _mainException == null;
            }
        }
    }

    public class OperationResult<T> : OperationResult
    {
        public T Data { get; set; }
    }
}
